using AppTccFrontend.Enums;
using System.ComponentModel.DataAnnotations.Schema;


namespace AppTccFrontend.Models
{
    public class PacienteModel : UsuarioModel
    {
        public MedicoModel? Medico { get; set; }
        [ForeignKey("Medico")]

        public Guid? MedicoId { get; set; }

        public ICollection<MedicaoModel>? Medicoes { get; set; }

        public TipoDiabetes TipoDiabetes { get; set; }


        public PacienteModel() { }

       
        public int Idade
        {
            get
            {
                DateTime hoje = DateTime.Today;
                int idade = hoje.Year - DataNascimento.Year;
                if (DataNascimento.Date > hoje.AddYears(-idade))
                    idade--;
                return idade;
            }
        }

    }
}
