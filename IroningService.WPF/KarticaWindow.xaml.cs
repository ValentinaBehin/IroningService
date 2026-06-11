using System;
using System.Windows;

namespace IroningService.WPF
{
     public partial class KarticaWindow : Window
    {
        public KarticaWindow()
        {
            InitializeComponent();
        }

        private void BtnPotvrdi_Click(object sender, RoutedEventArgs e)
        {
           try 
            {
                MessageBox.Show("Plaćanje uspješno provedeno!", "Uspjeh", MessageBoxButton.OK, MessageBoxImage.Information);
                
                this.DialogResult = true; 
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Došlo je do greške prilikom obrade kartice: " + ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}