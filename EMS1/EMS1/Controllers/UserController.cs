using EMS1.Services;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EMS1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        
        private readonly IUserServive _userServive;


        public UserController(IUserServive userServive)
        {
            _userServive = userServive;
        }

        // GET: api/User
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users=await _userServive.getAllAsync();

            return Ok(users);
        }

        // GET api/UserController/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/UserController
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/UserController/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/UserController/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
