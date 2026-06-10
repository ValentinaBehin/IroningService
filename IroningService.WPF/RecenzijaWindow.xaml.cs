using System;
using System.Net.Http;
using System.Net.Http.Json; 
using System.Windows; 
using IroningService.Domena.Entiteti;

namespace IroningService.WPF
{
    public partial class RecenzijaWindow : Window
    {
        private readonly int _narudzbaId;
        private readonly HttpClient _httpClient = new HttpClient();

        public RecenzijaWindow(int narudzbaId)
        {
            InitializeComponent();
            _narudzbaId = narudzbaId;
        }

        private async void BtnSpremi_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Provjera unosa
                if (!int.TryParse(txtOcjena.Text, out int ocjena))
                {
                    MessageBox.Show("Ocjena mora biti broj od 1 do 5.");
                    return;
                }

                var novaRecenzija = new {
                    NarudzbaId = _narudzbaId,
                    Ocjena = ocjena,
                    Komentar = txtKomentar.Text,
                    DatumRecenzije = DateTime.Now
                };

                // KORISTIMO PUNU PUTANJU
                string url = "http://localhost:5038/api/recenzije";
                
                var response = await _httpClient.PostAsJsonAsync(url, novaRecenzija);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Uspješno spremljeno!");
                    this.DialogResult = true;
                }
                else
                {
                    // Ovo će ti ispisati zašto API vraća 404
                    string error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"API greška ({response.StatusCode}): {error}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška pri spajanju na API: " + ex.Message);
            }
        }
    }
}