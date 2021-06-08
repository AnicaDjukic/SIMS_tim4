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

namespace Bolnica.Service
{
    public class RepositoryService
    {
        public List<Pacijent> DobijPacijente()
        {
            return new FileRepositoryPacijent().GetAll();
        }

        public List<Lekar> DobijLekare()
        {
            return new FileRepositoryLekar().GetAll();
        }

        public List<Korisnik> DobijKorisnike()
        {
            return new FileRepositoryKorisnik().GetAll();
        }

        public List<AntiTrol> DobijAntiTrol()
        {
            return new FileRepositoryAntiTrol().GetAll();
        }

        public List<Godisnji> DobijGodisnje()
        {
            return new FileRepositoryGodisnji().GetAll();
        }

        public List<Obavestenje> DobijObavestenja()
        {
            return new FileRepositoryObavestenje().GetAll();
        }

        public List<ZdravstveniKarton> DobijZdravstveneKartone()
        {
            return new FileRepositoryZdravstveniKarton().GetAll();
        }

        public List<Prostorija> DobijProstorije()
        {
            return new FileRepositoryProstorija().GetAll();
        }

        public List<BolnickaSoba> DobijBolnickeSobe()
        {
            return new FileRepositoryBolnickaSoba().GetAll();
        }

        public List<BuducaZaliha> DobijBuduceZalihe()
        {
            return new FileRepositoryBuducaZaliha().GetAll();
        }

        public List<Hospitalizacija> DobijHospitalizacije()
        {
            return new FileRepositoryHospitalizacija().GetAll();
        }

        public List<Oprema> DobijOpremu()
        {
            return new FileRepositoryOprema().GetAll();
        }

        public List<Renoviranje> DobijRenoviranja()
        {
            return new FileRepositoryRenoviranje().GetAll();
        }

        public List<Zaliha> DobijZalihe()
        {
            return new FileRepositoryZaliha().GetAll();
        }

        public List<Pregled> DobijPreglede()
        {
            return new FileRepositoryPregled().GetAll();
        }

        public List<Operacija> DobijOperacije()
        {
            return new FileRepositoryOperacija().GetAll();
        }

        public List<Anamneza> DobijAnamneze()
        {
            return new FileRepositoryAnamneza().GetAll();
        }

        public List<Lek> DobijLekove()
        {
            return new FileRepositoryLek().GetAll();
        }

        public List<Beleska> DobijBeleske()
        {
            return new FileRepositoryBeleska().GetAll();
        }

        public List<Ocena> DobijOcene()
        {
            return new FileRepositoryOcena().GetAll();
        }

        public List<Sastojak> DobijSastojke()
        {
            return new FileRepositorySastojak().GetAll();
        }
    }
}
