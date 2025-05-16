using ProjetoIPC.Models;
using ProjetoIPC.Services;

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
                DisplayAlert("Erro", "Nenhum utilizador est� logado.", "OK");
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
    }

}