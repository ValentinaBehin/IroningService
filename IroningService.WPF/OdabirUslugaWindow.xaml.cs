using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks; // Dodano za Task
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using IroningService.Domena.Entiteti;

namespace IroningService.WPF;

public partial class OdabirUslugaWindow : Window
{
    private readonly HttpClient _httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:5038/") };

    public OdabirUslugaWindow()
    {
        InitializeComponent();
        this.Loaded += OdabirUslugaWindow_Loaded;
    }

    private async void OdabirUslugaWindow_Loaded(object sender, RoutedEventArgs e)
    {
        await UcitajUsluge();
    }

    private async Task UcitajUsluge()
    {
        try
        {
            var usluge = await _httpClient.GetFromJsonAsync<List<UslugaPeglanja>>("api/usluge");
            if (usluge != null) icUsluge.ItemsSource = usluge;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Greška pri učitavanju usluga: {ex.Message}");
        }
    }

    private async void BtnSpremi_Click(object sender, RoutedEventArgs e)
    {
        // 1. Pripremi stavke
        var odabraneStavke = new List<StavkaNarudzbe>();
        foreach (var item in icUsluge.Items)
        {
            var container = icUsluge.ItemContainerGenerator.ContainerFromItem(item) as FrameworkElement;
            if (container == null) continue;

            var checkBox = FindVisualChild<CheckBox>(container);
            var textBox = FindVisualChild<TextBox>(container);

            if (checkBox?.IsChecked == true)
            {
                var usluga = item as UslugaPeglanja;
                int kolicina = (textBox != null && int.TryParse(textBox.Text, out int k)) ? k : 1;
                odabraneStavke.Add(new StavkaNarudzbe { UslugaId = usluga.Id, Kolicina = kolicina, CijenaUTrenutkuNarudzbe = usluga.Cijena });
            }
        }

        if (odabraneStavke.Count == 0) { MessageBox.Show("Odaberite uslugu!"); return; }

        // 2. Kreiraj objekt narudžbe
        var novaNarudzba = new Narudzba
        {
            KlijentEmail = UserSession.TrenutniEmail,
            DatumNarudzbe = DateTime.Now,
            Adresa = (pnlAdresa.Visibility == Visibility.Visible) ? txtAdresa.Text : "N/A",
            PotrebnaDostava = chkDostava.IsChecked ?? false,
            Stavke = odabraneStavke
        };

        // 3. Pošalji na API
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/narudzbe", novaNarudzba);
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Narudžba uspješno kreirana!");
                this.DialogResult = true; // Ovo zatvara prozor i vraća 'true' u MainWindow
                this.Close();
            }
            else
            {
                var err = await response.Content.ReadAsStringAsync();
                MessageBox.Show($"Greška: {err}");
            }
        }
        catch (Exception ex) { MessageBox.Show($"Greška: {ex.Message}"); }
    }

    private void Izracunaj_Event(object sender, RoutedEventArgs e) => IzracunajUkupno();

    private void ProvjeriAdresu(object sender, RoutedEventArgs e)
    {
        pnlAdresa.Visibility = (chkPrikup.IsChecked == true || chkDostava.IsChecked == true) 
                               ? Visibility.Visible : Visibility.Collapsed;
    }

    private void IzracunajUkupno()
    {
        decimal ukupno = 0;
        foreach (var item in icUsluge.Items)
        {
            var container = icUsluge.ItemContainerGenerator.ContainerFromItem(item) as FrameworkElement;
            if (container == null) continue;
            var checkBox = FindVisualChild<CheckBox>(container);
            var textBox = FindVisualChild<TextBox>(container);
            if (checkBox?.IsChecked == true)
            {
                var usluga = item as UslugaPeglanja;
                int kolicina = (textBox != null && int.TryParse(textBox.Text, out int k)) ? k : 1;
                if (usluga != null) ukupno += (usluga.Cijena * kolicina);
            }
        }
        txtUkupnaCijena.Text = ukupno.ToString("F2") + " €";
    }

    private T? FindVisualChild<T>(DependencyObject obj) where T : DependencyObject
    {
        for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
        {
            var child = VisualTreeHelper.GetChild(obj, i);
            if (child is T typedChild) return typedChild;
            var result = FindVisualChild<T>(child);
            if (result != null) return result;
        }
        return null;
    }
}