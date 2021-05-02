using Bolnica.Forms.Upravnik;
using Bolnica.Model.Prostorije;
using Model.Prostorije;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace Bolnica.Forms
{
    /// <summary>
    /// Interaction logic for ViewFormProstorije.xaml
    /// </summary>
    public partial class ViewFormProstorije : Window
    {
        public static ObservableCollection<Zaliha> OpremaSobe
        {
            get;
            set;
        }
        public ViewFormProstorije(string brojProstorije)
        {
            InitializeComponent();
            this.DataContext = this;
            OpremaSobe = new ObservableCollection<Zaliha>();
            FileStorageBuducaZaliha storageBuducaZaliha = new FileStorageBuducaZaliha();
            FileStorageZaliha storageZaliha = new FileStorageZaliha();
            List<Zaliha> noveZalihe = new List<Zaliha>();
            if (storageBuducaZaliha.GetAll() != null)
            {
                foreach (BuducaZaliha bz in storageBuducaZaliha.GetAll())
                {
                    if (bz.Datum <= DateTime.Now.Date)
                    {
                        Zaliha z = new Zaliha { Kolicina = bz.Kolicina, SifraOpreme = bz.SifraOpreme, BrojProstorije = bz.BrojProstorije };
                        noveZalihe.Add(z);
                        storageBuducaZaliha.Delete(bz);
                    }
                }

                if (storageZaliha.GetAll() != null)
                {
                    foreach (Zaliha z in storageZaliha.GetAll())
                    {
                        foreach (Zaliha nz in noveZalihe)
                        {
                            if (z.SifraOpreme == nz.SifraOpreme)
                            {
                                storageZaliha.Delete(z);
                            }
                        }
                    }
                }

                foreach(Zaliha z in noveZalihe)
                {
                    storageZaliha.Save(z);
                }
            }

            FileStorageOprema storageOprema = new FileStorageOprema();
            if (storageOprema.GetAll() != null)
            {
                foreach (Zaliha zaliha in storageZaliha.GetAll())
                {
                    if (zaliha.BrojProstorije == brojProstorije)
                    {
                        foreach (Oprema o in storageOprema.GetAll())
                        {
                            if (zaliha.SifraOpreme == o.Sifra)
                            {
                                zaliha.Oprema = o;
                                OpremaSobe.Add(zaliha);
                            }
                        }
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
