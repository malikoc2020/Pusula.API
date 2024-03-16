using DAL.Repository;
using DAL.UnitOfWork;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services.DTO;
using Services.Request.UserRequest;
using Services.Response;
using Services.Response.UserResponse;
using System.Data;

namespace Services.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<VerifyCode> _verifyCodeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;


        public UserService(IUnitOfWork unitOfWork, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _unitOfWork = unitOfWork;
            _userRepository = _unitOfWork.GetRepository<User>();
            _verifyCodeRepository = _unitOfWork.GetRepository<VerifyCode>();
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<BaseResponse> GetAllUsersAsync()
        {
            //var users = await _userRepository.GetAllAsync();
            var users = await _userRepository.GetAllAsQueryable().Include(x=>x.Permissions).Select(x=>new UserDTO() { 
            Id = x.Id,
            Name = x.Name,
            SurName = x.SurName,
            AccessFailedCount = x.AccessFailedCount,    
            ConcurrencyStamp = x.ConcurrencyStamp,
            CreatedAt = x.CreatedAt,
            CreatedBy = x.CreatedBy,    
            DateOfStart = x.DateOfStart,
            Email = x.Email,
            EmailConfirmed = x.EmailConfirmed,
            LockoutEnabled = x.LockoutEnabled,
            LockoutEnd = x.LockoutEnd,
            PhoneNumber = x.PhoneNumber,
            PhoneNumberConfirmed = x.PhoneNumberConfirmed,
            Salary = x.Salary,
            SecurityStamp = x.SecurityStamp,
            TwoFactorEnabled = x.TwoFactorEnabled,
            UpdatedAt = x.UpdatedAt,
            UpdatedBy = x.UpdatedBy,
            permissions = x.Permissions.Select(y=>new PermissionDTO() { 
                                                                        Id = y.Id,
                                                                        EndDate = y.EndDate,
                                                                        PermissionTypeId = y.PermissionTypeId,
                                                                        StartDate = y.StartDate,
                                                                        UserId = y.UserId

            
                                                                        }).ToList()
            }).ToListAsync();

            return new BaseResponse(true, "", users);
        }

        public async Task<UserDTO> GetUserByIdAsync(string id)
        {
            var user =  await _userRepository.GetByIdAsync(id);
            if (user is null)
            {
                return null;

            }
            var roles = await _userManager.GetRolesAsync(user);


            var userDTO = new UserDTO()
            {
                Id = user.Id,
                Name = user.Name,
                SurName = user.SurName,
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,
                PhoneNumber = user.PhoneNumber??"",
                PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                SecurityStamp = user.SecurityStamp,
                ConcurrencyStamp = user.ConcurrencyStamp,
                TwoFactorEnabled = user.TwoFactorEnabled,
                LockoutEnd = user.LockoutEnd,
                LockoutEnabled = user.LockoutEnabled,
                AccessFailedCount = user.AccessFailedCount,
                DateOfStart = user.DateOfStart,
                Salary = user.Salary
            };
            userDTO.UserRoles = (await _userManager.GetRolesAsync(user)).ToList();
            return userDTO;

        }
        public async Task<UserEditDTO> GetUserByIdForUserEdit(string id)
        {
            var res = new UserEditDTO();
            res.User = await GetUserByIdAsync(id);
            res.AllRoles = await _roleManager.Roles.Select(x=>x.Name).ToListAsync();
            return res;
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
        public async Task<BaseResponse> UpdateUserAsync(UserUpdateRequest userRequest)
        {
            var userProcess = await _userRepository.GetByIdAsync(userRequest.UserId);
            if (userProcess is not null)
            {
                var user = await _userRepository.GetByIdAsync(userRequest.Id);
                if (user is null)
                {
                    return new BaseResponse(false, "User could not found", null);
                }
                user.Name = userRequest.Name;
                user.SurName = userRequest.SurName;
                user.Email = userRequest.Email;
                user.PhoneNumber = userRequest.PhoneNumber;
                user.DateOfStart = userRequest.DateOfStart;
                user.Salary = userRequest.Salary;
                await _userRepository.UpdateAsync(user);
                await _unitOfWork.CommitAsync();

                // Update roles
                var currentRoles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, currentRoles);

                if (userRequest.UserRoles != null)
                {
                    await _userManager.AddToRolesAsync(user, userRequest.UserRoles);
                }

                return new BaseResponse(true, "", null);
            }
            else
            {
                return new BaseResponse(false, "Process User could not found", null);
            }
        }
    }
}
