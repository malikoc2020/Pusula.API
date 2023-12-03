namespace Domain.ExceptionCodes
{
    public static class ExceptionCodes
    {
        public static List<ExceptionCode> ExceptionCodeList = new List<ExceptionCode>
        {
           new ExceptionCode("E0001","Bu hata sistemde aynı telefon numarasını yada emaili kullanan birden fazla kullanıcı olduğunda alınmaktadır.")
        };
    }

    public class ExceptionCode
    {
        public ExceptionCode(string code, string message)
        {
            this.Code = code;
            this.Message = message;
        }
        public string Code { get; set; } = "";
        public string Message { get; set; } = "";
    }
}
