using Bolnica.Model.Korisnici;
using Bolnica.Model.Pregledi;
using Bolnica.Model.Prostorije;
using Bolnica.ViewModel;
using Model.Korisnici;
using Model.Pacijenti;
using Model.Pregledi;
using Model.Prostorije;
using System;
using System.Collections.Generic;

namespace Bolnica.Services
{
    public class IzmeniINapraviTerminLekarService
    {
        private FileStoragePacijenti skladistePacijenti = new FileStoragePacijenti();
        private FileStorageProstorija skladisteProstorije = new FileStorageProstorija();
        private FileStoragePregledi skladistePregledi = new FileStoragePregledi();
        private FileStorageLekar skladisteLekari = new FileStorageLekar();
        private FileStorageRenoviranje skladisteRenoviranja = new FileStorageRenoviranje();

       

        public void PotvrdiIzmenu(string datumPregleda, string vremePregleda, string trajanjePregleda, string lekarPodaci, string pacijentPodaci, bool predmetStavkiDaLiJeOperacija, TipOperacije tipOperacije, bool predmetStavkiDaLiJeHitan, string prostorijaBroj, Lekar ulogovaniLekar,PrikazOperacije staraOperacija, PrikazPregleda stariPregled) {
            bool daLiJeOperacija = false;
            if (CheckFields())
            {
                PrikazOperacije trenutnaOperacija = PopuniOperaciju(datumPregleda, vremePregleda, trajanjePregleda, lekarPodaci, pacijentPodaci, predmetStavkiDaLiJeOperacija, tipOperacije, predmetStavkiDaLiJeHitan, prostorijaBroj, ulogovaniLekar, staraOperacija, ref daLiJeOperacija);
                PrikazPregleda trenutniPregled = PopuniPregled(datumPregleda, vremePregleda, trajanjePregleda, lekarPodaci, pacijentPodaci, predmetStavkiDaLiJeHitan, prostorijaBroj, stariPregled);
                
                if (daLiJeOperacija)
                {
                   AzurirajListuOperacija(ulogovaniLekar, staraOperacija, trenutnaOperacija);
                    AzurirajTabeluISkladisteOperacija(ulogovaniLekar, staraOperacija, trenutnaOperacija); 
                }
                else
                {
                    AzurirajListuPregleda(ulogovaniLekar, stariPregled, trenutniPregled);
                    AzurirajTabeluISkladistePregleda(ulogovaniLekar, stariPregled, trenutniPregled);
                }
            }
        }

        public void AzurirajTabeluISkladistePregleda(Lekar ulogovaniLekar, PrikazPregleda stariPregled, PrikazPregleda trenutniPregled)
        {
            for (int p = 0; p < LekarViewModel.podaciLista.Items.Count; p++)
            {
                if (LekarViewModel.podaciLista.Items[p].Equals(stariPregled))
                {
                    if (ulogovaniLekar.Mbr.Equals(trenutniPregled.Lekar.Mbr))
                    {
                        LekarViewModel.podaciLista.Items[p] = trenutniPregled;
                        LekarViewModel.RefreshPodaciListu();
                        Pregled azuriraniPregled = new Pregled(trenutniPregled);
                        skladistePregledi.Izmeni(azuriraniPregled);
                    }
                    else
                    {
                        LekarViewModel.podaciLista.Items.RemoveAt(p);
                        LekarViewModel.RefreshPodaciListu();
                        Pregled azuriraniPregled = new Pregled(trenutniPregled);
                        skladistePregledi.Izmeni(azuriraniPregled);
                    }
                }
            }
        }

        public void AzurirajListuPregleda(Lekar ulogovaniLekar, PrikazPregleda stariPregled, PrikazPregleda trenutniPregled)
        {
            for (int i = 0; i < LekarViewModel.listaPregleda.Count; i++)
            {
                if (LekarViewModel.listaPregleda[i].Equals(stariPregled))
                {
                    if (ulogovaniLekar.Mbr.Equals(trenutniPregled.Lekar.Mbr))
                    {
                        LekarViewModel.listaPregleda[i] = new Pregled(trenutniPregled);
                    }
                    else
                    {
                        LekarViewModel.listaPregleda.RemoveAt(i);
                    }
                }
            }
        }
       
