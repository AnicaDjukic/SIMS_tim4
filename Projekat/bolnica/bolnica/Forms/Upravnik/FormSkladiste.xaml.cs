using Bolnica.Model.Prostorije;
using Model.Prostorije;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

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
        private FileStorageBuducaZaliha storageBuducaZaliha;
        private FileStorageZaliha storageZaliha;

        private Zaliha magacin;

        private Oprema opremaZaSkladistenje;

        private int kolicinaZaPremestanje;

        public int KolicinaZaPremestanje
        {
            get
            {
                return kolicinaZaPremestanje;
            }
            set
            {
                if (value != kolicinaZaPremestanje)
                {
                    kolicinaZaPremestanje = value;
                    OnPropertyChanged("KolicinaZaPremestanje");
                }
            }
        }

        public FormSkladiste(Oprema oprema)
        {
            InitializeComponent();
            this.DataContext = this;
            opremaZaSkladistenje = oprema;
            List<Zaliha> zaliheOpreme = FindZaliheOpreme(opremaZaSkladistenje);
            InitializeZalihe(zaliheOpreme);
            InitializeProstorijeZaSkladistenje(zaliheOpreme);
        }

        private List<Zaliha> FindZaliheOpreme(Oprema oprema)
        {
            // uzmi buduce zalihe i upisi ih u fajl sa zalihama
            List<Zaliha> zaliheOpreme = new List<Zaliha>();
            List<BuducaZaliha> buduceZalihe = FindBuduceZalihe();
            if (buduceZalihe.Count > 0)
            {
                DeleteOldZalihe();
                SaveBuduceZaliheToStorageZalihe(buduceZalihe);
            }
            // ucitaj zalihe iz fajla
            zaliheOpreme = LoadZaliheFromFile(oprema);

            return zaliheOpreme;
        }

        private List<BuducaZaliha> FindBuduceZalihe()
        {
            storageBuducaZaliha = new FileStorageBuducaZaliha();
            List<BuducaZaliha> buduceZalihe = new List<BuducaZaliha>();
            if (storageBuducaZaliha.GetAll() != null)
            {
                foreach (BuducaZaliha bz in storageBuducaZaliha.GetAll())
                {
                    if (bz.Oprema.Sifra == opremaZaSkladistenje.Sifra && bz.Datum <= DateTime.Now.Date)
                    {
                        buduceZalihe.Add(bz);
                        storageBuducaZaliha.Delete(bz);
                    }
                }
            }

            return buduceZalihe;
        }

        private void DeleteOldZalihe()
        {
            storageZaliha = new FileStorageZaliha();
            foreach (Zaliha z in storageZaliha.GetAll())
            {
                if (z.Oprema.Sifra == opremaZaSkladistenje.Sifra)
                    storageZaliha.Delete(z);
            }
        }

        private void SaveBuduceZaliheToStorageZalihe(List<BuducaZaliha> buduceZalihe)
        {
            foreach (BuducaZaliha bz in buduceZalihe)
            {
                Zaliha z = new Zaliha { Kolicina = bz.Kolicina};
                z.Prostorija = bz.Prostorija;
                z.Oprema = bz.Oprema;
                storageZaliha.Save(z);
            }
        }

        private List<Zaliha> LoadZaliheFromFile(Oprema oprema)
        {
            List<Zaliha> zaliheOpreme = new List<Zaliha>();
            storageZaliha = new FileStorageZaliha();
            foreach (Zaliha z in storageZaliha.GetAll())
            {
                if (oprema.Sifra == z.Oprema.Sifra)
                    zaliheOpreme.Add(z);
            }

            return zaliheOpreme;
        }

        private void InitializeZalihe(List<Zaliha> zaliheOpreme)
        {
            Zalihe = new ObservableCollection<Zaliha>();

            if (!ImaMagacin())
            {
                magacin = CreateMagacin();
                Zalihe.Add(magacin);
            }
            else
            {
                AddZaliheOpremeToZalihe(zaliheOpreme);
            }
        }

        private bool ImaMagacin()
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

            return imaMagacin;
        }

        private Zaliha CreateMagacin()
        {
            Prostorija prostorijaMagacin = new Prostorija { BrojProstorije = "magacin" };
            magacin = new Zaliha { Prostorija = prostorijaMagacin, Oprema = opremaZaSkladistenje, Kolicina = opremaZaSkladistenje.Kolicina };
            magacin.Oprema = opremaZaSkladistenje;
            return magacin;
        }

        private void AddZaliheOpremeToZalihe(List<Zaliha> zaliheOpreme)
        {
            int kolicinaUMagacinu = opremaZaSkladistenje.Kolicina;
            foreach (Zaliha z in zaliheOpreme)
            {
                if (z.Prostorija.BrojProstorije == "magacin")
                {
                    magacin = z;
                }
                else
                {
                    kolicinaUMagacinu -= z.Kolicina;
                    Zalihe.Add(z);
                }
            }

            magacin.Kolicina = kolicinaUMagacinu;
            Zalihe.Add(magacin);
        }

        private void InitializeProstorijeZaSkladistenje(List<Zaliha> zaliheOpreme)
        {
            ProstorijeZaSkladistenje = new ObservableCollection<Prostorija>();
            storageProstorija = new FileStorageProstorija();
            if (zaliheOpreme.Count == 0)
            {
                AddAllProstorije();
                AddAllBolnickeSobe();
            }   
            else
            {
                AddFreeProstorije(zaliheOpreme);    // Dodaj u prikaz prostorije u kojima ova oprema jos nije smestena
                AddFreeBolnickeSobe(zaliheOpreme);  // Dodaj u prikaz bolnicke sobe u kojima ova oprema jos nije smestena 
            }
        }

        private void AddAllProstorije()
        {
            List<Prostorija> prostorije = storageProstorija.GetAllProstorije();
            foreach (Prostorija p in prostorije)
            {
                if (!p.Obrisana)
                    ProstorijeZaSkladistenje.Add(p);
            }
        }

        private void AddAllBolnickeSobe()
        {
            List<BolnickaSoba> bolnickeSobe = storageProstorija.GetAllBolnickeSobe();
            foreach (BolnickaSoba b in bolnickeSobe)
            {
                if (!b.Obrisana)
                    ProstorijeZaSkladistenje.Add(b);
            }
        }

        private void AddFreeProstorije(List<Zaliha> zaliheOpreme)
        {
            List<Prostorija> prostorije = storageProstorija.GetAllProstorije();
            List<Prostorija> korisceneProstorije = new List<Prostorija>();
            foreach (Zaliha z in zaliheOpreme)
            {
                foreach (Prostorija k in korisceneProstorije)
                {
                    prostorije.Remove(k);
                }

                foreach (Prostorija p in prostorije)
                {
                    if (z.Prostorija.BrojProstorije != p.BrojProstorije && !p.Obrisana)
                    {
                        ProstorijeZaSkladistenje.Remove(p);
                        ProstorijeZaSkladistenje.Add(p);
                    }
                    else
                    {
                        ProstorijeZaSkladistenje.Remove(p);
                        korisceneProstorije.Add(p);
                    }
                }
            }
        }

        private void AddFreeBolnickeSobe(List<Zaliha> zaliheOpreme)
        {
            List<BolnickaSoba> bolnickeSobe = storageProstorija.GetAllBolnickeSobe();
            List<BolnickaSoba> korisceneBolnicke = new List<BolnickaSoba>();
            foreach (Zaliha z in zaliheOpreme)
            {
                foreach (BolnickaSoba k in korisceneBolnicke)
                {
                    bolnickeSobe.Remove(k);
                }

                foreach (BolnickaSoba b in bolnickeSobe)
                {
                    if (z.Prostorija.BrojProstorije != b.BrojProstorije && !b.Obrisana)
                    {
                        ProstorijeZaSkladistenje.Remove(b);
                        ProstorijeZaSkladistenje.Add(b);
                    }
                    else
                    {
                        ProstorijeZaSkladistenje.Remove(b);
                        korisceneBolnicke.Add(b);
                    }
                }
            }
        }

        private void Button_Click_Prebaci(object sender, RoutedEventArgs e)
        {
            if (GridProstorije.SelectedCells.Count > 0)
            {
                if (ValidnaKolicina())
                {
                    Prostorija row = (Prostorija)GridProstorije.SelectedItem;
                    RemoveProstorija(row);      // ukloni izabranu prostoriju iz prikaza prostorija

                    Zaliha zaliha = new Zaliha { Prostorija = row, Oprema = opremaZaSkladistenje, Kolicina = kolicinaZaPremestanje };
                    Zalihe.Add(zaliha);
                    ReduceAmountInMagacin();
                }
                else
                {
                    MessageBox.Show("Unesite validnu količinu. Količina mora biti veća od 0 i manja ili jednaka od količine opreme u magacinu.");
                }
            }
        }

        private bool ValidnaKolicina()
        {
            return kolicinaZaPremestanje != 0 && kolicinaZaPremestanje <= magacin.Kolicina;
        }

        private void RemoveProstorija(Prostorija prostorija)
        {
            for (int i = 0; i < ProstorijeZaSkladistenje.Count; i++)
            {
                if (ProstorijeZaSkladistenje[i].BrojProstorije == prostorija.BrojProstorije)
                    ProstorijeZaSkladistenje.Remove(ProstorijeZaSkladistenje[i]);
            }
        }

        private void ReduceAmountInMagacin()
        {
            Zalihe.Remove(magacin);
            magacin.Kolicina -= kolicinaZaPremestanje;
            Zalihe.Add(magacin);
        }

        private void Button_Click_Vrati(object sender, RoutedEventArgs e)
        {
            if (GridZalihe.SelectedCells.Count > 0 && ((Zaliha)GridZalihe.SelectedItem).Prostorija.BrojProstorije != "magacin")
            {
                Zaliha row = (Zaliha)GridZalihe.SelectedItem;
                RemoveZalihaFromZalihe(row);                // ukloni izabranu zalihu iz prikaza zaliha

                List<Prostorija> prostorije = storageProstorija.GetAllProstorije();
                ProstorijeZaSkladistenje.Add(row.Prostorija);

                Zalihe.Remove(magacin);
                IncreaseAmountInMagacin(row);
            }
        }

        private static void RemoveZalihaFromZalihe(Zaliha zaliha)
        {
            for (int i = 0; i < Zalihe.Count; i++)
            {
                if (Zalihe[i].Prostorija.BrojProstorije == zaliha.Prostorija.BrojProstorije)
                {
                    Zalihe.Remove(Zalihe[i]);
                }
            }
        }

        private void IncreaseAmountInMagacin(Zaliha zaliha)
        {
            KolicinaZaPremestanje = zaliha.Kolicina;
            magacin.Kolicina += zaliha.Kolicina;
            Zalihe.Add(magacin);
        }

        public void Button_Click_Potvrdi(object sender, RoutedEventArgs e)
        {
            if (opremaZaSkladistenje.TipOpreme != TipOpreme.staticka)
            {
                DeleteOldZalihe();
                foreach (Zaliha z in Zalihe)
                    storageZaliha.Save(z);
            }
            else
            {
                FormDatumPremestanja formPremestanje = new FormDatumPremestanja();
                formPremestanje.datePickerDatum.DisplayDateStart = DateTime.Now;
                formPremestanje.Show();
            }

            Close();
        }

        public void Button_Click_Odustani(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
