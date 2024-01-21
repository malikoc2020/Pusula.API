using Domain.Entities;
using Services.Request.UserRequest;
using Services.Response;
using Services.Response.UserResponse;

namespace Services.Services.UserService
{
    public interface IUserService
    {
        Task<BaseResponse> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(string id);
        Task<User> CreateUserAsync(User user);
        Task<User> UpdateUserAsync(User user);
        Task DeleteUserAsync(int id);
        Task<VerifyResponse> SendVerifyCode(string phoneNumber);
        Task<BaseResponse> VerifyPhone(VerifyRequest verifyRequest);
        Task<BaseResponse> UpdateUserAsync(UserUpdateRequest userRequest);
    }
}
