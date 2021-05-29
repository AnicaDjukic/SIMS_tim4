using Bolnica.Model.Korisnici;
using Bolnica.Services.Korisnici;
using Model.Korisnici;
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

namespace Bolnica.Forms.Upravnik
{
    /// <summary>
    /// Interaction logic for FormObavestenja.xaml
    /// </summary>
    public partial class FormObavestenja : Window
    {
        private ServiceObavestenje serviceObavestenje = new ServiceObavestenje();
        public FormObavestenja()
        {
            InitializeComponent();
            DataContext = this;
            
            List<Obavestenje> obavestenjaZaPrikaz = NadjiObavestenjaZaPrikaz();
            
            SortirajObavestenjaPoDatumu(obavestenjaZaPrikaz);

            lvDataBinding.ItemsSource = obavestenjaZaPrikaz;
        }

        private void SortirajObavestenjaPoDatumu(List<Obavestenje> obavestenjaZaPrikaz)
        {
            serviceObavestenje.SortirajObavestenja(obavestenjaZaPrikaz);
        }

        private List<Obavestenje> NadjiObavestenjaZaPrikaz()
        {
            return serviceObavestenje.NadjiObavestenjaKorisnika("upravnik");
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ViewFormObavestenje viewForm = new ViewFormObavestenje(((Obavestenje)lvDataBinding.SelectedItem).Id);
            viewForm.Show();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
