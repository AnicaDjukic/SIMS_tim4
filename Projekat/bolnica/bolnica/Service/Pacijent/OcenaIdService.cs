using Bolnica.Repository.Pregledi;
using Bolnica.Template;
using Model.Korisnici;
using System.Collections.Generic;

namespace Bolnica.Service
{
    public class OcenaIdService : RacunajId
    {
        public int IzracunajIdOcene()
        {
            return IzracunajId();
        }

        public override List<int> DobijListu()
        {
            List<int> ideovi = new List<int>();
            FileRepositoryOcena repositoryOcena = new FileRepositoryOcena();
            List<Ocena> ocene = repositoryOcena.GetAll();
            foreach (Ocena ocena in ocene)
            {
                ideovi.Add(ocena.IdOcene);
            }
            return ideovi;
        }
    }
}
