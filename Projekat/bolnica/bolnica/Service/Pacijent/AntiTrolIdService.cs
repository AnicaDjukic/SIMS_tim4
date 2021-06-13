using Bolnica.Repository.Korisnici;
using Bolnica.Template;
using Model.Korisnici;
using System.Collections.Generic;

namespace Bolnica.Service
{
    public class AntiTrolIdService : RacunajId
    {
        public int IzracunajIdAntiTrol()
        {
            return IzracunajId();
        }

        public override List<int> DobijListu()
        {
            List<int> ideovi = new List<int>();
            FileRepositoryAntiTrol repositoryAntiTrol = new FileRepositoryAntiTrol();
            List<AntiTrol> antiTrolovi = repositoryAntiTrol.GetAll();
            foreach (AntiTrol antiTrol in antiTrolovi)
            {
                ideovi.Add(antiTrol.Id);
            }
            return ideovi;
        }
    }
}
