namespace FribergHomeClient.Services
{
    //Author:Emelie
    public class ResponseService<T>
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; } = String.Empty;
        public T? Data { get; set; }
    }
}
