using Emule.Domain.Model.Repository;

namespace Emule.Domain.Model
{
    public class Usuario : Entidade
    {
        public Usuario(string username, string senha)
        {
            Username = username;
            Senha = senha;
            DataCriada = DateTime.Now;
        }

        public string Username { get; set; }

        public string Senha { get; set; }

        public DateTime DataCriada { get; set; }

        public List<Playlist> Playlists { get; set; } = new();
        public List<Musica> Favoritos { get; set; } = new();
        public List<Banda> BandasFavoritas { get; set; } = new();

        public List<Assinatura> Assinaturas { get; set; } = new();
        public Assinatura AssinaturaAtual { get; set; }

    }
}
