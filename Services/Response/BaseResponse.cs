namespace Services.Response
{
    public class BaseResponse
    {
        public BaseResponse()
        {
            IsSuccess = true;
        }
        public BaseResponse(bool IsSuccess, string ErrorMessage, object? Result)
        {
            this.IsSuccess = IsSuccess;
            this.ErrorMessage = ErrorMessage;
            this.Result = Result;
        }
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; } = "";
        public object? Result { get; set; }
    }
}
