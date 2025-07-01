using Emule.WebApp.Models;
using Emule.WebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace Emule.WebApp.Controllers
{
    public class PlanoController : Controller
    {
        private readonly PlanoApiClient _planoApiClient;

        public PlanoController(PlanoApiClient planoApiClient)
        {
            _planoApiClient = planoApiClient;
        }

        public async Task<IActionResult> Selecionar(Guid usuarioId)
        {
            var planos = await _planoApiClient.ListarAsync();
            TempData["UsuarioId"] = usuarioId;
            return View(planos);
        }
    }
}