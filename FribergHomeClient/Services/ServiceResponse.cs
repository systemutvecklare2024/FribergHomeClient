using FribergHomeClient.Validation;

// Author: Christoffer

namespace FribergHomeClient.Services
{
	public class ServiceResponse<T>
	{
		public bool Success { get; set; } = true;
		public string Message { get; set; } = String.Empty;
		public T? Data { get; set; }
		public List<ValidationProblemDetails>? ProblemDetails { get; set; }
	}

    public class ServiceResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
