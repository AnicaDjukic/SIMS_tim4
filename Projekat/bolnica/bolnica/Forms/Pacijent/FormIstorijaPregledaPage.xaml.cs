﻿using Bolnica.Model.Korisnici;
using Bolnica.Model.Pregledi;
using Bolnica.Repository.Pregledi;
using Bolnica.Repository.Prostorije;
using Model.Korisnici;
using Model.Pregledi;
using Model.Prostorije;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Bolnica.Forms
{
    /// <summary>
    /// Interaction logic for FormIstorijaPregledaPage.xaml
    /// </summary>
    public partial class FormIstorijaPregledaPage : Page
    {
        private Pacijent trenutniPacijent = new Pacijent();
        public static ObservableCollection<PrikazPregleda> PrikazZavrsenihPregleda
        {
            get;
            set;
        }

        private FileRepositoryPregled storagePregledi = new FileRepositoryPregled();
        private FileRepositoryOperacija storageOperacije = new FileRepositoryOperacija();
        private FileRepositoryLekar storageLekari = new FileRepositoryLekar();
        private FileRepositoryProstorija storageProstorije = new FileRepositoryProstorija();
        private FileRepositoryOcena storageOcene = new FileRepositoryOcena();

        private List<Lekar> lekari = new List<Lekar>();
        private List<Prostorija> prostorije = new List<Prostorija>();
        private List<Ocena> ocene = new List<Ocena>();

        public FormIstorijaPregledaPage(Pacijent pacijent)
        {
            InitializeComponent();

            trenutniPacijent = pacijent;

            this.DataContext = this;

            PrikazZavrsenihPregleda = new ObservableCollection<PrikazPregleda>();

            lekari = storageLekari.GetAll();

            prostorije = storageProstorije.GetAll();

            List<Pregled> pregledi = storagePregledi.GetAll();
            foreach (Pregled p in pregledi)
            {
                if (p.Pacijent.Jmbg.Equals(pacijent.Jmbg))
                {
                    if (!pacijent.Obrisan && !pacijent.Guest && p.Zavrsen)
                    {
                        PrikazPregleda prikaz = new PrikazPregleda
                        {
                            Datum = p.Datum,
                            Trajanje = p.Trajanje,
                            Zavrsen = p.Zavrsen,
                            Hitan = p.Hitan,
                            Id = p.Id,
                            Anamneza = p.Anamneza,
                            Pacijent = pacijent
                        };

                        foreach (Lekar l in lekari)
                        {
                            if (p.Lekar.Jmbg.Equals(l.Jmbg))
                            {
                                prikaz.Lekar = l;
                            }
                        }
                        
                        foreach (Prostorija pro in prostorije)
                        {
                            if (p.Prostorija.BrojProstorije.Equals(pro.BrojProstorije))
                            {
                                prikaz.Prostorija = pro;
                            }
                        }

                        PrikazZavrsenihPregleda.Add(prikaz);
                    }
                }
            }
            List<Operacija> operacije = storageOperacije.GetAll();
            foreach (Operacija o in operacije)
            {
                if (o.Pacijent.Jmbg.Equals(pacijent.Jmbg))
                {
                    if (!pacijent.Obrisan && !pacijent.Guest && o.Zavrsen)
                    {
                        PrikazOperacije prikaz = new PrikazOperacije
                        {
                            Datum = o.Datum,
                            Trajanje = o.Trajanje,
                            Zavrsen = o.Zavrsen,
                            Hitan = o.Hitan,
                            Id = o.Id,
                            Anamneza = o.Anamneza,
                            TipOperacije = o.TipOperacije,
                            Pacijent = pacijent
                        };

                        foreach (Lekar l in lekari)
                        {
                            if (o.Lekar.Jmbg.Equals(l.Jmbg))
                            {
                                prikaz.Lekar = l;
                            }
                        }

                        foreach (Prostorija p in prostorije)
                        {
                            if (o.Prostorija.BrojProstorije.Equals(p.BrojProstorije))
                            {
                                prikaz.Prostorija = p;
                            }
                        }

                        PrikazZavrsenihPregleda.Add(prikaz);
                    }
                }
            }
        }

        private void Button_Click_Oceni_Lekara(object sender, RoutedEventArgs e)
        {
            var objekat = pacijentIstorijaGrid.SelectedValue;

            if (objekat != null)
            {
                PrikazPregleda prikazPregleda = (PrikazPregleda)pacijentIstorijaGrid.SelectedItem;
                if (PregledVecOcenjen(prikazPregleda))
                {
                    MessageBox.Show("Za izabrani pregled ste vec ocenili lekara!", "Upozorenje");
                }
                else
                {
                    FormPacijentWeb.Forma.Pocetna.Content = new FormOceniLekaraPage(prikazPregleda);
                }
            }
            else
            {
                MessageBox.Show("Morate odabrati pregled kod odredjenog lekara koga zelite da ocenite!", "Upozorenje");
            }
        }

        private bool PregledVecOcenjen(PrikazPregleda prikazPregleda)
        {
            ocene = storageOcene.GetAll();
            foreach (Ocena ocena in ocene)
            {
                if (prikazPregleda.Id.Equals(ocena.Pregled.Id))
                {
                    return true;
                }
            }
            return false;
        }

        private void Button_Click_Oceni_Bolnicu(object sender, RoutedEventArgs e)
        {
            FormPacijentWeb.Forma.Pocetna.Content = new FormOceniBolnicuPage(trenutniPacijent);
        }

        private void Button_Click_Istorija_Ocena_I_Komentara(object sender, RoutedEventArgs e)
        {
            FormPacijentWeb.Forma.Pocetna.Content = new FormIstorijaOcenaPage(trenutniPacijent);
        }

        private void Button_Click_Anamneza(object sender, RoutedEventArgs e)
        {
            var objekat = pacijentIstorijaGrid.SelectedValue;

            if (objekat != null)
            {
                PrikazPregleda prikazPregleda = (PrikazPregleda)pacijentIstorijaGrid.SelectedItem;
                FormPacijentWeb.Forma.Pocetna.Content = new FormAnamnezaPage(prikazPregleda);
            }
            else
            {
                MessageBox.Show("Morate odabrati pregled za koji zelite da vidite anamnezu!", "Upozorenje");
            }
        }
    }
}
