using AppTccFrontend.Models.Dtos;
using Microsoft.Maui.Controls;

namespace AppTccFrontend.Pages
{
    public partial class DetalhesMedicaoPage : ContentPage
    {
        public DetalhesMedicaoPage(MedicaoPorDiaDto selectedDate)
        {
            InitializeComponent();
            BindingContext = selectedDate;
            Title = $"Detalhes das Medi��es de {selectedDate.DataDia:dd/MM/yyyy}";
        }
    }
}
