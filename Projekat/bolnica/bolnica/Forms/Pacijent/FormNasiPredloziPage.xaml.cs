using Bolnica.Model.Korisnici;
using Bolnica.Model.Pregledi;
using Bolnica.ViewModel;
using Model.Korisnici;
using Model.Pregledi;
using Model.Prostorije;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Bolnica.Forms
{
    /// <summary>
    /// Interaction logic for FormNasiPredloziPage.xaml
    /// </summary>
    public partial class FormNasiPredloziPage : Page
    {
        private Pacijent trenutniPacijent = new Pacijent();
        public static ObservableCollection<PrikazPregleda> PredlozeniTermini
        {
            get;
            set;
        }

        private List<Lekar> lekari = new List<Lekar>();
        private List<Pregled> pregledi = new List<Pregled>();
        private List<DateTime> zauzetiTermini = new List<DateTime>();
        private List<Prostorija> prostorije = new List<Prostorija>();
        private List<Prostorija> slobodneProstorije = new List<Prostorija>();

        private FileStoragePregledi storagePregledi = new FileStoragePregledi();
        private FileStorageProstorija storageProstorije = new FileStorageProstorija();
        private FileStorageLekar storageLekari = new FileStorageLekar();
        private FileStorageAntiTrol storageAntiTrol = new FileStorageAntiTrol();

        public FormNasiPredloziPage(Pacijent pacijent, DateTime datum, int sat, int minut, Lekar lekar)
        {
            InitializeComponent();

            this.DataContext = this;

            trenutniPacijent = pacijent;
            PredlozeniTermini = new ObservableCollection<PrikazPregleda>();

            pregledi = storagePregledi.GetAllPregledi();
            foreach (Pregled p in pregledi)
            {
                zauzetiTermini.Add(p.Datum);
            }

            prostorije = storageProstorije.GetAllProstorije();
            foreach (Prostorija p in prostorije)
            {
                if (p.TipProstorije.Equals(TipProstorije.salaZaPreglede) && !p.Obrisana)
                {
                    slobodneProstorije.Add(p);
                    break;
                }
            }

            lekari = storageLekari.GetAll();

            if (datum.Equals(new DateTime(1, 1, 1)) && sat == -1 && minut == -1 && lekar.Jmbg is null)
            {
                PrikazPregleda p1 = new PrikazPregleda();
                p1.Datum = DateTime.Today.AddDays(3).AddHours(10);
                p1.Lekar = lekari[0];
                p1.Trajanje = 30;
                p1.Zavrsen = false;
                p1.Pacijent = pacijent;
                p1.Anamneza.Id = -1;

                PrikazPregleda p2 = new PrikazPregleda();
                p2.Datum = DateTime.Today.AddDays(4).AddHours(11).AddMinutes(45);
                p2.Lekar = lekari[1];
                p2.Trajanje = 30;
                p2.Zavrsen = false;
                p2.Pacijent = pacijent;
                p2.Anamneza.Id = -1;

                PrikazPregleda p3 = new PrikazPregleda();
                p3.Datum = DateTime.Today.AddDays(5).AddHours(18).AddMinutes(15);
                p3.Lekar = lekari[2];
                p3.Trajanje = 30;
                p3.Zavrsen = false;
                p3.Pacijent = pacijent;
                p3.Anamneza.Id = -1;

                PrikazPregleda p4 = new PrikazPregleda();
                p4.Datum = DateTime.Today.AddDays(6).AddHours(7).AddMinutes(30);
                p4.Lekar = lekari[3];
                p4.Trajanje = 30;
                p4.Zavrsen = false;
                p4.Pacijent = pacijent;
                p4.Anamneza.Id = -1;

                if (slobodneProstorije.Count > 0)
                {
                    p1.Prostorija = slobodneProstorije[0];
                    if (slobodneProstorije.Count > 1)
                    {
                        p2.Prostorija = slobodneProstorije[1];
                        if (slobodneProstorije.Count > 2)
                        {
                            p3.Prostorija = slobodneProstorije[2];
                            if (slobodneProstorije.Count > 3)
                            {
                                p4.Prostorija = slobodneProstorije[3];
                            }
                            else
                            {
                                p4.Prostorija = slobodneProstorije[0];
                            }
                        }
                        else
                        {
                            p3.Prostorija = slobodneProstorije[0];
                            p4.Prostorija = slobodneProstorije[0];
                        }
                    }
                    else
                    {
                        p2.Prostorija = slobodneProstorije[0];
                        p3.Prostorija = slobodneProstorije[0];
                        p4.Prostorija = slobodneProstorije[0];
                    }
                }
                else
                {
                    MessageBox.Show("Nema slobodnih prostorija!");
                }

                int max = 0;
                foreach (Pregled p in pregledi)
                {
                    if (p.Id > max)
                    {
                        max = p.Id;
                    }
                }
                p1.Id = max + 1;
                p2.Id = max + 1;
                p3.Id = max + 1;
                p4.Id = max + 1;

                PredlozeniTermini.Add(p1);
                PredlozeniTermini.Add(p2);
                PredlozeniTermini.Add(p3);
                PredlozeniTermini.Add(p4);
            }
            else if (!datum.Equals(new DateTime(1, 1, 1)) && sat != -1 && minut != -1 && !(lekar.Jmbg is null))
            {
                PrikazPregleda p1 = new PrikazPregleda();
                p1.Datum = datum.AddHours(sat).AddMinutes(minut);
                p1.Lekar = lekar;
                p1.Trajanje = 30;
                p1.Zavrsen = false;
                p1.Pacijent = pacijent;
                p1.Anamneza.Id = -1;

                PrikazPregleda p2 = new PrikazPregleda();
                p2.Datum = datum.AddHours(sat + 1).AddMinutes(minut + 15);
                p2.Lekar = lekar;
                p2.Trajanje = 30;
                p2.Zavrsen = false;
                p2.Pacijent = pacijent;
                p2.Anamneza.Id = -1;

                PrikazPregleda p3 = new PrikazPregleda();
                p3.Datum = datum.AddHours(sat + 2).AddMinutes(minut);
                p3.Lekar = lekar;
                p3.Trajanje = 30;
                p3.Zavrsen = false;
                p3.Pacijent = pacijent;
                p3.Anamneza.Id = -1;

                PrikazPregleda p4 = new PrikazPregleda();
                p4.Datum = datum.AddHours(sat + 3).AddMinutes(minut + 45);
                p4.Lekar = lekar;
                p4.Trajanje = 30;
                p4.Zavrsen = false;
                p4.Pacijent = pacijent;
                p4.Anamneza.Id = -1;

                if (slobodneProstorije.Count > 0)
                {
                    p1.Prostorija = slobodneProstorije[0];
                    if (slobodneProstorije.Count > 1)
                    {
                        p2.Prostorija = slobodneProstorije[1];
                        if (slobodneProstorije.Count > 2)
                        {
                            p3.Prostorija = slobodneProstorije[2];
                            if (slobodneProstorije.Count > 3)
                            {
                                p4.Prostorija = slobodneProstorije[3];
                            }
                            else
                            {
                                p4.Prostorija = slobodneProstorije[0];
                            }
                        }
                        else
                        {
                            p3.Prostorija = slobodneProstorije[0];
                            p4.Prostorija = slobodneProstorije[0];
                        }
                    }
                    else
                    {
                        p2.Prostorija = slobodneProstorije[0];
                        p3.Prostorija = slobodneProstorije[0];
                        p4.Prostorija = slobodneProstorije[0];
                    }
                }
                else
                {
                    MessageBox.Show("Nema slobodnih prostorija!");
                }

                int max = 0;
                foreach (Pregled p in pregledi)
                {
                    if (p.Id > max)
                    {
                        max = p.Id;
                    }
                }
                p1.Id = max + 1;
                p2.Id = max + 1;
                p3.Id = max + 1;
                p4.Id = max + 1;

                PredlozeniTermini.Add(p1);
                PredlozeniTermini.Add(p2);
                PredlozeniTermini.Add(p3);
                PredlozeniTermini.Add(p4);
            }
            else if (!datum.Equals(new DateTime(1, 1, 1)) && lekar.Jmbg is null)
            {
                if (sat != -1 && minut != -1)
                {
                    DateTime dat = new DateTime(datum.Year, datum.Month, datum.Day, sat, minut, 0);

                    PrikazPregleda p1 = new PrikazPregleda();
                    p1.Datum = dat;
                    p1.Lekar = lekari[0];
                    p1.Trajanje = 30;
                    p1.Zavrsen = false;
                    p1.Pacijent = pacijent;
                    p1.Anamneza.Id = -1;

                    PrikazPregleda p2 = new PrikazPregleda();
                    p2.Datum = dat;
                    p2.Lekar = lekari[1];
                    p2.Trajanje = 30;
                    p2.Zavrsen = false;
                    p2.Pacijent = pacijent;
                    p2.Anamneza.Id = -1;

                    PrikazPregleda p3 = new PrikazPregleda();
                    p3.Datum = dat;
                    p3.Lekar = lekari[2];
                    p3.Trajanje = 30;
                    p3.Zavrsen = false;
                    p3.Pacijent = pacijent;
                    p3.Anamneza.Id = -1;

                    PrikazPregleda p4 = new PrikazPregleda();
                    p4.Datum = dat;
                    p4.Lekar = lekari[3];
                    p4.Trajanje = 30;
                    p4.Zavrsen = false;
                    p4.Pacijent = pacijent;
                    p4.Anamneza.Id = -1;

                    if (slobodneProstorije.Count > 0)
                    {
                        p1.Prostorija = slobodneProstorije[0];
                        if (slobodneProstorije.Count > 1)
                        {
                            p2.Prostorija = slobodneProstorije[1];
                            if (slobodneProstorije.Count > 2)
                            {
                                p3.Prostorija = slobodneProstorije[2];
                                if (slobodneProstorije.Count > 3)
                                {
                                    p4.Prostorija = slobodneProstorije[3];
                                }
                                else
                                {
                                    p4.Prostorija = slobodneProstorije[0];
                                }
                            }
                            else
                            {
                                p3.Prostorija = slobodneProstorije[0];
                                p4.Prostorija = slobodneProstorije[0];
                            }
                        }
                        else
                        {
                            p2.Prostorija = slobodneProstorije[0];
                            p3.Prostorija = slobodneProstorije[0];
                            p4.Prostorija = slobodneProstorije[0];
                        }
                    }
                    else
                    {
                        MessageBox.Show("Nema slobodnih prostorija!");
                    }

                    int max = 0;
                    foreach (Pregled p in pregledi)
                    {
                        if (p.Id > max)
                        {
                            max = p.Id;
                        }
                    }
                    p1.Id = max + 1;
                    p2.Id = max + 1;
                    p3.Id = max + 1;
                    p4.Id = max + 1;

                    PredlozeniTermini.Add(p1);
                    PredlozeniTermini.Add(p2);
                    PredlozeniTermini.Add(p3);
                    PredlozeniTermini.Add(p4);
                }
                else
                {
                    DateTime dat1 = new DateTime(datum.Year, datum.Month, datum.Day, 7, 15, 0);
                    DateTime dat2 = new DateTime(datum.Year, datum.Month, datum.Day, 8, 45, 0);
                    DateTime dat3 = new DateTime(datum.Year, datum.Month, datum.Day, 14, 30, 0);
                    DateTime dat4 = new DateTime(datum.Year, datum.Month, datum.Day, 16, 0, 0);

                    PrikazPregleda p1 = new PrikazPregleda();
                    p1.Datum = dat1;
                    p1.Lekar = lekari[0];
                    p1.Trajanje = 30;
                    p1.Zavrsen = false;
                    p1.Pacijent = pacijent;
                    p1.Anamneza.Id = -1;

                    PrikazPregleda p2 = new PrikazPregleda();
                    p2.Datum = dat2;
                    p2.Lekar = lekari[1];
                    p2.Trajanje = 30;
                    p2.Zavrsen = false;
                    p2.Pacijent = pacijent;
                    p2.Anamneza.Id = -1;

                    PrikazPregleda p3 = new PrikazPregleda();
                    p3.Datum = dat3;
                    p3.Lekar = lekari[2];
                    p3.Trajanje = 30;
                    p3.Zavrsen = false;
                    p3.Pacijent = pacijent;
                    p3.Anamneza.Id = -1;

                    PrikazPregleda p4 = new PrikazPregleda();
                    p4.Datum = dat4;
                    p4.Lekar = lekari[3];
                    p4.Trajanje = 30;
                    p4.Zavrsen = false;
                    p4.Pacijent = pacijent;
                    p4.Anamneza.Id = -1;

                    if (slobodneProstorije.Count > 0)
                    {
                        p1.Prostorija = slobodneProstorije[0];
                        if (slobodneProstorije.Count > 1)
                        {
                            p2.Prostorija = slobodneProstorije[1];
                            if (slobodneProstorije.Count > 2)
                            {
                                p3.Prostorija = slobodneProstorije[2];
                                if (slobodneProstorije.Count > 3)
                                {
                                    p4.Prostorija = slobodneProstorije[3];
                                }
                                else
                                {
                                    p4.Prostorija = slobodneProstorije[0];
                                }
                            }
                            else
                            {
                                p3.Prostorija = slobodneProstorije[0];
                                p4.Prostorija = slobodneProstorije[0];
                            }
                        }
                        else
                        {
                            p2.Prostorija = slobodneProstorije[0];
                            p3.Prostorija = slobodneProstorije[0];
                            p4.Prostorija = slobodneProstorije[0];
                        }
                    }
                    else
                    {
                        MessageBox.Show("Nema slobodnih prostorija!");
                    }

                    int max = 0;
                    foreach (Pregled p in pregledi)
                    {
                        if (p.Id > max)
                        {
                            max = p.Id;
                        }
                    }
                    p1.Id = max + 1;
                    p2.Id = max + 1;
                    p3.Id = max + 1;
                    p4.Id = max + 1;

                    PredlozeniTermini.Add(p1);
                    PredlozeniTermini.Add(p2);
                    PredlozeniTermini.Add(p3);
                    PredlozeniTermini.Add(p4);
                }


            }
            else if (datum.Equals(new DateTime(1, 1, 1)) && !(lekar.Jmbg is null))
            {
                PrikazPregleda p1 = new PrikazPregleda();
                p1.Datum = DateTime.Today.AddDays(3);
                p1.Lekar = lekar;
                p1.Trajanje = 30;
                p1.Zavrsen = false;
                p1.Pacijent = pacijent;
                p1.Anamneza.Id = -1;

                PrikazPregleda p2 = new PrikazPregleda();
                p2.Datum = DateTime.Today.AddDays(4).AddHours(17).AddMinutes(30);
                p2.Lekar = lekar;
                p2.Trajanje = 30;
                p2.Zavrsen = false;
                p2.Pacijent = pacijent;
                p2.Anamneza.Id = -1;

                PrikazPregleda p3 = new PrikazPregleda();
                p3.Datum = DateTime.Today.AddDays(5).AddHours(8);
                p3.Lekar = lekar;
                p3.Trajanje = 30;
                p3.Zavrsen = false;
                p3.Pacijent = pacijent;
                p3.Anamneza.Id = -1;

                PrikazPregleda p4 = new PrikazPregleda();
                p4.Datum = DateTime.Today.AddDays(6).AddHours(14).AddMinutes(15); ;
                p4.Lekar = lekar;
                p4.Trajanje = 30;
                p4.Zavrsen = false;
                p4.Pacijent = pacijent;
                p4.Anamneza.Id = -1;

                if (slobodneProstorije.Count > 0)
                {
                    p1.Prostorija = slobodneProstorije[0];
                    if (slobodneProstorije.Count > 1)
                    {
                        p2.Prostorija = slobodneProstorije[1];
                        if (slobodneProstorije.Count > 2)
                        {
                            p3.Prostorija = slobodneProstorije[2];
                            if (slobodneProstorije.Count > 3)
                            {
                                p4.Prostorija = slobodneProstorije[3];
                            }
                            else
                            {
                                p4.Prostorija = slobodneProstorije[0];
                            }
                        }
                        else
                        {
                            p3.Prostorija = slobodneProstorije[0];
                            p4.Prostorija = slobodneProstorije[0];
                        }
                    }
                    else
                    {
                        p2.Prostorija = slobodneProstorije[0];
                        p3.Prostorija = slobodneProstorije[0];
                        p4.Prostorija = slobodneProstorije[0];
                    }
                }
                else
                {
                    MessageBox.Show("Nema slobodnih prostorija!");
                }

                int max = 0;
                foreach (Pregled p in pregledi)
                {
                    if (p.Id > max)
                    {
                        max = p.Id;
                    }
                }
                p1.Id = max + 1;
                p2.Id = max + 1;
                p3.Id = max + 1;
                p4.Id = max + 1;

                PredlozeniTermini.Add(p1);
                PredlozeniTermini.Add(p2);
                PredlozeniTermini.Add(p3);
                PredlozeniTermini.Add(p4);
            }
        }

        private void PotvrdiIzabraniTermin(object sender, RoutedEventArgs e)
        {
            var objekat = nasiPredloziGrid.SelectedValue;

            if (objekat != null)
            {
                PrikazPregleda prikazPregleda = (PrikazPregleda)nasiPredloziGrid.SelectedItem;

                foreach (PrikazPregleda p in PredlozeniTermini)
                {
                    if (p.Equals(prikazPregleda))
                    {
                        Pregled pregled = new Pregled
                        {
                            Id = p.Id,
                            Datum = p.Datum,
                            Trajanje = p.Trajanje,
                            Zavrsen = p.Zavrsen,
                            Prostorija = p.Prostorija,
                            Anamneza = p.Anamneza,
                            Lekar = p.Lekar,
                            Pacijent = p.Pacijent
                        };

                        PacijentPageViewModel.PrikazNezavrsenihPregleda.Add(p);
                        storagePregledi.Save(pregled);

                        
                        AntiTrol antiTrol = new AntiTrol
                        {
                            Pacijent = p.Pacijent,
                            Datum = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second)
                        };
                        storageAntiTrol.Save(antiTrol);

                        PacijentPageViewModel pacijentPageViewModel = new PacijentPageViewModel(pregled.Pacijent);
                        FormPacijentWeb.Forma.Pocetna.Content = new FormPacijentPage(pacijentPageViewModel/*pregled.Pacijent*/);

                        break;
                    }
                }
            }
            else
            {
                MessageBox.Show("Morate odabrati pregled koji zelite da zakazete!", "Upozorenje");
            }
        }

        private void Odustani(object sender, RoutedEventArgs e)
        {
            PacijentPageViewModel pacijentPageViewModel = new PacijentPageViewModel(trenutniPacijent);
            FormPacijentWeb.Forma.Pocetna.Content = new FormPacijentPage(pacijentPageViewModel/*trenutniPacijent*/);
        }
    }
}
