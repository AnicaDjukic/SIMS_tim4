using Bolnica.Template;
using Model.Pregledi;
using System.Collections.Generic;

namespace Bolnica.Service
{
    public class PregledIdService : RacunajId
    {
        public int IzracunajIdPregleda()
        {
            return IzracunajId();
        }

        public override List<int> DobijListu()
        {
            List<int> ideovi = new List<int>();
            FileRepositoryPregled repositoryPregled = new FileRepositoryPregled();
            List<Pregled> pregledi = repositoryPregled.GetAll();
            foreach (Pregled pregled in pregledi)
            {
                ideovi.Add(pregled.Id);
            }
            return ideovi;
        }
    }
}
