using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emule.Domain.Model.Repository
{
    public interface IBandaRepository
    {
        Task<Banda> ObterPorIdAsync(Guid id);
        Task<List<Banda>> BuscarAsync(string? termo);
    }
}
