using ProjetoIPC.Models;
using ProjetoIPC.Services;

namespace ProjetoIPC.Views;

public partial class LatestDrives : ContentPage
{
    public LatestDrives()
    {
        InitializeComponent();
        base.OnAppearing();
        LoadTrips();
    }

    private async void LoadTrips()
    {
        DrivesStack.Children.Clear();
        var trips = (await App.Database.GetTripsOrderedByDateAsync())
            .Where(t => t.Status == "por aceitar")
            .ToList();

        foreach (var trip in trips)
        {
            // Buscar o utilizador associado à viagem
            User user = null;
            if (trip.UserId != null)
                user = await App.Database.GetUserByIdAsync(trip.UserId);

            // Frame com info da viagem, nome e condição
            var frame = new Frame
            {
                BackgroundColor = Colors.LightGray,
                Padding = 20,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Content = new Label
                {
                    Text = $"{trip.Origem} → {trip.Destino} ({trip.HoraSubmit})\n" +
                           $"Nome: {user?.Name ?? "Desconhecido"}\n" +
                           $"Condição: {user?.Condition ?? "Nenhuma"}",
                    VerticalOptions = LayoutOptions.Center
                },
                Margin = new Thickness(0, 0, -450, 0)
            };

            // Botão aceitar
            var acceptButton = new Button
            {
                Text = "accept",
                BackgroundColor = Colors.Green,
                WidthRequest = 100,
                Margin = new Thickness(5)
            };
            acceptButton.Clicked += async (s, e) =>
            {
                trip.Status = "aceite";
                await App.Database.UpdateTripAsync(trip);
                await DisplayAlert("Sucesso", "Viagem aceite!", "OK");
                LoadTrips();
            };

            // Botão cancelar
            var declineButton = new Button
            {
                Text = "decline",
                BackgroundColor = Colors.Red,
                WidthRequest = 100,
                Margin = new Thickness(5)
            };
            declineButton.Clicked += async (s, e) =>
            {
                trip.Status = "recusada";
                await App.Database.UpdateTripAsync(trip);
                LoadTrips();
            };

            // Stack dos botões
            var buttonStack = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.EndAndExpand,
                VerticalOptions = LayoutOptions.Center,
                Children = { acceptButton, declineButton }
            };

            // Stack da drive (linha)
            var tripStack = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Margin = new Thickness(5),
                Children = { frame, buttonStack }
            };

            DrivesStack.Children.Add(tripStack);
        }
    }

    private async void HomeClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///MainPage");
    }
    private void ActivityClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("///Atividade");
    }
    private async void ProfileClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//Profile");
    }
}
