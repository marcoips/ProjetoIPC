using ProjetoIPC.Models;
using ProjetoIPC.Services;

namespace ProjetoIPC.Views
{
    public partial class Profile : ContentPage
    {
        private User _currentUser;

        public Profile()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (Session.CurrentUser != null)
            {
                // Carrega o utilizador mais recente da base de dados
                _currentUser = await App.Database.GetUserByIdAsync(Session.CurrentUser.Id);
                LoadUserData(_currentUser);
            }
            else
            {
                await DisplayAlert("Erro", "Nenhum utilizador está logado.", "OK");
            }
        }

        private void LoadUserData(User user)
        {
            if (user == null) return;

            EmailEntry.Text = user.Email;
            NameEntry.Text = user.Name;
            BirthdayEntry.Text = user.DateOfBirth.ToString("yyyy-MM-dd");
            PhoneEntry.Text = user.Phone;
            AddressEntry.Text = user.Address;
            CountryEntry.Text = user.Country;
            CityEntry.Text = user.City;

            // Seleciona a condição no Picker
            if (!string.IsNullOrEmpty(user.Condition))
            {
                var idx = ConditionPicker.ItemsSource
                    .Cast<string>()
                    .ToList()
                    .FindIndex(c => c == user.Condition);
                ConditionPicker.SelectedIndex = idx >= 0 ? idx : 0;
            }
            else
            {
                ConditionPicker.SelectedIndex = 0;
            }
        }

        private async void OnUpdateButtonClicked(object sender, EventArgs e)
        {
            if (_currentUser == null)
                return;

            _currentUser.Email = EmailEntry.Text?.Trim();
            _currentUser.Name = NameEntry.Text?.Trim();
            DateTime.TryParse(BirthdayEntry.Text, out var dob);
            _currentUser.DateOfBirth = dob;
            _currentUser.Phone = PhoneEntry.Text?.Trim();
            _currentUser.Address = AddressEntry.Text?.Trim();
            _currentUser.Country = CountryEntry.Text?.Trim();
            _currentUser.City = CityEntry.Text?.Trim();
            _currentUser.Condition = ConditionPicker.SelectedItem?.ToString() ?? "Nenhuma condição";

            await App.Database.UpdateUserAsync(_currentUser);

            await DisplayAlert("Sucesso", "Dados atualizados com sucesso!", "OK");
        }

        private async void OnApplyButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DriverApplicationPage());
        }

        private async void HomeClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("///MainPage");
        }
        private void ActivityClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("///Atividade");
        }
    }
}
