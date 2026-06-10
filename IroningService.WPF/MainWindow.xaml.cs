using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;
using System.Windows.Controls;
using System.Threading.Tasks;
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
        await UcitajMojeNarudzbe(); 
    }

    private async void btnNovaNarudzba_Click(object sender, RoutedEventArgs e)
    {
        var prozor = new OdabirUslugaWindow();
    
        // ShowDialog zaustavlja izvršavanje koda dok se prozor ne zatvori
        if (prozor.ShowDialog() == true) 
        {
            await UcitajMojeNarudzbe(); 
        }
    }

    private async Task UcitajMojeNarudzbe()
    {
        try
        {
            string url = $"api/narudzbe?email={UserSession.TrenutniEmail}";
            var narudzbe = await _httpClient.GetFromJsonAsync<List<Narudzba>>(url);
            
            if (narudzbe != null)
            {
                MyDataGrid.ItemsSource = narudzbe;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Greška pri dohvaćanju narudžbi: " + ex.Message);
        }
    }

    private async void BtnPregled_Click(object sender, RoutedEventArgs e)
    {
        var narudzba = (sender as Button)?.DataContext as Narudzba;
        if (narudzba != null) 
        {
            var prozor = new DetaljiNarudzbeWindow(narudzba);
            
            // Kada se DetaljiNarudzbeWindow zatvori, osvježi listu u slučaju da je dodana recenzija
            prozor.ShowDialog();
            await UcitajMojeNarudzbe(); 
        }
    }
}