namespace Çore.ExceptionCodes
{
    public static class ExceptionCodes
    {
        public static List<ExceptionCode> ExceptionCodeList = new List<ExceptionCode>
        {
           new ExceptionCode("E0001","Bu hata sistemde aynı telefon numarasını yada emaili kullanan birden fazla kullanıcı olduğunda alınmaktadır."),
           new ExceptionCode("E0002","Bu hata sisteme yeni kullanıcı eklenirken beklenmedik bir exception fırlatıldığında alınır."),
           new ExceptionCode("E0003","Bu hata sistemde beklenmedik bi şekilde kullanıcı kaydedilemediğinde alınır.")
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
