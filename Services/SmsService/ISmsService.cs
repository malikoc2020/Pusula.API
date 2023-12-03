namespace Services.SmsService
{
    public interface ISmsService
    {
        Task<bool> SendSMS(string phoneNumber, string message);
    }
}
