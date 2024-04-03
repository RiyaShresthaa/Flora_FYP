using FloraServer.Repositories;
using FloraSharedLibrary.DTOs;
using FloraSharedLibrary.Responses;
using Microsoft.AspNetCore.Mvc;
namespace FloraServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(IUserAccount accountService) : ControllerBase
    {
        [HttpPost("register")]

        public async Task<ActionResult<ServiceResponse>> CreateAccount(UserDTO model)
        {
            if (model is null) return BadRequest("Model is Null");
            var response = await accountService.Register(model);
            return Ok(response);
        }

        [HttpPost("Login")]

        public async Task<ActionResult<LoginResponse>> LoginAccount(LoginDTO model)
        {
            if (model is null) return BadRequest("Model is Null");
            var response = await accountService.Login(model);
            return Ok(response);
        }
    }
}
