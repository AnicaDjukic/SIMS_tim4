using Bolnica.DTO;
using Bolnica.Model.Korisnici;
using Bolnica.Model.Pregledi;
using Bolnica.Model.Prostorije;
using Bolnica.Repository.Pregledi;
using Bolnica.Repository.Prostorije;
using Bolnica.ViewModel;
using Model.Korisnici;
using Model.Pacijenti;
using Model.Pregledi;
using Model.Prostorije;
using System;
using System.Collections.Generic;

namespace Bolnica.Services
{
    public class TerminLekarService
    {
        private FileRepositoryPacijent skladistePacijenti = new FileRepositoryPacijent();
        private FileRepositoryProstorija skladisteProstorije = new FileRepositoryProstorija();
        private FileRepositoryPregled skladistePregledi = new FileRepositoryPregled();
        private FileRepositoryOperacija skladisteOperacija = new FileRepositoryOperacija();
        private FileRepositoryLekar skladisteLekari = new FileRepositoryLekar();
        private FileRepositoryRenoviranje skladisteRenoviranja = new FileRepositoryRenoviranje();

        public List<Lekar> DobijLekare()
        {
            return skladisteLekari.GetAll();
        }
        public List<Pacijent> DobijPacijente()
        {
            return skladistePacijenti.GetAll();
        }
        public List<Pregled> DobijPreglede()
        {
            return skladistePregledi.GetAll();
        }
        public List<Operacija> DobijOperacije()
        {
            return skladisteOperacija.GetAll();
        }
        public List<Prostorija> DobijProstorije()
        {
            return skladisteProstorije.GetAll();
        }

