using bolnica.Forms;
using Bolnica.Model.Prostorije;
using Model.Prostorije;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Bolnica.Forms.Upravnik
{
    /// <summary>
    /// Interaction logic for FormSkladiste.xaml
    /// </summary>
    public partial class FormSkladiste : Window
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public ObservableCollection<Prostorija> ProstorijeZaSkladistenje 
        {
            get;
            set;
        }
        public class Skladiste
        {
            public string Prostorija { get; set; }
            public int Kolicina { get; set; }
        }

        public static ObservableCollection<Skladiste> Skladista
        {
            get;
            set;
        }

        FileStorageProstorija storage;

        private int kolicina;

        private Oprema oprema;
        private Skladiste magacin;

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
       
        public FormSkladiste(int UkKolicina)
        {
            InitializeComponent();
            this.DataContext = this;
            ProstorijeZaSkladistenje = new ObservableCollection<Prostorija>();
            storage = new FileStorageProstorija();
            List<Prostorija> prostorije = storage.GetAllProstorije();
            foreach(Prostorija p in prostorije)
            {
                ProstorijeZaSkladistenje.Add(p);
            }
            List<BolnickaSoba> bolnickeSobe = storage.GetAllBolnickeSobe();
            foreach (BolnickaSoba b in bolnickeSobe)
            {
                ProstorijeZaSkladistenje.Add(b);
            }
            Skladista = new ObservableCollection<Skladiste>();
            magacin = new Skladiste { Prostorija = "magacin", Kolicina = UkKolicina};
            Skladista.Add(magacin);
        }
        private void Button_Click_Prebaci(object sender, RoutedEventArgs e)
        {
            if(GridSkladiste.SelectedCells.Count > 0)
            {
                Prostorija row = (Prostorija)GridSkladiste.SelectedItem;
                Skladiste skladiste = new Skladiste();
                skladiste.Prostorija = row.BrojProstorije.ToString();
                skladiste.Kolicina = kolicina;
                Skladista.Add(skladiste);
                
                Skladista.Remove(magacin);
                magacin.Kolicina -= kolicina;
                Skladista.Add(magacin);
            }
        }

        public void Button_Click_Potvrdi(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
