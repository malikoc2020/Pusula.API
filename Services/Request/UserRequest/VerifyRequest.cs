using System.ComponentModel.DataAnnotations;

namespace Services.Request.UserRequest
{
    public class VerifyRequest
    {
        public string UserId { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public int? Code { get; set; }
    }
}
