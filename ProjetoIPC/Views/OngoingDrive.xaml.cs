using Microsoft.Maui.Platform;
using System.Reflection;

namespace ProjetoIPC.Views;

public partial class OngoingDrive : ContentPage
{
    public OngoingDrive()
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

    private async void HomeClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///MainPage");
    }
    private void ActivityClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("///Atividade");
    }
    private void ProfileClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("Home");
    }
}
