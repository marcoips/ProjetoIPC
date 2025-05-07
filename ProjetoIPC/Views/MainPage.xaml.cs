using System.Reflection;

namespace ProjetoIPC
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            LoadMap();
        }

        private async void LoadMap()
        {
            try
            {
                // Carrega o HTML do arquivo embutido
                var htmlSource = new HtmlWebViewSource();
                htmlSource.Html = await LoadHtmlFromAsset();
                mapWebView.Source = htmlSource;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", $"Falha ao carregar o mapa: {ex.Message}", "OK");
            }
        }

        private async Task<string> LoadHtmlFromAsset()
        {
            using var stream = await FileSystem.OpenAppPackageFileAsync("map.html");
            using var reader = new StreamReader(stream);
            return await reader.ReadToEndAsync();
        }
    }

}
