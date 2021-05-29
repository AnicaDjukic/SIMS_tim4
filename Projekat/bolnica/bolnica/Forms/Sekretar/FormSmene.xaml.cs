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

namespace Bolnica.Forms.Sekretar
{
    /// <summary>
    /// Interaction logic for FormSmene.xaml
    /// </summary>
    public partial class FormSmene : Window
    {
        private string jmbg;
        private FileRepositoryLekar storageLekara;
        private List<Lekar> sviLekari;
        public FormSmene(string jmbg)
        {
            InitializeComponent();
            this.jmbg = jmbg;
            storageLekara = new FileRepositoryLekar();
            sviLekari = storageLekara.GetAll();
        }

        private void Potvrdi(object sender, RoutedEventArgs e)
        {
            int selectedSmena = comboSmena.SelectedIndex;
            Smena smena = new Smena();
            if (selectedSmena == 0)
                smena = Smena.Prva;
            else if (selectedSmena == 1)
                smena = Smena.Druga;
            else if (selectedSmena == 2)
                smena = Smena.Treca;

            for (int i = 0; i < sviLekari.Count; i++)
                if (sviLekari[i].Jmbg == jmbg) 
                {
                    storageLekara.Delete(sviLekari[i]);
                    sviLekari[i].PostavljenaSmena = true;
                    sviLekari[i].Smena = smena;
                    storageLekara.Save(sviLekari[i]);
                    FormLekari.Lekari.Add(sviLekari[i]);
                    FormLekari.NoviLekari.RemoveAt(i);
                    break;
                }
            Close();
        }

        private void Zatvori(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
