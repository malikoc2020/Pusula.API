using Domain.Entities;
using Services.Request.AuthenticationRequest;
using Services.Response;

namespace Services.AuthenticationService
{
    public interface IAuthenticationService
    {
        Task<BaseResponse> Login(LoginRequest loginRequest);
        string GenerateJwtToken(User user);
        Task<BaseResponse> Register(RegisterRequest registerRequest);
    }
}
