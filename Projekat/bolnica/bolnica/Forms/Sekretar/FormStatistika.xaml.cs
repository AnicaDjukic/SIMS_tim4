using Bolnica.Sekretar;
using Model.Korisnici;
using Model.Pacijenti;
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
    /// Interaction logic for FormStatistika.xaml
    /// </summary>
    public partial class FormStatistika : Window
    {
        public FormStatistika()
        {
            InitializeComponent();
        }

        private void Button_Click_Pacijenti(object sender, RoutedEventArgs e)
        {
            var s = new FormSekretar();
            s.Show();
            this.Close();
        }

        private void Button_Click_Pregledi(object sender, RoutedEventArgs e)
        {
            var s = new FormPregledi();
            s.Show();
            this.Close();
        }

        private void Button_Click_Obavestenja(object sender, RoutedEventArgs e)
        {
            var s = new FormObavestenja();
            s.Show();
            this.Close();
        }

        private void Button_Click_Lekari(object sender, RoutedEventArgs e)
        {
            var s = new FormLekari();
            s.Show();
            this.Close();
        }
    }
}
