using AppTccFrontend.Models;
using Microsoft.Maui.Graphics;
using Newtonsoft.Json;

namespace AppTccFrontend.Pages;

public partial class HomeMedicoPage : ContentPage
{
    private ICollection<PacienteModel> _pacientes;
    private HttpClient _httpClient = new HttpClient();
    public HomeMedicoPage()
	{
		InitializeComponent();
	}
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        try
        {
            _pacientes = await ObterPacientesAsync();
            PacientesListView.ItemsSource = _pacientes;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", "Ocorreu um erro ao buscar os pacientes: " + ex.Message, "OK");
        }
    }

    private async Task<ICollection<PacienteModel>> ObterPacientesAsync()
    {
        HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:7125/api/paciente");
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