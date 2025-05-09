using Microsoft.Maui.Controls;
using ProjetoIPC.Models; // Certifique-se de usar o namespace correto
using System;

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
                var htmlSource = new HtmlWebViewSource();
                htmlSource.Html = await LoadHtmlFromAsset();
                mapWebView.Source = htmlSource;

                // Adiciona o evento para interceptar as URLs
                mapWebView.Navigating += MapWebView_Navigating;
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

        // Método que intercepta as URLs e armazena os dados na CoordenadasStore
        private void MapWebView_Navigating(object sender, WebNavigatingEventArgs e)
        {
            if (e.Url.StartsWith("coords://set"))
            {
                e.Cancel = true;

                var uri = new Uri(e.Url);
                var query = System.Web.HttpUtility.ParseQueryString(uri.Query);

                var origem = query["origem"];
                var destino = query["destino"];

                CoordenadasStore.EnderecoOrigem = Uri.UnescapeDataString(origem);
                CoordenadasStore.EnderecoDestino = Uri.UnescapeDataString(destino);

                // Após salvar, podemos navegar para outra página ou exibir os dados.
                Application.Current.MainPage = new AppShell();
            }
        }

        // Método do botão Submit para garantir que as coordenadas foram definidas
        private void OnSubmitClicked(object sender, EventArgs e)
        {
            var origem = CoordenadasStore.EnderecoOrigem;
            var destino = CoordenadasStore.EnderecoDestino;

            if (string.IsNullOrEmpty(origem) || string.IsNullOrEmpty(destino))
            {
                DisplayAlert("Erro", "Selecione a origem e destino no mapa antes de submeter.", "OK");
                return;
            }

            var horaDoSubmit = DateTime.Now.ToString("HH:mm:ss");

            CoordenadasStore.HoraSubmit = horaDoSubmit;

            DisplayAlert("Endereços selecionados",
                         $"Origem: {origem}\nDestino: {destino}", "OK");

            
            Application.Current.MainPage = new AppShell();
        }
    }
}
