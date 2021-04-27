using Bolnica.Model.Korisnici;
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
        public FormObavestenja()
        {
            InitializeComponent();
            DataContext = this;
            FileStorageObavestenja storage = new FileStorageObavestenja();
            List<Obavestenje> obavestenja = storage.GetAll();
            List<Obavestenje> obavestenjaZaPrikaz = new List<Obavestenje>();
            foreach(Obavestenje o in obavestenja)
            {
                foreach (string k in o.KorisnickaImena)
                {
                    if (k == "upravnik")
                    {
                        obavestenjaZaPrikaz.Add(o);
                        break;
                    }
                }
            }
            lvDataBinding.ItemsSource = obavestenjaZaPrikaz;
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
