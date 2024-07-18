using HMSLeaningJwt.Dto;

namespace HMSLeaningJwt.Implementation
{
    public interface IUserService
    {
        Task<BaseResponse<Guid>> CreateUser(CreateUser request);
        Task<BaseResponse<Guid>> DeleteUserAsync(string Id);
        Task<UserDto> GetUserByUserNAmeAsync(string username, string password);
    }
}
