using System.Windows;
using IroningService.Domena.Entiteti;

namespace IroningService.WPF;

public partial class DetaljiNarudzbeWindow : Window
{
    public DetaljiNarudzbeWindow(Narudzba narudzba)
    {
        InitializeComponent();
        // Postavljamo DataContext na primljeni objekt narudžbe
        this.DataContext = narudzba;
    }

    private void BtnZatvori_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
}