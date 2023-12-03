namespace Services.Request.AuthenticationRequest
{
    public class LoginRequest
    {
        public string LoginName { get; set; } = "";
        public string Password { get; set; } = "";
        public bool RememberMe { get; set; } = false;
    }
}
