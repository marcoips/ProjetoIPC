using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

namespace ProjetoIPC.Views;

public partial class DriverApplicationPage : ContentPage
{
    public DriverApplicationPage()
    {
        InitializeComponent();
    }

    private async void OnUploadDriverLicenseClicked(object sender, EventArgs e)
    {
        try
        {
            var result = await FilePicker.Default.PickAsync();
            if (result != null)
            {
                DriverLicenseFileNameLabel.Text = result.FileName;
            }
        }
        catch (Exception ex)
        {
            DriverLicenseFileNameLabel.Text = "Erro ao selecionar ficheiro.";
            Console.WriteLine($"Erro: {ex.Message}");
        }
    }

    private async void OnUploadIdentityCardClicked(object sender, EventArgs e)
    {
        try
        {
            var result = await FilePicker.Default.PickAsync();
            if (result != null)
            {
                IdentityCardFileNameLabel.Text = result.FileName;
            }
        }
        catch (Exception ex)
        {
            IdentityCardFileNameLabel.Text = "Erro ao selecionar ficheiro.";
            Console.WriteLine($"Erro: {ex.Message}");
        }
    }

    private async void OnUploadVehicleDocsClicked(object sender, EventArgs e)
    {
        try
        {
            var result = await FilePicker.Default.PickAsync();
            if (result != null)
            {
                VehicleDocsFileNameLabel.Text = result.FileName;
            }
        }
        catch (Exception ex)
        {
            VehicleDocsFileNameLabel.Text = "Erro ao selecionar ficheiro.";
            Console.WriteLine($"Erro: {ex.Message}");
        }
    }
}
