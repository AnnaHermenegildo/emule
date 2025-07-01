using Emule.ApplicationLayer.DTOs;
using Emule.WebApp.Models;
using Emule.WebApp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Emule.WebApp.Controllers
{
    public class MusicaController : Controller
    {
        private readonly MusicaApiClient _musicaApiClient;

        public MusicaController(MusicaApiClient musicaApiClient)
        {
            _musicaApiClient = musicaApiClient;
        }

        public async Task<IActionResult> Index(string? pesquisa)
        {
            var musicas = await _musicaApiClient.ListarAsync(pesquisa);
            ViewData["Pesquisa"] = pesquisa;
            return View(musicas);
        }

        [HttpPost]
        public async Task<IActionResult> Favoritar(Guid musicaId)
        {
            var usuarioId = Guid.Parse("00000000-0000-0000-0000-000000000001");
            await _musicaApiClient.FavoritarAsync(usuarioId, musicaId);
            TempData["Mensagem"] = "Música favoritada com sucesso!";
            return RedirectToAction("Index");
        }


        public async Task<List<MusicaDto>> ObterFavoritasPorUsuarioAsync(Guid usuarioId)
        {
            var response = await _musicaApiClient.ObterFavoritasPorUsuarioAsync(usuarioId);
            return response ?? new List<MusicaDto>();
        }

    }
}
