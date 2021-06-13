using Bolnica.DTO;
using Bolnica.Forms;
using Bolnica.Model.Korisnici;
using Bolnica.Model.Pregledi;
using Bolnica.Model.Prostorije;
using Bolnica.Repository.Prostorije;
using Bolnica.ViewModel;
using Model.Korisnici;
using Model.Pregledi;
using Model.Prostorije;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Bolnica.Service
{
    public class ZakaziPregledPacijentService
    {
        private PregledService servicePregled = new PregledService();
        private FileRepositoryLekar repositoryLekar = new FileRepositoryLekar();
        private FileRepositoryRenoviranje repositoryRenoviranje = new FileRepositoryRenoviranje();
        private AntiTrolService serviceAntiTrol = new AntiTrolService();

        private List<Lekar> lekari = new List<Lekar>();

        public void Potvrdi(ZakazaniPregledDTO pregledDTO)
        {
            DateTime datum = pregledDTO.Datum;
            int dan = datum.Day;
            int mesec = datum.Month;
            int godina = datum.Year;
            int sat = Int32.Parse(pregledDTO.Sat);
            int minut = Int32.Parse(pregledDTO.Minut);
            DateTime datumPregleda = new DateTime(godina, mesec, dan, sat, minut, 0);

            String imeLekara = pregledDTO.Lekar;
            String[] splited = imeLekara.Split(" ");
            string ime = splited[0];
            string prezime = splited[1];

            Lekar lekar = new Lekar();
            lekari = repositoryLekar.GetAll();
            foreach (Lekar l in lekari)
            {
                if (ime.Equals(l.Ime) && prezime.Equals(l.Prezime))
                {
                    lekar = l;
                    break;
                }
            }

            PrikazPregleda prikaz = new PrikazPregleda
            {
                Datum = datumPregleda,
                Lekar = lekar,
                Trajanje = 15,
                Zavrsen = false,
                Pacijent = pregledDTO.Pacijent
            };

            FileRepositoryProstorija storageProstorije = new FileRepositoryProstorija();
            List<Prostorija> prostorije = storageProstorije.GetAll();

            bool slobodna = false;
            foreach (Prostorija p in prostorije)
            {
                if (p.TipProstorije.Equals(TipProstorije.salaZaPreglede) && !p.Obrisana && !NaRenoviranju(p, pregledDTO.Datum))
                {
                    prikaz.Prostorija = p;
                    slobodna = true;
                    break;
                }
            }
            if (!slobodna)
            {
                MessageBox.Show("U izabranom terminu nema slobodnih sala za pregled! Molimo Vas odaberite neki drugi termin.");
            }
            else
            {
                prikaz.Id = servicePregled.IzracunajIdPregleda();
                PacijentPageViewModel.PrikazNezavrsenihPregleda.Add(prikaz);

                Pregled pregled = new Pregled
                {
                    Id = prikaz.Id,
                    Lekar = prikaz.Lekar,
                    Pacijent = prikaz.Pacijent,
                    Prostorija = prikaz.Prostorija,
                    Datum = prikaz.Datum,
                    Trajanje = prikaz.Trajanje,
                    Zavrsen = prikaz.Zavrsen
                };
                pregled.Anamneza.Id = -1;

                servicePregled.SacuvajPregled(pregled);

                AntiTrol antiTrol = serviceAntiTrol.KreirajAntiTrol(prikaz);
                serviceAntiTrol.SacuvajAntiTrol(antiTrol);

                PacijentPageViewModel pacijentPageViewModel = new PacijentPageViewModel(prikaz.Pacijent);
                FormPacijentWeb.Forma.Pocetna.Content = new FormPacijentPage(pacijentPageViewModel);
            }
        }

        public void Otkazi(ZakazaniPregledDTO pregledDTO)
        {
            PacijentPageViewModel pacijentPageViewModel = new PacijentPageViewModel(pregledDTO.Pacijent);
            FormPacijentWeb.Forma.Pocetna.Content = new FormPacijentPage(pacijentPageViewModel);
        }

        public void NasiPredlozi(ZakazaniPregledDTO pregledDTO)
        {
            DateTime datum = new DateTime(1, 1, 1);
            int sat = -1;
            int minut = -1;
            Lekar lekar = new Lekar();

            if (!(pregledDTO.Datum.Equals(new DateTime(1,1,1))))
            {
                datum = pregledDTO.Datum;
            }

            if (!(pregledDTO.Sat is null))
            {
                sat = int.Parse(pregledDTO.Sat);
            }

            if (!(pregledDTO.Minut is null))
            {
                minut = int.Parse(pregledDTO.Minut);
            }

            if (!(pregledDTO.Lekar is null))
            {
                string imeLekara = pregledDTO.Lekar;
                String[] splited = imeLekara.Split(" ");
                string ime = splited[0];
                string prezime = splited[1];
                lekari = repositoryLekar.GetAll();
                foreach (Lekar l in lekari)
                {
                    if (ime.Equals(l.Ime) && prezime.Equals(l.Prezime))
                    {
                        lekar = l;
                        break;
                    }
                }
            }
            FormPacijentWeb.Forma.Pocetna.Content = new FormNasiPredloziPage(pregledDTO.Pacijent, datum, sat, minut, lekar);
        }

        private bool NaRenoviranju(Prostorija p, DateTime datum)
        {
            List<Renoviranje> renoviranja = repositoryRenoviranje.GetAll();
            foreach (Renoviranje r in renoviranja)
            {
                if (p.BrojProstorije.Equals(r.Prostorija.BrojProstorije))
                {
                    if (r.PocetakRenoviranja.Date <= datum && datum <= r.KrajRenoviranja.Date)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
