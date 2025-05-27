using ProjetoIPC.Models;
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
            string username = UsernameEntry.Text?.Trim();
            string password = PasswordEntry.Text?.Trim();
            string email = EmailEntry.Text?.Trim();
            string name = NameEntry.Text?.Trim();
            DateTime dob = DOBPicker.Date;
            string phone = PhoneEntry.Text?.Trim();
            string address = AddressEntry.Text?.Trim();
            string country = CountryEntry.Text?.Trim();
            string city = CityEntry.Text?.Trim();
            string condition = ConditionPicker.SelectedItem?.ToString() ?? "Nenhuma condição";

            // Validação dos campos obrigatórios
            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(phone) ||
                string.IsNullOrWhiteSpace(address) ||
                string.IsNullOrWhiteSpace(country) ||
                string.IsNullOrWhiteSpace(city))
            {
                await DisplayAlert("Erro", "Todos os campos devem ser preenchidos.", "OK");
                return;
            }

            // Validação básica de e-mail
            if (!email.Contains("@") || !email.Contains("."))
            {
                await DisplayAlert("Erro", "E-mail inválido.", "OK");
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
                Password = password,
                Email = email,
                Name = name,
                DateOfBirth = dob,
                Phone = phone,
                Address = address,
                Country = country,
                City = city,
                Condition = condition // <-- Salva a condição selecionada
            };

            await App.Database.SaveUserAsync(user);

            await DisplayAlert("Sucesso", "Usuário registrado com sucesso!", "OK");

            Application.Current.MainPage = new Login();
        }

        private void PhoneEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Remove qualquer caractere que não seja dígito
            var entry = (Entry)sender;
            string onlyNumbers = new string(entry.Text.Where(char.IsDigit).ToArray());
            if (entry.Text != onlyNumbers)
                entry.Text = onlyNumbers;
        }
    }
}
