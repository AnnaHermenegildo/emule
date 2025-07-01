using Emule.Domain.Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emule.Domain.Model
{
    public class Banda : Entidade
    {
        public Banda(string nome, string genero)
        {
            Nome = nome;
            Genero = genero;
            DataCriada = DateTime.UtcNow;
        }

        public string Nome { get; set; }
        public string Genero { get; set; }
        public DateTime DataCriada { get; set; }

        public List<Musica> Musicas { get; set; } = new();
    }
}
