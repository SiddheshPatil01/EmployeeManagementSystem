using EMS1.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ZstdSharp.Unsafe;

namespace EMS1.Services
{
    public class UserService : IUserServive
    {
        private readonly IMongoCollection<Users> _userCollection;
        private readonly IOptions<DatabaseSettings> _dbSettings;
        public UserService(IOptions<DatabaseSettings> dbSettings) {
        _dbSettings = dbSettings;
            var mongoClient= new MongoClient(dbSettings.Value.ConnectionString);
            var mongoDatabase= mongoClient.GetDatabase(dbSettings.Value.DatabaseName);
            _userCollection = mongoDatabase.GetCollection<Users>(dbSettings.Value.UsersCollectionName);

        }

        public async Task<IEnumerable<Users>> getAllAsync()=>
            await _userCollection.Find(_=>true).ToListAsync();

        public async Task<Users> GetById(int _id) =>
            await _userCollection.Find(a => a.empId == _id).FirstOrDefaultAsync();

        public async Task CreateAsync(Users user) =>
            await _userCollection.InsertOneAsync(user);

        public async Task UpdateAsync(int _id, Users user) {
            var filter = Builders<Users>.Filter.Eq("empId", _id); // Assuming "empId" is the identifier
            var update = Builders<Users>.Update
                .Set("name", user.name) // Replace "name" with other fields you want to update
                .Set("email", user.email)
                .Set("password", user.password)
                .Set("role", user.role)
                .Set("mngId", user.mngId);
            await _userCollection.UpdateOneAsync(filter, update);
        }

          //  await _userCollection.ReplaceOneAsync(a=>a.empId==_id, user);

        public async Task DeleteAsync(int _id) =>
            await _userCollection.DeleteOneAsync(a => a.empId == _id);

        //Manager methods 
        public async Task<IEnumerable<Users>> GetAllEmployeeForManagerAsync(int mngId)
        {
            var filter = Builders<Users>.Filter.Eq("mngId", mngId);
            return await _userCollection.Find(filter).ToListAsync();
            //return await _userCollection.Find(a=>a.mngId==_mngId).ToListAsync();
        }

          // public async Task<Users> GetManagerDetails(int _mngId) =>
          // await _userCollection.Find(a => a.empId == _mngId).FirstOrDefaultAsync();
    }

    
   
}
