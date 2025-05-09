using Microsoft.Maui.Controls;
using ProjetoIPC.Models;

namespace ProjetoIPC.Views;

public partial class DriveInfo : ContentPage
{
    public DriveInfo()
    {
        InitializeComponent();

        enderecoLabel.Text = $"Origem: {CoordenadasStore.EnderecoOrigem}\n" +
                             $"Destino: {CoordenadasStore.EnderecoDestino}\n" +
                             $"Hora do Submit: {CoordenadasStore.HoraSubmit}";
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
