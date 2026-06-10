using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;
using IroningService.Domena.Entiteti;

namespace IroningService.WPF;

public partial class LoginWindow : Window
{
    private readonly HttpClient _httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:5038/") };

    public LoginWindow() => InitializeComponent();

    private async void BtnPrijava_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtLozinka.Password))
        {
            MessageBox.Show("Molimo unesite email i lozinku.");
            return;
        }

        var podaci = new { Email = txtEmail.Text.Trim(), Lozinka = txtLozinka.Password.Trim() };
        
        try 
        {
            var response = await _httpClient.PostAsJsonAsync("api/korisnici/prijava", podaci);
            
            if (response.IsSuccessStatusCode)
            {
                UserSession.TrenutniEmail = txtEmail.Text.Trim();
                
                // 1. Otvori glavni prozor
                NarudzbeWindow narudzbe = new NarudzbeWindow();
                narudzbe.Show();
                
                // 2. Samo sakrij Login prozor umjesto da ga zatvoriš
                this.Hide(); 
            }
            else
            {
                string poruka = await response.Content.ReadAsStringAsync();
                MessageBox.Show($"Greška: {poruka}");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Greška pri povezivanju: {ex.Message}");
        }
    }

    private void BtnRegistracija_Click(object sender, RoutedEventArgs e)
    {
        RegistracijaWindow reg = new RegistracijaWindow();
        reg.Show();
    }

}