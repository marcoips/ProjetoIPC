namespace ProjetoIPC.Views;

public partial class Login : ContentPage
{
	public Login()
	{
		InitializeComponent();
	}

    private async void OnRegisterButtonClicked(object sender, EventArgs e)
    {
        // Navega para a página Register
        await Navigation.PushAsync(new Register());
    }
    private async void OnLoginButtonClicked(object sender, EventArgs e)
    {
        
            
          Application.Current.MainPage = new AppShell();
        
    }
}