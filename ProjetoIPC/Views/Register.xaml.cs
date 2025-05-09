using Microsoft.Maui.Controls;

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
            Application.Current.MainPage = new Login();
        }
    }
}


