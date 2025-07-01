using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emule.Domain.Model.Repository
{
    public interface IPlanoRepository
    {
        Task<Plano?> ObterPorIdAsync(Guid id);
        Task<IList<Plano>> ObterTodosAsync();
        Task AdicionarAsync(Plano plano);
        Task AtualizarAsync(Plano plano);
        Task RemoverAsync(Guid id);
    }
}
