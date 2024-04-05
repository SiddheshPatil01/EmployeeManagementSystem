using EMS1.Models;
using EMS1.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EMS1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {

        private readonly IUserServive _userService;
        public ManagerController(IUserServive userService)
        {
            _userService = userService;
        }

        //get all employees below manager
        [HttpGet("{mngId}/employees")]
        public async Task<IActionResult>GetAllEmployees(int mngId)
        {
            var manager = await _userService.GetById(mngId);
            if ( manager == null || manager.role != "Manager" )
            {
                return NotFound();
            }
            var employees=await _userService.GetAllEmployeeForManagerAsync(mngId);
            return Ok(employees);
        }

        //see manager details
        [HttpGet("{mngId}")]
        public async Task<IActionResult> GetDetails(int mngId)
        {

            var manager = await _userService.GetById(mngId);
            if (manager == null||manager.role != "Manager") { 
                return NotFound();
            }
            return Ok(manager);
        }

        //add an employee
        [HttpPost]
        public async Task<IActionResult> Post(int _mngId,Users user)
        {
            var manager = await _userService.GetById(_mngId);
            if (manager == null || manager.role != "Manager")
            {
                return NotFound();
            }
            user.mngId = _mngId;
            user.role = "Employee";
            await _userService.CreateAsync(user);
            return Ok("Created successfully");
        }

        //change manager details 
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Users newUser)
        {
            var manager = await _userService.GetById(id);
            if (manager == null || manager.role != "Manager")
            {
                return NotFound();
            }
            await _userService.UpdateAsync(id, newUser);
            return Ok("Updated successfully");
        }
    }
}
