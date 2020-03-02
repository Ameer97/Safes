namespace Enjaz.Isp.Infrastructure.Helpers
{
    public class ClientResponse<T>
    {
        public ClientResponse(bool error, string message)
        {
            Error = error;
            Message = message;
        }

        public ClientResponse(T data)
        {
            Error = false;
            Message = null;
            Data = data;
        }
        public ClientResponse(T data, string msg)
        {
            Error = false;
            Message = msg;
            Data = data;
        }

        public bool Error { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}