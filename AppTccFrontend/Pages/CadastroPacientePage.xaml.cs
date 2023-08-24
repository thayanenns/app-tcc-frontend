using System.Net.Http.Json;

namespace AppTccFrontend.Pages;

public partial class CadastroPacientePage : ContentPage
{
	public CadastroPacientePage()
	{
		InitializeComponent();
	}

    private readonly string urlBase = "https://localhost:7125/api/Paciente";

    private async void SubmitClicked(object sender, EventArgs e)
    {
        try
        {
            var httpClient = new HttpClient();

            var data = new
            {

                nome = entNome.Text,
                dataNascimento = entDtNascimento.Date.ToString("yyyy-MM-dd"),
                sexo = pickerGenero.SelectedItem.ToString(),
                telefone = entTelefone.Text,
                email = entEmail.Text,
                senha = entSenha.Text,
                medicoId = entMedicoId.Text
                // acctualFileUrl = imageUpload.Source.ToString()

            };
            // Console.Write(data);
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