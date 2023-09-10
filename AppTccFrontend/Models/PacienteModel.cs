using System.ComponentModel.DataAnnotations.Schema;


namespace AppTccFrontend.Models
{
    public class PacienteModel : UsuarioModel
    {
        public MedicoModel? Medico { get; set; }
        [ForeignKey("Medico")]

        public Guid? MedicoId { get; set; }

        public ICollection<MedicaoModel>? Medicoes { get; set; }

        public PacienteModel() { }
    }
}
