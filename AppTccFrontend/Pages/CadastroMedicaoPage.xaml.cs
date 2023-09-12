using AppTccFrontend.Models;
using System.Net.Http;
using System.Net.Http.Json;

namespace AppTccFrontend.Pages;

public partial class CadastroMedicaoPage : ContentPage
{
    private readonly string urlBase = "https://localhost:7125/api/medicao";
    private PacienteModel _paciente;
    private bool rbJejum = false;
    public CadastroMedicaoPage(PacienteModel paciente)
    {
        InitializeComponent();
        _paciente = paciente;

    }

    private async void OnSalvarClicked(object sender, EventArgs e)
    {
        try
        {
            RadioButton_CheckedChanged(sender, null);
            DateTime dataAtual = DateTime.Now.Date;

            var httpClient = new HttpClient();

            var novaMedicao = new
            {
                DataMedicao = dataAtual.ToUniversalTime(),
                Batimentos = entBatimentos.Text,
                Glicemia = int.Parse(entGlicemia.Text),
                PressaoSistolica = int.Parse(entPressaoSistolica.Text),
                PressaoDiastolica = int.Parse(entPressaoDiastolica.Text),
                EmJejum =rbJejum,
                PacienteId = _paciente.Id
            };

            var response = await httpClient.PostAsJsonAsync(urlBase, novaMedicao);

            response.EnsureSuccessStatusCode();

            await DisplayAlert("Sucesso", "Cadastro realizado!", "OK");

            entBatimentos.Text = "";
            entGlicemia.Text = "";
            entPressaoSistolica.Text = "";
            entPressaoDiastolica.Text = "";
//EmJejumSwitch.IsToggled = false;

            await Navigation.PushAsync(new HomePacientePage(_paciente));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.Message, "OK");
        }
    }

    private void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {

        if (rbSim.IsChecked)
        {
            rbJejum = true;   
        }
        else if (rbNao.IsChecked)
        {
            rbJejum = false;
        }

    }
}

