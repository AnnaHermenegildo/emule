using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emule.ApplicationLayer.DTOs
{
    public class FavoritarBandaDto
    {
        public Guid UsuarioId { get; set; }
        public Guid BandaId { get; set; }
    }
}
