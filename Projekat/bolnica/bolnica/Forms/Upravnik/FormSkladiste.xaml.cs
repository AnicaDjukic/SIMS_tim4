﻿using Bolnica.Model.Prostorije;
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
        public FileStorageBuducaZaliha storageBuducaZaliha;
        public static FileStorageZaliha storageZaliha;

        private int kolicinaZaPremestanje;

        private Zaliha magacin;

        private Oprema opremaZaSkladistenje;

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
            // uzmi buduce zalihe i upisi ih u fajl sa zalihama
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
            storageZaliha = new FileStorageZaliha();
            List<Zaliha> zaliheOpreme = new List<Zaliha>();
            int maxId = 0;
            foreach (Zaliha z in storageZaliha.GetAll())
            {
                if (z.Id > maxId)
                    maxId = z.Id;
            }
            if (buduceZalihe.Count > 0)
            {
                foreach (Zaliha z in storageZaliha.GetAll())
                {
                    if (z.Oprema.Sifra == opremaZaSkladistenje.Sifra)
                        storageZaliha.Delete(z);
                }
                foreach (BuducaZaliha bz in buduceZalihe)
                {
                    Zaliha z = new Zaliha { Id = maxId + 1, Kolicina = bz.Kolicina};
                    z.Prostorija = bz.Prostorija;
                    z.Oprema = bz.Oprema;
                    storageZaliha.Save(z);
                    zaliheOpreme.Add(z);
                }
            }
            else
            {
                foreach (Zaliha z in storageZaliha.GetAll())
                {
                    if (oprema.Sifra == z.Oprema.Sifra)
                        zaliheOpreme.Add(z);
                }
            }
            // kraj
            ProstorijeZaSkladistenje = new ObservableCollection<Prostorija>();
            storageProstorija = new FileStorageProstorija();
            List<Prostorija> prostorije = storageProstorija.GetAllProstorije();
            List<BolnickaSoba> bolnickeSobe = storageProstorija.GetAllBolnickeSobe();
            List<Prostorija> korisceneProstorije = new List<Prostorija>();
            List<BolnickaSoba> korisceneBolnicke = new List<BolnickaSoba>();
            if (zaliheOpreme.Count == 0)
            {
                foreach (Prostorija p in prostorije)
                {
                    if (!p.Obrisana)
                        ProstorijeZaSkladistenje.Add(p);
                }

                foreach (BolnickaSoba b in bolnickeSobe)
                {
                    if (!b.Obrisana)
                        ProstorijeZaSkladistenje.Add(b);
                }
            }
            else
            {
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

            Zalihe = new ObservableCollection<Zaliha>();
            if (!imaMagacin())
            {
                Prostorija prostorijaMagacin = new Prostorija { BrojProstorije = "magacin" };
                magacin = new Zaliha { Prostorija = prostorijaMagacin, Oprema = opremaZaSkladistenje, Kolicina = opremaZaSkladistenje.Kolicina };
                magacin.Prostorija = new Prostorija();
                magacin.Prostorija.BrojProstorije = "magacin";
                magacin.Oprema = opremaZaSkladistenje;
                Zalihe.Add(magacin);

            }
            else
            {
                int kolicinaUMagacinu = opremaZaSkladistenje.Kolicina;
                foreach (Zaliha z in zaliheOpreme)
                {
                    if (z.Prostorija.BrojProstorije == "magacin")
                    {
                        magacin = z;
                        magacin.Prostorija.BrojProstorije = "magacin";
                        magacin.Oprema.Sifra = opremaZaSkladistenje.Sifra;
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

            return imaMagacin;
        }
        private void Button_Click_Prebaci(object sender, RoutedEventArgs e)
        {
            if (GridProstorije.SelectedCells.Count > 0)
            {
                if (kolicinaZaPremestanje != 0 && kolicinaZaPremestanje <= magacin.Kolicina)
                {
                    Prostorija row = (Prostorija)GridProstorije.SelectedItem;
                    for (int i = 0; i < ProstorijeZaSkladistenje.Count; i++)
                    {
                        if (ProstorijeZaSkladistenje[i].BrojProstorije == row.BrojProstorije)
                            ProstorijeZaSkladistenje.Remove(ProstorijeZaSkladistenje[i]);
                    }
                    Zaliha zaliha = new Zaliha();
                    zaliha.Prostorija = row;
                    zaliha.Prostorija.BrojProstorije = row.BrojProstorije;
                    zaliha.Kolicina = kolicinaZaPremestanje;
                    zaliha.Oprema = opremaZaSkladistenje;
                    zaliha.Oprema.Sifra = opremaZaSkladistenje.Sifra;
                    Zalihe.Add(zaliha);
                    Zalihe.Remove(magacin);
                    magacin.Kolicina -= kolicinaZaPremestanje;
                    Zalihe.Add(magacin);
                }
                else
                {
                    MessageBox.Show("Unesite validnu količinu. Količina mora biti veća od 0 i manja ili jednaka od količine opreme u magacinu.");
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
                    if (Zalihe[i].Prostorija.BrojProstorije == row.Prostorija.BrojProstorije)
                    {
                        Zalihe.Remove(Zalihe[i]);
                    }
                }

                List<Prostorija> prostorije = storageProstorija.GetAllProstorije();
                bool found = false;
                foreach (Prostorija p in prostorije)
                {
                    if (p.BrojProstorije == row.Prostorija.BrojProstorije)
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
                KolicinaZaPremestanje = row.Kolicina;
                magacin.Kolicina += row.Kolicina;
                Zalihe.Add(magacin);
            }
        }

        public void Button_Click_Potvrdi(object sender, RoutedEventArgs e)
        {
            if (opremaZaSkladistenje.TipOpreme != TipOpreme.staticka)
            {
                List<Zaliha> zalihe = storageZaliha.GetAll();
                if (zalihe != null)
                {
                    foreach (Zaliha z in zalihe)
                    {
                        if (z.Oprema.Sifra == opremaZaSkladistenje.Sifra)
                            storageZaliha.Delete(z);
                    }
                }

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
