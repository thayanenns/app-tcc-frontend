using AppTccFrontend.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.Intrinsics.Arm;

namespace AppTccFrontend.Pages;

public partial class CadastroMedicoPage : ContentPage
{
    public CadastroMedicoPage()
    {
        InitializeComponent();
    }
    private readonly string urlBase = "https://localhost:7125/api/Medico";

    private async void SubmitClicked(object sender, EventArgs e)
    {
        try
        {
            var httpClient = new HttpClient();

            var data = new
            {
                
                nome = entNome.Text,
                dataNascimento = entDtNascimento.Date.ToUniversalTime(),
                crm = entCrm.Text,
                sexo = pickerGenero.SelectedItem.ToString(),
                telefone = entTelefone.Text,
                email = entEmail.Text,
                senha = entSenha.Text
            };

            // Serializa o objeto para JSON
            var response = await httpClient.PostAsJsonAsync(urlBase, data);

            response.EnsureSuccessStatusCode();

            await DisplayAlert("Sucesso", "Post enviado com sucesso!", "OK");
            entNome.Text = "";
            await Navigation.PushAsync(new LoginPage());
        }
         

        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.Message, "OK");
        }
    }
}
    