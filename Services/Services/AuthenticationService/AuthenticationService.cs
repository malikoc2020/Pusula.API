using DAL.Repository;
using DAL.UnitOfWork;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Services.Request.AuthenticationRequest;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Services.Response;
using Microsoft.Extensions.Logging;
using Core.Extensions;
using Microsoft.Extensions.Configuration;
using Services.DTO;
using Newtonsoft.Json;

namespace Services.Services.AuthenticationService
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ILogger<AuthenticationService> _logger;
        private readonly IConfiguration _configuration;
        //private readonly IRepository<User> _userRepository;
        //private readonly IUnitOfWork _unitOfWork;
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;

        public AuthenticationService(ILogger<AuthenticationService> logger/*, IUnitOfWork unitOfWork*/, IConfiguration configuration, RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            _logger = logger;
            _configuration = configuration;
            //_unitOfWork = unitOfWork;
            //_userRepository = _unitOfWork.GetRepository<User>();
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<BaseResponse> Login(LoginRequest loginRequest)
        {
            //var users = await _userRepository.GetAllAsQueryable().Where(x=>x.Email== loginRequest.LoginName || x.PhoneNumber== loginRequest.LoginName).ToListAsync();
            var user = await _userManager.FindByNameAsync(loginRequest.LoginName);
            if (user is null)
            {
                //throw new Exception();//can not found
                return new BaseResponse(false, "Lütfen kullanıcı adınızı kontrol edip tekrar deneyiniz.", null);
            }
            else if (await _userManager.CheckPasswordAsync(user, loginRequest.Password))
            {
                //var user = users.FirstOrDefault();
                //var userPasswordGenerateDTO = new UserPasswordGenerateDTO(user.Name,user.SurName,user.Email,user.PhoneNumber);
                //var passwordHasher = new PasswordHasher<UserPasswordGenerateDTO>();
                //var passwordHash = passwordHasher.HashPassword(userPasswordGenerateDTO, loginRequest.Password);
                //if (passwordHash==user.PasswordHash)
                //var result = passwordHasher.VerifyHashedPassword(userPasswordGenerateDTO,user.PasswordHash, loginRequest.Password);

                //if (result == PasswordVerificationResult.Success)
                //{
                var jwtToken = GenerateJwtToken(user);
                var userDTO = new UserDTO()
                {
                    Id = user.Id,
                    Name = user.Name,
                    SurName = user.SurName,
                    Email = user.Email,
                    EmailConfirmed = user.EmailConfirmed,
                    PhoneNumber = user.PhoneNumber,
                    PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                    SecurityStamp = user.SecurityStamp,
                    ConcurrencyStamp = user.ConcurrencyStamp,
                    TwoFactorEnabled = user.TwoFactorEnabled,
                    LockoutEnd = user.LockoutEnd,
                    LockoutEnabled = user.LockoutEnabled,
                    AccessFailedCount = user.AccessFailedCount,
                    Token = jwtToken
                };
                return new BaseResponse(true, "", userDTO);
                //}
                //else
                //{
                //    return new BaseResponse(false,"Hatalı Şifre",null);
                //}
            }
            else
            {
                //_logger.LogWarning($"{nameof(Login)} ErrorCode=E0001 loginRequest={loginRequest.ToJson()}");
                //return new BaseResponse(false, "Girilen Bilgilerle İlgili Sistemde Beklenmedik Hata Oluştuğu Tespit Edilmiştir. Lütfen Hesabınızla ilgili sistem yöneticinizle iletişime geçiniz. Hata Kodu:E0001", null);
                return new BaseResponse(false, "Hatalı Şifre", null);
            }
        }

        public async Task<BaseResponse> Register(RegisterRequest registerRequest)
        {
            //var userDTO = new UserPasswordGenerateDTO(registerRequest.Name, registerRequest.SurName, registerRequest.Email, registerRequest.PhoneNumber);
            //var passwordHasher = new PasswordHasher<UserPasswordGenerateDTO>();
            //Random random = new Random();
            //var password = registerRequest.Password;//random.Next(100000, 999999);
            //var passwordHash = passwordHasher.HashPassword(userDTO, password);

            var newUser = new User()
            {
                Name = registerRequest.Name,
                SurName = registerRequest.SurName,
                Email = registerRequest.Email,
                EmailConfirmed = false,
                UserName = registerRequest.Email,
                PhoneNumber = registerRequest.PhoneNumber,
                PhoneNumberConfirmed = false,
                //PasswordHash= passwordHash,
                SecurityStamp = "",
                ConcurrencyStamp = "",
                TwoFactorEnabled = false,
                LockoutEnd = null,
                LockoutEnabled = false,
                AccessFailedCount = 0,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                CreatedBy = 1,
                UpdatedBy = 1
            };

            try
            {
                //return new BaseResponse(false, "Kayıt İşlemi BAŞARISIZ. Email adresiniz ile giriş yapabilirsiniz.", null);

                //await _userRepository.AddAsync(newUser);
                //await _unitOfWork.CommitAsync();
                //var res = await _userManager.CreateAsync(newUser, registerRequest.Password);
                var res = await _userManager.CreateAsync(newUser);
                if (res.Succeeded)
                {
                    // this line of code bypasses the password validation
                    newUser.PasswordHash = _userManager.PasswordHasher.HashPassword(newUser, registerRequest.Password);

                    // update the user object with the hashed password
                    var resPassUpdate = await _userManager.UpdateAsync(newUser);
                    if (resPassUpdate.Succeeded)
                    {
                        return new BaseResponse(res.Succeeded, "Kayıt İşlemi Başarılı. Email adresiniz ile giriş yapabilirsiniz.", null);
                    }
                    else
                    {
                        _logger.LogWarning($"{nameof(Login)} ErrorCode=E0001 loginRequest={registerRequest.ToJson()}");
                        return new BaseResponse(false, "Kayıt İşlemi Başarılı. Ancak Şifrenizle İlgili Sistemde Beklenmedik Hata Oluştuğu Tespit Edilmiştir. Lütfen Hesabınızla ilgili sistem yöneticinizle iletişime geçiniz. Hata Kodu:E0001", null);
                    }
                }
                else
                {
                    _logger.LogWarning($"{nameof(Register)} ErrorCode=E0002 registerRequest={registerRequest.ToJson()}");
                    return new BaseResponse(res.Succeeded, "Kayıt İşlemi BARAŞIRIZ. Lütfen tekrar deneyiniz. Hata Kodu:E0003", res.Errors);
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"{nameof(Register)} ErrorCode=E0003 registerRequest={registerRequest.ToJson()} Error:{ex.Message}");
                return new BaseResponse(false, "Kayıt İşlemi Başarısız. Lütfen tekrar deneyiniz. Hata Kodu:E0002", null);
            }
        }

        public string GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
                                        {
                                            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                                            new Claim(JwtRegisteredClaimNames.Email, user.Email)
                                        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
