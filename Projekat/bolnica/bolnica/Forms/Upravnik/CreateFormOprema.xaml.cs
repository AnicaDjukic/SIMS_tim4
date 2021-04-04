using bolnica.Forms;
using Bolnica.Model.Prostorije;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static Bolnica.Forms.Upravnik.FormSkladiste;

namespace Bolnica.Forms.Upravnik
{
    /// <summary>
    /// Interaction logic for CreateFormOprema.xaml
    /// </summary>
    public partial class CreateFormOprema : Window
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private string sifra;
        private string naziv;
        private int kolicina;
        private FileStorageOprema storage;
        private bool skladistiClicked;

        public string Sifra
        {
            get
            {
                return sifra;
            }
            set
            {
                if (value != sifra)
                {
                    sifra = value;
                    OnPropertyChanged("Sifra");
                }
            }
        }

        public string Naziv
        {
            get
            {
                return naziv;
            }
            set
            {
                if (value != naziv)
                {
                    naziv = value;
                    OnPropertyChanged("Naziv");
                }
            }
        }

        public int Kolicina
        {
            get
            {
                return kolicina;
            }
            set
            {
                if (value != kolicina)
                {
                    kolicina = value;
                    OnPropertyChanged("Kolicina");
                }
            }
        }

        public CreateFormOprema()
        {
            InitializeComponent();
            storage = new FileStorageOprema();
            this.DataContext = this;
            skladistiClicked = false;
        }

        private void Button_Click_Potvrdi(object sender, RoutedEventArgs e)
        {
            bool postoji = false;
            for(int i = 0; i < FormUpravnik.Oprema.Count; i++)
            {
                if(FormUpravnik.Oprema[i].Sifra == sifra)
                {
                    MessageBox.Show("Oprema sa tom sifrom vec postoji!");
                    postoji = true;
                    break;
                } 
            }
            if(!postoji)
            {
                Oprema oprema = new Oprema { Sifra = sifra, Naziv = naziv, Kolicina = kolicina };
                if (ComboTipOpreme.SelectedIndex == 0)
                    oprema.TipOpreme = TipOpreme.staticka;
                else
                    oprema.TipOpreme = TipOpreme.dinamicka;

                if(skladistiClicked)
                {
                    foreach(Skladiste s in FormSkladiste.Skladista)
                    {
                        oprema.OpremaPoSobama.Add(s.Prostorija, s.Kolicina);
                    }
                }
                FormUpravnik.Oprema.Add(oprema);
                storage.Save(oprema);
                Close();
            }
            
        }

        private void Button_Click_Skladisti(object sender, RoutedEventArgs e)
        {
            skladistiClicked = true;
            var s = new FormSkladiste(kolicina);
            s.Show();
        }
    }
}