        public void AzurirajTabeluISkladisteOperacija(Lekar ulogovaniLekar, PrikazOperacije staraOperacija, PrikazOperacije trenutnaOperacija)
        {
            for (int p = 0; p < LekarViewModel.podaciLista.Items.Count; p++)
            {
                if (LekarViewModel.podaciLista.Items[p].Equals(staraOperacija))
                {
                    if (ulogovaniLekar.Mbr.Equals(trenutnaOperacija.Lekar.Mbr))
                    {
                        LekarViewModel.podaciLista.Items[p] = trenutnaOperacija;
                        LekarViewModel.RefreshPodaciListu();
                        Operacija azuriranaOperacija = new Operacija(trenutnaOperacija);
                        skladistePregledi.Izmeni(azuriranaOperacija);
                    }
                    else
                    {
                        LekarViewModel.podaciLista.Items.RemoveAt(p);
                        LekarViewModel.RefreshPodaciListu();
                        Operacija azuriranaOperacija = new Operacija(trenutnaOperacija);
                        skladistePregledi.Izmeni(azuriranaOperacija);
                    }
                }
            }
        }
        public void AzurirajListuOperacija(Lekar ulogovaniLekar, PrikazOperacije staraOperacija, PrikazOperacije trenutnaOperacija)
        {
            for (int i = 0; i < LekarViewModel.listaOperacija.Count; i++)
            {
                if (LekarViewModel.listaOperacija[i].Id.Equals(staraOperacija.Id))
                {
                    if (ulogovaniLekar.Mbr.Equals(trenutnaOperacija.Lekar.Mbr))
                    {
                        LekarViewModel.listaOperacija[i] = new Operacija(trenutnaOperacija);

                    }
                    else
                    {
                        LekarViewModel.listaOperacija.RemoveAt(i);
                    }

                }
            }
        }
        public int RacunajIdOperacije()
        {
            List<Operacija> operacijeZaRacunanjeId = new List<Operacija>();
            operacijeZaRacunanjeId = skladistePregledi.GetAllOperacije();
            int max = 0;
            for (int i = 0; i < operacijeZaRacunanjeId.Count; i++)
            {
                if (operacijeZaRacunanjeId[i].Id > max)
                    max = operacijeZaRacunanjeId[i].Id;
            }
            max = max + 1;
            return max;
        }
        public int RacunajIdPregleda()
        {
            List<Pregled> preglediZaRacunanjeId = new List<Pregled>();
            preglediZaRacunanjeId = skladistePregledi.GetAllPregledi();
            int max = 0;
            for (int i = 0; i < preglediZaRacunanjeId.Count; i++)
            {
                if (preglediZaRacunanjeId[i].Id > max)
                    max = preglediZaRacunanjeId[i].Id;
            }
            max = max + 1;
            return max;
        }
        public void Potvrdi(string datumPregleda, string vremePregleda, string trajanjePregleda, List<Lekar> sviLekari, string lekarPodaci, List<Pacijent> sviPacijenti, string pacijentPodaci, List<Prostorija> sveProstorije, bool predmetStavkiDaLiJeOperacija, TipOperacije tipOperacije, bool predmetStavkiDaLiJeHitan, string prostorijaBroj, Lekar ulogovaniLekar)
        {
            Anamneza praznaAnamneza = new Anamneza();
            bool daLiJeOperacija = false;
            if (CheckFields())
            {
                PrikazOperacije trenutnaOperacija = PopuniOperaciju(datumPregleda, vremePregleda, trajanjePregleda, lekarPodaci, pacijentPodaci, predmetStavkiDaLiJeOperacija, tipOperacije, predmetStavkiDaLiJeHitan, prostorijaBroj, ulogovaniLekar, new PrikazOperacije(-1, praznaAnamneza), ref daLiJeOperacija);
                PrikazPregleda trenutniPregled = PopuniPregled(datumPregleda, vremePregleda, trajanjePregleda, lekarPodaci, pacijentPodaci, predmetStavkiDaLiJeHitan, prostorijaBroj, new PrikazPregleda(-1, praznaAnamneza));           
                if (daLiJeOperacija)
                {
                    trenutnaOperacija.Id = RacunajIdOperacije();
                    trenutnaOperacija.Anamneza.Id = -1;
                    Operacija azuriranaOperacija = new Operacija(trenutnaOperacija);
                    AzurirajListuTabeluISkladisteOperacije(ulogovaniLekar, trenutnaOperacija, azuriranaOperacija);
                }
                else
                {     
                    trenutniPregled.Id = RacunajIdPregleda();
                    trenutniPregled.Anamneza.Id = -1;
                    trenutniPregled.Hitan = false;
                    Pregled azuriraniPregled = new Pregled(trenutniPregled);
                    AzurirajListuTabeluISkladistePregleda(ulogovaniLekar, trenutniPregled, azuriraniPregled);
                }
            }
        }
        public void AzurirajListuTabeluISkladistePregleda(Lekar ulogovaniLekar, PrikazPregleda trenutniPregled, Pregled azuriraniPregled)
        {
            if (ulogovaniLekar.Mbr.Equals(trenutniPregled.Lekar.Mbr))
            {
                LekarViewModel.listaPregleda.Add(azuriraniPregled);
                LekarViewModel.podaciLista.Items.Add(trenutniPregled);
                LekarViewModel.RefreshPodaciListu();
            }
            skladistePregledi.Save(azuriraniPregled);
        }
        public void AzurirajListuTabeluISkladisteOperacije(Lekar ulogovaniLekar,PrikazOperacije trenutnaOperacija,Operacija azuriranaOperacija)
        {
            if (ulogovaniLekar.Mbr.Equals(trenutnaOperacija.Lekar.Mbr))
            {
                LekarViewModel.listaOperacija.Add(azuriranaOperacija);
                LekarViewModel.podaciLista.Items.Add(trenutnaOperacija);
                LekarViewModel.RefreshPodaciListu();
            }
            skladistePregledi.Save(azuriranaOperacija);
        }

