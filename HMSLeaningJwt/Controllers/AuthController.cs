using HMSLeaningJwt.Dto;
using HMSLeaningJwt.Implementation;
using HMSLeaningJwt.Implementation.Jwt_Web_Token;
using Microsoft.AspNetCore.Mvc;
namespace HMSLeaningJwt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJWTService _JWTService;
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public AuthController(IJWTService JWTService, IUserService userService, IConfiguration configuration)
        {
            _JWTService = JWTService;
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _userService.GetUserByUserNAmeAsync(request.UserName, request.Password);
            if (user == null)
            {
                return Unauthorized();
            }

            string tokenResponse = _JWTService.JwtWebToken(user);
            return Ok(new { tokenResponse });
        }
    }
}
