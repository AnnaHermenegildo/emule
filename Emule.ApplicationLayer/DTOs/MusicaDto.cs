using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emule.ApplicationLayer.DTOs
{
    public class MusicaDto
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public Guid BandaId { get; set; }
        public string BandaNome { get; set; }
        public string Nome { get; set; }
        public TimeSpan Duracao { get; set; }
    }
}
