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

        public async Task<Users> GetById(string id) =>
            await _userCollection.Find(a => a.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Users user) =>
            await _userCollection.InsertOneAsync(user);

        public async Task UpdateAsync(string id, Users user) =>
            await _userCollection.ReplaceOneAsync(a=>a.Id==id, user);

        public async Task DeleteAsync(string id) =>
            await _userCollection.DeleteOneAsync(a => a.Id == id);
    }

    
}
