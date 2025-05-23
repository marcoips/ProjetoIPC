using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices.Sensors;
using ProjetoIPC.Models;
using System;
using System.Threading.Tasks;
using ProjetoIPC.Services;
using System.Linq;

namespace ProjetoIPC
{
    public partial class MainPage : ContentPage
    {
        private Trip _pendingTrip;

        public MainPage()
        {
            InitializeComponent();
            CheckAndRequestLocationPermission();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await CheckPendingTrip();
        }

        private async Task CheckPendingTrip()
        {
            var userId = Session.CurrentUser?.Id;
            if (userId == null)
            {
                _pendingTrip = null;
                CancelTripButton.IsVisible = false;
                return;
            }

            var trips = await App.Database.GetTripsOrderedByDateAsync();
            _pendingTrip = trips.FirstOrDefault(t =>
                t.UserId == userId &&
                (t.Status == "por aceitar"));

            CancelTripButton.IsVisible = _pendingTrip != null && _pendingTrip.Status == "por aceitar";
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
            }
        }

        private async void OnSubmitClicked(object sender, EventArgs e)
        {
            await CheckPendingTrip();
            if (_pendingTrip != null)
            {
                await DisplayAlert("Atenção", "Já existe uma viagem pendente ou aceite. Cancele ou aguarde antes de submeter outra.", "OK");
                return;
            }

            var origem = CoordenadasStore.EnderecoOrigem;
            var destino = CoordenadasStore.EnderecoDestino;

            if (string.IsNullOrEmpty(origem) || string.IsNullOrEmpty(destino))
            {
                await DisplayAlert("Erro", "Selecione a origem e destino no mapa antes de submeter.", "OK");
                return;
            }

            CoordenadasStore.HoraSubmit = DateTime.Now.ToString("HH:mm:ss");

            var trip = new Trip
            {
                Origem = origem,
                Destino = destino,
                Status = "por aceitar",
                HoraSubmit = CoordenadasStore.HoraSubmit,
                UserId = Session.CurrentUser?.Id
            };
            await App.Database.SaveTripAsync(trip);

            await DisplayAlert("Sucesso", "Viagem submetida e aguardando confirmação de um condutor.", "OK");
            await CheckPendingTrip();
            Application.Current.MainPage = new AppShell();
        }

        private async void OnCancelTripClicked(object sender, EventArgs e)
        {
            if (_pendingTrip != null && _pendingTrip.Status == "por aceitar")
            {
                _pendingTrip.Status = "cancelada";
                await App.Database.UpdateTripAsync(_pendingTrip);
                await DisplayAlert("Cancelada", "A sua viagem foi cancelada.", "OK");
                _pendingTrip = null;
                CancelTripButton.IsVisible = false;
            }
        }
    }
}
