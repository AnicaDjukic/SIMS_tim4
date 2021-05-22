using Bolnica.Model.Pregledi;
using Model.Korisnici;
using Model.Pregledi;
using Model.Prostorije;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.DTO
{
    public class IzmeniINapraviTerminLekarServiceDTO
    {
        public string datumPregledaString { get; set; }
        public DateTime datumPregledaDatum { get; set; }
        public string vremePregledaString { get; set; }
        public List<TimeSpan> vremePregleda { get; set; }
        public string trajanjePregleda { get; set; }
        public string lekarPodaci { get; set; }
        public string pacijentPodaci { get; set; }
        public bool predmetStavkiDaLiJeOperacija { get; set; }
        public TipOperacije tipOperacije { get; set; }
        public bool predmetStavkiDaLiJeHitan { get; set; }
        public string prostorijaBroj { get; set; }
        public Lekar ulogovaniLekar { get; set; }
        public PrikazOperacije staraOperacija { get; set; }
        public PrikazPregleda stariPregled { get; set; }
        public List<Lekar> sviLekari { get; set; }
        public List<Pacijent> sviPacijenti { get; set; }
        public List<Prostorija> sveProstorije { get; set; }
        public PrikazPregleda trenutniPregled { get; set; }
        public PrikazOperacije trenutnaOperacija { get; set; }

        public Pregled azuriraniPregled { get; set; }

        public Operacija azuriranaOperacija { get; set; }

        public string specijalizacija { get; set; }

        public List<Pregled> preglediPacijenta { get; set; }

        public List<Operacija> operacijePacijenta { get; set; }

        public string jmbgPacijent { get; set; }

        public List<TimeSpan> zauzetiTermini { get; set; }

        public List<TimeSpan> predmetStavkiVreme { get; set; }

        public string filtriraniLekar { get; set; }

        public DateTime filtriraniDatum { get; set; }

        public List<string> predmetStavkiProstorije { get; set; }

        public List<string> specijalizacije { get; set; }

        public List<Pregled> preglediLekara { get; set; }

        public List<Operacija> operacijeLekara { get; set; }

        public bool daLiJeOperacija { get; set; } 

        public IzmeniINapraviTerminLekarServiceDTO(string datumPregleda, string vremePregleda, string trajanjePregleda, string lekarPodaci, string pacijentPodaci, bool predmetStavkiDaLiJeOperacija, TipOperacije tipOperacije, bool predmetStavkiDaLiJeHitan, string prostorijaBroj, Lekar ulogovaniLekar, PrikazOperacije staraOperacija, PrikazPregleda stariPregled)
        {
            this.datumPregledaString = datumPregleda;
            this.vremePregledaString = vremePregleda;
            this.trajanjePregleda = trajanjePregleda;
            this.lekarPodaci = lekarPodaci;
            this.pacijentPodaci = pacijentPodaci;
            this.predmetStavkiDaLiJeOperacija = predmetStavkiDaLiJeOperacija;
            this.tipOperacije = tipOperacije;
            this.predmetStavkiDaLiJeHitan = predmetStavkiDaLiJeHitan;
            this.prostorijaBroj = prostorijaBroj;
            this.ulogovaniLekar = ulogovaniLekar;
            this.staraOperacija = staraOperacija;
            this.stariPregled = stariPregled;

            
        }
        public IzmeniINapraviTerminLekarServiceDTO(string datumPregleda, string vremePregleda, string trajanjePregleda, List<Lekar> sviLekari, string lekarPodaci,List<Pacijent> sviPacijenti, string pacijentPodaci, List<Prostorija> sveProstorije, bool predmetStavkiDaLiJeOperacija,TipOperacije tipOperacije, bool predmetStavkiDaLiJeHitan, string prostorijaBroj, Lekar ulogovaniLekar)
        {
            this.datumPregledaString = datumPregleda;
            this.vremePregledaString = vremePregleda;
            this.trajanjePregleda = trajanjePregleda;
            this.lekarPodaci = lekarPodaci;
            this.pacijentPodaci = pacijentPodaci;
            this.predmetStavkiDaLiJeOperacija = predmetStavkiDaLiJeOperacija;
            this.tipOperacije = tipOperacije;
            this.predmetStavkiDaLiJeHitan = predmetStavkiDaLiJeHitan;
            this.prostorijaBroj = prostorijaBroj;
            this.ulogovaniLekar = ulogovaniLekar;
            this.sviLekari = sviLekari;
            this.sviPacijenti = sviPacijenti;
            this.sveProstorije = sveProstorije;


        }
        public IzmeniINapraviTerminLekarServiceDTO(Lekar ulogovaniLekar, PrikazPregleda stariPregled, PrikazPregleda trenutniPregled)
        {
            this.ulogovaniLekar = ulogovaniLekar;
            this.stariPregled = stariPregled;
            this.trenutniPregled = trenutniPregled;
        }

        public IzmeniINapraviTerminLekarServiceDTO(Lekar ulogovaniLekar, PrikazOperacije staraOperacija, PrikazOperacije trenutnaOperacija)
        {
            this.ulogovaniLekar = ulogovaniLekar;
            this.staraOperacija = staraOperacija;
            this.trenutnaOperacija = trenutnaOperacija;
        }

        public IzmeniINapraviTerminLekarServiceDTO(Lekar ulogovaniLekar, PrikazPregleda trenutniPregled, Pregled azuriraniPregled)
        {
            this.ulogovaniLekar = ulogovaniLekar;
            this.trenutniPregled = trenutniPregled;
            this.azuriraniPregled = azuriraniPregled;
        }
        public IzmeniINapraviTerminLekarServiceDTO(Lekar ulogovaniLekar, PrikazOperacije trenutnaOperacija, Operacija azuriranaOperacija)
        {
            this.ulogovaniLekar = ulogovaniLekar;
            this.trenutnaOperacija = trenutnaOperacija;
            this.azuriranaOperacija = azuriranaOperacija;

        }
        public IzmeniINapraviTerminLekarServiceDTO(string specijalizacija, string lekarPodaci)
        {
            this.specijalizacija = specijalizacija;
            this.lekarPodaci = lekarPodaci;
        }
        public IzmeniINapraviTerminLekarServiceDTO(List<Pacijent> sviPacijenti, string pacijentPodaci, DateTime datumPregleda, string trajanjePregleda, string vremePregleda)
        {
            this.sviPacijenti = sviPacijenti;
            this.pacijentPodaci = pacijentPodaci;
            this.datumPregledaDatum = datumPregleda;
            this.trajanjePregleda = trajanjePregleda;
            this.vremePregledaString = vremePregleda;
        }

        public IzmeniINapraviTerminLekarServiceDTO(string pacijentPodaci, DateTime datumPregleda, string trajanjePregleda, string vremePregleda, PrikazPregleda trenutniPregled, PrikazOperacije trenutnaOperacija)
        {
            this.pacijentPodaci = pacijentPodaci;
            this.datumPregledaDatum = datumPregleda;
            this.trajanjePregleda = trajanjePregleda;
            this.vremePregledaString = vremePregleda;
            this.trenutniPregled = trenutniPregled;
            this.trenutnaOperacija = trenutnaOperacija;
        }
       public IzmeniINapraviTerminLekarServiceDTO(List<Pacijent> sviPacijenti, string pacijentPodaci)
        {
            this.sviPacijenti = sviPacijenti;
            this.pacijentPodaci = pacijentPodaci;
        }



        public IzmeniINapraviTerminLekarServiceDTO(List<Pregled> preglediPacijenta, string jmbgPacijent)
        {
            this.preglediPacijenta = preglediPacijenta;
            this.jmbgPacijent = jmbgPacijent;
        }
        public IzmeniINapraviTerminLekarServiceDTO(List<Operacija> operacijePacijenta, string jmbgPacijent)
        {
            this.operacijePacijenta = operacijePacijenta;
            this.jmbgPacijent = jmbgPacijent;
        }
        public IzmeniINapraviTerminLekarServiceDTO(List<Prostorija> sveProstorije, string prostorijaBroj)
        {
            this.sveProstorije = sveProstorije;
            this.prostorijaBroj = prostorijaBroj;
        }
        public IzmeniINapraviTerminLekarServiceDTO(List<Prostorija> sveProstorije, string prostorijaBroj, DateTime datumPregleda)
        {
            this.sveProstorije = sveProstorije;
            this.prostorijaBroj = prostorijaBroj;
            this.datumPregledaDatum = datumPregleda;
        }
        public IzmeniINapraviTerminLekarServiceDTO(List<TimeSpan> zauzetiTermini, List<Pregled> preglediPacijenta, List<Operacija> operacijePacijenta, DateTime datumPregleda, PrikazPregleda trenutniPregled, PrikazOperacije trenutnaOperacija)
        {
            this.zauzetiTermini = zauzetiTermini;
            this.preglediPacijenta = preglediPacijenta;
            this.operacijePacijenta = operacijePacijenta;
            this.datumPregledaDatum = datumPregleda;
            this.trenutniPregled = trenutniPregled;
            this.trenutnaOperacija = trenutnaOperacija;
        }
        public IzmeniINapraviTerminLekarServiceDTO(List<TimeSpan> zauzetiTermini, List<Pregled> preglediPacijenta, List<Operacija> operacijePacijenta, DateTime datumPregleda)
        {
            this.zauzetiTermini = zauzetiTermini;
            this.preglediPacijenta = preglediPacijenta;
            this.operacijePacijenta = operacijePacijenta;
            this.datumPregledaDatum = datumPregleda;
        }
        public IzmeniINapraviTerminLekarServiceDTO(List<TimeSpan> predmetStavkiVreme, string filtriraniLekar, List<Lekar> sviLekari, string lekarPodaci, DateTime datumPregleda)
        {
            this.predmetStavkiVreme = predmetStavkiVreme;
            this.filtriraniLekar = filtriraniLekar;
            this.sviLekari = sviLekari;
            this.lekarPodaci = lekarPodaci;
            this.datumPregledaDatum = datumPregleda;
        }
        public IzmeniINapraviTerminLekarServiceDTO(string lekarPodaci, List<Lekar> sviLekari)
        {
            this.lekarPodaci = lekarPodaci;
            this.sviLekari = sviLekari;
        }
        public IzmeniINapraviTerminLekarServiceDTO(DateTime filtriraniDatum, DateTime datumPregleda, List<Prostorija> sveProstorije, List<string> predmetStavkiProstorije)
        {
            this.filtriraniDatum = filtriraniDatum;
            this.datumPregledaDatum = datumPregleda;
            this.sveProstorije = sveProstorije;
            this.predmetStavkiProstorije = predmetStavkiProstorije;
        }

        public IzmeniINapraviTerminLekarServiceDTO(string filtriraniLekar, string lekarPodaci, List<Lekar> sviLekari, string specijalizacija)
        {
            this.filtriraniLekar = filtriraniLekar;
            this.sviLekari = sviLekari;
            this.specijalizacija = specijalizacija;
        }
        public IzmeniINapraviTerminLekarServiceDTO(List<string> specijalizacije, String specijalizacija, List<Lekar> sviLekari)
        {
            this.specijalizacije = specijalizacije;
            this.specijalizacija = specijalizacija;
            this.sviLekari = sviLekari;
        }
        public IzmeniINapraviTerminLekarServiceDTO(string lekarPodaci, DateTime datumPregleda, string trajanjePregleda, string vremePregleda)
        {
            this.lekarPodaci = lekarPodaci;
            this.datumPregledaDatum = datumPregleda;
            this.trajanjePregleda = trajanjePregleda;
            this.vremePregledaString = vremePregleda;
        }
        public IzmeniINapraviTerminLekarServiceDTO(List<TimeSpan> vremePregleda, List<Lekar> sviLekari, string lekarPodaci, DateTime datumPregleda)
        {
            this.vremePregleda = vremePregleda;
            this.sviLekari = sviLekari;
            this.lekarPodaci = lekarPodaci;
            this.datumPregledaDatum = datumPregleda;

        }
        public IzmeniINapraviTerminLekarServiceDTO(string lekarPodaci, DateTime datumPregleda)
        {
            this.lekarPodaci = lekarPodaci;
            this.datumPregledaDatum = datumPregleda;

        }
        public IzmeniINapraviTerminLekarServiceDTO(string lekarPodaci, DateTime datumPregleda, PrikazPregleda trenutniPregled, PrikazOperacije trenutnaOperacija)
        {
            this.lekarPodaci = lekarPodaci;
            this.datumPregledaDatum = datumPregleda;
            this.trenutniPregled = trenutniPregled;
            this.trenutnaOperacija = trenutnaOperacija;
        }
        public IzmeniINapraviTerminLekarServiceDTO(DateTime datumPregleda, string lekarPodaci)
        {
            this.datumPregledaDatum = datumPregleda;
            this.lekarPodaci = lekarPodaci;
        }
        public IzmeniINapraviTerminLekarServiceDTO(DateTime datumPregleda, string lekarPodaci, PrikazPregleda trenutniPregled, PrikazOperacije trenutnaOperacija)
        {
            this.datumPregledaDatum = datumPregleda;
            this.lekarPodaci = lekarPodaci;
            this.trenutniPregled = trenutniPregled;
            this.trenutnaOperacija = trenutnaOperacija;
        
        }

        public IzmeniINapraviTerminLekarServiceDTO(List<Pregled> preglediLekara, List<Operacija> operacijeLekara, List<TimeSpan> zauzetiTermini, DateTime datumPregleda, PrikazPregleda trenutniPregled, PrikazOperacije trenutnaOperacija)
        {
            this.preglediLekara = preglediLekara;
            this.operacijeLekara = operacijeLekara;
            this.zauzetiTermini = zauzetiTermini;
            this.datumPregledaDatum = datumPregleda;
            this.trenutniPregled = trenutniPregled;
            this.trenutnaOperacija = trenutnaOperacija;

        }
        public IzmeniINapraviTerminLekarServiceDTO(List<Pregled> preglediLekara, List<Operacija> operacijeLekara, List<TimeSpan> zauzetiTermini, DateTime datumPregleda)
        {
            this.preglediLekara = preglediLekara;
            this.operacijeLekara = operacijeLekara;
            this.zauzetiTermini = zauzetiTermini;
            this.datumPregledaDatum = datumPregleda;
        }
        
        public IzmeniINapraviTerminLekarServiceDTO(string datumPregleda, string vremePregleda, string trajanjePregleda, string lekarPodaci, string pacijentPodaci, bool predmetStavkiDaLiJeOperacija, TipOperacije tipOperacije, bool predmetStavkiDaLiJeHitan, string prostorijaBroj, Lekar ulogovaniLekar, PrikazOperacije staraOperacija)
        {
            this.datumPregledaString = datumPregleda;
            this.vremePregledaString = vremePregleda;
            this.trajanjePregleda = trajanjePregleda;
            this.lekarPodaci = lekarPodaci;
            this.pacijentPodaci = pacijentPodaci;
            this.predmetStavkiDaLiJeOperacija = predmetStavkiDaLiJeOperacija;
            this.tipOperacije = tipOperacije;
            this.predmetStavkiDaLiJeHitan = predmetStavkiDaLiJeHitan;
            this.prostorijaBroj = prostorijaBroj;
            this.ulogovaniLekar = ulogovaniLekar;
            this.staraOperacija = staraOperacija;
            
        }
        public IzmeniINapraviTerminLekarServiceDTO(string datumPregleda, string vremePregleda, string trajanjePregleda, string lekarPodaci, string pacijentPodaci, bool predmetStavkiDaLiJeHitan, string prostorijaBroj, PrikazPregleda stariPregled)
        {
            this.datumPregledaString = datumPregleda;
            this.vremePregledaString = vremePregleda;
            this.trajanjePregleda = trajanjePregleda;
            this.lekarPodaci = lekarPodaci;
            this.pacijentPodaci = pacijentPodaci;
            this.predmetStavkiDaLiJeHitan = predmetStavkiDaLiJeHitan;
            this.prostorijaBroj = prostorijaBroj;
            this.stariPregled = stariPregled;
        }
    }
}
