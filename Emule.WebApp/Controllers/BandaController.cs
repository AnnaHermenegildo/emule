using Emule.WebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace Emule.WebApp.Controllers
{
    public class BandaController : Controller
    {
        private readonly BandaApiClient _bandaApiClient;

        public BandaController(BandaApiClient bandaApiClient)
        {
            _bandaApiClient = bandaApiClient;
        }

        public async Task<IActionResult> Index(string? pesquisa)
        {
            var bandas = await _bandaApiClient.ListarAsync(pesquisa);
            ViewData["Pesquisa"] = pesquisa;
            return View(bandas);
        }

        [HttpPost]
        public async Task<IActionResult> Favoritar(Guid bandaId)
        {
            var usuarioId = Guid.Parse("00000000-0000-0000-0000-000000000001"); 
            await _bandaApiClient.FavoritarAsync(usuarioId, bandaId);
            TempData["Mensagem"] = "Banda favoritada com sucesso!";
            return RedirectToAction("Index");
        }
    }
}