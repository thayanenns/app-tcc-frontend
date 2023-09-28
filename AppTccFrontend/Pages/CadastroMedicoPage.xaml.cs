using AppTccFrontend.Enums;
using AppTccFrontend.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.Intrinsics.Arm;

namespace AppTccFrontend.Pages;

public partial class CadastroMedicoPage : ContentPage
{
    public CadastroMedicoPage(TipoUsuario tipoUsuario)
    {
        InitializeComponent();
        _tipoUsuario = tipoUsuario;


    }
    HttpClient client = new HttpClient();
    private TipoUsuario _tipoUsuario;

    private readonly string urlBase = "https://localhost:7125/api/medico";
    private async void SubmitClicked(object sender, EventArgs e)
    {
        try
        {
            var httpClient = new HttpClient();

            var novoUsuario = new
            {

                Nome = entNome.Text,
                DataNascimento = entDtNascimento.Date.ToUniversalTime(),
                Sexo = entSexo.Text,
                Telefone = entTelefone.Text,
                Tipo = _tipoUsuario,
                Email = entEmail.Text,
                Senha = entSenha.Text,
                Crm = entCrm.Text,
                Domicilio = entDomicilio.Text


                // acctualFileUrl = imageUpload.Source.ToString()

            };
            var response = await httpClient.PostAsJsonAsync(urlBase, novoUsuario);

            response.EnsureSuccessStatusCode();

            await DisplayAlert("Sucesso", "Cadastro realizado!", "OK");
            entNome.Text = "";
            await Navigation.PushAsync(new LoginPage());
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.Message, "OK");
        }

    }
}