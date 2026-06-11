using System.Diagnostics;
using System.Windows;

namespace IroningService.WPF
{
    public partial class NaplataWindow : Window
    {
        public NaplataWindow() { InitializeComponent(); }

        private void BtnPlatiPayPal_Click(object sender, RoutedEventArgs e)
        {
            // Ovdje bi išao link na tvoj PayPal checkout proces
            string paypalUrl = "https://www.paypal.com/checkoutnow?item_name=Usluga+peglanja&amount=25.00";
            Process.Start(new ProcessStartInfo(paypalUrl) { UseShellExecute = true });
        }
        private void BtnPlatiKarticom_Click(object sender, RoutedEventArgs e)
{
    KarticaWindow karticaProzor = new KarticaWindow();
    if (karticaProzor.ShowDialog() == true)
    {
        this.DialogResult = true; 
        this.Close();
    }
}
        private void BtnOznaciPlaceno_Click(object sender, RoutedEventArgs e)
{
    
    MessageBox.Show("Narudžba je označena kao plaćena.", "Potvrda", MessageBoxButton.OK, MessageBoxImage.Information);
    this.DialogResult = true;
    this.Close();
}
    }
}