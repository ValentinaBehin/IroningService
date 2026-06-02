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
        var podaci = new Korisnik { Email = txtEmail.Text, Lozinka = txtLozinka.Password };
        
        try 
        {
            var response = await _httpClient.PostAsJsonAsync("api/korisnici/prijava", podaci);
            
            if (response.IsSuccessStatusCode)
            {
                // 1. Spremi email u sesiju
                UserSession.TrenutniEmail = txtEmail.Text;

                // 2. Otvori NarudzbeWindow umjesto MainWindow
                NarudzbeWindow narudzbe = new NarudzbeWindow();
                narudzbe.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Pogrešan email ili lozinka!");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Greška pri povezivanju s API-jem: {ex.Message}");
        }
    }

    private void BtnRegistracija_Click(object sender, RoutedEventArgs e)
    {
        RegistracijaWindow reg = new RegistracijaWindow();
        reg.Show();
    }
}