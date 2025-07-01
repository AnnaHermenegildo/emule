using Emule.ApplicationLayer.Services;
using Emule.ApplicationLayer.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Emule.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet("ObterPorId")]
        public IActionResult Get([FromQuery] Guid id)
        {
            var usuario = _usuarioService.ObterPorId(id);
            return Ok(new {code = 200, data =  usuario});
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var autenticado = await _usuarioService.LoginAsync(dto.Username, dto.Senha);
            if (autenticado == null)
                return Unauthorized();

            return Ok(autenticado);
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] NovoUsuarioDto dto)
        {
            try
            {
                await _usuarioService.RegistrarAsync(dto);
                return CreatedAtAction("Registrar", dto);
            }
            catch (Exception e)
            {
                return UnprocessableEntity(new {code = 422, message = $"Erro ao adicionar novo usuário: {e.Message}"});
            }
        }
    }
}
