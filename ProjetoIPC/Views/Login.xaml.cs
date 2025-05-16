using ProjetoIPC.Models;

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
            await DisplayAlert("Sucesso", "Login realizado com sucesso!", "OK");

            // Redireciona para a AppShell (ou outra página principal)
            Application.Current.MainPage = new AppShell();
        }
        else
        {
            await DisplayAlert("Erro", "Usuário ou senha incorretos.", "OK");
        }
    }
}
