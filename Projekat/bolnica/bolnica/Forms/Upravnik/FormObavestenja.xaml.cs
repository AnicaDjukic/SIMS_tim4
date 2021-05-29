using Bolnica.Model.Korisnici;
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
        public FormObavestenja()
        {
            InitializeComponent();
            DataContext = this;
            FileRepositoryObavestenje storage = new FileRepositoryObavestenje();
            List<Obavestenje> obavestenja = storage.GetAll();
            List<Obavestenje> obavestenjaZaPrikaz = new List<Obavestenje>();
            foreach(Obavestenje o in obavestenja)
            {
                foreach (Korisnik k in o.Korisnici)
                {
                    if (k.KorisnickoIme == "upravnik")
                    {
                        o.Sadrzaj = o.Sadrzaj.Split(",")[0] + "...";
                        obavestenjaZaPrikaz.Add(o);
                        break;
                    }
                }
            }
            Obavestenje temp = new Obavestenje();
            for (int j = 0; j <= obavestenjaZaPrikaz.Count - 2; j++)
            {
                for (int i = 0; i <= obavestenjaZaPrikaz.Count - 2; i++)
                {
                    if (obavestenjaZaPrikaz[i].Datum < obavestenjaZaPrikaz[i + 1].Datum)
                    {
                        temp = obavestenjaZaPrikaz[i + 1];
                        obavestenjaZaPrikaz[i + 1] = obavestenjaZaPrikaz[i];
                        obavestenjaZaPrikaz[i] = temp;
                    }
                }
            }
            lvDataBinding.ItemsSource = obavestenjaZaPrikaz;
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
