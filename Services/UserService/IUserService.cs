using Domain.Entities;
using Services.Response;

namespace Services.UserService
{
    public interface IUserService
    {
        Task<BaseResponse> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(string id);
        Task<User> CreateUserAsync(User user);
        Task<User> UpdateUserAsync(User user);
        Task DeleteUserAsync(int id);
    }
}
