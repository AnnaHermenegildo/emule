using Emule.WebApp.Models;

namespace Emule.WebApp.Services
{
    public class PlanoApiClient : BaseApiClient
    {
        public PlanoApiClient(HttpClient httpClient)
            : base(httpClient, "planos")  
        {
        }

        public async Task<List<PlanoViewModel>?> ListarAsync()
        {
            return await GetAsync<List<PlanoViewModel>>("");
        }

        public async Task<PlanoViewModel?> ObterPorIdAsync(Guid id)
        {
            return await GetAsync<PlanoViewModel>($"{id}");
        }
    }
}
