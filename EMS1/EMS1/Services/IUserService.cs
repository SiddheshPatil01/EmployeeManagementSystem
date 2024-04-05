using EMS1.Models;

namespace EMS1.Services
{
    public interface IUserService
    {
        //Admin
        Task<IEnumerable<Users>> getAllAsync();
        Task<IEnumerable<Users>> getAllManagers();
        Task<IEnumerable<Users>> getAllEmpBelowManager(string empId);
        Task DeleteAsync(string id);
 
 
        //Manager
 
        //Task<IEnumerable<Users>> getAllEmpBelowManager();
        Task<Users> GetById(string EmpId);
        Task Update(String id, Users user);
        Task CreateAsync(Users user);
 
 
        //User
        //Task<Users> GetById(string EmpId);
        //Task Update(String id, Users user);
 
        //for Authentication
        //if A user exit with correct password
 
        //Authenticate  
        Task<bool> Check(string empId, string password, string role);


    }
}
