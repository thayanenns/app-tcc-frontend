using AppTccFrontend.Enums;

namespace AppTccFrontend.Pages;

public partial class EscolhaPerfilPage : ContentPage
{
	public EscolhaPerfilPage()
	{
		InitializeComponent();
	}
    
    private async void OnPacienteClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CadastroPacientePage(TipoUsuario.Paciente, TipoDiabetes.Outro));
    }

    private async void OnMedicoClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CadastroMedicoPage(TipoUsuario.Medico));
    }
}