        public void PotvrdiIzmenu(TerminLekarDTO terminDTO) {
            bool daLiJeOperacija = false;
            if (CheckFields())
            {
                PrikazOperacije trenutnaOperacija = PopuniOperaciju(terminDTO, ref daLiJeOperacija);
                PrikazPregleda trenutniPregled = PopuniPregled(terminDTO);
                terminDTO.trenutniPregled = trenutniPregled;
                terminDTO.trenutnaOperacija = trenutnaOperacija;


                if (daLiJeOperacija)
                {
                   AzurirajListuOperacija(terminDTO);
                    AzurirajTabeluISkladisteOperacija(terminDTO); 
                }
                else
                {
                    AzurirajListuPregleda(terminDTO);
                    AzurirajTabeluISkladistePregleda(terminDTO);
                }
            }
        }
        private void AzurirajTabeluISkladistePregleda(TerminLekarDTO terminDTO)
        {
            for (int p = 0; p < LekarViewModel.podaciLista.Items.Count; p++)
            {
                if (LekarViewModel.podaciLista.Items[p].Equals(terminDTO.stariPregled))
                {
                    if (terminDTO.ulogovaniLekar.Mbr.Equals(terminDTO.trenutniPregled.Lekar.Mbr))
                    {
                        LekarViewModel.podaciLista.Items[p] = terminDTO.trenutniPregled;
                        LekarViewModel.RefreshPodaciListu();
                        Pregled azuriraniPregled = new Pregled(terminDTO.trenutniPregled);
                        skladistePregledi.Update(azuriraniPregled);
                    }
                    else
                    {
                        LekarViewModel.podaciLista.Items.RemoveAt(p);
                        LekarViewModel.RefreshPodaciListu();
                        Pregled azuriraniPregled = new Pregled(terminDTO.trenutniPregled);
                        skladistePregledi.Update(azuriraniPregled);
                    }
                }
            }
        }
        private void AzurirajListuPregleda(TerminLekarDTO terminDTO)
        {
            for (int i = 0; i < LekarViewModel.listaPregleda.Count; i++)
            {
                if (LekarViewModel.listaPregleda[i].Equals(terminDTO.stariPregled))
                {
                    if (terminDTO.ulogovaniLekar.Mbr.Equals(terminDTO.trenutniPregled.Lekar.Mbr))
                    {
                        LekarViewModel.listaPregleda[i] = new Pregled(terminDTO.trenutniPregled);
                    }
                    else
                    {
                        LekarViewModel.listaPregleda.RemoveAt(i);
                    }
                }
            }
        }
        private void AzurirajTabeluISkladisteOperacija(TerminLekarDTO terminDTO)
        {
            for (int p = 0; p < LekarViewModel.podaciLista.Items.Count; p++)
            {
                if (LekarViewModel.podaciLista.Items[p].Equals(terminDTO.staraOperacija))
                {
                    if (terminDTO.ulogovaniLekar.Mbr.Equals(terminDTO.trenutnaOperacija.Lekar.Mbr))
                    {
                        LekarViewModel.podaciLista.Items[p] = terminDTO.trenutnaOperacija;
                        LekarViewModel.RefreshPodaciListu();
                        Operacija azuriranaOperacija = new Operacija(terminDTO.trenutnaOperacija);
                        skladisteOperacija.Update(azuriranaOperacija);
                    }
                    else
                    {
                        LekarViewModel.podaciLista.Items.RemoveAt(p);
                        LekarViewModel.RefreshPodaciListu();
                        Operacija azuriranaOperacija = new Operacija(terminDTO.trenutnaOperacija);
                        skladisteOperacija.Update(azuriranaOperacija);
                    }
                }
            }
        }
        private void AzurirajListuOperacija(TerminLekarDTO terminDTO)
        {
            for (int i = 0; i < LekarViewModel.listaOperacija.Count; i++)
            {
                if (LekarViewModel.listaOperacija[i].Id.Equals(terminDTO.staraOperacija.Id))
                {
                    if (terminDTO.ulogovaniLekar.Mbr.Equals(terminDTO.trenutnaOperacija.Lekar.Mbr))
                    {
                        LekarViewModel.listaOperacija[i] = new Operacija(terminDTO.trenutnaOperacija);

                    }
                    else
                    {
                        LekarViewModel.listaOperacija.RemoveAt(i);
                    }

                }
            }
        }
        private int RacunajIdOperacije()
        {
            List<Operacija> operacijeZaRacunanjeId = new List<Operacija>();
            operacijeZaRacunanjeId = skladisteOperacija.GetAll();
            int max = 0;
            for (int i = 0; i < operacijeZaRacunanjeId.Count; i++)
            {
                if (operacijeZaRacunanjeId[i].Id > max)
                    max = operacijeZaRacunanjeId[i].Id;
            }
            max = max + 1;
            return max;
        }
        private int RacunajIdPregleda()
        {
            List<Pregled> preglediZaRacunanjeId = new List<Pregled>();
            preglediZaRacunanjeId = skladistePregledi.GetAll();
            int max = 0;
            for (int i = 0; i < preglediZaRacunanjeId.Count; i++)
            {
                if (preglediZaRacunanjeId[i].Id > max)
                    max = preglediZaRacunanjeId[i].Id;
            }
            max = max + 1;
            return max;
        }
        public void Potvrdi(TerminLekarDTO terminDTO)
        {
            Anamneza praznaAnamneza = new Anamneza();
            terminDTO.staraOperacija = new PrikazOperacije(-1, praznaAnamneza);
            terminDTO.stariPregled = new PrikazPregleda(-1, praznaAnamneza);
            bool daLiJeOperacija = false;
            if (CheckFields())
            {
                PrikazOperacije trenutnaOperacija = PopuniOperaciju(terminDTO,  ref daLiJeOperacija);
                PrikazPregleda trenutniPregled = PopuniPregled(terminDTO);           
                if (daLiJeOperacija)
                {
                    trenutnaOperacija.Id = RacunajIdOperacije();
                    trenutnaOperacija.Anamneza.Id = -1;
                    Operacija azuriranaOperacija = new Operacija(trenutnaOperacija);
                    terminDTO.trenutnaOperacija = trenutnaOperacija;
                    terminDTO.azuriranaOperacija = azuriranaOperacija;
                    AzurirajListuTabeluISkladisteOperacije(terminDTO);
                }
                else
                {     
                    trenutniPregled.Id = RacunajIdPregleda();
                    trenutniPregled.Anamneza.Id = -1;
                    trenutniPregled.Hitan = false;
                    Pregled azuriraniPregled = new Pregled(trenutniPregled);
                    terminDTO.trenutniPregled = trenutniPregled;
                    terminDTO.azuriraniPregled = azuriraniPregled;
                    AzurirajListuTabeluISkladistePregleda(terminDTO);
                }
            }
        }
        private void AzurirajListuTabeluISkladistePregleda(TerminLekarDTO terminDTO)
        {
            if (terminDTO.ulogovaniLekar.Mbr.Equals(terminDTO.trenutniPregled.Lekar.Mbr))
            {
                LekarViewModel.listaPregleda.Add(terminDTO.azuriraniPregled);
                LekarViewModel.podaciLista.Items.Add(terminDTO.trenutniPregled);
                LekarViewModel.RefreshPodaciListu();
            }
            skladistePregledi.Save(terminDTO.azuriraniPregled);
        }
        private void AzurirajListuTabeluISkladisteOperacije(TerminLekarDTO terminDTO)
        {
            if (terminDTO.ulogovaniLekar.Mbr.Equals(terminDTO.trenutnaOperacija.Lekar.Mbr))
            {
                LekarViewModel.listaOperacija.Add(terminDTO.azuriranaOperacija);
                LekarViewModel.podaciLista.Items.Add(terminDTO.trenutnaOperacija);
                LekarViewModel.RefreshPodaciListu();
            }
            skladisteOperacija.Save(terminDTO.azuriranaOperacija);
        }
        public bool PostojiLekar(TerminLekarDTO terminDTO)
        {

            List<Lekar> lekari = new List<Lekar>();
            lekari = skladisteLekari.GetAll();
            for (int i = 0; i < lekari.Count; i++)
            {
                string podaciOLekaru = lekari[i].Prezime + ' ' + lekari[i].Ime + ' ' + lekari[i].Jmbg;
                if (lekari[i].Specijalizacija.OblastMedicine.Equals(terminDTO.specijalizacija) && podaciOLekaru.Equals(terminDTO.lekarPodaci))
                {
                    return true;
                }

            }
            return false;

        }
        public bool PacijentSlobodanUToVreme(TerminLekarDTO terminDTO)
        {
            List<TimeSpan> zauzetiTermini = new List<TimeSpan>();
            zauzetiTermini = RacunajZauzeteTerminePacijenta(terminDTO);
            for (int i = 0; i < int.Parse(terminDTO.trajanjePregleda); i++)
            {
                TimeSpan ts = new TimeSpan(0, i, 0);
                if (zauzetiTermini.Contains(TimeSpan.Parse(terminDTO.vremePregledaString) + ts))
                {
                    return false;
                }
            }
            return true;


        }
        public bool PacijentSlobodanUToVremeIzmeni(TerminLekarDTO terminDTO)
        {

            List<TimeSpan> zauzetiTermini = new List<TimeSpan>();
            zauzetiTermini = RacunajZauzeteTerminePacijentaIzmeni(terminDTO);
            for (int i = 0; i < int.Parse(terminDTO.trajanjePregleda); i++)
            {
                TimeSpan ts = new TimeSpan(0, i, 0);
                if (zauzetiTermini.Contains(TimeSpan.Parse(terminDTO.vremePregledaString) + ts))
                {
                    return false;
                }
            }
            return true;


        }
        private List<TimeSpan> RacunajZauzeteTerminePacijentaIzmeni(TerminLekarDTO terminDTO)
        {
            List<Pacijent> pacijenti = skladistePacijenti.GetAll();
            List<TimeSpan> zauzetiTermini = new List<TimeSpan>();
            List<Pregled> preglediPacijenta = skladistePregledi.GetAll();
            List<Operacija> operacijePacijenta = skladisteOperacija.GetAll();
            
            for (int i = 0; i < pacijenti.Count; i++)
            {
                string podaciOPacijentu;
                podaciOPacijentu = pacijenti[i].Prezime + ' ' + pacijenti[i].Ime + ' ' + pacijenti[i].Jmbg;


                if (podaciOPacijentu.Equals(terminDTO.pacijentPodaci))
                {

                    pacijenti = skladistePacijenti.GetAll();
                    terminDTO.sviPacijenti = pacijenti;
                    string jmbgPacijent = DobijJmgbPacijenta(terminDTO);
                    preglediPacijenta = FiltrirajPregledePacijenta(new TerminLekarDTO(preglediPacijenta, jmbgPacijent));
                    operacijePacijenta = FiltrirajOperacijePacijenta(new TerminLekarDTO(operacijePacijenta, jmbgPacijent));
                    terminDTO.zauzetiTermini = zauzetiTermini;
                    terminDTO.preglediPacijenta = preglediPacijenta;
                    terminDTO.operacijePacijenta = operacijePacijenta;
                    zauzetiTermini = DobijZauzeteTerminePacijentaIzmeni(terminDTO);


                }

            }
            return zauzetiTermini;
        }
        private List<TimeSpan> RacunajZauzeteTerminePacijenta(TerminLekarDTO terminDTO)
        {
            List<Pacijent> pacijenti = skladistePacijenti.GetAll();
            List<TimeSpan> zauzetiTermini = new List<TimeSpan>();
            List<Pregled> preglediPacijenta = skladistePregledi.GetAll();
            List<Operacija> operacijePacijenta = skladisteOperacija.GetAll();
            for (int i = 0; i < pacijenti.Count; i++)
            {
                string podaciOPacijentu;
                podaciOPacijentu = terminDTO.sviPacijenti[i].Prezime + ' ' + terminDTO.sviPacijenti[i].Ime + ' ' + terminDTO.sviPacijenti[i].Jmbg;
                if (podaciOPacijentu.Equals(terminDTO.pacijentPodaci))
                {
                    pacijenti = skladistePacijenti.GetAll();
                    string jmbgPacijent = DobijJmgbPacijenta(new TerminLekarDTO(pacijenti, terminDTO.pacijentPodaci));
                    preglediPacijenta = FiltrirajPregledePacijenta(new TerminLekarDTO(preglediPacijenta, jmbgPacijent));
                    operacijePacijenta = FiltrirajOperacijePacijenta(new TerminLekarDTO(operacijePacijenta, jmbgPacijent));
                    zauzetiTermini = DobijZauzeteTerminePacijenta(new TerminLekarDTO(zauzetiTermini, preglediPacijenta, operacijePacijenta, terminDTO.datumPregledaDatum));
                }

            }
            return zauzetiTermini;
        }
        private string DobijJmgbPacijenta(TerminLekarDTO terminDTO)
        {
            string jmbgPacijent = "";
            for (int i = 0; i < terminDTO.sviPacijenti.Count; i++)
            {
                string podaciOPacijentu;
                podaciOPacijentu = terminDTO.sviPacijenti[i].Prezime + ' ' + terminDTO.sviPacijenti[i].Ime + ' ' + terminDTO.sviPacijenti[i].Jmbg;

                if (podaciOPacijentu.Equals(terminDTO.pacijentPodaci))
                {
                    jmbgPacijent = terminDTO.sviPacijenti[i].Jmbg;
                    return jmbgPacijent;
                }
            }
            return jmbgPacijent;
        }
        private List<Pregled> FiltrirajPregledePacijenta(TerminLekarDTO terminDTO)
        {
            for (int i = 0; i < terminDTO.preglediPacijenta.Count; i++)
            {
                if (!terminDTO.preglediPacijenta[i].Pacijent.Jmbg.Equals(terminDTO.jmbgPacijent))
                {
                    terminDTO.preglediPacijenta.RemoveAt(i);
                    i = i - 1;
                }
            }
            return terminDTO.preglediPacijenta;
        }
        private List<Operacija> FiltrirajOperacijePacijenta(TerminLekarDTO terminDTO)
        {
            for (int i = 0; i < terminDTO.operacijePacijenta.Count; i++)
            {
                if (!terminDTO.operacijePacijenta[i].Pacijent.Jmbg.Equals(terminDTO.jmbgPacijent))
                {
                    terminDTO.operacijePacijenta.RemoveAt(i);
                    i = i - 1;
                }
            }
            return terminDTO.operacijePacijenta;
        }
        public bool PostojiProstorija(TerminLekarDTO terminDTO)
        {
            for (int i = 0; i < terminDTO.sveProstorije.Count; i++)
            {

                if (terminDTO.sveProstorije[i].BrojProstorije.ToString().Equals(terminDTO.prostorijaBroj) && !terminDTO.sveProstorije[i].Obrisana)
                {
                    return true;
                }

            }
            return false;
        }
        public bool ProstorijaSlobodna(TerminLekarDTO terminDTO)
        {
            for (int i = 0; i < terminDTO.sveProstorije.Count; i++)
            {

                if (terminDTO.sveProstorije[i].BrojProstorije.ToString().Equals(terminDTO.prostorijaBroj) && !terminDTO.sveProstorije[i].Obrisana)
                {
                    if (naRenoviranju(terminDTO.sveProstorije[i],terminDTO.datumPregledaDatum))
                    {
                        return false;
                    }
                    if (terminDTO.sveProstorije[i].Zauzeta)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }

            }
            return false;

        }
        private bool CheckFields()
        {
            return true;
        }
        private List<TimeSpan> DobijZauzeteTerminePacijentaIzmeni(TerminLekarDTO terminDTO)
        {
            for (int i = 0; i < terminDTO.preglediPacijenta.Count; i++)
            {
                if (terminDTO.preglediPacijenta[i].Datum.Date.Equals(terminDTO.datumPregledaDatum.Date) && !terminDTO.preglediPacijenta[i].Id.Equals(terminDTO.trenutniPregled.Id))
                {
                    string[] datum = terminDTO.preglediPacijenta[i].Datum.ToString().Split(" ");
                    string vreme = datum[1];
                    TimeSpan pocetni = TimeSpan.Parse(vreme);
                    for (int p = 0; p <= terminDTO.preglediPacijenta[i].Trajanje; p++)
                    {
                        TimeSpan dodatni = new TimeSpan(0, p, 0);
                        terminDTO.zauzetiTermini.Add(pocetni + dodatni);
                    }
                }
            }
            for (int i = 0; i < terminDTO.operacijePacijenta.Count; i++)
            {
                if (terminDTO.operacijePacijenta[i].Datum.Date.Equals(terminDTO.datumPregledaDatum.Date) && !terminDTO.operacijePacijenta[i].Id.Equals(terminDTO.trenutnaOperacija.Id))
                {
                    string[] datum = terminDTO.operacijePacijenta[i].Datum.ToString().Split(" ");
                    string vreme = datum[1];
                    TimeSpan pocetni = TimeSpan.Parse(vreme);
                    for (int p = 0; p <= terminDTO.operacijePacijenta[i].Trajanje; p++)
                    {
                        TimeSpan dodatni = new TimeSpan(0, p, 0);
                        terminDTO.zauzetiTermini.Add(pocetni + dodatni);
                    }
                }
            }
            return terminDTO.zauzetiTermini;
        }
        private List<TimeSpan> DobijZauzeteTerminePacijenta(TerminLekarDTO terminDTO)
        {
            for (int i = 0; i < terminDTO.preglediPacijenta.Count; i++)
            {
                if (terminDTO.preglediPacijenta[i].Datum.Date.Equals(terminDTO.datumPregledaDatum.Date))
                {
                    string[] datum = terminDTO.preglediPacijenta[i].Datum.ToString().Split(" ");
                    string vreme = datum[1];
                    TimeSpan pocetni = TimeSpan.Parse(vreme);
                    for (int p = 0; p <= terminDTO.preglediPacijenta[i].Trajanje; p++)
                    {
                        TimeSpan dodatni = new TimeSpan(0, p, 0);
                        terminDTO.zauzetiTermini.Add(pocetni + dodatni);
                    }
                }
            }
            for (int i = 0; i < terminDTO.operacijePacijenta.Count; i++)
            {
                if (terminDTO.operacijePacijenta[i].Datum.Date.Equals(terminDTO.datumPregledaDatum.Date))
                {
                    string[] datum = terminDTO.operacijePacijenta[i].Datum.ToString().Split(" ");
                    string vreme = datum[1];
                    TimeSpan pocetni = TimeSpan.Parse(vreme);
                    for (int p = 0; p <= terminDTO.operacijePacijenta[i].Trajanje; p++)
                    {
                        TimeSpan dodatni = new TimeSpan(0, p, 0);
                        terminDTO.zauzetiTermini.Add(pocetni + dodatni);
                    }
                }
            }
            return terminDTO.zauzetiTermini;
        }
        public List<TimeSpan> LekarFiltriranje(TerminLekarDTO terminDTO)
        {
            if (terminDTO.filtriraniLekar != terminDTO.lekarPodaci)
            {
                terminDTO.predmetStavkiVreme = InicirajVreme();

                terminDTO.predmetStavkiVreme = FiltrirajZauzeteTermineLekara(terminDTO);
               
                return terminDTO.predmetStavkiVreme;

            }
            return null;
        }
        private string DobijJmbgLekara(TerminLekarDTO terminDTO)
        {
            string jmbgLekar="";
            for (int i = 0; i < terminDTO.sviLekari.Count; i++)
            {
                string podaciOLekaru = terminDTO.sviLekari[i].Prezime + ' ' + terminDTO.sviLekari[i].Ime + ' ' + terminDTO.sviLekari[i].Jmbg;
                if (podaciOLekaru.Equals(terminDTO.lekarPodaci))
                {
                    jmbgLekar = terminDTO.sviLekari[i].Jmbg;
                    return jmbgLekar;
                }
            }
            return jmbgLekar;
        }
        private List<TimeSpan> InicirajVreme()
        {
            List<TimeSpan> itemSourceVremeB = new List<TimeSpan>();
            for (int sati = 0; sati < 24; sati++)
            {
                for (int min = 0; min < 59;)
                {
                    TimeSpan ts = new TimeSpan(sati, min, 0);
                    min = min + 15;
                    itemSourceVremeB.Add(ts);
                }

            }
            return itemSourceVremeB;
        }
        public List<string> DatumFiltriranje(TerminLekarDTO terminDTO)
        {
            if (terminDTO.filtriraniDatum != terminDTO.datumPregledaDatum)
            {
                terminDTO.filtriraniDatum = terminDTO.datumPregledaDatum;
                terminDTO.predmetStavkiProstorije = new List<string>();
                for (int i = 0; i < terminDTO.sveProstorije.Count; i++)
                {
                    if (terminDTO.sveProstorije[i].Obrisana == false && terminDTO.sveProstorije[i].Zauzeta == false && terminDTO.sveProstorije[i].TipProstorije.Equals(TipProstorije.salaZaPreglede) && !naRenoviranju(terminDTO.sveProstorije[i],terminDTO.datumPregledaDatum))
                    {
                        terminDTO.predmetStavkiProstorije.Add(terminDTO.sveProstorije[i].BrojProstorije);
                    }
                }
                return terminDTO.predmetStavkiProstorije;
            }
            return null;

        }
        public string LekarComboNaTab(TerminLekarDTO terminDTO)
        {

            if (terminDTO.filtriraniLekar != terminDTO.lekarPodaci)
            {
                terminDTO.filtriraniLekar = terminDTO.lekarPodaci;
                for (int i = 0; i < terminDTO.sviLekari.Count; i++)
                {
                    string podaciOLekaru = terminDTO.sviLekari[i].Prezime + ' ' + terminDTO.sviLekari[i].Ime + ' ' + terminDTO.sviLekari[i].Jmbg;
                    if (podaciOLekaru.Equals(terminDTO.lekarPodaci))
                    {
                        terminDTO.specijalizacija = terminDTO.sviLekari[i].Specijalizacija.OblastMedicine;
                    }

                }
                return terminDTO.specijalizacija;

            }
            return null;

        }
        public List<string> SpecijalizacijaComboNaTab(TerminLekarDTO terminDTO)
        {

            if (terminDTO.specijalizacije.Contains(terminDTO.specijalizacija))
            {
                List<string> filtriraniLekari = new List<string>();
                for (int i = 0; i < terminDTO.sviLekari.Count; i++)
                {
                    if (terminDTO.sviLekari[i].Specijalizacija.OblastMedicine.Equals(terminDTO.specijalizacija))
                    {
                        string podaciOLekaru = terminDTO.sviLekari[i].Prezime + ' ' + terminDTO.sviLekari[i].Ime + ' ' + terminDTO.sviLekari[i].Jmbg;
                        filtriraniLekari.Add(podaciOLekaru);
                    }
                }
                return filtriraniLekari;
            }
            return null;

        }
        public bool LekarSlobodanUToVreme(TerminLekarDTO terminDTO)
        {
            List<TimeSpan> zauzetiTermini = new List<TimeSpan>();
            zauzetiTermini = DobijZauzeteTermineLekara(terminDTO);

            for (int i = 0; i < int.Parse(terminDTO.trajanjePregleda); i++)
            {
                TimeSpan p = new TimeSpan(0, i, 0);
                if (zauzetiTermini.Contains(TimeSpan.Parse(terminDTO.vremePregledaString) + p))
                {
                    return false;
                }

            }
            return true;

        }
        public bool LekarSlobodanUToVremeIzmeni(TerminLekarDTO terminDTO)
        {
            List<TimeSpan> zauzetiTermini = new List<TimeSpan>();
            zauzetiTermini = DobijZauzeteTermineLekaraIzmeni(terminDTO);

            for (int i = 0; i < int.Parse(terminDTO.trajanjePregleda); i++)
            {
                TimeSpan p = new TimeSpan(0, i, 0);
                if (zauzetiTermini.Contains(TimeSpan.Parse(terminDTO.vremePregledaString) + p))
                {
                    return false;
                }

            }
            return true;

        }
        private List<TimeSpan> FiltrirajZauzeteTermineLekara(TerminLekarDTO terminDTO)
        {
            for (int i = 0; i < terminDTO.sviLekari.Count; i++)
            {
                string podaciOLekaru = terminDTO.sviLekari[i].Prezime + ' ' + terminDTO.sviLekari[i].Ime + ' ' + terminDTO.sviLekari[i].Jmbg;
                if (podaciOLekaru.Equals(terminDTO.lekarPodaci) && terminDTO.sviLekari[i].Specijalizacija.OblastMedicine != null)
                {
                    List<TimeSpan> zauzetiTermini = RacunajZauzeteTermineLekara(terminDTO);
                    for (int p = 0; p < zauzetiTermini.Count; p++)
                    {
                        terminDTO.predmetStavkiVreme.Remove(zauzetiTermini[p]);
                    }
                    return terminDTO.predmetStavkiVreme;
                }

            }
            return terminDTO.predmetStavkiVreme;
        }
        private List<TimeSpan> DobijZauzeteTermineLekara(TerminLekarDTO terminDTO)
        {
            List<Lekar> lekari = new List<Lekar>();
            lekari = skladisteLekari.GetAll();
            List<TimeSpan> zauzetiTermini = new List<TimeSpan>();
            List<Pregled> preglediLekara1 = skladistePregledi.GetAll();
            List<Operacija> operacijeLekara1 = skladisteOperacija.GetAll();
            for (int i = 0; i < lekari.Count; i++)
            {
                string podaciOLekaru = lekari[i].Prezime + ' ' + lekari[i].Ime + ' ' + lekari[i].Jmbg;
                if (podaciOLekaru.Equals(terminDTO.lekarPodaci) && lekari[i].Specijalizacija.OblastMedicine != null)
                {
                    zauzetiTermini = RacunajZauzeteTermineLekara(terminDTO);
                }

            }
            return zauzetiTermini;
        }
        private List<TimeSpan> DobijZauzeteTermineLekaraIzmeni(TerminLekarDTO terminDTO)
        {
            List<Lekar> lekari = new List<Lekar>();
            lekari = skladisteLekari.GetAll();
            List<TimeSpan> zauzetiTermini = new List<TimeSpan>();
            List<Pregled> preglediLekara1 = skladistePregledi.GetAll();
            List<Operacija> operacijeLekara1 = skladisteOperacija.GetAll();
            for (int i = 0; i < lekari.Count; i++)
            {
                string podaciOLekaru = lekari[i].Prezime + ' ' + lekari[i].Ime + ' ' + lekari[i].Jmbg;
                if (podaciOLekaru.Equals(terminDTO.lekarPodaci) && lekari[i].Specijalizacija.OblastMedicine != null)
                {
                    zauzetiTermini = RacunajZauzeteTermineLekaraIzmeni(terminDTO);
                }

            }
            return zauzetiTermini;
        }
        private List<TimeSpan> RacunajZauzeteTermineLekara(TerminLekarDTO terminDTO)
        {
            List<TimeSpan> zauzetiTermini = new List<TimeSpan>();
            List<Pregled> preglediLekara = skladistePregledi.GetAll();
            List<Operacija> operacijeLekara = skladisteOperacija.GetAll();
            List<Lekar> lekari = skladisteLekari.GetAll();
            string jmbgLekar = DobijJmbgLekara(new TerminLekarDTO(terminDTO.lekarPodaci, lekari));

            preglediLekara = FiltrirajPregledeLekara(new TerminLekarDTO(preglediLekara, jmbgLekar));
            operacijeLekara = FiltrirajOperacijeLekara(new TerminLekarDTO(operacijeLekara, jmbgLekar));
            zauzetiTermini = OdrediZauzeteTermineIzmeni(new TerminLekarDTO(preglediLekara, operacijeLekara, zauzetiTermini, terminDTO.datumPregledaDatum));
            return zauzetiTermini;
        }
        private List<TimeSpan> RacunajZauzeteTermineLekaraIzmeni(TerminLekarDTO terminDTO)
        {
            List<TimeSpan> zauzetiTermini = new List<TimeSpan>();
            List<Pregled> preglediLekara = skladistePregledi.GetAll();
            List<Operacija> operacijeLekara = skladisteOperacija.GetAll();
            List<Lekar> lekari = skladisteLekari.GetAll();
            string jmbgLekar = DobijJmbgLekara(new TerminLekarDTO(terminDTO.lekarPodaci, lekari));

            preglediLekara = FiltrirajPregledeLekara(new TerminLekarDTO(preglediLekara, jmbgLekar));
            operacijeLekara = FiltrirajOperacijeLekara(new TerminLekarDTO(operacijeLekara, jmbgLekar));
            zauzetiTermini = OdrediZauzeteTermine(new TerminLekarDTO(preglediLekara, operacijeLekara, zauzetiTermini, terminDTO.datumPregledaDatum, terminDTO.trenutniPregled, terminDTO.trenutnaOperacija));
            return zauzetiTermini;
        }
        private List<TimeSpan> OdrediZauzeteTermine(TerminLekarDTO terminDTO)
        {

            for (int i = 0; i < terminDTO.preglediLekara.Count; i++)
            {
                if (terminDTO.preglediLekara[i].Datum.Date.Equals(terminDTO.datumPregledaDatum.Date) && !terminDTO.preglediLekara[i].Id.Equals(terminDTO.trenutniPregled.Id))
                {
                    string[] datum = terminDTO.preglediLekara[i].Datum.ToString().Split(" ");
                    string vreme = datum[1];
                    TimeSpan pocetni = TimeSpan.Parse(vreme);
                    for (int p = 0; p <= terminDTO.preglediLekara[i].Trajanje; p++)
                    {
                        TimeSpan dodatni = new TimeSpan(0, p, 0);
                        terminDTO.zauzetiTermini.Add(pocetni + dodatni);
                    }
                }
            }
            for (int i = 0; i < terminDTO.operacijeLekara.Count; i++)
            {
                if (terminDTO.operacijeLekara[i].Datum.Date.Equals(terminDTO.datumPregledaDatum.Date) && !terminDTO.operacijeLekara[i].Id.Equals(terminDTO.trenutnaOperacija.Id))
                {
                    string[] datum = terminDTO.operacijeLekara[i].Datum.ToString().Split(" ");
                    string vreme = datum[1];
                    TimeSpan pocetni = TimeSpan.Parse(vreme);
                    for (int p = 0; p <= terminDTO.operacijeLekara[i].Trajanje; p++)
                    {
                        TimeSpan dodatni = new TimeSpan(0, p, 0);
                        terminDTO.zauzetiTermini.Add(pocetni + dodatni);
                    }
                }



            }
            return terminDTO.zauzetiTermini;
        }
        private List<TimeSpan> OdrediZauzeteTermineIzmeni(TerminLekarDTO terminDTO)
        {
            for (int i = 0; i < terminDTO.preglediLekara.Count; i++)
            {
                if (terminDTO.preglediLekara[i].Datum.Date.Equals(terminDTO.datumPregledaDatum.Date))
                {
                    string[] datum = terminDTO.preglediLekara[i].Datum.ToString().Split(" ");
                    string vreme = datum[1];
                    TimeSpan pocetni = TimeSpan.Parse(vreme);
                    for (int p = 0; p <= terminDTO.preglediLekara[i].Trajanje; p++)
                    {
                        TimeSpan dodatni = new TimeSpan(0, p, 0);
                        terminDTO.zauzetiTermini.Add(pocetni + dodatni);
                    }
                }
            }
            for (int i = 0; i < terminDTO.operacijeLekara.Count; i++)
            {
                if (terminDTO.operacijeLekara[i].Datum.Date.Equals(terminDTO.datumPregledaDatum.Date))
                {
                    string[] datum = terminDTO.operacijeLekara[i].Datum.ToString().Split(" ");
                    string vreme = datum[1];
                    TimeSpan pocetni = TimeSpan.Parse(vreme);
                    for (int p = 0; p <= terminDTO.operacijeLekara[i].Trajanje; p++)
                    {
                        TimeSpan dodatni = new TimeSpan(0, p, 0);
                        terminDTO.zauzetiTermini.Add(pocetni + dodatni);
                    }
                }



            }
            return terminDTO.zauzetiTermini;
        }
        private List<Operacija> FiltrirajOperacijeLekara(TerminLekarDTO terminDTO)
        {
            for (int i = 0; i < terminDTO.operacijePacijenta.Count; i++)
            {
                if (!terminDTO.operacijePacijenta[i].Lekar.Jmbg.Equals(terminDTO.jmbgPacijent))
                {
                    terminDTO.operacijePacijenta.RemoveAt(i);
                    i = i - 1;
                }
            }
            return terminDTO.operacijePacijenta;
        }
        private List<Pregled> FiltrirajPregledeLekara(TerminLekarDTO terminDTO)
        {
            for (int i = 0; i < terminDTO.preglediPacijenta.Count; i++)
            {
                if (!terminDTO.preglediPacijenta[i].Lekar.Jmbg.Equals(terminDTO.jmbgPacijent))
                {
                    terminDTO.preglediPacijenta.RemoveAt(i);
                    i = i - 1;
                }
            }
            return terminDTO.preglediPacijenta;
        }
        private bool naRenoviranju(Prostorija p,DateTime datumPregledaDatum)
        {
            foreach (Renoviranje r in skladisteRenoviranja.GetAll())
            {
                if (r.Prostorija.BrojProstorije == p.BrojProstorije)
                {
                    if (r.PocetakRenoviranja.Date <= datumPregledaDatum && datumPregledaDatum <= r.KrajRenoviranja.Date)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private PrikazOperacije PopuniOperaciju(TerminLekarDTO terminDTO, ref bool daLiJeOperacija)
        {
            List<Lekar> lekariTrenutni = skladisteLekari.GetAll();
            List<Pacijent> pacijentiZa = skladistePacijenti.GetAll();
            List<Prostorija> prostorijaZa = skladisteProstorije.GetAll();
            PrikazOperacije trenutnaOperacija = new PrikazOperacije(terminDTO.staraOperacija.Id, DateTime.Parse(terminDTO.datumPregledaString + TimeSpan.Parse(terminDTO.vremePregledaString)), int.Parse(terminDTO.trajanjePregleda), false, terminDTO.predmetStavkiDaLiJeHitan, terminDTO.staraOperacija.Anamneza);
            for (int i = 0; i < lekariTrenutni.Count; i++)
            {
                string podaciOLekaru = lekariTrenutni[i].Prezime + ' ' + lekariTrenutni[i].Ime + ' ' + lekariTrenutni[i].Jmbg;
                if (podaciOLekaru.Equals(terminDTO.lekarPodaci))
                {
                    trenutnaOperacija.Lekar = lekariTrenutni[i];
                }
            }
            for (int i = 0; i < pacijentiZa.Count; i++)
            {
                string podaciOPacijentu;
                podaciOPacijentu = pacijentiZa[i].Prezime + ' ' + pacijentiZa[i].Ime + ' ' + pacijentiZa[i].Jmbg;


                if (podaciOPacijentu.Equals(terminDTO.pacijentPodaci))
                {
                    trenutnaOperacija.Pacijent = pacijentiZa[i];
                    break;

                }
            }
            for (int i = 0; i < prostorijaZa.Count; i++)
            {
                if (prostorijaZa[i].BrojProstorije.ToString().Equals(terminDTO.prostorijaBroj))
                {
                    trenutnaOperacija.Prostorija = prostorijaZa[i];
                    break;
                }
            }
            if (terminDTO.predmetStavkiDaLiJeOperacija.Equals(true))
            {
                if (terminDTO.tipOperacije.Equals(TipOperacije.prvaKat))
                {
                    daLiJeOperacija = true;
                    trenutnaOperacija.TipOperacije = TipOperacije.prvaKat;
                }
                else if (terminDTO.tipOperacije.Equals(TipOperacije.drugaKat))
                {
                    daLiJeOperacija = true;
                    trenutnaOperacija.TipOperacije = TipOperacije.drugaKat;
                }
                else if (terminDTO.tipOperacije.Equals(TipOperacije.trecaKat))
                {
                    daLiJeOperacija = true;
                    trenutnaOperacija.TipOperacije = TipOperacije.trecaKat;
                }

            }
            return trenutnaOperacija;
        }
        private PrikazPregleda PopuniPregled(TerminLekarDTO terminDTO)
        {
            PrikazPregleda trenutniPregled = new PrikazPregleda(terminDTO.stariPregled.Id, DateTime.Parse(terminDTO.datumPregledaString + TimeSpan.Parse(terminDTO.vremePregledaString)), int.Parse(terminDTO.trajanjePregleda), false, terminDTO.predmetStavkiDaLiJeHitan, terminDTO.stariPregled.Anamneza);
            List<Lekar> lekariTrenutni = skladisteLekari.GetAll();
            List<Pacijent> pacijentiZa = skladistePacijenti.GetAll();
            List<Prostorija> prostorijaZa = skladisteProstorije.GetAll();
            for (int i = 0; i < lekariTrenutni.Count; i++)
            {
                string podaciOLekaru = lekariTrenutni[i].Prezime + ' ' + lekariTrenutni[i].Ime + ' ' + lekariTrenutni[i].Jmbg;
                if (podaciOLekaru.Equals(terminDTO.lekarPodaci))
                {
                    trenutniPregled.Lekar = lekariTrenutni[i];
                }
            }


            for (int i = 0; i < pacijentiZa.Count; i++)
            {
                string podaciOPacijentu;
                podaciOPacijentu = pacijentiZa[i].Prezime + ' ' + pacijentiZa[i].Ime + ' ' + pacijentiZa[i].Jmbg;


                if (podaciOPacijentu.Equals(terminDTO.pacijentPodaci))
                {
                    trenutniPregled.Pacijent = pacijentiZa[i];
                    break;

                }
            }
            for (int i = 0; i < prostorijaZa.Count; i++)
            {
                if (prostorijaZa[i].BrojProstorije.ToString().Equals(terminDTO.prostorijaBroj))
                {
                    trenutniPregled.Prostorija = prostorijaZa[i];
                    break;
                }
            }
            return trenutniPregled;
        }

    }
}
