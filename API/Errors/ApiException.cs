namespace API.Errors
{
    //public class ApiException
    //{
    //    public ApiException(int statusCode,string message,string details)
    //    {
    //        StatusCode = statusCode;
    //        Message = message;
    //        Details = details;
    //    }

    //    public int StatusCode { get; set; }
    //    public string Message { get; set; }
    //    public string Details { get; set; }
    //}

    public class ApiException
    {
        public ApiException() { }

        public ApiException(int statusCode, string message, string details = null, string errorCode = null, DateTime? timestamp = null)
        {
            StatusCode = statusCode;
            Message = message;
            Details = details;
            ErrorCode = errorCode;
            Timestamp = timestamp ?? DateTime.UtcNow;
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }
        public string ErrorCode { get; set; }
        public DateTime Timestamp { get; set; }
    }
    public class NotFoundException : Exception
    {
        public NotFoundException() { }

        public NotFoundException(string message) : base(message) { }

        public NotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }


}