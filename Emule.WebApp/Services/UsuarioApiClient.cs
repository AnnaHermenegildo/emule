using Emule.ApplicationLayer.DTOs;
using Emule.WebApp.Models;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Emule.WebApp.Services
{
    public class UsuarioApiClient : BaseApiClient
    {
        public UsuarioApiClient(HttpClient httpClient)
            : base(httpClient, "usuario") { }

        public async Task<UsuarioDto> LoginAsync(LoginViewModel vm)
        {
            var response = await PostAsync("login", vm);
            var conteudo = await response.Content.ReadAsStringAsync();
            var retornoJson = JsonSerializer.Deserialize<UsuarioDto>(conteudo, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return retornoJson;
        }

        public async Task RegistrarAsync(NovoUsuarioDto vm)
        {
            await PostAsync("registrar", vm);
        }

        public async Task<UsuarioDto> Obter(Guid id)
        {
            return await GetAsync<UsuarioDto>($"ObterPorId?id={id}");
        }
    }
}