using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emule.ApplicationLayer.DTOs
{
    public class FavoritarMusicaDto
    {
        public Guid UsuarioId { get; set; }
        public Guid MusicaId { get; set; }
    }

}
