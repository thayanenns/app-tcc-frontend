using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTccFrontend.Models
{
    internal class UsuarioModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; }

        public string Senha { get; set; }
    }
}
