namespace Emule.WebApp.Models
{
    public class MusicaViewModel
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public Guid BandaId { get; set; }
        public string BandaNome { get; set; }
        public TimeSpan Duracao { get; set; }

        public bool Favoritada { get; set; }
        public string Nome { get; set; }
    }
}
