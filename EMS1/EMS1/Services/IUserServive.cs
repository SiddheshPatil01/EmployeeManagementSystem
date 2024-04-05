using EMS1.Models;

namespace EMS1.Services
{
    public interface IUserServive
    {
        Task<IEnumerable<Users>> getAllAsync();
        Task<Users> GetById(int id);
        Task CreateAsync(Users user);
        Task UpdateAsync(int id,Users user);

        Task DeleteAsync(int id);


        //manager methods
        Task<IEnumerable<Users>> GetAllEmployeeForManagerAsync(int mngId);
       // Task<Users> GetManagerDetails(int _mngId);
    }
}