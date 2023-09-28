using AppTccFrontend.Enums;
using AppTccFrontend.Models;
using AppTccFrontend.Models.Dtos;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;

namespace AppTccFrontend.Pages;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
        lblCadastro.GestureRecognizers.Add(new TapGestureRecognizer
        {
            Command = new Command(() =>
            {
                Navigation.PushAsync(new EscolhaPerfilPage());
            })
        });
    }
    private readonly string urlBase = "https://localhost:7125/api/login";

    private HttpClient _httpClient = new HttpClient();

    private void btnRegistrar_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new EscolhaPerfilPage());
    }
    private async void btnEntrar_Clicked(object sender, EventArgs e)
    {
        try
        {

            var dadosLogin = new
            {
                email = entEmail.Text,
                senha = entSenha.Text
            };

            var response = await _httpClient.PostAsJsonAsync(urlBase, dadosLogin);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var jsonData = JsonConvert.DeserializeObject<JObject>(responseBody);

                if (jsonData.ContainsKey("usuario") && jsonData.ContainsKey("tipoUsuario"))
                {
                    var usuario = jsonData["usuario"].ToObject<UsuarioModel>();
                    var tipoUsuario = jsonData["tipoUsuario"].ToObject<TipoUsuario>();

                    if (tipoUsuario == TipoUsuario.Medico)
                    {
                        var medicoId = await ObterMedicoIdAsync(usuario.Id);
                        await Navigation.PushAsync(new HomeMedicoPage(usuario));
                    }
                    else if (tipoUsuario == TipoUsuario.Paciente)
                    {
                        await Navigation.PushAsync(new HomePacientePage(usuario));
                    }
                    else
                    {
                        await DisplayAlert("Erro", "Credenciais inválidas. Verifique o email e senha.", "OK");
                    }
                }
                else
                {
                    await DisplayAlert("Erro", "Resposta inválida do servidor.", "OK");
                }
            }
            else
            {
                await DisplayAlert("Erro", "Erro ao fazer login. Verifique as credenciais e tente novamente.", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.Message, "OK");
        }
    }

  
    private async Task<Guid?> ObterMedicoIdAsync(Guid usuarioId)
    {
        HttpResponseMessage response = await _httpClient.GetAsync($"https://localhost:7125/api/Usuario/{usuarioId}");
        response.EnsureSuccessStatusCode();

        string json = await response.Content.ReadAsStringAsync();
        var usuario = JsonConvert.DeserializeObject<UsuarioModel>(json);

        if (usuario is MedicoModel medico)
        {
            var pacientesDoMedico = medico.Pacientes;

            var primeiroPaciente = pacientesDoMedico?.FirstOrDefault();
            if (primeiroPaciente != null)
            {
                return primeiroPaciente.MedicoId;
            }
        }

        return null;
    }

}


