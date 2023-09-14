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
        public UsuarioModel Paciente { get; private set; }


        public HomeMedicoPage(UsuarioModel medico)
        {
            InitializeComponent();
            _medico = medico;
        }

        public HomeMedicoPage(UsuarioModel medico, UsuarioModel paciente)
        {
            InitializeComponent();
            _medico = medico;
            Paciente = paciente;

        }


        protected override async void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                var medicoId = _medico.Id; // Use o Id do m�dico autenticado
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
            if (sender is Button button && button.CommandParameter is PacienteModel paciente)
            {
                // V� para a p�gina HomePacientePage com o paciente selecionado
                Navigation.PushAsync(new HomePacientePage(paciente));
            }
            // Crie a p�gina HomeMedicoPage e passe o m�dico e o paciente como par�metros
            
        }

            private void sbBusca_SearchButtonPressed(object sender, EventArgs e)
        {

        }
    }
}
