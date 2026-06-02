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

    // Učitavanje narudžbi samo za prijavljenog korisnika
    private async void NarudzbeWindow_Loaded(object sender, RoutedEventArgs e)
    {
        try
        {
            // Koristimo email koji smo spremili tijekom prijave (UserSession)
            string email = UserSession.TrenutniEmail; 

            // Pozivamo API s emailom kao parametrom
            var narudzbe = await _httpClient.GetFromJsonAsync<List<Narudzba>>($"api/narudzbe?email={email}");
            dgNarudzbe.ItemsSource = narudzbe;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Greška pri učitavanju narudžbi: {ex.Message}");
        }
    }

    private void BtnOdjava_Click(object sender, RoutedEventArgs e)
    {
        // Čistimo sesiju pri odjavi
        UserSession.TrenutniEmail = string.Empty;
        
        LoginWindow login = new LoginWindow();
        login.Show();
        this.Close();
    }

    private void BtnNovaNarudzba_Click(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("Ovdje će se otvoriti prozor za novu narudžbu.");
    }

    private async void BtnPonovi_Click(object sender, RoutedEventArgs e)
    {
        var button = sender as System.Windows.Controls.Button;
        var narudzba = button?.DataContext as Narudzba;

        if (narudzba != null)
        {
            var response = await _httpClient.PostAsJsonAsync("api/narudzbe/ponovi", narudzba.Id);
            
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Narudžba uspješno ponovljena!");
                // Osvježavamo listu nakon ponavljanja
                NarudzbeWindow_Loaded(null, null); 
            }
            else
            {
                MessageBox.Show("Greška pri ponavljanju narudžbe.");
            }
        }
    }
}