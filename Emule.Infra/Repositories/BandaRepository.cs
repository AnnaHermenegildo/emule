using Emule.Domain.Model;
using Emule.Domain.Model.Repository;
using Emule.Infra;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emule.Infra.Repositories
{
    public class BandaRepository : IBandaRepository
    {
        private readonly EmuleDbContext _context;

        public BandaRepository(EmuleDbContext context)
        {
            _context = context;
        }

        public async Task<Banda> ObterPorIdAsync(Guid id)
        {
            return await _context.Bandas
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<List<Banda>> BuscarAsync(string? termo)
        {
            IQueryable<Banda> query = _context.Bandas;

            if (!string.IsNullOrWhiteSpace(termo))
            {
                termo = termo.ToLower();
                query = query.Where(b => b.Nome.ToLower().Contains(termo));
            }

            return await query.ToListAsync();
        }
    }
}
