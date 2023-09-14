using AppTccFrontend.Enums;
using AppTccFrontend.Models;
using Newtonsoft.Json;
using System.Net.Http.Json;
using AppTccFrontend.NovaPasta1;


namespace AppTccFrontend.Pages;

public partial class CadastroPacientePage : ContentPage
{
    private List<MedicoModel> medicos = new List<MedicoModel>();

    public CadastroPacientePage(TipoUsuario tipoUsuario)
    {
        InitializeComponent();
        _tipoUsuario = tipoUsuario;
        
        CarregarMedicosAsync();



    }

    private TipoUsuario _tipoUsuario;



    private readonly string urlBase = "https://localhost:7125/api/paciente";

    private HttpClient _httpClient = new HttpClient();
    private readonly UsuarioModel _usuario;
    private List<MedicoModel> _medicos;



    private async void SubmitClicked(object sender, EventArgs e)
    {
        try
        {
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
                MedicoId = ((MedicoModel)pickerMedico.SelectedItem)?.Id

                // acctualFileUrl = imageUpload.Source.ToString()

            };
            var response = await _httpClient.PostAsJsonAsync(urlBase, novoUsuario);

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
    private async Task CarregarMedicosAsync()
    {
        try
        {
            HttpClient httpClient = new HttpClient();
            string url = "https://localhost:7125/api/medico";
            string respostaJson = await httpClient.GetStringAsync(url);

            medicos = JsonConvert.DeserializeObject<List<MedicoModel>>(respostaJson);

            // Associe a lista de médicos ao Picker
            pickerMedico.ItemsSource = medicos;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", "Ocorreu um erro ao carregar os médicos: " + ex.Message, "OK");
        }
    }


    /*
    private async Task<List<MedicoModel>> ObterMedicosAsync()
    {


        private async Task<List<MedicoModel>> ObterMedicosAsync()
    {
        var response = await _httpClient.GetAsync($"https://localhost:7125/api/Medico");
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();

        List<MedicoModel> medicos = JsonConvert.DeserializeObject<List<MedicoModel>>(json);
        return medicos;
    }
    */
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
