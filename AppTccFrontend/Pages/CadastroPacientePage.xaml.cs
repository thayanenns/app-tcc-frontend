using AppTccFrontend.Enums;
using AppTccFrontend.Models;
using Newtonsoft.Json;
using System.Net.Http.Json;
using AppTccFrontend.NovaPasta1;
using System.Collections.ObjectModel;

namespace AppTccFrontend.Pages;

public partial class CadastroPacientePage : ContentPage
{
    private List<MedicoModel> medicos = new List<MedicoModel>();

    public CadastroPacientePage(TipoUsuario tipoUsuario, TipoDiabetes tipoDiabetes)
    {
        InitializeComponent();
        _tipoUsuario = tipoUsuario;
        _tipoDiabetes = tipoDiabetes;

        tipoDiabetesPicker.ItemsSource = Enum.GetValues(typeof(TipoDiabetes)).Cast<TipoDiabetes>().ToList();

        CarregarMedicosAsync();

    }

    private TipoUsuario _tipoUsuario;

    private TipoDiabetes _tipoDiabetes;

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
                Tipo = _tipoUsuario,
                Sexo = entSexo.Text,
                Telefone = entTelefone.Text,
                Domicilio = entDomicilio.Text,
                Email = entEmail.Text,
                Senha = entSenha.Text,
                MedicoId = ((MedicoModel)pickerMedico.SelectedItem)?.Id,
                TipoDiabetes = (TipoDiabetes)tipoDiabetesPicker.SelectedItem

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

            pickerMedico.ItemsSource = medicos;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", "Ocorreu um erro ao carregar os médicos: " + ex.Message, "OK");
        }
    }

    public List<string> TiposDiabetes
    {
        get
        {
            return Enum.GetNames(typeof(TipoDiabetes)).ToList();
        }
    }
}
