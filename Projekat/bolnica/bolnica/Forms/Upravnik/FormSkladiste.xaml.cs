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

        public static ObservableCollection<Zaliha> Zalihe
        {
            get;
            set;
        }

        FileStorageProstorija storage;

        private int kolicina;

        private Zaliha magacin;

        private Oprema novaOprema;

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
       
        public FormSkladiste(Oprema oprema)
        {
            InitializeComponent();
            this.DataContext = this;
            ProstorijeZaSkladistenje = new ObservableCollection<Prostorija>();
            storage = new FileStorageProstorija();
            List<Prostorija> prostorije = storage.GetAllProstorije();
            foreach(Prostorija p in prostorije)
            {
                if(!p.Obrisana)
                    ProstorijeZaSkladistenje.Add(p);
            }

            List<BolnickaSoba> bolnickeSobe = storage.GetAllBolnickeSobe();
            foreach (BolnickaSoba b in bolnickeSobe)
            {
                if(!b.Obrisana)
                    ProstorijeZaSkladistenje.Add(b);
            }

            Zalihe = new ObservableCollection<Zaliha>();
            novaOprema = oprema;
            int UkKolicnina = novaOprema.Kolicina;
            bool imaMagacin = false;
            foreach(string k in novaOprema.OpremaPoSobama.Keys)
            {
                Zaliha z = new Zaliha();
                z.Prostorija = k;
                z.Kolicina = novaOprema.OpremaPoSobama.GetValueOrDefault<string, int>(k);
                z.Oprema = novaOprema.Sifra;
                Zalihe.Add(z);
                UkKolicnina -= z.Kolicina;
                if(z.Prostorija == "magacin")
                {
                    imaMagacin = true;
                }
            }
            magacin = new Zaliha { Prostorija = "magacin", Kolicina = UkKolicnina};
            if(!imaMagacin)
            {
                Zalihe.Add(magacin);
            }
        }
        private void Button_Click_Prebaci(object sender, RoutedEventArgs e)
        {
            if(GridProstorije.SelectedCells.Count > 0)
            {
                if (kolicina != 0 && kolicina <= novaOprema.Kolicina)
                {
                    Prostorija row = (Prostorija)GridProstorije.SelectedItem;
                    for(int i = 0; i < ProstorijeZaSkladistenje.Count; i++)
                    {
                        if (ProstorijeZaSkladistenje[i].BrojProstorije == row.BrojProstorije)
                            ProstorijeZaSkladistenje.Remove(ProstorijeZaSkladistenje[i]);
                    }
                    Zaliha zaliha = new Zaliha();
                    zaliha.Prostorija = row.BrojProstorije.ToString();
                    zaliha.Kolicina = kolicina;
                    zaliha.Oprema = novaOprema.Sifra;
                    Zalihe.Add(zaliha);
                    Zalihe.Remove(magacin);
                    magacin.Kolicina -= kolicina;
                    Zalihe.Add(magacin);
                } else
                {
                    MessageBox.Show("Unesite validnu količinu. Količina mora biti veća od 0 i manja ili jednaka od ukupne količine opreme.");
                }
            }
        }

        private void Button_Click_Vrati(object sender, RoutedEventArgs e)
        {
            if (GridZalihe.SelectedCells.Count > 0 && ((Zaliha)GridZalihe.SelectedItem).Prostorija != "magacin")
            {
                Zaliha row = (Zaliha)GridZalihe.SelectedItem;
                for (int i = 0; i < Zalihe.Count; i++)
                {
                    if (Zalihe[i].Prostorija == row.Prostorija)
                        Zalihe.Remove(Zalihe[i]);
                }

                List<Prostorija> prostorije = storage.GetAllProstorije();
                bool found = false;
                foreach (Prostorija p in prostorije)
                {
                    if(p.BrojProstorije.ToString() == row.Prostorija)
                    {
                        ProstorijeZaSkladistenje.Add(p);
                        found = true;
                    }
                }
                if (!found)
                {
                    List<BolnickaSoba> bolnickeSobe = storage.GetAllBolnickeSobe();
                    foreach (BolnickaSoba b in bolnickeSobe)
                    {
                        if (b.BrojProstorije.ToString() == row.Prostorija)
                        {
                            ProstorijeZaSkladistenje.Add(b);
                        }
                    }
                }

                Zalihe.Remove(magacin);
                //Kolicina = row.Kolicina;
                magacin.Kolicina += row.Kolicina;
                Zalihe.Add(magacin);
            }
        }

        public void Button_Click_Potvrdi(object sender, RoutedEventArgs e)
        {
            for(int i = 0; i < Zalihe.Count; i++)
            {
                novaOprema.OpremaPoSobama.Add(Zalihe[i].Prostorija, Zalihe[i].Kolicina);
            }
            Close();
        }

        public void Button_Click_Odustani(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
