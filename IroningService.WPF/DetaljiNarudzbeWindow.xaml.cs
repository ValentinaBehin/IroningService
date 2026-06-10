using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;
using IroningService.Domena.Entiteti;

namespace IroningService.WPF;

public partial class DetaljiNarudzbeWindow : Window
{
    private readonly int _narudzbaId; 
    private readonly HttpClient _httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:5038/") };

    public DetaljiNarudzbeWindow(Narudzba narudzba)
    {
        InitializeComponent();
        
        // 1. Pohrani ID za kasniji poziv API-ja
        _narudzbaId = narudzba.NarudzbaId;
        
        // 2. Postavi DataContext za postojeće vezivanje (Datum, Adresa, Stavke...)
        this.DataContext = narudzba;
        
        // 3. Učitaj dodatne podatke (recenziju) s API-ja
        UcitajRecenziju(); 
    }

    private async void UcitajRecenziju()
    {
        try 
        {
            // Ovdje šaljemo zahtjev na API. Ako dobiješ 404, provjeri u svom 
            // API kontroleru imaš li metodu koja odgovara na: GET api/narudzbe/{id}
            var narudzbaIzApi = await _httpClient.GetFromJsonAsync<Narudzba>($"api/narudzbe/{_narudzbaId}");
            
            if (narudzbaIzApi != null)
            {
                // Provjeri u klasi Narudzba da li se polje zove točno 'RecenzijaKomentar'
                // Ako je prazno, prikaži poruku
                txtRecenzijaPrikaz.Text = !string.IsNullOrEmpty(narudzbaIzApi.RecenzijaKomentar) 
                                          ? narudzbaIzApi.RecenzijaKomentar 
                                          : "Nema još recenzije.";
            }
        }
        catch (HttpRequestException ex)
        {
            // 404 greška često znači da URL nije ispravan ili resurs ne postoji
            // Ovdje možemo biti tihi ako recenzija nije obavezna
            System.Diagnostics.Debug.WriteLine($"Greška pri dohvaćanju: {ex.Message}");
        }
        catch (Exception ex)
        {
            MessageBox.Show("Greška pri učitavanju recenzije: " + ex.Message);
        }
    }

    private void BtnOtvoriRecenziju_Click(object sender, RoutedEventArgs e)
    {
        RecenzijaWindow prozor = new RecenzijaWindow(_narudzbaId);
        bool? rezultat = prozor.ShowDialog();
        // Ako je korisnik uspješno spremio recenziju
        if (rezultat == true)
        {
            UcitajRecenziju(); // Osvježi prikaz
        }
    }

    private void BtnZatvori_Click(object sender, RoutedEventArgs e) 
    {
        this.Close();
    }
}