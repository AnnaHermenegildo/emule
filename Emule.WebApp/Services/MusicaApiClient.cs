using Emule.ApplicationLayer.DTOs;
using Emule.WebApp.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace Emule.WebApp.Services
{
    public class MusicaApiClient : BaseApiClient
    {
        public MusicaApiClient(HttpClient httpClient) : base(httpClient, "musica") { }

        public async Task<List<MusicaViewModel>> ListarAsync(string? pesquisa = null)
        {
            var url = string.IsNullOrWhiteSpace(pesquisa) ? "" : $"?pesquisa={pesquisa}";
            return await GetAsync<List<MusicaViewModel>>(url);
        }

        public async Task FavoritarAsync(Guid usuarioId, Guid musicaId)
        {
            var body = new { usuarioId, musicaId };
            await PostAsync("favoritar", body);
        }


        public async Task<List<MusicaDto>> ObterFavoritasPorUsuarioAsync(Guid usuarioId)
        {
            var url = $"api/musica/favoritas/{usuarioId}";


            var response = await _httpClient.GetAsync($"favoritas?id={usuarioId}");

            if (!response.IsSuccessStatusCode)
            {
                return new List<MusicaDto>();
            }

            var json = await response.Content.ReadAsStringAsync();

            // Desserializa para lista de MusicaDto
            var musicas = JsonSerializer.Deserialize<List<MusicaDto>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return musicas ?? new List<MusicaDto>();
        }
    }
}
