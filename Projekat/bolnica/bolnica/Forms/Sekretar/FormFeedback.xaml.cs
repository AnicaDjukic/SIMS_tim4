using Bolnica.Model.Korisnici;
using Bolnica.Repository.Korisnici;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Bolnica.Forms.Sekretar
{
    /// <summary>
    /// Interaction logic for FormFeedback.xaml
    /// </summary>
    public partial class FormFeedback : Window
    {
        private FileRepositoryOcenaAplikacije skladisteOcenaAplikacije;
        public FormFeedback()
        {
            InitializeComponent();
            skladisteOcenaAplikacije = new FileRepositoryOcenaAplikacije();
        }

        private void Zatvori(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void PosaljiFeedback(object sender, RoutedEventArgs e)
        {
            int ocena;
            if ((bool)rb1.IsChecked)
                ocena = 1;
            else if ((bool)rb2.IsChecked)
                ocena = 2;
            else if ((bool)rb3.IsChecked)
                ocena = 3;
            else if ((bool)rb4.IsChecked)
                ocena = 4;
            else
                ocena = 5;

            string komentar = txtText.Text;
            OcenaAplikacije ocenaAplikacije = new OcenaAplikacije { BrojnaVrednost = ocena, Komentar = komentar };
            skladisteOcenaAplikacije.Save(ocenaAplikacije);
            Close();
            MessageBox.Show("Povratna informacija poslata", "Povratna informacija", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
