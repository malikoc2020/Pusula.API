using DAL.Repository;
using DAL.UnitOfWork;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Services.DTO;
using Services.Request.UserRequest;
using Services.Response;
using Services.Response.UserResponse;

namespace Services.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<VerifyCode> _verifyCodeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userRepository = _unitOfWork.GetRepository<User>();
            _verifyCodeRepository = _unitOfWork.GetRepository<VerifyCode>();
        }

        public async Task<BaseResponse> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return new BaseResponse(true, "", users);
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
        public async Task<VerifyResponse> SendVerifyCode(string phoneNumber)
        {
            Random random = new Random();
            var code = random.Next(100000, 999999);

            var verifyCode = new VerifyCode() {PhoneNumber = phoneNumber
                                                , Code = code
                                                , CreatedAt = DateTime.Now
                                                , CreatedBy = 1
            };
            await _verifyCodeRepository.AddAsync(verifyCode);
            await _unitOfWork.CommitAsync();
            return new VerifyResponse() { PhoneNumber = phoneNumber, Code = code };
        }
        public async Task<BaseResponse> VerifyPhone(VerifyRequest verifyRequest)
        {
            var verifyCode = await _verifyCodeRepository.GetAllAsQueryable().Where(x=>x.PhoneNumber == verifyRequest.PhoneNumber).OrderByDescending(x=>x.CreatedAt).FirstOrDefaultAsync();
            if (verifyCode?.Code == verifyRequest.Code)
            {
                var user = await _userRepository.GetByIdAsync(verifyRequest.UserId);
                if (user is not null)
                {
                    user.PhoneNumberConfirmed = true;
                    await _userRepository.UpdateAsync(user);
                    await _unitOfWork.CommitAsync();
                    return new BaseResponse(true, "", null);
                }
                else
                {
                    return new BaseResponse(false, "User could not found", null);
                }
            }
            return new BaseResponse(false, "Wrong Code", null);
        }
    }
}
