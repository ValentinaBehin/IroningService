using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
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
        UcitajUsluge();
    }

    // Poziva se na promjenu CheckBox-a ili TextBox-a (iz XAML-a preko Izracunaj_Event)
    private void Izracunaj_Event(object sender, RoutedEventArgs e)
    {
        IzracunajUkupno();
    }

    private void ProvjeriAdresu(object sender, RoutedEventArgs e)
    {
        if (chkPrikup.IsChecked == true || chkDostava.IsChecked == true)
        {
            pnlAdresa.Visibility = Visibility.Visible;
        }
        else
        {
            pnlAdresa.Visibility = Visibility.Collapsed;
            txtAdresa.Text = string.Empty;
        }
    }

    private async void UcitajUsluge()
    {
        try
        {
            var usluge = await _httpClient.GetFromJsonAsync<List<UslugaPeglanja>>("api/usluge");
            icUsluge.ItemsSource = usluge;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Greška pri učitavanju usluga: {ex.Message}");
        }
    }

    private async void BtnSpremi_Click(object sender, RoutedEventArgs e)
    {
        var odabraneStavke = new List<StavkaNarudzbe>();

        foreach (var item in icUsluge.Items)
        {
            var container = icUsluge.ItemContainerGenerator.ContainerFromItem(item) as FrameworkElement;
            if (container == null) continue;

            var checkBox = FindVisualChild<CheckBox>(container);
            var textBox = FindVisualChild<TextBox>(container);

            if (checkBox != null && checkBox.IsChecked == true)
            {
                var usluga = item as UslugaPeglanja;
                int kolicina = 1;

                if (textBox != null && int.TryParse(textBox.Text, out int parsedKolicina))
                    kolicina = parsedKolicina;

                if (usluga != null)
                {
                    odabraneStavke.Add(new StavkaNarudzbe
                    {
                        UslugaId = usluga.Id,
                        Kolicina = kolicina,
                        CijenaUTrenutkuNarudzbe = usluga.Cijena
                    });
                }
            }
        }

        if (odabraneStavke.Count == 0)
        {
            MessageBox.Show("Molimo odaberite barem jednu uslugu!");
            return;
        }

        var novaNarudzba = new Narudzba
        {
            KlijentEmail = UserSession.TrenutniEmail,
            DatumNarudzbe = DateTime.Now,
            TerminDostave = DateTime.Now.AddDays(2),
            Adresa = pnlAdresa.Visibility == Visibility.Visible ? txtAdresa.Text : "N/A",
            PotrebnaDostava = chkDostava.IsChecked ?? false,
            Stavke = odabraneStavke
        };

        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/narudzbe", novaNarudzba);
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Narudžba uspješno kreirana!");
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                MessageBox.Show($"Greška pri slanju: {error}");
            }
        }
        catch (Exception ex) { MessageBox.Show(ex.Message); }
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

            if (checkBox != null && checkBox.IsChecked == true)
            {
                var usluga = item as UslugaPeglanja;
                int kolicina = 1;

                if (textBox != null && int.TryParse(textBox.Text, out int parsedKolicina))
                    kolicina = parsedKolicina;

                if (usluga != null)
                    ukupno += (usluga.Cijena * kolicina);
            }
        }

        txtUkupnaCijena.Text = ukupno.ToString("C");
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