using System.Net.Http.Json;
using System.Runtime.Intrinsics.Arm;

namespace AppTccFrontend.Pages;

public partial class CadastroUsuarioPage : ContentPage
{
	public CadastroUsuarioPage()
	{
		InitializeComponent();
	}
    private string selectedProfile = "";
    /*
        private void OnPacienteClicked(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value)
            {
                selectedProfile = "Paciente";
            }
        }

        private void OnMedicoClicked(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value)
            {
                selectedProfile = "Médico";
            }
        }

        private async void OnContinuarClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(selectedProfile))
            {
                if (selectedProfile == "Paciente")
                {
                    await Navigation.PushAsync(new CadastroPacientePage());
                }
                else if (selectedProfile == "Médico")
                {
                    await Navigation.PushAsync(new CadastroMedicoPage());
                }
            }
            else
            {
                await DisplayAlert("Erro", "Por favor, selecione um perfil.", "OK");
            }
        }
    */
    private async void OnPacienteClicked(object sender, EventArgs e)
    {
        // Redirecione para a página de cadastro de paciente
        await Navigation.PushAsync(new CadastroPacientePage());
    }

    private async void OnMedicoClicked(object sender, EventArgs e)
    {
        // Redirecione para a página de cadastro de médico
        await Navigation.PushAsync(new CadastroMedicoPage());
    }
}
