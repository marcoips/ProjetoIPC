using ProjetoIPC.Views;

namespace ProjetoIPC
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Login());
        }

       /* protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new Views.Login());
        }*/
    }
}