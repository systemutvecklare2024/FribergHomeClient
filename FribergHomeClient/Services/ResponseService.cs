namespace FribergHomeClient.Services
{
    public class ResponseService<T>
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; } = String.Empty;
        public T? Data { get; set; }
    }

    public class ResponseService
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
