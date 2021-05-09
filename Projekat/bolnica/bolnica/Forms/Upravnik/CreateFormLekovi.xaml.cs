using bolnica.Forms;
using Bolnica.Model.Korisnici;
using Bolnica.Model.Pregledi;
using Model.Korisnici;
using Model.Pregledi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace Bolnica.Forms.Upravnik
{
    /// <summary>
    /// Interaction logic for CreateFormLekovi.xaml
    /// </summary>
    public partial class CreateFormLekovi : Window
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private int id;
        private string naziv;
        private int kolicinaUMg;
        private string proizvodjac;
        private int zalihe;
        private Lek lek;
        private FileStorageLek storage = new FileStorageLek();
        public static ObservableCollection<Sastojak> Sastojci
        {
            get;
            set;
        }

        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                if (value != id)
                {
                    id = value;
                    OnPropertyChanged("Id");
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

        public int KolicinaUMg
        {
            get
            {
                return kolicinaUMg;
            }
            set
            {
                if (value != kolicinaUMg)
                {
                    kolicinaUMg = value;
                    OnPropertyChanged("KolicinaUMg");
                }
            }
        }

        public string Proizvodjac
        {
            get
            {
                return proizvodjac;
            }
            set
            {
                if (value != proizvodjac)
                {
                    proizvodjac = value;
                    OnPropertyChanged("Proizvodjac");
                }
            }
        }

        public int Zalihe
        {
            get
            {
                return zalihe;
            }
            set
            {
                if (value != zalihe)
                {
                    zalihe = value;
                    OnPropertyChanged("Zalihe");
                }
            }
        }
        public CreateFormLekovi(Lek lekZaIzmenu)
        {
            InitializeComponent();
            DataContext = this;
            Sastojci = new ObservableCollection<Sastojak>();
            if (FormUpravnik.clickedDodaj)
            {
                lblZalihe.Visibility = Visibility.Hidden;
                txtZalihe.Visibility = Visibility.Hidden;
            }
            lek = lekZaIzmenu;
        }

        private void Button_Click_Validacija(object sender, RoutedEventArgs e)
        {
            lek.Id = Id;
            List<Lek> sviLekovi = storage.GetAll();
            bool postoji = false;
            foreach (Lek l in sviLekovi)
            {
                if (l.Id == lek.Id)
                {
                    postoji = true;
                    break;
                }
            }
            if (!postoji || !FormUpravnik.clickedDodaj)
            {
                if (!FormUpravnik.clickedDodaj)
                {
                    storage.Delete(lek);
                    foreach(Lek l in FormUpravnik.Lekovi)
                    {
                        if(lek.Id == l.Id)
                        {
                            FormUpravnik.Lekovi.Remove(l);
                            break;
                        }
                    }
                }
                lek.Naziv = Naziv;
                lek.KolicinaUMg = KolicinaUMg;
                lek.Proizvodjac = Proizvodjac;
                if (FormUpravnik.clickedDodaj || lek.Status == StatusLeka.odbijen)
                {
                    lek.Status = StatusLeka.cekaValidaciju;
                    lek.Zalihe = 0;
                }
                else
                {
                    lek.Zalihe = zalihe;
                }
                storage.Save(lek);
                FormUpravnik.Lekovi.Add(lek);
                FileStorageObavestenja storageObavestenja = new FileStorageObavestenja();
                List<Obavestenje> obavestenja = storageObavestenja.GetAll();
                int maxId = 0;
                foreach (Obavestenje o in obavestenja)
                {
                    if (maxId < o.Id)
                        maxId = o.Id;
                }
                if (lek.Status != StatusLeka.odobren)
                {
                    Obavestenje obavestenje = new Obavestenje { Id = maxId + 1, Naslov = "Lek za validaciju", Datum = DateTime.Now.Date, Sadrzaj = "Za lek \"" + lek.Naziv + "\" je potrebno izvršiti validaciju" };
                    Korisnik lekar = new Korisnik();
                    lekar.KorisnickoIme = "mico";
                    obavestenje.Korisnici.Add(lekar);
                    storageObavestenja.Save(obavestenje);
                }
                Close();
            }
            else
            {
                MessageBox.Show("Lek koji ima id: " + lek.Id + " već postoji!");
            }
        }

        private void Button_Click_Dodaj(object sender, RoutedEventArgs e)
        {
            FormSastojci s = new FormSastojci(lek);
            s.Show();
        }

        private void Button_Click_Ukloni(object sender, RoutedEventArgs e)
        {
            lek.Sastojak.Remove((Sastojak)dataGridSastojci.SelectedItem);
            Sastojci.Remove((Sastojak)dataGridSastojci.SelectedItem);
        }

        private void Button_Click_Odustani(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
