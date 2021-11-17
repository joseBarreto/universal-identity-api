namespace UniversalIdentity.Domain.Wrappers
{
    public class Response<T>
    {
        public static Response<T> Create()
        {
            return new Response<T>();
        }

        public static Response<T> Create(T data)
        {
            return new Response<T>(data);
        }

        public Response()
        {
        }

        public Response(T data)
        {
            Succeeded = data != null;
            Message = string.Empty;
            Errors = null;
            Data = data;
        }

        public T Data { get; set; }
        public bool Succeeded { get; set; }
        public string[] Errors { get; set; }
        public string Message { get; set; }
    }
}
