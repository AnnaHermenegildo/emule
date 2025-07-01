using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emule.Domain.Model
{
    public class Musica : Entidade
    {
        public Musica(string titulo, TimeSpan duracao, Guid bandaId)
        {
            Titulo = titulo;
            Duracao = duracao;
            BandaId = bandaId;
        }

        public string Titulo { get; set; }
        public TimeSpan Duracao { get; set; }

        // Relacionamento com Banda
        public Guid BandaId { get; set; }
        public Banda Banda { get; set; }
    }
}
