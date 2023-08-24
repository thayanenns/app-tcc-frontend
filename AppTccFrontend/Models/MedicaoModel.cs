using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTccFrontend.Models
{
    internal class MedicaoModel
    {
        public int Id { get; set; }
        public DateTime DataMedicao { get; set; }
        public int Glicemia { get; set; }
        public int PressaoSistolica { get; set; }
        public int PressaoDiastolica { get; set; }
        public Boolean EmJejum { get; set; }
        public PacienteModel Paciente { get; set; }
        public int PacienteId { get; set; }
    }
}
