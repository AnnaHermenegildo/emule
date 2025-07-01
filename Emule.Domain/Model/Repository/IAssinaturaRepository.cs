using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emule.Domain.Model.Repository
{
    public interface IAssinaturaRepository
    {
        Task<Assinatura?> ObterPorIdAsync(Guid id);
        Task<IList<Assinatura>> ObterPorUsuarioIdAsync(Guid usuarioId);
        Task AdicionarAsync(Assinatura assinatura);
        Task AtualizarAsync(Assinatura assinatura);
        Task RemoverAsync(Guid id);
    }
}
