using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;
using IroningService.Domena.Entiteti;

namespace IroningService.WPF;

public partial class MainWindow : Window
{
    private readonly HttpClient _httpClient;

    public MainWindow()
    {
        InitializeComponent();
        
        // Postavljamo klijent na tvoj API port
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri("http://localhost:5038/");

        // Pokrećemo učitavanje nakon što se prozor učita
        Loaded += MainWindow_Loaded;
    }

    private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        await UcitajNarudzbe();
    }

    private async Task UcitajNarudzbe()
    {
        try
        {
            // Pozivamo API endpoint (provjeri je li putanja u kontroleru /api/narudzbe)
            var narudzbe = await _httpClient.GetFromJsonAsync<List<Narudzba>>("api/narudzbe");
            
            // Povezujemo podatke s DataGridom
            // Koristimo Dispatcher jer dolazimo iz asinkronog poziva
            Dispatcher.Invoke(() => 
            {
                MyDataGrid.ItemsSource = narudzbe;
            });
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Greška pri dohvaćanju narudžbi: {ex.Message}", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}