using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Emule.WebApp.Services
{
    public abstract class BaseApiClient
    {
        protected readonly HttpClient _httpClient;

       
        protected BaseApiClient(HttpClient httpClient, string baseRoute)
        {
            if (!baseRoute.EndsWith("/"))
                baseRoute += "/";

            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri($"http://localhost:5171/api/{baseRoute}");
        }

        protected async Task<T?> GetAsync<T>(string endpoint = "")
        {
            try
            {
                var response = await _httpClient.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<T>();
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Erro HTTP na requisição GET: {e.Message}");
                throw; 
            }
        }

        protected async Task<TResponse?> PostAsync<TRequest, TResponse>(string endpoint, TRequest data)
        {
            var response = await _httpClient.PostAsJsonAsync(endpoint, data);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TResponse>();
        }

        protected async Task<HttpResponseMessage> PostAsync<TRequest>(string endpoint, TRequest data)
        {
            var response = await _httpClient.PostAsJsonAsync(endpoint, data);
            return response;
        }

        protected async Task<HttpResponseMessage> PutAsync<TRequest>(string endpoint, TRequest data)
        {
            var response = await _httpClient.PutAsJsonAsync(endpoint, data);
            return response;
        }

        // DELETE genérico que recebe um endpoint e retorna HttpResponseMessage
        protected async Task<HttpResponseMessage> DeleteAsync(string endpoint)
        {
            var response = await _httpClient.DeleteAsync(endpoint);
            return response;
        }
    }
}
