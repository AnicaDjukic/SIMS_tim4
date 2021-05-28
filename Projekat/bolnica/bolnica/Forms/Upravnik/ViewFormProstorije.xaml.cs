using Bolnica.Forms.Upravnik;
using Bolnica.Model.Prostorije;
using Bolnica.Services.Prostorije;
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

        private ServiceZaliha serviceZaliha = new ServiceZaliha();
        public ViewFormProstorije(string brojProstorije)
        {
            InitializeComponent();
            this.DataContext = this;
            OpremaSobe = new ObservableCollection<Zaliha>();
            FileStorageBuducaZaliha storageBuducaZaliha = new FileStorageBuducaZaliha();
            List<Zaliha> noveZalihe = new List<Zaliha>();
            if (storageBuducaZaliha.GetAll() != null)
            {
                foreach (BuducaZaliha bz in storageBuducaZaliha.GetAll())
                {
                    if (bz.Datum <= DateTime.Now.Date)
                    {
                        Zaliha z = new Zaliha { Kolicina = bz.Kolicina };
                        z.Prostorija = bz.Prostorija;
                        z.Oprema = bz.Oprema;
                        noveZalihe.Add(z);
                        storageBuducaZaliha.Delete(bz);
                    }
                }

                if (serviceZaliha.DobaviZalihe() != null)
                {
                    foreach (Zaliha z in serviceZaliha.DobaviZalihe())
                    {
                        foreach (Zaliha nz in noveZalihe)
                        {
                            if (z.Oprema.Sifra == nz.Oprema.Sifra)
                            {
                                serviceZaliha.ObrisiZalihu(z);
                            }
                        }
                    }
                }

                foreach(Zaliha z in noveZalihe)
                {
                    serviceZaliha.SacuvajZalihu(z);
                }
            }

            FileStorageOprema storageOprema = new FileStorageOprema();
            if (storageOprema.GetAll() != null)
            {
                foreach (Zaliha zaliha in serviceZaliha.DobaviZalihe())
                {
                    if (zaliha.Prostorija.BrojProstorije == brojProstorije)
                    {
                        foreach (Oprema o in storageOprema.GetAll())
                        {
                            if (zaliha.Oprema.Sifra == o.Sifra)
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
