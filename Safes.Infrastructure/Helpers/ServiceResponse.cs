namespace Enjaz.Isp.Infrastructure.Helpers
{
    public class ServiceResponse<T>
    {
        public ServiceResponse(T value)
        {
            Value = value;
        }

        public T Value { get; set; }
        public string ResponseMessage { get; set; }
        public ResponseError Error { get; set; }

        public void SetSucsessResponse(T value, string msg = null)
        {
            ResponseMessage = msg;
            Value = value;
        }
    }
}