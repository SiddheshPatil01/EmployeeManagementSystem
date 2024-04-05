using EMS1.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ZstdSharp.Unsafe;

namespace EMS1.Services
{
    public class UserService : IUserService
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

        public async Task<Users> GetById(string EmpId) =>
            await _userCollection.Find(a => a.empId == EmpId).FirstOrDefaultAsync();

        public async Task CreateAsync(Users user) =>
            await _userCollection.InsertOneAsync(user);

        public async Task Update(String id , Users user) =>
            //await _userCollection.ReplaceOneAsync(a => a.empId.Equals(id), user);
            await _userCollection.ReplaceOneAsync(a=>a.empId.Equals(id),user);

        public async Task DeleteAsync(string id) =>
            await _userCollection.DeleteOneAsync(a => a.empId.Equals(id));

          async Task<IEnumerable<Users>> IUserService.getAllManagers()
        {
            var filter = Builders<Users>.Filter.Eq(u => u.role, "Manager");
            var managerList = await _userCollection.Find(filter).ToListAsync();
            return managerList;
        }

        async Task<IEnumerable<Users>> IUserService.getAllEmpBelowManager(string empId)
        {
            var filter = Builders<Users>.Filter.Eq(u => u.managerId, empId);
            var ListOfUsersBelowManger =  _userCollection.Find(filter).ToList(); 
            return ListOfUsersBelowManger;
        }

         public async Task<bool> Check(string empId, string password , string role)
        {
            //var filter = Builders<Users>.Filter.Eq(u => u.empId, empId);
            //var user = await _userCollection.Find(filter).FirstOrDefaultAsync();
            var user =await _userCollection.Find(a => a.empId == empId).FirstOrDefaultAsync();
            // If user exists and passwords match, return true
            return user != null && user.Password == password && user.role==role;
        }

         
    }

    
}
