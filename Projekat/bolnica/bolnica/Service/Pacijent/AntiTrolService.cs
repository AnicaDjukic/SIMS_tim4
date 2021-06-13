using Bolnica.Model.Pregledi;
using Bolnica.Repository.Korisnici;
using Model.Korisnici;
using System;
using System.Collections.Generic;

namespace Bolnica.Service
{
    public class AntiTrolService
    {
        private FileRepositoryAntiTrol repositoryAntiTrol = new FileRepositoryAntiTrol();
        private AntiTrolIdService antiTrolIdService = new AntiTrolIdService();

        public List<AntiTrol> DobaviSveAntiTrolove()
        {
            return repositoryAntiTrol.GetAll();
        }

        public void SacuvajAntiTrol(AntiTrol noviAntiTrol)
        {
            repositoryAntiTrol.Save(noviAntiTrol);
        }

        public AntiTrol KreirajAntiTrol(PrikazPregleda prikazPregleda)
        {
            return new AntiTrol
            {
                Id = antiTrolIdService.IzracunajIdAntiTrol(),
                Pacijent = prikazPregleda.Pacijent,
                Datum = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second)
            };
        }
    }
}
