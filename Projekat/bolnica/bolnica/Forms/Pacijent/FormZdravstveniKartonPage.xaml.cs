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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Bolnica.Forms
{
    /// <summary>
    /// Interaction logic for FormZdravstveniKartonPage.xaml
    /// </summary>
    public partial class FormZdravstveniKartonPage : Page
    {
        public Pacijent Pacijent { get; set; }
        public FormZdravstveniKartonPage(Pacijent trenutniPacijent)
        {
            InitializeComponent();
            this.DataContext = this;
            Pacijent = trenutniPacijent;
        }

        private void Button_Click_Istorija_Pregleda(object sender, RoutedEventArgs e)
        {
            FormPacijentWeb.Forma.Pocetna.Content = new FormIstorijaPregledaPage(Pacijent);
        }
    }
}
