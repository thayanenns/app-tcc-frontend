using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTccFrontend.Models
{
    public class MedicoModel : UsuarioModel
    {
        public string Crm { get; set; }
        [InverseProperty("Medico")]

        public ICollection<PacienteModel>? Pacientes { get; set; }
    }
}
