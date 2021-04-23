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
    public partial class FormSkladiste : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
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

        private FileStorageProstorija storageProstorija;
        public static FileStorageZaliha storageZaliha;

        private int kolicina;

        private Zaliha magacin;

        private Oprema opremaZaSkladistenje;

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
            opremaZaSkladistenje = oprema;
            storageZaliha = new FileStorageZaliha();
            List<Zaliha> zalihe = storageZaliha.GetAll();
            ProstorijeZaSkladistenje = new ObservableCollection<Prostorija>();
            storageProstorija = new FileStorageProstorija();
            List<Prostorija> prostorije = storageProstorija.GetAllProstorije();
            foreach (Prostorija p in prostorije)
            {
                if (!p.Obrisana)
                    ProstorijeZaSkladistenje.Add(p);
               
            }

            List<BolnickaSoba> bolnickeSobe = storageProstorija.GetAllBolnickeSobe();
            foreach (BolnickaSoba b in bolnickeSobe)
            {
                if (!b.Obrisana)
                    ProstorijeZaSkladistenje.Add(b);
            }

            Zalihe = new ObservableCollection<Zaliha>();
            //Zalihe.Clear();
            if(!imaMagacin())
            {
                Prostorija prostorijaMagacin = new Prostorija { BrojProstorije = "magacin" };
                magacin = new Zaliha { Prostorija = prostorijaMagacin , Oprema = opremaZaSkladistenje, Kolicina = opremaZaSkladistenje.Kolicina };
                Zalihe.Add(magacin);
                
            } else
            {
                foreach (Zaliha z in zalihe)
                {
                    if (z.Oprema.Sifra == opremaZaSkladistenje.Sifra)
                    {
                        if (z.Prostorija.BrojProstorije == "magacin")
                            magacin = z;
                        Zalihe.Add(z);
                    }
                }
            }
        }

        private bool imaMagacin()
        {
            bool imaMagacin = false;
            List<Zaliha> zalihe = storageZaliha.GetAll();
            if (zalihe != null)
            {
                foreach (Zaliha z in zalihe)
                {
                    if (z.Oprema.Sifra == opremaZaSkladistenje.Sifra)
                    {
                        imaMagacin = true;
                        break;
                    }

                }
            }

            /*if(!imaMagacin)
            {
                if (Zalihe != null)
                    imaMagacin = true;
            }*/
            return imaMagacin;
        }
        private void Button_Click_Prebaci(object sender, RoutedEventArgs e)
        {
            if(GridProstorije.SelectedCells.Count > 0)
            {
                if (kolicina != 0 && kolicina <= opremaZaSkladistenje.Kolicina)
                {
                    Prostorija row = (Prostorija)GridProstorije.SelectedItem;
                    for(int i = 0; i < ProstorijeZaSkladistenje.Count; i++)
                    {
                        if (ProstorijeZaSkladistenje[i].BrojProstorije == row.BrojProstorije)
                            ProstorijeZaSkladistenje.Remove(ProstorijeZaSkladistenje[i]);
                    }
                    Zaliha zaliha = new Zaliha();
                    zaliha.Prostorija = row;
                    zaliha.Kolicina = kolicina;
                    zaliha.Oprema = opremaZaSkladistenje;
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
            if (GridZalihe.SelectedCells.Count > 0 && ((Zaliha)GridZalihe.SelectedItem).Prostorija.BrojProstorije != "magacin")
            {
                Zaliha row = (Zaliha)GridZalihe.SelectedItem;
                for (int i = 0; i < Zalihe.Count; i++)
                {
                    if (Zalihe[i].Prostorija == row.Prostorija)
                    {
                        Zalihe.Remove(Zalihe[i]);
                    }
                }

                List<Zaliha> zalihe = storageZaliha.GetAll();
                foreach (Zaliha z in zalihe)
                {
                    if(z.Prostorija.BrojProstorije == row.Prostorija.BrojProstorije)
                    {
                        storageZaliha.Delete(z);
                    }
                }

                List<Prostorija> prostorije = storageProstorija.GetAllProstorije();
                bool found = false;
                foreach (Prostorija p in prostorije)
                {
                    if(p.BrojProstorije == row.Prostorija.BrojProstorije)
                    {
                        ProstorijeZaSkladistenje.Add(p);
                        found = true;
                    }
                }
                if (!found)
                {
                    List<BolnickaSoba> bolnickeSobe = storageProstorija.GetAllBolnickeSobe();
                    foreach (BolnickaSoba b in bolnickeSobe)
                    {
                        if (b.BrojProstorije == row.Prostorija.BrojProstorije)
                        {
                            ProstorijeZaSkladistenje.Add(b);
                        }
                    }
                }

                Zalihe.Remove(magacin);
                Kolicina = row.Kolicina;
                magacin.Kolicina += row.Kolicina;
                Zalihe.Add(magacin);
            }
        }

        public void Button_Click_Potvrdi(object sender, RoutedEventArgs e)
        {
            List<Zaliha> zalihe = storageZaliha.GetAll();
            if(zalihe != null)
            {
                foreach (Zaliha z in zalihe)
                {
                    if (z.Oprema.Sifra == opremaZaSkladistenje.Sifra)
                        storageZaliha.Delete(z);
                }
            }

            foreach (Zaliha z in Zalihe)
            {
                z.Oprema = opremaZaSkladistenje;
                FormSkladiste.storageZaliha.Save(z);
            }

            Close();
        }

        public void Button_Click_Odustani(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
