using Bolnica.Forms;
using Bolnica.Model.Pregledi;
using Bolnica.Services;
using Bolnica.Services.Prostorije;
using Bolnica.ViewModel;
using Model.Korisnici;
using Model.Pregledi;
using Model.Prostorije;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace Bolnica.Service
{
    public class NasiPredloziService
    {
        private AntiTrolService serviceAntiTrol = new AntiTrolService();
        private PregledService servicePregled = new PregledService();
        private PregledIdService servicePregledId = new PregledIdService();
        private ServiceProstorija serviceProstorija = new ServiceProstorija();
        private LekarService serviceLekar = new LekarService();

        public void DobijPredlozeneTermine(Pacijent pacijent, DateTime datum, int sat, int minut, Lekar lekar)
        {
            FormNasiPredloziPage.PredlozeniTermini = new ObservableCollection<PrikazPregleda>();
            List<DateTime> zauzetiTermini = DobijZauzeteTermine();
            List<Prostorija> slobodneProstorije = DobijSlobodneProstorije();
            List<Lekar> lekari = serviceLekar.DobijLekare();

            if (datum.Date.Equals(DateTime.Today) && sat == -1 && minut == -1 && lekar.Jmbg is null)
            {
                PrikazPregleda p1, p2, p3, p4;
                KreirajNasumicneTermine(pacijent, lekari, out p1, out p2, out p3, out p4);

                PronadjiSlobodnuProstoriju(slobodneProstorije, p1, p2, p3, p4);
                DodajPredlozeneTermine(p1, p2, p3, p4);
            }
            else if (!datum.Equals(new DateTime(1, 1, 1)) && sat != -1 && minut != -1 && !(lekar.Jmbg is null))
            {
                PrikazPregleda p1, p2, p3, p4;
                KreirajTermineSpramLekaraIDatuma(pacijent, datum, sat, minut, lekar, out p1, out p2, out p3, out p4);

                PronadjiSlobodnuProstoriju(slobodneProstorije, p1, p2, p3, p4);
                DodajPredlozeneTermine(p1, p2, p3, p4);
            }
            else if (!datum.Equals(new DateTime(1, 1, 1)) && sat != -1 && minut != -1 && lekar.Jmbg is null)
            {
                DateTime dat = new DateTime(datum.Year, datum.Month, datum.Day, sat, minut, 0);

                PrikazPregleda p1, p2, p3, p4;
                KreirajTermineSpramDatuma(pacijent, lekari, dat, out p1, out p2, out p3, out p4);

                PronadjiSlobodnuProstoriju(slobodneProstorije, p1, p2, p3, p4);
                DodajPredlozeneTermine(p1, p2, p3, p4);
            }
            else if (datum.Date.Equals(DateTime.Today) && !(lekar.Jmbg is null))
            {
                PrikazPregleda p1, p2, p3, p4;
                KreirajTermineSpramLekara(pacijent, lekar, out p1, out p2, out p3, out p4);

                PronadjiSlobodnuProstoriju(slobodneProstorije, p1, p2, p3, p4);
                DodajPredlozeneTermine(p1, p2, p3, p4);
            }
        }

        private List<DateTime> DobijZauzeteTermine()
        {
            List<Pregled> pregledi = servicePregled.DobaviSvePreglede();
            List<DateTime> zauzetiTermini = new List<DateTime>();
            foreach (Pregled p in pregledi)
            {
                zauzetiTermini.Add(p.Datum);
            }

            return zauzetiTermini;
        }

        private List<Prostorija> DobijSlobodneProstorije()
        {
            List<Prostorija> prostorije = serviceProstorija.DobaviSveProstorije();
            List<Prostorija> slobodneProstorije = new List<Prostorija>();
            foreach (Prostorija p in prostorije)
            {
                if (p.TipProstorije.Equals(TipProstorije.salaZaPreglede) && !p.Obrisana)
                {
                    slobodneProstorije.Add(p);
                    break;
                }
            }

            return slobodneProstorije;
        }

        private static void KreirajNasumicneTermine(Pacijent pacijent, List<Lekar> lekari, out PrikazPregleda p1, out PrikazPregleda p2, out PrikazPregleda p3, out PrikazPregleda p4)
        {
            p1 = new PrikazPregleda();
            p1.Datum = DateTime.Today.AddDays(3).AddHours(10);
            p1.Lekar = lekari[0];
            p1.Trajanje = 30;
            p1.Zavrsen = false;
            p1.Pacijent = pacijent;
            p1.Anamneza.Id = -1;

            p2 = new PrikazPregleda();
            p2.Datum = DateTime.Today.AddDays(4).AddHours(11).AddMinutes(45);
            p2.Lekar = lekari[1];
            p2.Trajanje = 30;
            p2.Zavrsen = false;
            p2.Pacijent = pacijent;
            p2.Anamneza.Id = -1;

            p3 = new PrikazPregleda();
            p3.Datum = DateTime.Today.AddDays(5).AddHours(18).AddMinutes(15);
            p3.Lekar = lekari[2];
            p3.Trajanje = 30;
            p3.Zavrsen = false;
            p3.Pacijent = pacijent;
            p3.Anamneza.Id = -1;

            p4 = new PrikazPregleda();
            p4.Datum = DateTime.Today.AddDays(6).AddHours(7).AddMinutes(30);
            p4.Lekar = lekari[3];
            p4.Trajanje = 30;
            p4.Zavrsen = false;
            p4.Pacijent = pacijent;
            p4.Anamneza.Id = -1;
        }

        private static void KreirajTermineSpramLekaraIDatuma(Pacijent pacijent, DateTime datum, int sat, int minut, Lekar lekar, out PrikazPregleda p1, out PrikazPregleda p2, out PrikazPregleda p3, out PrikazPregleda p4)
        {
            p1 = new PrikazPregleda();
            p1.Datum = datum.AddHours(sat).AddMinutes(minut);
            p1.Lekar = lekar;
            p1.Trajanje = 30;
            p1.Zavrsen = false;
            p1.Pacijent = pacijent;
            p1.Anamneza.Id = -1;

            p2 = new PrikazPregleda();
            p2.Datum = datum.AddHours(sat + 1).AddMinutes(minut + 15);
            p2.Lekar = lekar;
            p2.Trajanje = 30;
            p2.Zavrsen = false;
            p2.Pacijent = pacijent;
            p2.Anamneza.Id = -1;

            p3 = new PrikazPregleda();
            p3.Datum = datum.AddHours(sat + 2).AddMinutes(minut);
            p3.Lekar = lekar;
            p3.Trajanje = 30;
            p3.Zavrsen = false;
            p3.Pacijent = pacijent;
            p3.Anamneza.Id = -1;

            p4 = new PrikazPregleda();
            p4.Datum = datum.AddHours(sat + 3).AddMinutes(minut + 45);
            p4.Lekar = lekar;
            p4.Trajanje = 30;
            p4.Zavrsen = false;
            p4.Pacijent = pacijent;
            p4.Anamneza.Id = -1;
        }

        private static void KreirajTermineSpramDatuma(Pacijent pacijent, List<Lekar> lekari, DateTime dat, out PrikazPregleda p1, out PrikazPregleda p2, out PrikazPregleda p3, out PrikazPregleda p4)
        {
            p1 = new PrikazPregleda();
            p1.Datum = dat;
            p1.Lekar = lekari[0];
            p1.Trajanje = 30;
            p1.Zavrsen = false;
            p1.Pacijent = pacijent;
            p1.Anamneza.Id = -1;

            p2 = new PrikazPregleda();
            p2.Datum = dat;
            p2.Lekar = lekari[1];
            p2.Trajanje = 30;
            p2.Zavrsen = false;
            p2.Pacijent = pacijent;
            p2.Anamneza.Id = -1;

            p3 = new PrikazPregleda();
            p3.Datum = dat;
            p3.Lekar = lekari[2];
            p3.Trajanje = 30;
            p3.Zavrsen = false;
            p3.Pacijent = pacijent;
            p3.Anamneza.Id = -1;

            p4 = new PrikazPregleda();
            p4.Datum = dat;
            p4.Lekar = lekari[3];
            p4.Trajanje = 30;
            p4.Zavrsen = false;
            p4.Pacijent = pacijent;
            p4.Anamneza.Id = -1;
        }

        private static void KreirajTermineSpramLekara(Pacijent pacijent, Lekar lekar, out PrikazPregleda p1, out PrikazPregleda p2, out PrikazPregleda p3, out PrikazPregleda p4)
        {
            p1 = new PrikazPregleda();
            p1.Datum = DateTime.Today.AddDays(3);
            p1.Lekar = lekar;
            p1.Trajanje = 30;
            p1.Zavrsen = false;
            p1.Pacijent = pacijent;
            p1.Anamneza.Id = -1;

            p2 = new PrikazPregleda();
            p2.Datum = DateTime.Today.AddDays(4).AddHours(17).AddMinutes(30);
            p2.Lekar = lekar;
            p2.Trajanje = 30;
            p2.Zavrsen = false;
            p2.Pacijent = pacijent;
            p2.Anamneza.Id = -1;

            p3 = new PrikazPregleda();
            p3.Datum = DateTime.Today.AddDays(5).AddHours(8);
            p3.Lekar = lekar;
            p3.Trajanje = 30;
            p3.Zavrsen = false;
            p3.Pacijent = pacijent;
            p3.Anamneza.Id = -1;

            p4 = new PrikazPregleda();
            p4.Datum = DateTime.Today.AddDays(6).AddHours(14).AddMinutes(15); ;
            p4.Lekar = lekar;
            p4.Trajanje = 30;
            p4.Zavrsen = false;
            p4.Pacijent = pacijent;
            p4.Anamneza.Id = -1;
        }

        private static void PronadjiSlobodnuProstoriju(List<Prostorija> slobodneProstorije, PrikazPregleda p1, PrikazPregleda p2, PrikazPregleda p3, PrikazPregleda p4)
        {
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
        }

        private void DodajPredlozeneTermine(PrikazPregleda p1, PrikazPregleda p2, PrikazPregleda p3, PrikazPregleda p4)
        {
            p1.Id = servicePregledId.IzracunajIdPregleda();
            p2.Id = servicePregledId.IzracunajIdPregleda();
            p3.Id = servicePregledId.IzracunajIdPregleda();
            p4.Id = servicePregledId.IzracunajIdPregleda();

            FormNasiPredloziPage.PredlozeniTermini.Add(p1);
            FormNasiPredloziPage.PredlozeniTermini.Add(p2);
            FormNasiPredloziPage.PredlozeniTermini.Add(p3);
            FormNasiPredloziPage.PredlozeniTermini.Add(p4);
        }

        public void OdaberiTermin(PrikazPregleda prikazPregleda)
        {
            foreach (PrikazPregleda p in FormNasiPredloziPage.PredlozeniTermini)
            {
                if (p.Equals(prikazPregleda))
                {
                    Pregled pregled = servicePregled.KreirajPregled(p);

                    PacijentPageViewModel.PrikazNezavrsenihPregleda.Add(p);
                    servicePregled.SacuvajPregled(pregled);

                    AntiTrol antiTrol = serviceAntiTrol.KreirajAntiTrol(p);
                    serviceAntiTrol.SacuvajAntiTrol(antiTrol);

                    PacijentPageViewModel pacijentPageViewModel = new PacijentPageViewModel(pregled.Pacijent);
                    FormPacijentWeb.Forma.Pocetna.Content = new FormPacijentPage(pacijentPageViewModel);

                    break;
                }
            }
        }
    }
}
