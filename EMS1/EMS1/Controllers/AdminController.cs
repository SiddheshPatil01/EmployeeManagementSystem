using EMS1.Models;
using EMS1.Services;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EMS1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        
        private readonly IUserServive _userService;


        public AdminController(IUserServive userService)
        {
            _userService = userService;
        }

        // GET
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            Console.WriteLine("all employees");
            var users=await _userService.getAllAsync();

            return Ok(users);
        }

        // GET 
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
       {
            
            var users = await _userService.GetById(id);
            if (users == null) { 
                return NotFound();
            }

            return Ok(users);
        }

        // POST 
        [HttpPost]
        public async Task<IActionResult> Post(Users user)
        {
            await _userService.CreateAsync(user);
            return Ok("Created successfully");
        }

        // PUT 
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Users newUser)
        {   
            var user=await _userService.GetById(id);
            if (user == null) { 
                    return NotFound();
                }
            await _userService.UpdateAsync(id,newUser);
            return Ok("Updated successfully");
        }

        // DELETE 
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userService.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            await _userService.DeleteAsync(id);
            return Ok("Deleted successfully");
        }
    }
}
