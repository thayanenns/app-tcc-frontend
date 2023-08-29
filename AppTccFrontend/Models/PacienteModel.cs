using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.Maui.ApplicationModel.Permissions;

namespace AppTccFrontend.Models
{
    public class PacienteModel : UsuarioModel
    {
        public MedicoModel? Medico { get; set; }
        [ForeignKey("Medico")]

        public Guid? MedicoId { get; set; }

        public ICollection<MedicaoModel>? Medicoes { get; set; }
    }
}
