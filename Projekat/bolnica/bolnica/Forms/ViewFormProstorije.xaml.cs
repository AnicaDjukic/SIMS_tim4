using bolnica.Forms;
using Bolnica.Model.Prostorije;
using Model.Prostorije;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Bolnica.Forms
{
    /// <summary>
    /// Interaction logic for ViewFormProstorije.xaml
    /// </summary>
    public partial class ViewFormProstorije : Window
    {
        public static ObservableCollection<Oprema> OpremaSobe
        {
            get;
            set;
        }
        public ViewFormProstorije(int brojProstorije)
        {
            InitializeComponent();
            this.DataContext = this;
            OpremaSobe = new ObservableCollection<Oprema>();
            Oprema op = new Oprema();

            foreach (Oprema o in FormUpravnik.Oprema)
            {
                foreach(int key in o.OpremaPoSobama.Keys)
                {
                    if(brojProstorije == key)
                    {
                        op.Sifra = o.Sifra;
                        op.Naziv = o.Naziv;
                        op.TipOpreme = o.TipOpreme;
                        op.Kolicina = o.OpremaPoSobama.GetValueOrDefault<int, int>(key);
                        OpremaSobe.Add(op);
                    }
                }
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
