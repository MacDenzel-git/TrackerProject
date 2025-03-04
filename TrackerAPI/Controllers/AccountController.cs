using AllinOne.DataHandlers;
using BusinessLogicLayer.Services.AccountServiceContainer;
using DataAccessLayer.DataTransferObjects;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace TrackerAPI.Controllers
{    
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
       private readonly IAccountService _accountService;


        public AccountController(IAccountService accountService)
        {
             _accountService = accountService;
        }


        [HttpGet("GetRoles")]
        public async Task<IActionResult> GetRoles()
        {
            var data = await _accountService.GetRoles();
            if (data == null)
            {
                return Ok(new OutputHandler
                {
                    Message = "Something went wrong"
                });
            }
            else
            {
                return Ok(data);
            }
        }


        [HttpPost("Create")]
        public async Task<IActionResult> Create(UserDTO user)
        {
            var data = await _accountService.CreateUser(user);
            if (data == null)
            {
                return Ok(new OutputHandler
                {
                    Message = "Something went wrong"
                });
            }
            else
            {
                return Ok(data);
            }
        }



        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO login)
        {
            var data = await _accountService.Login(login.Username, login.Password);
            if (data == null)
            {
                return Ok(new OutputHandler
                {
                    Message = "Something went wrong"
                });
            }
            else
            {
                return Ok(data);
            }
        }



        [HttpPost("Reset")]
        public async Task<IActionResult> Reset(string username)
        {
            var data = await _accountService.ResetPassword(username);
            if (data == null)
            {
                return Ok(new OutputHandler
                {
                    Message = "Something went wrong"
                });
            }
            else
            {
                return Ok(data);
            }
        }


        [HttpPost("ChangeAccountStatus")]
        public async Task<IActionResult> ChangeAccountStatus(string username)
        {
            var data = await _accountService.UnlockUser(username);
            if (data == null)
            {
                return Ok(new OutputHandler
                {
                    Message = "Something went wrong"
                });
            }
            else
            {
                return Ok(data);
            }
        }


    }
}
