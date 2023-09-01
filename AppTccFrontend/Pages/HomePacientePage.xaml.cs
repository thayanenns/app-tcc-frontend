using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Maui.Controls;
using System.Net.Http;
using System.Threading.Tasks;
using AppTccFrontend.Models;
using AppTccFrontend.Models.Dtos;
using Newtonsoft.Json;

namespace AppTccFrontend.Pages
{
    public partial class HomePacientePage : ContentPage
    {
        private HttpClient _httpClient = new HttpClient();
        private List<MedicaoPorDiaDto> _medicoesPorDia;
        private readonly UsuarioModel _paciente;

        public HomePacientePage(UsuarioModel paciente)
        {
            InitializeComponent();
            _medicoesPorDia = new List<MedicaoPorDiaDto>();
            _paciente = paciente;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                var pacienteId = _paciente.Id; // Substitua pelo ID do paciente logado
                _medicoesPorDia = await ObterMedicoesPorDiaAsync(pacienteId);
                DiasListView.ItemsSource = _medicoesPorDia;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", "Ocorreu um erro ao buscar as medições: " + ex.Message, "OK");
            }
        }

        private async Task<List<MedicaoPorDiaDto>> ObterMedicoesPorDiaAsync(Guid pacienteId)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7125/api/Medicao/dia/{pacienteId}");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            List<MedicaoPorDiaDto> medicoes = JsonConvert.DeserializeObject<List<MedicaoPorDiaDto>>(json);
            return medicoes;
        }

        private async void OnDateSelected(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is MedicaoPorDiaDto selectedDate)
            {
                // Crie a página de detalhes das medições (ajuste o nome da página conforme necessário)
                // Use a navegação para direcionar para a página de detalhes
                await Navigation.PushAsync(new DetalhesMedicaoPage(selectedDate));
            }
        }
    }
}
