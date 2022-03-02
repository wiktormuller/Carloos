namespace JobJetRestApi.Application.Contracts.V1.Responses
{
    public class Response<T>
    {
        public T Data { get; }
        public string Message { get; } = string.Empty;
        public bool Succeeded { get; } = true;
        public string[] Errors { get; } = null;
        
        public Response(T data, string message, bool succeeded, string[] errors)
        {
            Data = data;
            Message = message;
            Succeeded = succeeded;
            Errors = errors;
        }
    }
}