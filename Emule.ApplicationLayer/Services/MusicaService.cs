using Emule.ApplicationLayer.DTOs;
using Emule.Domain.Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emule.ApplicationLayer.Services
{
    public class MusicaService
    {
        private readonly IMusicaRepository _musicaRepo;

        public MusicaService(IMusicaRepository musicaRepo)
        {
            _musicaRepo = musicaRepo;
        }

        public async Task<List<MusicaDto>> BuscarAsync(string? termo)
        {
            var musicas = await _musicaRepo.BuscarComBandaAsync(termo);
            return musicas.Select(m => new MusicaDto
            {
                Id = m.Id,
                Titulo = m.Titulo,
                BandaId = m.Banda.Id,
                BandaNome = m.Banda.Nome,
                Duracao = m.Duracao
            }).ToList();
        }


        public async Task<List<MusicaDto>> ObterFavoritasPorUsuarioAsync(Guid id)
        {
            var musicas = await _musicaRepo.ObterMusicasFavoritadasPorId(id);
            return musicas.Select(m => new MusicaDto
            {
                Id = m.Id,
                Titulo = m.Titulo,
                BandaId = default,
                BandaNome = "",
                Duracao = m.Duracao
            }).ToList();
        }

    }
}
