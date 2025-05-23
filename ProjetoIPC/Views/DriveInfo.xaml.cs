using ProjetoIPC.Models;
using ProjetoIPC.Services;

namespace ProjetoIPC.Views;

public partial class DriveInfo : ContentPage
{
    public DriveInfo()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadAcceptedTrips();
    }

    private async Task LoadAcceptedTrips()
    {
        TripsStack.Children.Clear();

        var userId = Session.CurrentUser?.Id;
        if (userId == null)
        {
            TripsStack.Children.Add(new Label { Text = "Nenhum utilizador autenticado.", HorizontalOptions = LayoutOptions.Center });
            return;
        }

        var trips = (await App.Database.GetTripsOrderedByDateAsync())
            .Where(t => t.UserId == userId && t.Status == "aceite")
            .ToList();

        if (trips.Count == 0)
        {
            TripsStack.Children.Add(new Label { Text = "Nenhuma viagem aceite encontrada.", HorizontalOptions = LayoutOptions.Center });
            return;
        }

        foreach (var trip in trips)
        {
            var frame = new Frame
            {
                BackgroundColor = Colors.LightGray,
                CornerRadius = 10,
                Padding = 15,
                Margin = new Thickness(0, 0, 0, 10),
                Content = new Label
                {
                    Text = $"{trip.Origem} → {trip.Destino}\nData: {trip.HoraSubmit}",
                    FontSize = 16
                }
            };
            TripsStack.Children.Add(frame);
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
