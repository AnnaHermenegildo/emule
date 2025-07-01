using Emule.ApplicationLayer.DTOs;
using Emule.ApplicationLayer.Services;
using Microsoft.AspNetCore.Mvc;

namespace Emule.API.Controllers
{
    [ApiController]
    [Route("api/musica")]
    public class MusicaController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly MusicaService _musicaService;
        

        public MusicaController(IUsuarioService usuarioService, MusicaService musicaService)
        {
            _usuarioService = usuarioService;
            _musicaService = musicaService;
        }

        [HttpPost("favoritar")]
        public async Task<IActionResult> Favoritar([FromBody] FavoritarMusicaDto dto)
        {
            await _usuarioService.FavoritarMusicaAsync(dto.UsuarioId, dto.MusicaId);
            return Ok();
        }

        [HttpGet("favoritas")]
        public async Task<IActionResult> ObterFavoritasPorUsuario([FromQuery] Guid id)
        {
            var listaMusicas =  await _musicaService.ObterFavoritasPorUsuarioAsync(id);
            return Ok(listaMusicas);
        }
    }

    [ApiController]
    [Route("api/banda")]
    public class BandaController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public BandaController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost("favoritar")]
        public async Task<IActionResult> Favoritar([FromBody] FavoritarBandaDto dto)
        {
            await _usuarioService.FavoritarBandaAsync(dto.UsuarioId, dto.BandaId);
            return Ok();
        }
    }
}
