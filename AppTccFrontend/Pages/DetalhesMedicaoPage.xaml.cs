using AppTccFrontend.Models;
using AppTccFrontend.Models.Dtos;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;

namespace AppTccFrontend.Pages
{
    public partial class DetalhesMedicaoPage : ContentPage
    {
        public DetalhesMedicaoPage(MedicaoPorDiaDto selectedDate)
        {
            InitializeComponent();
            BindingContext = selectedDate;
            Title = $"Detalhes das Medições de {selectedDate.DataDia:dd/MM/yyyy}";
        }
    }
}
 