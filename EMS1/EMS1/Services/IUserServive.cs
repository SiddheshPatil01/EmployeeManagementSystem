using EMS1.Models;

namespace EMS1.Services
{
    public interface IUserServive
    {
        Task<IEnumerable<Users>> getAllAsync();
        Task<Users> GetById(string id);
        Task CreateAsync(Users user);
        Task UpdateAsync(string id,Users user);

        Task DeleteAsync(string id);


    }
}