using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace AppTccFrontend.Pages;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}
    private readonly string urlBase = "https://localhost:7125/login";

    private void btnRegistrar_Clicked(object sender, EventArgs e)
    {
		Navigation.PushAsync(new EscolhaPerfilPage());
    }

    private async void btnEntrar_Clicked(object sender, EventArgs e)
    {
        string email = entEmail.Text;
        string senha = entSenha.Text;

        var credentials = new { Email = email, Senha = senha };
        var content = new StringContent(JsonConvert.SerializeObject(credentials), Encoding.UTF8, "application/json");

        using (HttpClient client = new HttpClient())
        {
            try
            {
                HttpResponseMessage response = await client.PostAsync(urlBase, content);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    // Aqui você pode analisar o responseBody, se necessário

                    await DisplayAlert("Sucesso", "Login bem-sucedido!", "OK");
                    Navigation.PushAsync(new HomeMedicoPage());


                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    await DisplayAlert("Erro", "Credenciais inválidas. Verifique seu email e senha.", "OK");
                }
                else
                {
                    await DisplayAlert("Erro", $"Erro na solicitação. Código de status: {response.StatusCode}", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", "Ocorreu um erro: " + ex.Message, "OK");
            }
        }
    }

}
