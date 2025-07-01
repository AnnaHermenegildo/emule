using System.Numerics;

namespace Emule.Domain.Model
{
    public class Assinatura : Entidade
    {
        public Assinatura()
        {
                
        }

        public Assinatura(Guid idUsuario, Guid idPlano, DateTime inicio, int duracao, Plano? planos)
        {
            IdUsuario = idUsuario;
            IdPlano = idPlano;
            Inicio = inicio;
            Duracao = duracao;
            Plano = planos;
        }

        public Guid IdUsuario { get; set; }

        public Guid IdPlano { get; set; }

        public DateTime Inicio { get; set; }
        public int Duracao { get; set; }

        //public Usuario? Usuario { get; set; }
        public Plano? Plano { get; set; }

        //public DateTime Fim => Inicio.Add(Duracao);

        public bool EstaAtiva()
        {
            //   return DateTime.UtcNow <= Fim;
            return true;
        }

        public void Cancelar()
        {
            Duracao = 0;
        }
    }
}
