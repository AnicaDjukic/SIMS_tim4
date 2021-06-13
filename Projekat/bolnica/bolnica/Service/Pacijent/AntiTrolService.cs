using Bolnica.Model.Pregledi;
using Bolnica.Repository.Korisnici;
using Bolnica.Template;
using Model.Korisnici;
using System;
using System.Collections.Generic;

namespace Bolnica.Service
{
    public class AntiTrolService : RacunajId
    {
        private FileRepositoryAntiTrol repositoryAntiTrol = new FileRepositoryAntiTrol();

        public List<AntiTrol> DobaviSveAntiTrolove()
        {
            return repositoryAntiTrol.GetAll();
        }

        public void SacuvajAntiTrol(AntiTrol noviAntiTrol)
        {
            repositoryAntiTrol.Save(noviAntiTrol);
        }

        public int IzracunajIdAntiTrol()
        {
            return IzracunajId();
        }

        public AntiTrol KreirajAntiTrol(PrikazPregleda prikazPregleda)
        {
            return new AntiTrol
            {
                Id = IzracunajIdAntiTrol(),
                Pacijent = prikazPregleda.Pacijent,
                Datum = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second)
            };
        }

        public override List<int> DobijListu()
        {
            List<int> ideovi = new List<int>();
            List<AntiTrol> antiTrolovi = DobaviSveAntiTrolove();
            foreach (AntiTrol antiTrol in antiTrolovi)
            {
                ideovi.Add(antiTrol.Id);
            }
            return ideovi;
        }
    }
}
