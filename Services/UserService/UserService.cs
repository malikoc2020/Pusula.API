using DAL.Repository;
using DAL.UnitOfWork;
using Domain.Entities;
using Services.Response;

namespace Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userRepository = _unitOfWork.GetRepository<User>();
        }

        public async Task<BaseResponse> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return new BaseResponse(true,"",users);
        }

        public async Task<User> GetUserByIdAsync(string id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<User> CreateUserAsync(User user)
        {
            await _userRepository.AddAsync(user);
            await _unitOfWork.CommitAsync();
            return user;
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            await _userRepository.UpdateAsync(user);
            await _unitOfWork.CommitAsync();
            return user;
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user is not null)
            {
                await _userRepository.DeleteAsync(user);
                await _unitOfWork.CommitAsync();
                return;
            }
            return;
        }
    }
}
