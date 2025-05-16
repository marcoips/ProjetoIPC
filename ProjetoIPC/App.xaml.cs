using ProjetoIPC.Data;
using ProjetoIPC.Views;

namespace ProjetoIPC
{
    public partial class App : Application
    {
        private static UserDatabase _database;
        public static UserDatabase Database =>
            _database ??= new UserDatabase(Path.Combine(FileSystem.AppDataDirectory, "users.db3"));


        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Login());
        }       
    }
}