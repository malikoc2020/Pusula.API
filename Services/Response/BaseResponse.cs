namespace Services.Response
{
    public class BaseResponse
    {
        public BaseResponse()
        {
            IsSuccess = true;
        }
        public BaseResponse(bool IsSuccess, string Message, object? Result)
        {
            this.IsSuccess = IsSuccess;
            this.Message = Message;
            this.Result = Result;
        }
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = "";
        public object? Result { get; set; }
    }
}
