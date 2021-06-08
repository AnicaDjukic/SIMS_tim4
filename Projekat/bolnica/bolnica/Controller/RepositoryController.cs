using Bolnica.Service;
using Bolnica.Model.Korisnici;
using Bolnica.Model.Pacijenti;
using Bolnica.Model.Pregledi;
using Bolnica.Model.Prostorije;
using Bolnica.Repository.Korisnici;
using Bolnica.Repository.Pregledi;
using Bolnica.Repository.Prostorije;
using Model.Korisnici;
using Model.Pacijenti;
using Model.Pregledi;
using Model.Prostorije;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Controller
{
    public class RepositoryController
    {
        private RepositoryService service = new RepositoryService();

        public List<Pacijent> DobijPacijente()
        {
            return service.DobijPacijente();
        }

        public List<Lekar> DobijLekare()
        {
            return service.DobijLekare();
        }

        public List<Korisnik> DobijKorisnike()
        {
            return service.DobijKorisnike();
        }
        public List<AntiTrol> DobijAntiTrol()
        {
            return service.DobijAntiTrol();
        }

        public List<Godisnji> DobijGodisnje()
        {
            return service.DobijGodisnje();
        }

        public List<Obavestenje> DobijObavestenja()
        {
            return service.DobijObavestenja();
        }

        public List<ZdravstveniKarton> DobijZdravstveneKartone()
        {
            return service.DobijZdravstveneKartone();
        }

        public List<Prostorija> DobijProstorije()
        {
            return service.DobijProstorije();
        }

        public List<BolnickaSoba> DobijBolnickeSobe()
        {
            return service.DobijBolnickeSobe();
        }

        public List<BuducaZaliha> DobijBuduceZalihe()
        {
            return service.DobijBuduceZalihe();
        }

        public List<Hospitalizacija> DobijHospitalizacije()
        {
            return service.DobijHospitalizacije();
        }

        public List<Oprema> DobijOpremu()
        {
            return service.DobijOpremu();
        }

        public List<Renoviranje> DobijRenoviranja()
        {
            return service.DobijRenoviranja();
        }

        public List<Zaliha> DobijZalihe()
        {
            return service.DobijZalihe();
        }

        public List<Pregled> DobijPreglede()
        {
            return service.DobijPreglede();
        }

        public List<Operacija> DobijOperacije()
        {
            return service.DobijOperacije();
        }

        public List<Anamneza> DobijAnamneze()
        {
            return service.DobijAnamneze();
        }

        public List<Lek> DobijLekove()
        {
            return service.DobijLekove();
        }

        public List<Beleska> DobijBeleske()
        {
            return service.DobijBeleske();
        }

        public List<Ocena> DobijOcene()
        {
            return service.DobijOcene();
        }

        public List<Sastojak> DobijSastojke()
        {
            return service.DobijSastojke();
        }
    }
}
