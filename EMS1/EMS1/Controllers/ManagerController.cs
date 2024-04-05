using Microsoft.AspNetCore.Mvc;
using MongoDemo.Services;
using MongoDemo.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MongoDemo.Controllers
{
    [Route("Manager")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        // GET: api/<ManagerController>
        private readonly IUserService _userService;

        public ManagerController(IUserService userService)
        {
            _userService = userService;
        }
        // GET: api/User
        [HttpGet("{mid}")]
        public async Task<IActionResult> GetEmployeesUnderManager(string mid)
        {
           
            var users = await _userService.GetEmpAsync(mid);
            return Ok(users);
        }

        //// GET api/Manager/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/Manager
        [HttpPost]
        public async Task<IActionResult> AddEmployeeUnderManager(User user)
        {

            user.role = "employee";
            await _userService.CreateAsync(user);
            return Ok("Created Successfully");
        }


        // PUT api/Manager/5
        [HttpPut("{id}")]
        public async Task<IActionResult> EditManagerDetails(string id, User user)
        {
            var manager = await _userService.GetById(id);
            if(manager == null)
            {
                return NotFound();
            }
            user.role = "Manager";
            await _userService.UpdateAsync(id, user);
            return Ok("Updated Successfully");
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(string id)
        {

            var user = await _userService.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            await _userService.DeleteAsync(id);

            return Ok("Deleted Successfully");

        }

        //// DELETE api/Manager/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
