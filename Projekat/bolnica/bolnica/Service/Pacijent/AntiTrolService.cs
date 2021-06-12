using Bolnica.Model.Pregledi;
using Bolnica.Repository.Korisnici;
using Model.Korisnici;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Service
{
    public class AntiTrolService
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

        public void IzmeniAntiTrol(AntiTrol noviAntiTrol)
        {
            repositoryAntiTrol.Update(noviAntiTrol);
        }

        public int IzracunajIdAntiTrol()
        {
            int max = 0;
            List<AntiTrol> antiTrolovi = DobaviSveAntiTrolove();
            foreach (AntiTrol antiTrol in antiTrolovi)
            {
                if (antiTrol.Id > max)
                {
                    max = antiTrol.Id;
                }
            }
            return max + 1;
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

        public int DobijBrojAktivnosti(Pacijent trenutniPacijent)
        {
            int brojac = 0;
            List<AntiTrol> antiTrolovi = DobaviSveAntiTrolove();
            foreach (AntiTrol antiTrol in antiTrolovi)
            {
                if (trenutniPacijent.Jmbg.Equals(antiTrol.Pacijent.Jmbg) && antiTrol.Datum.AddDays(3).CompareTo(DateTime.Now) > 0)
                {
                    brojac++;
                }
            }
            return brojac;
        }
    }
}
