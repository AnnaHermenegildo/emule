using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emule.Domain.Model.Repository
{
    public interface IMusicaRepository
    {
        Task<List<Musica>> BuscarComBandaAsync(string? termo);
        Task<Musica> ObterPorIdAsync(Guid id);

        Task<List<Musica>> ObterMusicasFavoritadasPorId(Guid id);
    }
}
