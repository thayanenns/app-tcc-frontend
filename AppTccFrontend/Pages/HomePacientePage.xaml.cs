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
                var pacienteId = _paciente.Id;
                _medicoesPorDia = await ObterMedicoesPorDiaAsync(pacienteId);
                DiasListView.ItemsSource = _medicoesPorDia;
                NomeLabel.Text = $"Nome: {_paciente.Nome}";
                DataNascimentoLabel.Text = $"Data de Nascimento: {_paciente.DataNascimento.ToString("dd/MM/yyyy")}";
                SexoLabel.Text = $"Sexo: {_paciente.Sexo}";
                TelefoneLabel.Text = $"Telefone: {_paciente.Telefone}";


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
      
                await Navigation.PushAsync(new DetalhesMedicaoPage(selectedDate));
            }
        }

        private async void OnInserirMedicaoClicked(object sender, EventArgs e)
        {
            PacienteModel paciente = new PacienteModel
            {
                Id = _paciente.Id

            };
            await Navigation.PushAsync(new CadastroMedicaoPage(paciente));
           
        }



        private async Task<PacienteModel> ObterPacientePorIdAsync(Guid pacienteId)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"https://localhost:7125/api/Paciente/{pacienteId}");
                response.EnsureSuccessStatusCode();

                string json = await response.Content.ReadAsStringAsync();
                var paciente = JsonConvert.DeserializeObject<PacienteModel>(json);

                return paciente;
            }
            catch (Exception ex)
            {
                // Trate qualquer exceção que possa ocorrer ao fazer a chamada à API
                Console.WriteLine($"Erro ao buscar informações do paciente: {ex.Message}");
                return null;
            }
        }
    }
}
