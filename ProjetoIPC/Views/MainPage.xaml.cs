using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices.Sensors;
using ProjetoIPC.Models;
using System;
using System.Threading.Tasks;

namespace ProjetoIPC
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            CheckAndRequestLocationPermission();
        }

        private async void CheckAndRequestLocationPermission()
        {
            try
            {
                var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

                if (status != PermissionStatus.Granted)
                {
                    status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                }

                if (status == PermissionStatus.Granted)
                {
                    LoadMap();
                }
                else
                {
                    await DisplayAlert("Permissão necessária",
                        "A permissão de localização é necessária para centralizar o mapa na sua posição atual.",
                        "OK");
                    LoadMap(); // Carrega mesmo sem permissão
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro na permissão: {ex.Message}");
                LoadMap(); // Carrega mesmo com erro
            }
        }

        private async void LoadMap()
        {
            try
            {
                var htmlSource = new HtmlWebViewSource();
                htmlSource.Html = await LoadHtmlFromAsset();
                mapWebView.Source = htmlSource;
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

        private void MapWebView_Navigating(object sender, WebNavigatingEventArgs e)
        {
            if (e.Url.StartsWith("coords://set"))
            {
                e.Cancel = true;
                var uri = new Uri(e.Url);
                var query = System.Web.HttpUtility.ParseQueryString(uri.Query);

                var origem = query["origem"] ?? string.Empty;
                var destino = query["destino"] ?? string.Empty;

                CoordenadasStore.EnderecoOrigem = Uri.UnescapeDataString(origem);
                CoordenadasStore.EnderecoDestino = Uri.UnescapeDataString(destino);

                Application.Current.MainPage = new AppShell();
            }
        }

        private void OnSubmitClicked(object sender, EventArgs e)
        {
            var origem = CoordenadasStore.EnderecoOrigem;
            var destino = CoordenadasStore.EnderecoDestino;

            if (string.IsNullOrEmpty(origem) || string.IsNullOrEmpty(destino))
            {
                DisplayAlert("Erro", "Selecione a origem e destino no mapa antes de submeter.", "OK");
                return;
            }

            CoordenadasStore.HoraSubmit = DateTime.Now.ToString("HH:mm:ss");
            Application.Current.MainPage = new AppShell();
        }
    }
}