namespace ProjetoIPC.Views
{
    public partial class Message : ContentPage
    {
        public Message()
        {
            InitializeComponent();
        }

        private async void HomeClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("///MainPage");
        }
        private void ActivityClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("///Atividade");
        }
        private void ProfileClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("Home");
        }
    }
}
