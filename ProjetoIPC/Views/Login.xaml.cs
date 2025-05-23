using ProjetoIPC.Models;
using ProjetoIPC.Services;

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
        string username = LoginEntry.Text;
        string password = PasswordEntry.Text;

        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            await DisplayAlert("Erro", "Por favor, preencha todos os campos.", "OK");
            return;
        }

        // Verifica se o usuário existe na base de dados
        var user = await App.Database.GetUserAsync(username, password);

        if (user != null)
        {   
            Session.CurrentUser = user;
            await DisplayAlert("Sucesso", "Login realizado com sucesso!", "OK");

            var shell = new AppShell();
            shell.SetLatestDrivesVisibility(); // Atualiza a visibilidade conforme o usuário logado
            Application.Current.MainPage = shell; ;
        }
        else
        {
            await DisplayAlert("Erro", "Usuário ou senha incorretos.", "OK");
        }
    }
}
