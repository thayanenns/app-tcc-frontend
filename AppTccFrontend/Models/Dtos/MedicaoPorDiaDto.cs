using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTccFrontend.Models.Dtos
{
    public class MedicaoPorDiaDto
    {
        public DateTime DataDia { get; set; }
        public List<MedicaoModel> Medicoes { get; set; }
    }
}
