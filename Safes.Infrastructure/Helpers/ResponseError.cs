namespace Enjaz.Isp.Infrastructure.Helpers
{
    public class ResponseError
    {
        public ResponseError(int? errorTypecode, string message, string errorDetails = null)
        {
            ErrorCode = errorTypecode ?? 0;
            Message = message;
            Message = errorDetails;
        }

        public ResponseError(string message, string errorDetails = null)
        {
            Message = message;
            DetailMessage = errorDetails;
        }

        public string Message { get; set; }
        public string DetailMessage { get; set; }
        public int ErrorCode { get; set; }

        public override string ToString()
        {
            return Message;
        }
    }
}