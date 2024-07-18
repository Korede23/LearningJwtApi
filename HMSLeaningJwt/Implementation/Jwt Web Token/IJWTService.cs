using HMSLeaningJwt.Dto;

namespace HMSLeaningJwt.Implementation.Jwt_Web_Token
{
    public interface IJWTService
    {
        string JwtWebToken(UserDto user);
    }
}
