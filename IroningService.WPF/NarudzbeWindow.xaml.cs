using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;
using IroningService.Domena.Entiteti;

namespace IroningService.WPF;

public partial class NarudzbeWindow : Window
{
    private readonly HttpClient _httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:5038/") };

    public NarudzbeWindow()
    {
        InitializeComponent();
        this.Loaded += NarudzbeWindow_Loaded;
    }

    private async void NarudzbeWindow_Loaded(object sender, RoutedEventArgs e)
    {
        await UcitajNarudzbe();
    }

    private async System.Threading.Tasks.Task UcitajNarudzbe()
    {
        try
        {
            string email = UserSession.TrenutniEmail; 
            var narudzbe = await _httpClient.GetFromJsonAsync<List<Narudzba>>($"api/narudzbe?email={email}");

            // --- DEBUG PROVJERA ---
            if (narudzbe != null && narudzbe.Count > 0)
            {
                // Ako ovo prikaže praznu poruku, problem je u API-ju ili bazi (Adresa ne dolazi)
                // Ako prikaže adresu, problem je u DataGrid bindingu
                var prvaNarudzba = narudzbe[0];
                System.Diagnostics.Debug.WriteLine($"Debug: Prva narudžba ima adresu: '{narudzbe[0].Adresa}'");
            }
            // ----------------------

            dgNarudzbe.ItemsSource = narudzbe;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Greška pri učitavanju narudžbi: {ex.Message}");
        }
    }

    private void BtnOdjava_Click(object sender, RoutedEventArgs e)
    {
        UserSession.TrenutniEmail = string.Empty;
        LoginWindow login = new LoginWindow();
        login.Show();
        this.Close();
    }

    private async void BtnNovaNarudzba_Click(object sender, RoutedEventArgs e)
    {
        OdabirUslugaWindow odabirUsluga = new OdabirUslugaWindow();
        
        if (odabirUsluga.ShowDialog() == true)
        {
            await UcitajNarudzbe();
        }
    }

    private async void BtnPonovi_Click(object sender, RoutedEventArgs e)
    {
        var button = sender as System.Windows.Controls.Button;
        var narudzba = button?.DataContext as Narudzba;

        if (narudzba != null)
        {
            var response = await _httpClient.PostAsJsonAsync("api/narudzbe/ponovi", narudzba.NarudzbaId);
            
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Narudžba uspješno ponovljena!");
                await UcitajNarudzbe();
            }
            else
            {
                MessageBox.Show("Greška pri ponavljanju narudžbe.");
            }
        }
    }
    private void BtnPregled_Click(object sender, RoutedEventArgs e)
{
    var button = sender as System.Windows.Controls.Button;
    
    var narudzba = button?.DataContext as Narudzba;

    if (narudzba != null)
    {
        DetaljiNarudzbeWindow detalji = new DetaljiNarudzbeWindow(narudzba);
        detalji.ShowDialog();
    }
}
}