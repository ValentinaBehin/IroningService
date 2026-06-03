using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;
using IroningService.Domena.Entiteti;

namespace IroningService.WPF;

public partial class NovaNarudzbaWindow : Window
{
    private readonly HttpClient _httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:5038/") };
    private ObservableCollection<UslugaPeglanja> _odabraneStavke = new ObservableCollection<UslugaPeglanja>();

    public NovaNarudzbaWindow()
    {
        InitializeComponent();
        UcitajUsluge();
        dgStavke.ItemsSource = _odabraneStavke; 
    }

    private async void UcitajUsluge()
    {
        try
        {
            var usluge = await _httpClient.GetFromJsonAsync<List<UslugaPeglanja>>("api/usluge");
            cmbUsluge.ItemsSource = usluge;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Greška pri učitavanju usluga: {ex.Message}");
        }
    }
    
private void ProvjeriAdresu(object sender, RoutedEventArgs e)
{
    // Provjera da li su kontrole inicijalizirane prije pristupa
    if (pnlAdresa == null) return;

    if (chkPrikup?.IsChecked == true || chkDostava?.IsChecked == true)
    {
        pnlAdresa.Visibility = Visibility.Visible;
    }
    else
    {
        pnlAdresa.Visibility = Visibility.Collapsed;
        txtAdresa.Text = string.Empty;
    }
}

    private void btnDodajStavku_Click(object sender, RoutedEventArgs e)
    {
        var odabrana = cmbUsluge.SelectedItem as UslugaPeglanja;
        if (odabrana != null)
        {
            _odabraneStavke.Add(odabrana);
        }
        else
        {
            MessageBox.Show("Molimo odaberite uslugu iz padajućeg izbornika.");
        }
    }

    

    private async void btnSpremi_Click(object sender, RoutedEventArgs e)
    {
        if (_odabraneStavke.Count == 0) 
        { 
            MessageBox.Show("Košarica je prazna. Dodajte barem jednu uslugu!"); 
            return; 
        }

        var novaNarudzba = new Narudzba
        {
            KlijentEmail = UserSession.TrenutniEmail, 
            DatumNarudzbe = DateTime.Now,
            TerminDostave = DateTime.Now.AddDays(2), 
            PotrebnaDostava = chkDostava.IsChecked ?? false,
            Adresa = pnlAdresa.Visibility == Visibility.Visible ? txtAdresa.Text : "N/A",
            
            Stavke = _odabraneStavke.Select(u => new StavkaNarudzbe 
            { 
                UslugaId = u.Id,
                Kolicina = 1,
                CijenaUTrenutkuNarudzbe = u.Cijena 
            }).ToList(),
            
            UkupnaCijena = _odabraneStavke.Sum(u => u.Cijena)
        };

        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/narudzbe", novaNarudzba);
            
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Narudžba uspješno spremljena!");
                this.DialogResult = true; 
                this.Close();
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                MessageBox.Show($"Greška pri slanju: {error}");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Greška: {ex.Message}");
        }
    }
}