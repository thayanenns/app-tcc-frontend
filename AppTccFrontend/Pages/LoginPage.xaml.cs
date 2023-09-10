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


    /* private async Task<ICollection<PacienteModel>> ObterPacientesAsync(Guid medicoId)
     {
         HttpResponseMessage response = await _httpClient.GetAsync($"https://localhost:7125/api/paciente?medicoId={medicoId}");
         // ...
     }*/

    private async Task<Guid?> ObterMedicoIdAsync(Guid usuarioId)
    {
        HttpResponseMessage response = await _httpClient.GetAsync($"https://localhost:7125/api/Usuario/{usuarioId}");
        response.EnsureSuccessStatusCode();

        string json = await response.Content.ReadAsStringAsync();
        var usuario = JsonConvert.DeserializeObject<UsuarioModel>(json);

        if (usuario is MedicoModel medico)
        {
            // Acessar a coleção de pacientes associados ao médico
            var pacientesDoMedico = medico.Pacientes;

            // Assumindo que cada paciente tenha uma propriedade MedicoId
            // Você precisará ajustar essa lógica de acordo com a estrutura real do seu modelo
            var primeiroPaciente = pacientesDoMedico?.FirstOrDefault();
            if (primeiroPaciente != null)
            {
                return primeiroPaciente.MedicoId;
            }
        }

        return null;
    }

}




        /*
        private async Task<Guid?> ObterMedicoIdAsync(Guid usuarioId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"https://localhost:7125/api/Usuario/{usuarioId}");
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();
            var usuario = JsonConvert.DeserializeObject<UsuarioModel>(json);

            // Verifique se o usuário é do tipo médico e retorne o ID do médico associado
            if (usuario.Tipo == TipoUsuario.Medico)
            {
                return (usuario as MedicoModel)?.MedicoId;
            }

            return null;
        }
        */




/*private async void btnEntrar_Clicked(object sender, EventArgs e)
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
*/

