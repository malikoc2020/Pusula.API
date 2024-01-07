using Domain.Base;

namespace Domain.Entities
{
    public class VerifyCode:BaseEntity
    {
        public string PhoneNumber { get; set; }
        public int Code { get; set; }
    }
}
