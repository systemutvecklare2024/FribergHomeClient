using Newtonsoft.Json;

namespace FribergHomeClient.Services
{
	public class HttpClientService
	{

		public static async Task<List<ValidationProblemDetails>> GetValidationProblemsAsync(HttpResponseMessage response)
		{
			var errorContent = await response.Content.ReadAsStringAsync();
			List<ValidationProblemDetails> problemDetails = new List<ValidationProblemDetails>();

			try
			{
				Dictionary<string, List<string>> errorDictionary = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(errorContent);

				foreach (var p in errorDictionary)
				{
					ValidationProblemDetails validationProblemDetails = new ValidationProblemDetails()
					{
						Key = p.Key,
						Value = p.Value[0]
					};
					problemDetails.Add(validationProblemDetails);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message.ToString());

			}
			return problemDetails;
		}
	}
}
