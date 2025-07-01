using Emule.Domain.Model;
using Emule.Domain.Model.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emule.Infra.Repositories
{
    public class MusicaRepository : IMusicaRepository
    {
        private readonly EmuleDbContext _context;

        public MusicaRepository(EmuleDbContext context)
        {
            _context = context;
        }

        public async Task<List<Musica>> BuscarComBandaAsync(string? termo)
        {
            var query = _context.Musicas.Include(m => m.Banda).AsQueryable();

            if (!string.IsNullOrWhiteSpace(termo))
            {
                query = query.Where(m =>
                    m.Titulo.Contains(termo) ||
                    m.Banda.Nome.Contains(termo));
            }

            return await query.ToListAsync();
        }
        public async Task<Musica> ObterPorIdAsync(Guid id)
        {
            return await _context.Musicas
                .Include(m => m.Banda)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<List<Musica>> ObterMusicasFavoritadasPorId(Guid id)
        {
            var usuario = await _context.Usuarios
                .Include(x => x.Favoritos)
                .Include(x => x.BandasFavoritas)
                .Where(x => x.Id == id).FirstOrDefaultAsync();
            return usuario.Favoritos;
        }
    }
}
