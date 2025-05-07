using Microsoft.Maui.Controls;

namespace ProjetoIPC.Views
{
    public partial class Confirmation : ContentPage
    {
        public Confirmation()
        {
            InitializeComponent();
        }

        private async void OnBackButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
