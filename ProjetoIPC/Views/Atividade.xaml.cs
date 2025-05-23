using ProjetoIPC.Models;
using ProjetoIPC.Services;
using System.Windows.Input;

namespace ProjetoIPC.Views
{
    public partial class Atividade : ContentPage
    {
        public ICommand DriveTappedCommand { get; }

        private Frame _lastTappedFrame; // Track the last tapped frame

        public Atividade()
        {
            InitializeComponent();

            

            // Bind the command to the page's BindingContext
            BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadAllUserDrives();
        }

        private async Task LoadAllUserDrives()
        {
            DrivesStack.Children.Clear();

            var userId = Session.CurrentUser?.Id;
            if (userId == null)
            {
                DrivesStack.Children.Add(new Label { Text = "Nenhum utilizador autenticado.", HorizontalOptions = LayoutOptions.Center });
                return;
            }

            var trips = (await App.Database.GetTripsOrderedByDateAsync())
                .Where(t => t.UserId == userId)
                .ToList();

            if (trips.Count == 0)
            {
                DrivesStack.Children.Add(new Label { Text = "Nenhuma viagem encontrada.", HorizontalOptions = LayoutOptions.Center });
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
                        Text = $"{trip.Origem} → {trip.Destino}\nData: {trip.HoraSubmit}\nEstado: {trip.Status}",
                        FontSize = 16
                    }
                };
                DrivesStack.Children.Add(frame);
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
}
