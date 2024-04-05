using EMS1.Models;
using EMS1.Services;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using System.Threading.Tasks; // Add this namespace for Task

namespace EMS1.Controllers
{
    [Route("admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IUserService _userService;

        public AdminController(IUserService userService)
        {
            _userService = userService;
        }

         

        // GET: api/admin/getAll
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllEmployees() // Add async keyword here
        {
            string empId = HttpContext.Request.Headers["empId"];
            string password = HttpContext.Request.Headers["password"];
            bool isAdmin = await _userService.Check(empId, password, "Admin");
            if (isAdmin)
            {
                var allUsers = await _userService.getAllAsync(); // Await the asynchronous task
                return Ok(allUsers);
            } else return BadRequest("You Are unAuthorized!");
            
        }

        // GET: api/admin/getAllManagers
        [HttpGet("getAllManagers")]
        public async Task<IActionResult> GetAllManagers()  
        {
            var allManagers = await _userService.getAllManagers();  
            return Ok(allManagers);
        }

        // GET: api/admin/{empId}
        [HttpGet("{empId}")]
        public async Task<IActionResult> GetAllEmployeesForManager(string empId)
        {
            var getAllEmployeesForManager = await _userService.getAllEmpBelowManager(empId);  
            return Ok(getAllEmployeesForManager);
        }

        // DELETE: api/admin/{empid}
        [HttpDelete("{empid}")]
        public async Task<IActionResult> Delete(string empid)
        {
            var u = await _userService.GetById(empid);
            if (u == null)
            {
                return NotFound();
            }
            await _userService.DeleteAsync(empid); // Await the asynchronous task
            return Ok("Deleted");
        }
    }
}
