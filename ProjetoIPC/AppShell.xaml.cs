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
    }
}
