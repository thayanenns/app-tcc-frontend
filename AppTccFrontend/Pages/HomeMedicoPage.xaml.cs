using AppTccFrontend.Models;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;

namespace AppTccFrontend.Pages
{
    public partial class HomeMedicoPage : ContentPage
    {
        private ICollection<PacienteModel> _pacientes;
        private HttpClient _httpClient = new HttpClient();
        private readonly UsuarioModel _medico;

        public HomeMedicoPage(UsuarioModel medico)
        {
            InitializeComponent();
            _medico = medico;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                var medicoId = _medico.Id; // Use o Id do médico autenticado
                _pacientes = await ObterPacientesAsync(medicoId);
                PacientesListView.ItemsSource = _pacientes;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", "Ocorreu um erro ao buscar os pacientes: " + ex.Message, "OK");
            }
        }



        private async Task<ICollection<PacienteModel>> ObterPacientesAsync(Guid medicoId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"https://localhost:7125/api/paciente?medicoId={medicoId}");
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();
            ICollection<PacienteModel> pacientes = JsonConvert.DeserializeObject<ICollection<PacienteModel>>(json);

            return pacientes;
        }

        private void OnButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DetalhesPaciente());
        }
    }
}
