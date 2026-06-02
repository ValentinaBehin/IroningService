using System; // Dodano zbog Uri
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;
using IroningService.Domena.Entiteti;

namespace IroningService.WPF;

public partial class RegistracijaWindow : Window
{
    private readonly HttpClient _httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:5038/") };

    public RegistracijaWindow() => InitializeComponent();

    private async void BtnRegistriraj_Click(object sender, RoutedEventArgs e)
    {
        var noviKorisnik = new Korisnik
        {
            Ime = txtIme.Text,
            Prezime = txtPrezime.Text,
            Email = txtEmail.Text,
            Lozinka = txtLozinka.Password
        };

        try 
        {
            var response = await _httpClient.PostAsJsonAsync("api/korisnici/registracija", noviKorisnik);

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Uspješno ste se registrirali! Sada se možete prijaviti.");
                // Ovo zatvara prozor registracije i vraća te na LoginWindow
                this.Close();
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                MessageBox.Show($"Greška pri registraciji: {error}");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Nije moguće povezati se s API-jem: {ex.Message}");
        }
    }
}