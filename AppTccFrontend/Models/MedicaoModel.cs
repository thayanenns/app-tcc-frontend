using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTccFrontend.Models
{
    public class MedicaoModel
    {
        public Guid Id { get; set; }
        public DateTime DataMedicao { get; set; }
        public int Batimentos { get; set; }

        public int Glicemia { get; set; }
        public int PressaoSistolica { get; set; }
        public int PressaoDiastolica { get; set; }
        public bool EmJejum { get; set; }
        public PacienteModel? Paciente { get; set; }
        public Guid PacienteId { get; set; }

       
    }
}
