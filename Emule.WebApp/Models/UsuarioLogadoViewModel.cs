namespace Emule.WebApp.Models
{
    public class UsuarioLogadoViewModel
    {
        public string NomeUsuario { get; set; }
        public List<MusicaViewModel> MusicasFavoritas { get; set; } = new();
        public string TermoPesquisa { get; set; }
    }

    public class MusicasViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public bool Favoritada { get; set; }
    }
}
