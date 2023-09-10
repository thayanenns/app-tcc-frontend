using AppTccFrontend.Enums;
using AppTccFrontend.Models;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace AppTccFrontend.Pages;

public partial class CadastroPacientePage : ContentPage
{
    public CadastroPacientePage(TipoUsuario tipoUsuario)
    {
        InitializeComponent();
        _tipoUsuario = tipoUsuario;
    }

    private TipoUsuario _tipoUsuario;



    private readonly string urlBase = "https://localhost:7125/api/paciente";
    private async void SubmitClicked(object sender, EventArgs e)
    {
        try
        {
            var httpClient = new HttpClient();

            var novoUsuario = new 
            {

                Nome = entNome.Text,
                DataNascimento = entDtNascimento.Date.ToUniversalTime(),
                //Tipo = (TipoUsuario)pickerTipo.SelectedItem,
                Tipo = _tipoUsuario,
                Sexo = entSexo.Text,
                Telefone = entTelefone.Text,
                Email = entEmail.Text,
                Senha = entSenha.Text,
                MedicoId = entMedicoId.Text

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

/*
    private async void SubmitClicked(object sender, EventArgs e)
    {
        try
        {
            var novoUsuario = new
            {

                Nome = entNome.Text,
                DataNascimento = entDtNascimento.Date,
                Tipo = (TipoUsuario)pickerTipo.SelectedItem,
                Sexo = entSexo.Text,
                Telefone = entTelefone.Text,
                Email = entEmail.Text,
                Senha = entSenha.Text,
                // acctualFileUrl = imageUpload.Source.ToString()

            };

            var data = JsonConvert.SerializeObject(novoUsuario);
            var content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;

            response = await client.PostAsync(urlBase, content);
            if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Erro ao incluir produto");
        }
    }
            catch(Exception ex)
            {
            await DisplayAlert("Erro", ex.Message, "OK");
        }

    }
}
*/
