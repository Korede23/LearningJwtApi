using HMSLeaningJwt.DbContext;
using HMSLeaningJwt.Dto;
using HMSLeaningJwt.Models;
using Microsoft.EntityFrameworkCore;

namespace HMSLeaningJwt.Implementation
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _dbContext;

        public UserService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<BaseResponse<Guid>> CreateUser(CreateUser request)
        {


            if (request != null)
            {
                var existingUser = await _dbContext.Users.FirstOrDefaultAsync(x =>
                        x.Id == request.Id);

                if (existingUser != null)
                {
                    // Room already exists
                    return new BaseResponse<Guid>
                    {
                        Success = true,
                        Message = $"User {request.UserName} already exists.",
                        Hasherror = true
                    };
                }

                //create a new one
                var user = new User
                {
                    Id = request.Id,
                    UserName = request.UserName,
                    Age = request.Age,
                    Name = request.Name,
                    Password = request.Password,
                };

                await _dbContext.Users.AddAsync(user);
                _dbContext.SaveChanges();
            }
            return new BaseResponse<Guid>
            {
                Success = true,
                Message = $"User {request.UserName} Created Successfully"

            };

        }


        public async Task<BaseResponse<Guid>> DeleteUserAsync(string Id)
        {
            try
            {
                var user = await _dbContext.Users.FirstOrDefaultAsync(r => r.Id == Id);
                if (user == null)
                {
                    return new BaseResponse<Guid>
                    {
                        Success = false,
                        Message = $"User with ID {Id} not found."
                    };
                }

                _dbContext.Users.Remove(user);
                if (await _dbContext.SaveChangesAsync() > 0)
                {
                    return new BaseResponse<Guid>
                    {
                        Success = true,
                        Message = $"User with ID {Id} has been deleted successfully.",

                    };
                }
                else
                {
                    return new BaseResponse<Guid>
                    {
                        Success = false,
                        Message = $"Failed to delete user with ID {Id}. There was an error in the deletion process."
                    };
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<Guid>
                {
                    Success = false,
                    Message = $"An error occurred while deleting the user with ID {Id}: {ex.Message}"
                };
            }
        }

        public async Task<UserDto> GetUserByUserNAmeAsync(string username, string password)
        {

            var users = await _dbContext.Users
             .Where(x => x.UserName == username)
             .Select(x => new UserDto()
             {
                 Id = x.Id,
                 Name = x.Name,
                 Age = x.Age,
                 UserName = username,
                 Password = password

             }).FirstOrDefaultAsync();
            if (users != null)
            {
                return new UserDto()
                {
                    Id = users.Id,
                    Name = users.Name,
                    Age = users.Age,
                    UserName = username,
                    Password = password

                };
            }
            return null;
        }


        //public async Task<BaseResponse<IList<RoomDto>>> GetAllRoomsCreatedAsync()
        //{
        //    var rooms = await _dbContext.Rooms
        //     .Select(x => new RoomDto()
        //     {
        //         Id = x.Id,
        //         RoomName = x.RoomName,
        //         Description = x.Description,
        //         RoomPrize = x.RoomPrize,
        //         RoomType = x.RoomType,
        //         MaxOccupancy = x.MaxOccupancy,
        //         // Amenity = x.Amenity
        //     }).ToListAsync();


        //    return new BaseResponse<IList<RoomDto>>
        //    {
        //        Success = true,
        //        Message = "Rooms Succesfully Retrieved",
        //        Data = rooms
        //    };
        //}


        //public async Task<BaseResponse<RoomDto>> GetRoomsByIdAsync(string Id)
        //{

        //    var rooms = await _dbContext.Rooms
        //     .Where(x => x.Id == Id)
        //     .Select(x => new RoomDto()
        //     {
        //         Id = x.Id,
        //         RoomName = x.RoomName,
        //         Description = x.Description,
        //         RoomPrize = x.RoomPrize,
        //         RoomType = x.RoomType,
        //         MaxOccupancy = x.MaxOccupancy,
        //     }).FirstOrDefaultAsync();
        //    if (rooms != null)
        //    {
        //        return new BaseResponse<RoomDto>
        //        {
        //            Success = true,
        //            Message = $"Room {Id} Retrieved succesfully",
        //            Data = rooms

        //        };
        //    }
        //    else
        //    {
        //        return new BaseResponse<RoomDto>
        //        {
        //            Success = false,
        //            Message = $"Room {Id} Retrieval Failed"
        //        };
        //    }

        //}



        //public async Task<BaseResponse<RoomDto>> UpdateRoom(string Id, UpdateRoom request)
        //{
        //    try
        //    {
        //        var room = _dbContext.Rooms.FirstOrDefault(x => x.Id == Id);
        //        if (room == null)
        //        {
        //            return new BaseResponse<RoomDto>
        //            {
        //                Success = false,
        //                Message = $"Room {request.RoomName} Update failed",
        //                Hasherror = true
        //            };

        //        }
        //        room.Description = request.Description;
        //        room.RoomPrize = request.RoomPrize;
        //        room.RoomType = request.RoomType;
        //        room.MaxOccupancy = request.MaxOccupancy;
        //        _dbContext.Rooms.Update(room);
        //        if (await _dbContext.SaveChangesAsync() > 0)
        //        {
        //            return new BaseResponse<RoomDto>
        //            {
        //                Success = true,
        //                Message = $"Room {request.Id} Updated Succesfully",
        //            };

        //        }
        //        else
        //        {
        //            return new BaseResponse<RoomDto>
        //            {
        //                Success = false,
        //                Message = $"Room {request.Id} Update failed",
        //                Hasherror = true
        //            };
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return new BaseResponse<RoomDto>
        //        {
        //            Success = false,
        //            Message = $"Room {request.Id} Update failed",
        //            Hasherror = true
        //        };
        //    }

    }
}
