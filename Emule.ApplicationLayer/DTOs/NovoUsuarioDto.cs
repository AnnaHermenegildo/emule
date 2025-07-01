using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emule.ApplicationLayer.DTOs
{
    public class NovoUsuarioDto
    {
        public string Username { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;

        public Guid PlanoId { get; set; }
    }
}
