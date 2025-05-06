namespace FribergHomeClient.Services
{
	public class ResponseService<T>
	{
		public bool Success { get; set; } = true;
		public string Message { get; set; } = String.Empty;
		public T? Data { get; set; }
		public List<ValidationProblemDetails>? ProblemDetails { get; set; }
	}

	public class ResponseService
	{
		public bool Success { get; set; }
		public string Message { get; set; }
	}

	public class ValidationProblemDetails
	{
		public string Key { get; set; }
		public string Value { get; set; }
	}
}
