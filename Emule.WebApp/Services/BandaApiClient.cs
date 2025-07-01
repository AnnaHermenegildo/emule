using Emule.WebApp.Models;

namespace Emule.WebApp.Services
{
    public class BandaApiClient : BaseApiClient
    {
        public BandaApiClient(HttpClient httpClient) : base(httpClient, "banda") { }

        public async Task<List<BandaViewModel>> ListarAsync(string? pesquisa = null)
        {
            var url = string.IsNullOrWhiteSpace(pesquisa) ? "" : $"?pesquisa={pesquisa}";
            return await GetAsync<List<BandaViewModel>>(url);
        }

        public async Task FavoritarAsync(Guid usuarioId, Guid bandaId)
        {
            var body = new { usuarioId, bandaId };
            await PostAsync("favoritar", body);
        }
    }
}