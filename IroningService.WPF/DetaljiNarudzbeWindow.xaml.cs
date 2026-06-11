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
        
        _narudzbaId = narudzba.NarudzbaId;
        this.DataContext = narudzba;
        
        UcitajRecenziju(); 
    }

    private void BtnOtvoriRecenziju_Click(object sender, RoutedEventArgs e)
    {
        RecenzijaWindow prozor = new RecenzijaWindow(_narudzbaId);
        
        if (prozor.ShowDialog() == true)
        {
            UcitajRecenziju(); 
        }
    }
    private void BtnNaplata_Click(object sender, RoutedEventArgs e)
{
    NaplataWindow naplata = new NaplataWindow();
    
    if (naplata.ShowDialog() == true) 
    {
         txtStatusNaplate.Visibility = Visibility.Visible;
    }
}

    private async void UcitajRecenziju()
    {
        try 
        {
            string url = $"api/narudzbe/{_narudzbaId}?t={DateTime.Now.Ticks}";
            
            var narudzbaIzApi = await _httpClient.GetFromJsonAsync<Narudzba>(url);
            
            if (narudzbaIzApi != null)
            {
                // Ovdje pristupamo objektu Recenzija umjesto direktnim poljima
                if (narudzbaIzApi.Recenzija != null)
                {
                    txtRecenzijaPrikaz.Text = $"Ocjena: {narudzbaIzApi.Recenzija.Ocjena}/5\nKomentar: {narudzbaIzApi.Recenzija.Komentar}";
                }
                else
                {
                    txtRecenzijaPrikaz.Text = "Nema recenzije za ovu narudžbu.";
                }
            }
        }
        catch (Exception ex)
        {
            txtRecenzijaPrikaz.Text = "Greška: " + ex.Message;
        }
    }

    private void BtnZatvori_Click(object sender, RoutedEventArgs e) 
    {
        this.Close();
    }
}