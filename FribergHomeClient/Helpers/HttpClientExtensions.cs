using FribergHomeClient.Services;
using FribergHomeClient.Validation;
using System.Net.Http.Json;

namespace FribergHomeClient.Helpers
{
    //Author: Emelie
    public static class HttpClientExtensions
    {
        public static async Task<ServiceResponse<T>> GetServiceResponseAsync<T>(this HttpClient client, string url)
        {
            try
            {
                var response = await client.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    List<ValidationProblemDetails> problemDetails = await BengelService.GetValidationProblemsAsync(response);
                    return new ServiceResponse<T>
                    {
                        Success = false,
                        Message = $"Något gick fel: {response.ReasonPhrase}",
                        ProblemDetails = problemDetails
                    };
                }

                return new ServiceResponse<T>
                {
                    Success = true,
                    Message = response.ReasonPhrase ?? "",
                    Data = await response.Content.ReadFromJsonAsync<T>()
                };
            }
            catch (HttpRequestException ex)
            {
                return new ServiceResponse<T>
                {
                    Success = false,
                    Message = $"Något gick fel: {ex.Message}"
                };
            }
        }

        public static async Task<ServiceResponse> PutServiceResponseAsync<T>(this HttpClient client, string url, T data)
        {
            try
            {
                var response = await client.PutAsJsonAsync(url, data);
                if (!response.IsSuccessStatusCode)
                {
                    List<ValidationProblemDetails> problemDetails = await BengelService.GetValidationProblemsAsync(response);
                    return new ServiceResponse
                    {
                        Success = false,
                        Message = $"Något gick fel: {response.ReasonPhrase}",
                        ProblemDetails = problemDetails
                    };
                }

                return new ServiceResponse
                {
                    Success = true,
                    Message = response.ReasonPhrase ?? ""
                };
            }
            catch (HttpRequestException ex)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = $"Något gick fel: {ex.Message}"
                };
            }
        }

        public static async Task<ServiceResponse> PostServiceResponseAsync<T>(this HttpClient client, string url, T data)
        {
            try
            {
                var response = await client.PostAsJsonAsync(url, data);
                if (!response.IsSuccessStatusCode)
                {
                    List<ValidationProblemDetails> problemDetails = await BengelService.GetValidationProblemsAsync(response);
                    return new ServiceResponse
                    {
                        Success = false,
                        Message = $"Något gick fel: {response.ReasonPhrase}",
                        ProblemDetails = problemDetails
                    };
                }

                return new ServiceResponse
                {
                    Success = true,
                    Message = response.ReasonPhrase ?? "",
                };
            }
            catch (HttpRequestException ex)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = $"Något gick fel: {ex.Message}"
                };
            }
        }

        public static async Task<ServiceResponse> DeleteServiceResponseAsync(this HttpClient client, string url)
        {
            try
            {
                var response = await client.DeleteAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    List<ValidationProblemDetails> problemDetails = await BengelService.GetValidationProblemsAsync(response);
                    return new ServiceResponse
                    {
                        Success = false,
                        Message = $"Något gick fel: {response.ReasonPhrase}",
                        ProblemDetails = problemDetails
                    };
                }

                return new ServiceResponse
                {
                    Success = true,
                    Message = response.ReasonPhrase ?? ""
                };
            }
            catch (HttpRequestException ex)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = $"Något gick fel: {ex.Message}"
                };
            }
        }
    }
}