        public bool PostojiLekar(string specijalizacija, string lekarPodaci)
        {

            List<Lekar> lekari = new List<Lekar>();
            lekari = skladisteLekari.GetAll();
            for (int i = 0; i < lekari.Count; i++)
            {
                string podaciOLekaru = lekari[i].Prezime + ' ' + lekari[i].Ime + ' ' + lekari[i].Jmbg;
                if (lekari[i].Specijalizacija.OblastMedicine.Equals(specijalizacija) && podaciOLekaru.Equals(lekarPodaci))
                {
                    return true;
                }

            }
            return false;

        }

        
        public bool PacijentSlobodanUToVreme(List<Pacijent> sviPacijenti, string pacijentPodaci, DateTime datumPregleda, string trajanjePregleda, string vremePregleda)
        {
            List<TimeSpan> zauzetiTermini = new List<TimeSpan>();
            zauzetiTermini = RacunajZauzeteTerminePacijenta(sviPacijenti, pacijentPodaci, datumPregleda, trajanjePregleda, vremePregleda);
            for (int i = 0; i < int.Parse(trajanjePregleda); i++)
            {
                TimeSpan ts = new TimeSpan(0, i, 0);
                if (zauzetiTermini.Contains(TimeSpan.Parse(vremePregleda) + ts))
                {
                    return false;
                }
            }
            return true;


        }

        public bool PacijentSlobodanUToVremeIzmeni(string pacijentPodaci, DateTime datumPregleda, string trajanjePregleda, string vremePregleda, PrikazPregleda trenutniPregled, PrikazOperacije trenutnaOperacija)
        {

            List<TimeSpan> zauzetiTermini = new List<TimeSpan>();
            zauzetiTermini = RacunajZauzeteTerminePacijenta(pacijentPodaci,datumPregleda,trajanjePregleda,vremePregleda,trenutniPregled,trenutnaOperacija);
            for (int i = 0; i < int.Parse(trajanjePregleda); i++)
            {
                TimeSpan ts = new TimeSpan(0, i, 0);
                if (zauzetiTermini.Contains(TimeSpan.Parse(vremePregleda) + ts))
                {
                    return false;
                }
            }
            return true;


        }
        public List<TimeSpan> RacunajZauzeteTerminePacijenta(string pacijentPodaci, DateTime datumPregleda, string trajanjePregleda, string vremePregleda, PrikazPregleda trenutniPregled, PrikazOperacije trenutnaOperacija)
        {
            List<Pacijent> pacijenti = skladistePacijenti.GetAll();
            List<TimeSpan> zauzetiTermini = new List<TimeSpan>();
            List<Pregled> preglediPacijenta = skladistePregledi.GetAllPregledi();
            List<Operacija> operacijePacijenta = skladistePregledi.GetAllOperacije();
            for (int i = 0; i < pacijenti.Count; i++)
            {
                string podaciOPacijentu;
                podaciOPacijentu = pacijenti[i].Prezime + ' ' + pacijenti[i].Ime + ' ' + pacijenti[i].Jmbg;


                if (podaciOPacijentu.Equals(pacijentPodaci))
                {

                    pacijenti = skladistePacijenti.GetAll();
                    string jmbgPacijent = DobijJmgbPacijenta(pacijenti, pacijentPodaci);
                    preglediPacijenta = FiltrirajPregledePacijenta(preglediPacijenta, jmbgPacijent);
                    operacijePacijenta = FiltrirajOperacijePacijenta(operacijePacijenta, jmbgPacijent);
                    zauzetiTermini = DobijZauzeteTerminePacijenta(zauzetiTermini, preglediPacijenta, operacijePacijenta, datumPregleda, trenutniPregled, trenutnaOperacija);


                }

            }
            return zauzetiTermini;
        }
        public List<TimeSpan> RacunajZauzeteTerminePacijenta(List<Pacijent> sviPacijenti, string pacijentPodaci, DateTime datumPregleda, string trajanjePregleda, string vremePregleda)
        {
            List<Pacijent> pacijenti = skladistePacijenti.GetAll();
            List<TimeSpan> zauzetiTermini = new List<TimeSpan>();
            List<Pregled> preglediPacijenta = skladistePregledi.GetAllPregledi();
            List<Operacija> operacijePacijenta = skladistePregledi.GetAllOperacije();
            for (int i = 0; i < pacijenti.Count; i++)
            {
                string podaciOPacijentu;
                podaciOPacijentu = sviPacijenti[i].Prezime + ' ' + sviPacijenti[i].Ime + ' ' + sviPacijenti[i].Jmbg;
                if (podaciOPacijentu.Equals(pacijentPodaci))
                {
                    pacijenti = skladistePacijenti.GetAll();
                    string jmbgPacijent = DobijJmgbPacijenta(pacijenti, pacijentPodaci);
                    preglediPacijenta = FiltrirajPregledePacijenta(preglediPacijenta, jmbgPacijent);
                    operacijePacijenta = FiltrirajOperacijePacijenta(operacijePacijenta, jmbgPacijent);
                    zauzetiTermini = DobijZauzeteTerminePacijenta(zauzetiTermini, preglediPacijenta, operacijePacijenta, datumPregleda);
                }

            }
            return zauzetiTermini;
        }

