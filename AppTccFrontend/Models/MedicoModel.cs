using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTccFrontend.Models
{
    internal class MedicoModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Crm { get; set; }


        public string Sexo { get; set; }

        public string Telefone { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }

        //public string Tipo { get; set; }

        public string Tipo => "Medico";
        public ICollection<PacienteModel> Pacientes { get; set; }
    }
}
