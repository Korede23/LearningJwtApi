using HMSLeaningJwt.Dto;

namespace HMSLeaningJwt.Implementation
{
    public interface IRoomService
    {
        Task<BaseResponse<Guid>> CreateRoom(CreateRoom request);
        Task<BaseResponse<Guid>> DeleteRoomAsync(string Id);
        Task<BaseResponse<IList<RoomDto>>> GetAllRoomsCreatedAsync();
        Task<BaseResponse<RoomDto>> GetRoomsByIdAsync(string Id);
        Task<BaseResponse<RoomDto>> UpdateRoom(string Id, UpdateRoom request);
    }
}
