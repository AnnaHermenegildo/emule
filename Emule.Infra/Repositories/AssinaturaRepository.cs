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
    public class AssinaturaRepository : IAssinaturaRepository
    {
        private readonly EmuleDbContext _context;

        public AssinaturaRepository(EmuleDbContext context)
        {
            _context = context;
        }

        public async Task<Assinatura?> ObterPorIdAsync(Guid id)
        {
            return await _context.Assinaturas
                .Include(a => a.Plano)
                .Include(a => a.IdUsuario)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IList<Assinatura>> ObterPorUsuarioIdAsync(Guid usuarioId)
        {
            return await _context.Assinaturas
                .Include(a => a.Plano)
                .Where(a => a.IdUsuario == usuarioId)
                .ToListAsync();
        }

        public async Task AdicionarAsync(Assinatura assinatura)
        {
            await _context.Assinaturas.AddAsync(assinatura);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Assinatura assinatura)
        {
            _context.Assinaturas.Update(assinatura);
            await _context.SaveChangesAsync();
        }

        public async Task RemoverAsync(Guid id)
        {
            var assinatura = await ObterPorIdAsync(id);
            if (assinatura != null)
            {
                _context.Assinaturas.Remove(assinatura);
                await _context.SaveChangesAsync();
            }
        }
    }
}
