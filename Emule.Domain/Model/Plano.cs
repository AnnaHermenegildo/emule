namespace Emule.Domain.Model
{
    public class Plano : Entidade
    {
        public Plano(string nome, string descricao, decimal valor, int duracaoDias)
        {
            Nome = nome;
            Descricao = descricao;
            Valor = valor;
            DuracaoDias = duracaoDias;
        }

        public string Nome { get; private set; } = null!;

        public string Descricao { get; private set; } = null!;

        public decimal Valor { get; private set; }

        public bool TemAnuncios { get; set; }

        public int DuracaoDias { get; private set; }

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public TimeSpan Duration => TimeSpan.FromDays(DuracaoDias);
    }
}
