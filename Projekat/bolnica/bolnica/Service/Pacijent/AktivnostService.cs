using Model.Korisnici;
using System;
using System.Collections.Generic;

namespace Bolnica.Service
{
    public class AktivnostService
    {
        private AntiTrolService serviceAntiTrol = new AntiTrolService();

        public int DobijBrojAktivnosti(Pacijent trenutniPacijent)
        {
            List<AntiTrol> antiTrolovi = serviceAntiTrol.DobaviSveAntiTrolove();
            return DobijBrojac(trenutniPacijent, antiTrolovi);
        }

        private int DobijBrojac(Pacijent trenutniPacijent, List<AntiTrol> antiTrolovi)
        {
            int brojac = 0;
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
