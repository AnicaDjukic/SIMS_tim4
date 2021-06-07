using Bolnica.DTO;
using Bolnica.Forms;
using Bolnica.Model.Korisnici;
using Bolnica.Model.Pregledi;
using Bolnica.Model.Prostorije;
using Bolnica.Repository.Korisnici;
using Bolnica.Repository.Prostorije;
using Bolnica.ViewModel;
using Model.Korisnici;
using Model.Pregledi;
using Model.Prostorije;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace Bolnica.Service
{
    public class ZakaziPregledPacijentService
    {
        private FileRepositoryPregled storagePregledi = new FileRepositoryPregled();
        private FileRepositoryLekar storageLekari = new FileRepositoryLekar();
        private FileRepositoryRenoviranje storageRenoviranje = new FileRepositoryRenoviranje();
        private FileRepositoryAntiTrol storageAntiTrol = new FileRepositoryAntiTrol();

        private List<Pregled> pregledi = new List<Pregled>();
        private List<Lekar> lekari = new List<Lekar>();

        public void Potvrdi(ZakazaniPregledDTO pregledDTO)
        {
            //DateTime datum = (DateTime)datumPicker.SelectedDate;
            DateTime datum = pregledDTO.Datum;
            int dan = datum.Day;
            int mesec = datum.Month;
            int godina = datum.Year;
            int sat = Int32.Parse(pregledDTO.Sat);
            int minut = Int32.Parse(pregledDTO.Minut);
            /*int sat = comboSat.SelectedIndex;
            int minut = comboMinut.SelectedIndex * 15;*/
            DateTime datumPregleda = new DateTime(godina, mesec, dan, sat, minut, 0);

            //string imeLekara = comboLekar.Text;
            String imeLekara = pregledDTO.Lekar;
            String[] splited = imeLekara.Split(" ");
            string ime = splited[0];
            string prezime = splited[1];

            Lekar lekar = new Lekar();
            lekari = new FileRepositoryLekar().GetAll();
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
                /*datumPicker.IsEnabled = true;
                datumPicker.Background = Brushes.Aqua;*/
                /*ZakaziPregledPacijentViewModel.DatumEnable = true;*/
            }
            else
            {
                pregledi = storagePregledi.GetAll();
                int max = 0;
                foreach (Pregled p in pregledi)
                {
                    if (p.Id > max)
                    {
                        max = p.Id;
                    }
                }
                prikaz.Id = max + 1;

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

                storagePregledi.Save(pregled);

                AntiTrol antiTrol = new AntiTrol
                {
                    Id = DobijIdAntiTrol(),
                    Pacijent = prikaz.Pacijent,
                    Datum = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second)
                };
                storageAntiTrol.Save(antiTrol);

                PacijentPageViewModel pacijentPageViewModel = new PacijentPageViewModel(prikaz.Pacijent);
                FormPacijentWeb.Forma.Pocetna.Content = new FormPacijentPage(pacijentPageViewModel/*prikaz.Pacijent*/);
            }
        }

        private int DobijIdAntiTrol()
        {
            List<AntiTrol> antiTrolList = storageAntiTrol.GetAll();
            int max = 0;
            foreach (AntiTrol antiTrol in antiTrolList)
            {
                if (antiTrol.Id > max)
                {
                    max = antiTrol.Id;
                }
            }
            return max + 1;
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

            if (!(pregledDTO.Datum.Equals(new DateTime(1,1,1))/*datumPicker.SelectedDate is null*/))
            {
                datum = pregledDTO.Datum;// (DateTime)datumPicker.SelectedDate;
            }

            if (!(pregledDTO.Sat is null)/*comboSat.SelectedIndex >= 0*/)
            {
                sat = int.Parse(pregledDTO.Sat)/*comboSat.SelectedIndex*/;
            }

            if (!(pregledDTO.Minut is null)/*comboMinut.SelectedIndex >= 0*/)
            {
                minut = int.Parse(pregledDTO.Minut)/*comboMinut.SelectedIndex * 15*/;
            }

            if (!(pregledDTO.Lekar is null)/*!comboLekar.Text.Equals("")*/)
            {
                string imeLekara = pregledDTO.Lekar;// comboLekar.Text;
                String[] splited = imeLekara.Split(" ");
                string ime = splited[0];
                string prezime = splited[1];
                lekari = storageLekari.GetAll();
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
            List<Renoviranje> renoviranja = storageRenoviranje.GetAll();
            foreach (Renoviranje r in renoviranja)
            {
                if (p.BrojProstorije.Equals(r.Prostorija.BrojProstorije))
                {
                    if (r.PocetakRenoviranja.Date <= datum/*datumPicker.SelectedDate.Value*/ && /*datumPicker.SelectedDate.Value*/datum <= r.KrajRenoviranja.Date)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
