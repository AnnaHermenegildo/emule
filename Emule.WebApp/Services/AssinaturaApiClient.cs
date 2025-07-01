using Emule.WebApp.Models; 
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Emule.WebApp.Services
{
    public class AssinaturaApiClient : BaseApiClient
    {
        public AssinaturaApiClient(HttpClient httpClient)
            : base(httpClient, "assinatura") { }

        public async Task CriarAsync(CriarAssinaturaViewModel model)
        {
            await PostAsync("criar", model);
        }
    }
}