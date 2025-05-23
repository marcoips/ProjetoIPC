using ProjetoIPC.Models;
using ProjetoIPC.Services;
using ProjetoIPC.Views;

namespace ProjetoIPC.Views
{
    public partial class Profile : ContentPage
    {
        public Profile()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (Session.CurrentUser != null)
            {
                LoadUserData(Session.CurrentUser);
            }
            else
            {
                DisplayAlert("Erro", "Nenhum utilizador está logado.", "OK");
            }
        }

        private void LoadUserData(User user)
        {
            EmailEntry.Text = user.Email;
            NameEntry.Text = user.Name;
            BirthdayEntry.Text = user.DateOfBirth.ToString("yyyy-MM-dd");
            PhoneEntry.Text = user.Phone;
            AddressEntry.Text = user.Address;
            CountryEntry.Text = user.Country;
            CityEntry.Text = user.City;
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