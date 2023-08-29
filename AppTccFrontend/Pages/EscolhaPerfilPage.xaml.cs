using AppTccFrontend.Enums;

namespace AppTccFrontend.Pages;

public partial class EscolhaPerfilPage : ContentPage
{
	public EscolhaPerfilPage()
	{
		InitializeComponent();
	}

    //private string selectedProfile = "";
    
    private async void OnPacienteClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CadastroPacientePage(TipoUsuario.Paciente));
    }

    private async void OnMedicoClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CadastroMedicoPage(TipoUsuario.Medico));
    }
}
