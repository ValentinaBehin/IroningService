using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;
using IroningService.Domena.Entiteti;

namespace IroningService.WPF;

public partial class RegistracijaWindow : Window
{
    // Kreiramo HttpClient instancu
    private readonly HttpClient _httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:5038/") };

    public RegistracijaWindow() => InitializeComponent();

    private async void BtnRegistriraj_Click(object sender, RoutedEventArgs e)
    {
        // Priprema podataka iz forme
        var noviKorisnik = new Korisnik
        {
            Ime = txtIme.Text,
            Prezime = txtPrezime.Text,
            Email = txtEmail.Text,
            Lozinka = txtLozinka.Password
        };

        try 
        {
            // Slanje zahtjeva na API (samo jedan poziv!)
            var response = await _httpClient.PostAsJsonAsync("api/korisnici/registriraj", noviKorisnik);

            // Čitamo cijeli odgovor kao tekst
            string jsonString = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Uspjeh!");
            }
            else
            {
                // Ovdje ćemo vidjeti točnu poruku iz API-ja (npr. "Email već postoji")
                MessageBox.Show("Greška: " + jsonString);
            }
        }
        catch (Exception ex)
        {
            // Hvatanje grešaka poput "Server nije dostupan"
            MessageBox.Show($"Nije moguće povezati se s API-jem: {ex.Message}");
        }
    }
}