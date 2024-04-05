using EMS1.Models;
using EMS1.Services;
using Microsoft.AspNetCore.Mvc;

namespace EMS1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly IUserServive _userService;
        public EmployeeController(IUserServive userService)
        {
            _userService = userService;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {

            var users = await _userService.GetById(id);
            if (users == null)
            {
                return NotFound();
            }

            return Ok(users);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Users newUser)
        {
            var user = await _userService.GetById(id);
            if (user == null||user.role!="Employee")
            {
                return NotFound();
            }
            
            await _userService.UpdateAsync(id, newUser);
            return Ok("Updated successfully");
        }
    }
}
