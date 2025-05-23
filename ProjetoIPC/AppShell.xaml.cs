using ProjetoIPC.Services;

namespace ProjetoIPC
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            SetLatestDrivesVisibility();
        }

        public void SetLatestDrivesVisibility()
        {
            // Supondo que Session.CurrentUser está disponível globalmente
            if (Session.CurrentUser != null && Session.CurrentUser.IsDriver)
            {
                LatestDrivesMenu.IsVisible = true;
            }
            else
            {
                LatestDrivesMenu.IsVisible = false;
            }
        }

        private async void OnLogoutClicked(object sender, EventArgs e)
        {
            // Limpa a sessão do usuário
            ProjetoIPC.Services.Session.CurrentUser = null;

            // Redireciona para a tela de login
            Application.Current.MainPage = new NavigationPage(new ProjetoIPC.Views.Login());
        }

    }
}
