using ProjetoIPC.Models; // Importa a classe User
using System;

namespace ProjetoIPC.Views
{
    public partial class Register : ContentPage
    {
        public Register()
        {
            InitializeComponent();
        }

        private async void OnBackButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void SubmitClick(object sender, EventArgs e)
        {
            string username = UsernameEntry.Text;
            string password = PasswordEntry.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                await DisplayAlert("Erro", "Preencha todos os campos.", "OK");
                return;
            }

            var existingUser = await App.Database.GetUserByUsernameAsync(username);
            if (existingUser != null)
            {
                await DisplayAlert("Erro", "Usuário já existe.", "OK");
                return;
            }

            var user = new User
            {
                Username = username,
                Password = password
            };

            await App.Database.SaveUserAsync(user);

            await DisplayAlert("Sucesso", "Usuário registrado com sucesso!", "OK");

            // Volta para o Login
            Application.Current.MainPage = new Login();
        }
    }
}
