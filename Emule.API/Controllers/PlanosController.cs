using Emule.Domain.Model;
using Emule.Domain.Model.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Emule.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanosController : ControllerBase
    {
        private readonly IPlanoRepository _planoRepository;

        public PlanosController(IPlanoRepository planoRepository)
        {
            _planoRepository = planoRepository;
        }

        // GET api/planos
        [HttpGet]
        public async Task<ActionResult<List<Plano>>> Listar()
        {
            var planos = await _planoRepository.ObterTodosAsync();
            return Ok(planos);
        }

        // GET api/planos/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Plano>> ObterPorId(System.Guid id)
        {
            var plano = await _planoRepository.ObterPorIdAsync(id);
            if (plano == null)
                return NotFound();

            return Ok(plano);
        }
    }
}
