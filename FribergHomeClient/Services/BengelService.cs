using FribergHomeClient.Validation;
using Newtonsoft.Json;

// Author: Glate

namespace FribergHomeClient.Services
{
	public class BengelService
	{
		public static async Task<List<ValidationProblemDetails>> GetValidationProblemsAsync(HttpResponseMessage response)
		{
			var errorContent = await response.Content.ReadAsStringAsync();
			List<ValidationProblemDetails> problemDetails = new List<ValidationProblemDetails>();

			try
			{
				Dictionary<string, List<string>> errorDictionary = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(errorContent);

				if (errorDictionary != null)
				{
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
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message.ToString());

			}
			return problemDetails;
		}
	}
}
