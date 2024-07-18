using HMSLeaningJwt.Dto;
using HMSLeaningJwt.Implementation.Jwt_Web_Token;
using HMSLeaningJwt.Implementation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HMSLeaningJwt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJWTService _jWTService;

        public UserController(IUserService userService, IJWTService jWTService)
        {
            _userService = userService;
            _jWTService = jWTService;
        }

        [HttpPost("create-user")]
        public async Task<IActionResult> CreateUser(CreateUser request)
        {
            var user = await _userService.CreateUser(request);
            if (user.Success)
            {
                return Ok(user);
            }
            else
            {
                return BadRequest(user);
            }
        }

        [HttpDelete("delete-user")]
        public async Task<IActionResult> DeleteUserAsync([FromRoute] string id)
        {
            var user = await _userService.DeleteUserAsync(id);
            if (user.Success)
            {
                return Ok(user);
            }
            return BadRequest(user);

        }

        //[HttpGet("get-user-by-username/{id}")]
        //public async Task<IActionResult> GetUsersUsernameAsync(string username, string password)
        //{

        //    var user = await _userService.GetUserByUserNAmeAsync(username , password);
        //    if (user!= null)
        //    {
        //        return Ok(user);
        //    }
        //    else
        //    {
        //        return BadRequest(user);
        //    }

        //}

    }
}
