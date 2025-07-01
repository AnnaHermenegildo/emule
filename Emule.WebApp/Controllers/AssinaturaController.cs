using Emule.WebApp.Models;
using Emule.WebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace Emule.WebApp.Controllers
{
    public class AssinaturaController : Controller
    {
        private readonly AssinaturaApiClient _assinaturaApiClient;

        public AssinaturaController(AssinaturaApiClient assinaturaApiClient)
        {
            _assinaturaApiClient = assinaturaApiClient;
        }

        [HttpPost]
        public async Task<IActionResult> Criar(CriarAssinaturaViewModel model)
        {
            await _assinaturaApiClient.CriarAsync(model);
            TempData["Mensagem"] = "Assinatura criada com sucesso!";
            return RedirectToAction("Index", "Home");
        }
    }
}
