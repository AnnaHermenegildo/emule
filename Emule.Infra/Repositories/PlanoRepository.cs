using Emule.Domain.Model.Repository;
using Emule.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emule.Infra.Repositories
{
    public class PlanoRepository : IPlanoRepository
    {
        private readonly EmuleDbContext _context;

        public PlanoRepository(EmuleDbContext context)
        {
            _context = context;
        }

        public async Task<Plano?> ObterPorIdAsync(Guid id)
        {
            return await _context.Planos
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IList<Plano>> ObterTodosAsync()
        {
            return await _context.Planos
                .ToListAsync();
        }

        public async Task AdicionarAsync(Plano plano)
        {
            await _context.Planos.AddAsync(plano);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Plano plano)
        {
            _context.Planos.Update(plano);
            await _context.SaveChangesAsync();
        }

        public async Task RemoverAsync(Guid id)
        {
            var plano = await ObterPorIdAsync(id);
            if (plano != null)
            {
                _context.Planos.Remove(plano);
                await _context.SaveChangesAsync();
            }
        }
    }
}
