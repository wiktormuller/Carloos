namespace JobJetRestApi.Web.Contracts.V1.Responses
{
    public class Result<T>
    {
        public T Data { get; }
        //public string Message { get; }
        //public bool Succeeded { get; }
        //public string[] Errors { get; }
        
        public Result(T data)
        {
            Data = data;
        }
    }
}