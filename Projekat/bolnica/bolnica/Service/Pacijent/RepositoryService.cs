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
using System.Collections.Generic;

namespace Bolnica.Service
{
    public class RepositoryService
    {
        private FileRepositoryAntiTrol repositoryAntiTrol = new FileRepositoryAntiTrol();
        private FileRepositoryGodisnji repositoryGodisnji = new FileRepositoryGodisnji();
        private FileRepositoryKorisnik repositoryKorisnik = new FileRepositoryKorisnik();
        private FileRepositoryLekar repositoryLekar = new FileRepositoryLekar();
        private FileRepositoryObavestenje repositoryObavestenje = new FileRepositoryObavestenje();
        private FileRepositoryPacijent repositoryPacijent = new FileRepositoryPacijent();
        private FileRepositoryZdravstveniKarton repositoryZdravstveniKarton = new FileRepositoryZdravstveniKarton();

        private FileRepositoryAnamneza repositoryAnamneza = new FileRepositoryAnamneza();
        private FileRepositoryBeleska repositoryBeleska = new FileRepositoryBeleska();
        private FileRepositoryLek repositoryLek = new FileRepositoryLek();
        private FileRepositoryOcena repositoryOcena = new FileRepositoryOcena();
        private FileRepositoryOperacija repositoryOperacija = new FileRepositoryOperacija();
        private FileRepositoryPregled repositoryPregled = new FileRepositoryPregled();
        private FileRepositorySastojak repositorySastojak = new FileRepositorySastojak();

        private FileRepositoryBolnickaSoba repositoryBolnickaSoba = new FileRepositoryBolnickaSoba();
        private FileRepositoryBuducaZaliha repositoryBuducaZaliha = new FileRepositoryBuducaZaliha();
        private FileRepositoryHospitalizacija repositoryHospitalizacija = new FileRepositoryHospitalizacija();
        private FileRepositoryOprema repositoryOprema = new FileRepositoryOprema();
        private FileRepositoryProstorija repositoryProstorija = new FileRepositoryProstorija();
        private FileRepositoryRenoviranje repositoryRenoviranje = new FileRepositoryRenoviranje();
        private FileRepositoryZaliha repositoryZaliha = new FileRepositoryZaliha();

        public List<Pacijent> DobijPacijente()
        {
            return repositoryPacijent.GetAll();
        }

        public List<Lekar> DobijLekare()
        {
            return repositoryLekar.GetAll();
        }

        public List<Korisnik> DobijKorisnike()
        {
            return repositoryKorisnik.GetAll();
        }

        public List<AntiTrol> DobijAntiTrol()
        {
            return repositoryAntiTrol.GetAll();
        }

        public List<Godisnji> DobijGodisnje()
        {
            return repositoryGodisnji.GetAll();
        }

        public List<Obavestenje> DobijObavestenja()
        {
            return repositoryObavestenje.GetAll();
        }

        public List<ZdravstveniKarton> DobijZdravstveneKartone()
        {
            return repositoryZdravstveniKarton.GetAll();
        }

        public List<Prostorija> DobijProstorije()
        {
            return repositoryProstorija.GetAll();
        }

        public List<BolnickaSoba> DobijBolnickeSobe()
        {
            return repositoryBolnickaSoba.GetAll();
        }

        public List<BuducaZaliha> DobijBuduceZalihe()
        {
            return repositoryBuducaZaliha.GetAll();
        }

        public List<Hospitalizacija> DobijHospitalizacije()
        {
            return repositoryHospitalizacija.GetAll();
        }

        public List<Oprema> DobijOpremu()
        {
            return repositoryOprema.GetAll();
        }

        public List<Renoviranje> DobijRenoviranja()
        {
            return repositoryRenoviranje.GetAll();
        }

        public List<Zaliha> DobijZalihe()
        {
            return repositoryZaliha.GetAll();
        }

        public List<Pregled> DobijPreglede()
        {
            return repositoryPregled.GetAll();
        }

        public List<Operacija> DobijOperacije()
        {
            return repositoryOperacija.GetAll();
        }

        public List<Anamneza> DobijAnamneze()
        {
            return repositoryAnamneza.GetAll();
        }

        public List<Lek> DobijLekove()
        {
            return repositoryLek.GetAll();
        }

        public List<Beleska> DobijBeleske()
        {
            return repositoryBeleska.GetAll();
        }

        public List<Ocena> DobijOcene()
        {
            return repositoryOcena.GetAll();
        }

        public List<Sastojak> DobijSastojke()
        {
            return repositorySastojak.GetAll();
        }

        public void SacuvajPregled(Pregled pregled)
        {
            repositoryPregled.Save(pregled);
        }

        public void SacuvajOcenu(Ocena ocena)
        {
            repositoryOcena.Save(ocena);
        }

        public void SacuvajAntiTrol(AntiTrol antiTrol)
        {
            repositoryAntiTrol.Save(antiTrol);
        }

        public void SacuvajBelesku(Beleska beleska)
        {
            repositoryBeleska.Save(beleska);
        }

        public void IzmeniPregled(Pregled noviPregled)
        {
            repositoryPregled.Update(noviPregled);
        }

        public void IzmeniPacijenta(Pacijent noviPacijent)
        {
            repositoryPacijent.Update(noviPacijent);
        }

        public void IzmeniAnamnezu(Anamneza novaAmaneza)
        {
            repositoryAnamneza.Update(novaAmaneza);
        }

        public void IzmeniBelesku(Beleska novaBeleska)
        {
            repositoryBeleska.Update(novaBeleska);
        }

        public void IzbrisiPregled(Pregled pregled)
        {
            repositoryPregled.Delete(pregled);
        }

        public void IzbrisiOperaciju(Operacija operacija)
        {
            repositoryOperacija.Delete(operacija);
        }
    }
}
