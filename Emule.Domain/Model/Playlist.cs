using Emule.Domain.Model;
using Emule.Domain.Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emule.Domain
{
    public class Playlist : Entidade
    {
        public string Nome { get; set; }
        public List<Musica> Musicas { get; set; } = new();

        public Usuario Usuario { get; set; }

        public Guid UsuarioId { get; set; }
    }
}
