using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;
using IroningService.Domena.Entiteti;

namespace IroningService.WPF;

public partial class DetaljiNarudzbeWindow : Window
{
    private readonly int _narudzbaId; 
    // HttpClient bi idealno trebao biti statičan u aplikaciji, ali ostavljamo ovako radi jednostavnosti
    private readonly HttpClient _httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:5038/") };

    public DetaljiNarudzbeWindow(Narudzba narudzba)
    {
        InitializeComponent();
        
        _narudzbaId = narudzba.NarudzbaId;
        this.DataContext = narudzba;
        
        // Poziv učitavanja
        UcitajRecenziju(); 
    }

    private void BtnOtvoriRecenziju_Click(object sender, RoutedEventArgs e)
    {
        RecenzijaWindow prozor = new RecenzijaWindow(_narudzbaId);
        
        // ShowDialog vraća true ako se postavi DialogResult = true
        if (prozor.ShowDialog() == true)
        {
            UcitajRecenziju(); 
        }
    }

    private async void UcitajRecenziju()
    {
        try 
        {
            // Pozivamo API jednom
            string url = $"api/narudzbe/{_narudzbaId}?t={DateTime.Now.Ticks}";
            var narudzbaIzApi = await _httpClient.GetFromJsonAsync<Narudzba>(url);
            
            // Logiramo za debugiranje (vidiš u Output prozoru VS-a)
            System.Diagnostics.Debug.WriteLine($"API odziv: {narudzbaIzApi?.RecenzijaKomentar}");

            if (narudzbaIzApi != null)
            {
                // Provjera je li polje NULL ili prazno
                if (!string.IsNullOrEmpty(narudzbaIzApi.RecenzijaKomentar))
                {
                    txtRecenzijaPrikaz.Text = $"Ocjena: {narudzbaIzApi.RecenzijaOcjena}/5\nKomentar: {narudzbaIzApi.RecenzijaKomentar}";
                }
                else
                {
                    txtRecenzijaPrikaz.Text = "Nema još recenzije.";
                }
            }
        }
        catch (Exception ex)
        {
            txtRecenzijaPrikaz.Text = $"Greška: {ex.Message}";
    System.Diagnostics.Debug.WriteLine($"DETALJI GREŠKE: {ex}");
    }
    }

    private void BtnZatvori_Click(object sender, RoutedEventArgs e) 
    {
        this.Close();
    }
}