        public string DobijJmgbPacijenta(List<Pacijent> sviPacijenti,string pacijentPodaci)
        {
            string jmbgPacijent = "";
            for (int i = 0; i < sviPacijenti.Count; i++)
            {
                string podaciOPacijentu;
                podaciOPacijentu = sviPacijenti[i].Prezime + ' ' + sviPacijenti[i].Ime + ' ' + sviPacijenti[i].Jmbg;

                if (podaciOPacijentu.Equals(pacijentPodaci))
                {
                    jmbgPacijent = sviPacijenti[i].Jmbg;
                    return jmbgPacijent;
                }
            }
            return jmbgPacijent;
        }
        public List<Pregled> FiltrirajPregledePacijenta(List<Pregled> preglediPacijenta,string jmbgPacijent)
        {
            for (int i = 0; i < preglediPacijenta.Count; i++)
            {
                if (!preglediPacijenta[i].Pacijent.Jmbg.Equals(jmbgPacijent))
                {
                    preglediPacijenta.RemoveAt(i);
                    i = i - 1;
                }
            }
            return preglediPacijenta;
        }

        public List<Operacija> FiltrirajOperacijePacijenta(List<Operacija> operacijePacijenta,string jmbgPacijent)
        {
            for (int i = 0; i < operacijePacijenta.Count; i++)
            {
                if (!operacijePacijenta[i].Pacijent.Jmbg.Equals(jmbgPacijent))
                {
                    operacijePacijenta.RemoveAt(i);
                    i = i - 1;
                }
            }
            return operacijePacijenta;
        }

        public bool PostojiProstorija(List<Prostorija> sveProstorije, string prostorijaBroj)
        {
            for (int i = 0; i < sveProstorije.Count; i++)
            {

                if (sveProstorije[i].BrojProstorije.ToString().Equals(prostorijaBroj) && !sveProstorije[i].Obrisana)
                {
                    return true;
                }

            }
            return false;
        }

