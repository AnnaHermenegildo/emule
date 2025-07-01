using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Emule.WebApp.Models
{
    public class NovoUsuarioViewModel
    {
        [Required(ErrorMessage = "Informe o nome de usuário")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Informe a senha")]
        public string Senha { get; set; }

        public Guid PlanoSelecionadoId { get; set; }

        public List<SelectListItem> Planos { get; set; } = new();

        // Campos do cartão
        public string? NumeroCartao { get; set; }
        public string? NomeCartao { get; set; }
        public string? ValidadeCartao { get; set; }
        public string? CVV { get; set; }
    }
}