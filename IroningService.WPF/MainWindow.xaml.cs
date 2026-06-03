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
        
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri("http://localhost:5038/");

        Loaded += MainWindow_Loaded;
    }

    private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        await UcitajNarudzbe();
    }

    private void btnNovaNarudzba_Click(object sender, RoutedEventArgs e)
    {
        NovaNarudzbaWindow noviProzor = new NovaNarudzbaWindow();
        noviProzor.ShowDialog();
        
        // Nakon zatvaranja prozora, osvježi listu
        _ = UcitajNarudzbe();
    }

    private async Task UcitajNarudzbe()
{
    // Ako email nije postavljen, nemoj ni pokušavati zvati API
    if (string.IsNullOrEmpty(UserSession.TrenutniEmail)) return;

    try
    {
        string url = $"api/narudzbe?email={UserSession.TrenutniEmail}";
        var narudzbe = await _httpClient.GetFromJsonAsync<List<Narudzba>>(url);
        
        Dispatcher.Invoke(() => { MyDataGrid.ItemsSource = narudzbe; });
    }
    catch (Exception ex)
    {
        MessageBox.Show($"Greška: {ex.Message}");
    }
}
}