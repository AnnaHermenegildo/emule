using Emule.Domain.Model;
using Emule.Domain.Model.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Emule.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssinaturasController : ControllerBase
    {
        private readonly IAssinaturaRepository _assinaturaRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IPlanoRepository _planoRepository;

        public AssinaturasController(IAssinaturaRepository assinaturaRepository, IUsuarioRepository usuarioRepository, IPlanoRepository planoRepository)
        {
            _assinaturaRepository = assinaturaRepository;
            _usuarioRepository = usuarioRepository;
            _planoRepository = planoRepository;
        }

        // POST api/assinaturas
        [HttpPost]
        public async Task<IActionResult> CriarAssinatura([FromBody] CriarAssinaturaRequest request)
        {
            var usuario = await _usuarioRepository.ObterPorIdAsync(request.UsuarioId);
            if (usuario == null) return NotFound("Usuário não encontrado");

            var plano = await _planoRepository.ObterPorIdAsync(request.PlanoId);
            if (plano == null) return NotFound("Plano não encontrado");

            // Verifica se já tem assinatura ativa
            if (usuario.AssinaturaAtual != null && usuario.AssinaturaAtual.EstaAtiva())
                return BadRequest("Usuário já possui assinatura ativa.");

            var novaAssinatura = new Assinatura(
                request.UsuarioId,
                request.PlanoId,
                DateTime.UtcNow,
                plano.Duration.Days,
                plano);

            await _assinaturaRepository.AdicionarAsync(novaAssinatura);

            return Ok();
        }
    }

    public class CriarAssinaturaRequest
    {
        public Guid UsuarioId { get; set; }
        public Guid PlanoId { get; set; }
    }
}