        public bool ProstorijaSlobodna(List<Prostorija> sveProstorije, string prostorijaBroj, DateTime datumPregleda)
        {
            for (int i = 0; i < sveProstorije.Count; i++)
            {

                if (sveProstorije[i].BrojProstorije.ToString().Equals(prostorijaBroj) && !sveProstorije[i].Obrisana)
                {
                    if (naRenoviranju(sveProstorije[i], datumPregleda))
                    {
                        return false;
                    }
                    if (sveProstorije[i].Zauzeta)
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

        public bool CheckFields()
        {
            return true;
        }

        public List<TimeSpan> DobijZauzeteTerminePacijenta(List<TimeSpan> zauzetiTermini, List<Pregled> preglediPacijenta, List<Operacija> operacijePacijenta, DateTime datumPregleda, PrikazPregleda trenutniPregled, PrikazOperacije trenutnaOperacija)
        {
            for (int i = 0; i < preglediPacijenta.Count; i++)
            {
                if (preglediPacijenta[i].Datum.Date.Equals(datumPregleda.Date) && !preglediPacijenta[i].Id.Equals(trenutniPregled.Id))
                {
                    string[] datum = preglediPacijenta[i].Datum.ToString().Split(" ");
                    string vreme = datum[1];
                    TimeSpan pocetni = TimeSpan.Parse(vreme);
                    for (int p = 0; p <= preglediPacijenta[i].Trajanje; p++)
                    {
                        TimeSpan dodatni = new TimeSpan(0, p, 0);
                        zauzetiTermini.Add(pocetni + dodatni);
                    }
                }
            }
            for (int i = 0; i < operacijePacijenta.Count; i++)
            {
                if (operacijePacijenta[i].Datum.Date.Equals(datumPregleda.Date) && !operacijePacijenta[i].Id.Equals(trenutnaOperacija.Id))
                {
                    string[] datum = operacijePacijenta[i].Datum.ToString().Split(" ");
                    string vreme = datum[1];
                    TimeSpan pocetni = TimeSpan.Parse(vreme);
                    for (int p = 0; p <= operacijePacijenta[i].Trajanje; p++)
                    {
                        TimeSpan dodatni = new TimeSpan(0, p, 0);
                        zauzetiTermini.Add(pocetni + dodatni);
                    }
                }
            }
            return zauzetiTermini;
        }

        public List<TimeSpan> DobijZauzeteTerminePacijenta(List<TimeSpan> zauzetiTermini, List<Pregled> preglediPacijenta, List<Operacija> operacijePacijenta, DateTime datumPregleda)
        {
            for (int i = 0; i < preglediPacijenta.Count; i++)
            {
                if (preglediPacijenta[i].Datum.Date.Equals(datumPregleda.Date))
                {
                    string[] datum = preglediPacijenta[i].Datum.ToString().Split(" ");
                    string vreme = datum[1];
                    TimeSpan pocetni = TimeSpan.Parse(vreme);
                    for (int p = 0; p <= preglediPacijenta[i].Trajanje; p++)
                    {
                        TimeSpan dodatni = new TimeSpan(0, p, 0);
                        zauzetiTermini.Add(pocetni + dodatni);
                    }
                }
            }
            for (int i = 0; i < operacijePacijenta.Count; i++)
            {
                if (operacijePacijenta[i].Datum.Date.Equals(datumPregleda.Date))
                {
                    string[] datum = operacijePacijenta[i].Datum.ToString().Split(" ");
                    string vreme = datum[1];
                    TimeSpan pocetni = TimeSpan.Parse(vreme);
                    for (int p = 0; p <= operacijePacijenta[i].Trajanje; p++)
                    {
                        TimeSpan dodatni = new TimeSpan(0, p, 0);
                        zauzetiTermini.Add(pocetni + dodatni);
                    }
                }
            }
            return zauzetiTermini;
        }

        public List<TimeSpan> LekarFiltriranje(List<TimeSpan> predmetStavkiVreme, string filtriraniLekar, List<Lekar> sviLekari, string lekarPodaci, DateTime datumPregleda)
        {
            if (filtriraniLekar != lekarPodaci)
            {
                predmetStavkiVreme = InicirajVreme();

                predmetStavkiVreme = FiltrirajZauzeteTermineLekara(predmetStavkiVreme, sviLekari, lekarPodaci, datumPregleda);
               
                return predmetStavkiVreme;

            }
            return null;
        }
       
        public string DobijJmbgLekara(string lekarPodaci,List<Lekar> sviLekari)
        {
            string jmbgLekar="";
            for (int i = 0; i < sviLekari.Count; i++)
            {
                string podaciOLekaru = sviLekari[i].Prezime + ' ' + sviLekari[i].Ime + ' ' + sviLekari[i].Jmbg;
                if (podaciOLekaru.Equals(lekarPodaci))
                {
                    jmbgLekar = sviLekari[i].Jmbg;
                    return jmbgLekar;
                }
            }
            return jmbgLekar;
        }
        public List<TimeSpan> InicirajVreme()
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

        public List<string> DatumFiltriranje(DateTime filtriraniDatum, DateTime datumPregleda, List<Prostorija> sveProstorije,List<string> predmetStavkiProstorije)
        {
            if (filtriraniDatum != datumPregleda)
            {
                filtriraniDatum = datumPregleda;
                predmetStavkiProstorije = new List<string>();
                for (int i = 0; i < sveProstorije.Count; i++)
                {
                    if (sveProstorije[i].Obrisana == false && sveProstorije[i].Zauzeta == false && sveProstorije[i].TipProstorije.Equals(TipProstorije.salaZaPreglede) && !naRenoviranju(sveProstorije[i], datumPregleda))
                    {
                        predmetStavkiProstorije.Add(sveProstorije[i].BrojProstorije);
                    }
                }
                return predmetStavkiProstorije;
            }
            return null;

        }

        public string LekarComboNaTab(string filtriraniLekar, string lekarPodaci, List<Lekar> sviLekari, string specijalizacija)
        {

            if (filtriraniLekar != lekarPodaci)
            {
                filtriraniLekar = lekarPodaci;
                for (int i = 0; i < sviLekari.Count; i++)
                {
                    string podaciOLekaru = sviLekari[i].Prezime + ' ' + sviLekari[i].Ime + ' ' + sviLekari[i].Jmbg;
                    if (podaciOLekaru.Equals(lekarPodaci))
                    {
                        specijalizacija = sviLekari[i].Specijalizacija.OblastMedicine;
                    }

                }
                return specijalizacija;

            }
            return null;

        }

        public List<string> SpecijalizacijaComboNaTab(List<string> specijalizacije, String specijalizacija, List<Lekar> sviLekari)
        {

            if (specijalizacije.Contains(specijalizacija))
            {
                List<string> filtriraniLekari = new List<string>();
                for (int i = 0; i < sviLekari.Count; i++)
                {
                    if (sviLekari[i].Specijalizacija.OblastMedicine.Equals(specijalizacija))
                    {
                        string podaciOLekaru = sviLekari[i].Prezime + ' ' + sviLekari[i].Ime + ' ' + sviLekari[i].Jmbg;
                        filtriraniLekari.Add(podaciOLekaru);
                    }
                }
                return filtriraniLekari;
            }
            return null;

        }


        public bool LekarSlobodanUToVreme(string lekarPodaci, DateTime datumPregleda, string trajanjePregleda, string vremePregleda)
        {
            List<TimeSpan> zauzetiTermini = new List<TimeSpan>();
            zauzetiTermini = DobijZauzeteTermineLekara(lekarPodaci, datumPregleda);

            for (int i = 0; i < int.Parse(trajanjePregleda); i++)
            {
                TimeSpan p = new TimeSpan(0, i, 0);
                if (zauzetiTermini.Contains(TimeSpan.Parse(vremePregleda) + p))
                {
                    return false;
                }

            }
            return true;

        }
        public bool LekarSlobodanUToVremeIzmeni(string lekarPodaci, DateTime datumPregleda, string trajanjePregleda, string vremePregleda, PrikazPregleda trenutniPregled, PrikazOperacije trenutnaOperacija)
        {
            List<TimeSpan> zauzetiTermini = new List<TimeSpan>();
            zauzetiTermini = DobijZauzeteTermineLekara(lekarPodaci, datumPregleda, trenutniPregled, trenutnaOperacija);

            for (int i = 0; i < int.Parse(trajanjePregleda); i++)
            {
                TimeSpan p = new TimeSpan(0, i, 0);
                if (zauzetiTermini.Contains(TimeSpan.Parse(vremePregleda) + p))
                {
                    return false;
                }

            }
            return true;

        }


        public List<TimeSpan> FiltrirajZauzeteTermineLekara(List<TimeSpan> vremePregleda, List<Lekar> sviLekari, string lekarPodaci, DateTime datumPregleda)
        {
            for (int i = 0; i < sviLekari.Count; i++)
            {
                string podaciOLekaru = sviLekari[i].Prezime + ' ' + sviLekari[i].Ime + ' ' + sviLekari[i].Jmbg;
                if (podaciOLekaru.Equals(lekarPodaci) && sviLekari[i].Specijalizacija.OblastMedicine != null)
                {
                    List<TimeSpan> zauzetiTermini = RacunajZauzeteTermineLekara(datumPregleda, lekarPodaci);
                    for (int p = 0; p < zauzetiTermini.Count; p++)
                    {
                        vremePregleda.Remove(zauzetiTermini[p]);
                    }
                    return vremePregleda;
                }

            }
            return vremePregleda;
        }

        public List<TimeSpan> DobijZauzeteTermineLekara(string lekarPodaci, DateTime datumPregleda)
        {
            List<Lekar> lekari = new List<Lekar>();
            lekari = skladisteLekari.GetAll();
            List<TimeSpan> zauzetiTermini = new List<TimeSpan>();
            List<Pregled> preglediLekara1 = skladistePregledi.GetAllPregledi();
            List<Operacija> operacijeLekara1 = skladistePregledi.GetAllOperacije();
            for (int i = 0; i < lekari.Count; i++)
            {
                string podaciOLekaru = lekari[i].Prezime + ' ' + lekari[i].Ime + ' ' + lekari[i].Jmbg;
                if (podaciOLekaru.Equals(lekarPodaci) && lekari[i].Specijalizacija.OblastMedicine != null)
                {
                    zauzetiTermini = RacunajZauzeteTermineLekara(datumPregleda, lekarPodaci);
                }

            }
            return zauzetiTermini;
        }

        public List<TimeSpan> DobijZauzeteTermineLekara(string lekarPodaci, DateTime datumPregleda, PrikazPregleda trenutniPregled, PrikazOperacije trenutnaOperacija)
        {
            List<Lekar> lekari = new List<Lekar>();
            lekari = skladisteLekari.GetAll();
            List<TimeSpan> zauzetiTermini = new List<TimeSpan>();
            List<Pregled> preglediLekara1 = skladistePregledi.GetAllPregledi();
            List<Operacija> operacijeLekara1 = skladistePregledi.GetAllOperacije();
            for (int i = 0; i < lekari.Count; i++)
            {
                string podaciOLekaru = lekari[i].Prezime + ' ' + lekari[i].Ime + ' ' + lekari[i].Jmbg;
                if (podaciOLekaru.Equals(lekarPodaci) && lekari[i].Specijalizacija.OblastMedicine != null)
                {
                    zauzetiTermini = RacunajZauzeteTermineLekara(datumPregleda, lekarPodaci, trenutniPregled, trenutnaOperacija);
                }

            }
            return zauzetiTermini;
        }
        public List<TimeSpan> RacunajZauzeteTermineLekara(DateTime datumPregleda, string lekarPodaci)
        {
            List<TimeSpan> zauzetiTermini = new List<TimeSpan>();
            List<Pregled> preglediLekara = skladistePregledi.GetAllPregledi();
            List<Operacija> operacijeLekara = skladistePregledi.GetAllOperacije();
            List<Lekar> lekari = skladisteLekari.GetAll();
            string jmbgLekar = DobijJmbgLekara(lekarPodaci, lekari);

            preglediLekara = FiltrirajPregledeLekara(preglediLekara, jmbgLekar);
            operacijeLekara = FiltrirajOperacijeLekara(operacijeLekara, jmbgLekar);
            zauzetiTermini = OdrediZauzeteTermine(preglediLekara, operacijeLekara, zauzetiTermini, datumPregleda);
            return zauzetiTermini;
        }
        public List<TimeSpan> RacunajZauzeteTermineLekara(DateTime datumPregleda, string lekarPodaci, PrikazPregleda trenutniPregled, PrikazOperacije trenutnaOperacija)
        {
            List<TimeSpan> zauzetiTermini = new List<TimeSpan>();
            List<Pregled> preglediLekara = skladistePregledi.GetAllPregledi();
            List<Operacija> operacijeLekara = skladistePregledi.GetAllOperacije();
            List<Lekar> lekari = skladisteLekari.GetAll();
            string jmbgLekar = DobijJmbgLekara(lekarPodaci, lekari);

            preglediLekara = FiltrirajPregledeLekara(preglediLekara, jmbgLekar);
            operacijeLekara = FiltrirajOperacijeLekara(operacijeLekara, jmbgLekar);
            zauzetiTermini = OdrediZauzeteTermine(preglediLekara, operacijeLekara, zauzetiTermini, datumPregleda, trenutniPregled, trenutnaOperacija);
            return zauzetiTermini;
        }

        public List<TimeSpan> OdrediZauzeteTermine(List<Pregled> preglediLekara, List<Operacija> operacijeLekara, List<TimeSpan> zauzetiTermini, DateTime datumPregleda, PrikazPregleda trenutniPregled, PrikazOperacije trenutnaOperacija)
        {

            for (int i = 0; i < preglediLekara.Count; i++)
            {
                if (preglediLekara[i].Datum.Date.Equals(datumPregleda.Date) && !preglediLekara[i].Id.Equals(trenutniPregled.Id))
                {
                    string[] datum = preglediLekara[i].Datum.ToString().Split(" ");
                    string vreme = datum[1];
                    TimeSpan pocetni = TimeSpan.Parse(vreme);
                    for (int p = 0; p <= preglediLekara[i].Trajanje; p++)
                    {
                        TimeSpan dodatni = new TimeSpan(0, p, 0);
                        zauzetiTermini.Add(pocetni + dodatni);
                    }
                }
            }
            for (int i = 0; i < operacijeLekara.Count; i++)
            {
                if (operacijeLekara[i].Datum.Date.Equals(datumPregleda.Date) && !operacijeLekara[i].Id.Equals(trenutnaOperacija.Id))
                {
                    string[] datum = operacijeLekara[i].Datum.ToString().Split(" ");
                    string vreme = datum[1];
                    TimeSpan pocetni = TimeSpan.Parse(vreme);
                    for (int p = 0; p <= operacijeLekara[i].Trajanje; p++)
                    {
                        TimeSpan dodatni = new TimeSpan(0, p, 0);
                        zauzetiTermini.Add(pocetni + dodatni);
                    }
                }



            }
            return zauzetiTermini;
        }
        public List<TimeSpan> OdrediZauzeteTermine(List<Pregled> preglediLekara, List<Operacija> operacijeLekara, List<TimeSpan> zauzetiTermini, DateTime datumPregleda)
        {
            for (int i = 0; i < preglediLekara.Count; i++)
            {
                if (preglediLekara[i].Datum.Date.Equals(datumPregleda.Date))
                {
                    string[] datum = preglediLekara[i].Datum.ToString().Split(" ");
                    string vreme = datum[1];
                    TimeSpan pocetni = TimeSpan.Parse(vreme);
                    for (int p = 0; p <= preglediLekara[i].Trajanje; p++)
                    {
                        TimeSpan dodatni = new TimeSpan(0, p, 0);
                        zauzetiTermini.Add(pocetni + dodatni);
                    }
                }
            }
            for (int i = 0; i < operacijeLekara.Count; i++)
            {
                if (operacijeLekara[i].Datum.Date.Equals(datumPregleda.Date))
                {
                    string[] datum = operacijeLekara[i].Datum.ToString().Split(" ");
                    string vreme = datum[1];
                    TimeSpan pocetni = TimeSpan.Parse(vreme);
                    for (int p = 0; p <= operacijeLekara[i].Trajanje; p++)
                    {
                        TimeSpan dodatni = new TimeSpan(0, p, 0);
                        zauzetiTermini.Add(pocetni + dodatni);
                    }
                }



            }
            return zauzetiTermini;
        }
        

        public List<Operacija> FiltrirajOperacijeLekara(List<Operacija> operacijeLekara, string jmbgLekar)
        {
            for (int i = 0; i < operacijeLekara.Count; i++)
            {
                if (!operacijeLekara[i].Lekar.Jmbg.Equals(jmbgLekar))
                {
                    operacijeLekara.RemoveAt(i);
                    i = i - 1;
                }
            }
            return operacijeLekara;
        }
        public List<Pregled> FiltrirajPregledeLekara(List<Pregled> preglediLekara, string jmbgLekar)
        {
            for (int i = 0; i < preglediLekara.Count; i++)
            {
                if (!preglediLekara[i].Lekar.Jmbg.Equals(jmbgLekar))
                {
                    preglediLekara.RemoveAt(i);
                    i = i - 1;
                }
            }
            return preglediLekara;
        }

        private bool naRenoviranju(Prostorija p, DateTime datumB)
        {
            foreach (Renoviranje r in skladisteRenoviranja.GetAll())
            {
                if (r.Prostorija.BrojProstorije == p.BrojProstorije)
                {
                    if (r.PocetakRenoviranja.Date <= datumB && datumB <= r.KrajRenoviranja.Date)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public PrikazOperacije PopuniOperaciju(string datumPregleda, string vremePregleda, string trajanjePregleda, string lekarPodaci, string pacijentPodaci, bool predmetStavkiDaLiJeOperacija, TipOperacije tipOperacije, bool predmetStavkiDaLiJeHitan, string prostorijaBroj, Lekar ulogovaniLekar, PrikazOperacije staraOperacija, ref bool daLiJeOperacija)
        {
            List<Lekar> lekariTrenutni = skladisteLekari.GetAll();
            List<Pacijent> pacijentiZa = skladistePacijenti.GetAll();
            List<Prostorija> prostorijaZa = skladisteProstorije.GetAllProstorije();
            PrikazOperacije trenutnaOperacija = new PrikazOperacije(staraOperacija.Id, DateTime.Parse(datumPregleda + TimeSpan.Parse(vremePregleda)), int.Parse(trajanjePregleda), false, predmetStavkiDaLiJeHitan, staraOperacija.Anamneza);
            for (int i = 0; i < lekariTrenutni.Count; i++)
            {
                string podaciOLekaru = lekariTrenutni[i].Prezime + ' ' + lekariTrenutni[i].Ime + ' ' + lekariTrenutni[i].Jmbg;
                if (podaciOLekaru.Equals(lekarPodaci))
                {
                    trenutnaOperacija.Lekar = lekariTrenutni[i];
                }
            }
            for (int i = 0; i < pacijentiZa.Count; i++)
            {
                string podaciOPacijentu;
                podaciOPacijentu = pacijentiZa[i].Prezime + ' ' + pacijentiZa[i].Ime + ' ' + pacijentiZa[i].Jmbg;


                if (podaciOPacijentu.Equals(pacijentPodaci))
                {
                    trenutnaOperacija.Pacijent = pacijentiZa[i];
                    break;

                }
            }
            for (int i = 0; i < prostorijaZa.Count; i++)
            {
                if (prostorijaZa[i].BrojProstorije.ToString().Equals(prostorijaBroj))
                {
                    trenutnaOperacija.Prostorija = prostorijaZa[i];
                    break;
                }
            }
            if (predmetStavkiDaLiJeOperacija.Equals(true))
            {
                if (tipOperacije.Equals(TipOperacije.prvaKat))
                {
                    daLiJeOperacija = true;
                    trenutnaOperacija.TipOperacije = TipOperacije.prvaKat;
                }
                else if (tipOperacije.Equals(TipOperacije.drugaKat))
                {
                    daLiJeOperacija = true;
                    trenutnaOperacija.TipOperacije = TipOperacije.drugaKat;
                }
                else if (tipOperacije.Equals(TipOperacije.trecaKat))
                {
                    daLiJeOperacija = true;
                    trenutnaOperacija.TipOperacije = TipOperacije.trecaKat;
                }

            }
            return trenutnaOperacija;
        }
        public PrikazPregleda PopuniPregled(string datumPregleda, string vremePregleda, string trajanjePregleda, string lekarPodaci, string pacijentPodaci, bool predmetStavkiDaLiJeHitan, string prostorijaBroj, PrikazPregleda stariPregled)
        {
            PrikazPregleda trenutniPregled = new PrikazPregleda(stariPregled.Id, DateTime.Parse(datumPregleda + TimeSpan.Parse(vremePregleda)), int.Parse(trajanjePregleda), false, predmetStavkiDaLiJeHitan, stariPregled.Anamneza);
            List<Lekar> lekariTrenutni = skladisteLekari.GetAll();
            List<Pacijent> pacijentiZa = skladistePacijenti.GetAll();
            List<Prostorija> prostorijaZa = skladisteProstorije.GetAllProstorije();
            for (int i = 0; i < lekariTrenutni.Count; i++)
            {
                string podaciOLekaru = lekariTrenutni[i].Prezime + ' ' + lekariTrenutni[i].Ime + ' ' + lekariTrenutni[i].Jmbg;
                if (podaciOLekaru.Equals(lekarPodaci))
                {
                    trenutniPregled.Lekar = lekariTrenutni[i];
                }
            }


            for (int i = 0; i < pacijentiZa.Count; i++)
            {
                string podaciOPacijentu;
                podaciOPacijentu = pacijentiZa[i].Prezime + ' ' + pacijentiZa[i].Ime + ' ' + pacijentiZa[i].Jmbg;


                if (podaciOPacijentu.Equals(pacijentPodaci))
                {
                    trenutniPregled.Pacijent = pacijentiZa[i];
                    break;

                }
            }
            for (int i = 0; i < prostorijaZa.Count; i++)
            {
                if (prostorijaZa[i].BrojProstorije.ToString().Equals(prostorijaBroj))
                {
                    trenutniPregled.Prostorija = prostorijaZa[i];
                    break;
                }
            }
            return trenutniPregled;
        }

    }
}
