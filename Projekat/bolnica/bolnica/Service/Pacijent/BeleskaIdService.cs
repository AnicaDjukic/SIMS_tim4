using Bolnica.Repository.Pregledi;
using Bolnica.Template;
using Model.Pregledi;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Service
{
    public class BeleskaIdService : RacunajId
    {
        public int IzracunajIdBeleske()
        {
            return IzracunajId();
        }

        public override List<int> DobijListu()
        {
            List<int> ideovi = new List<int>();
            FileRepositoryBeleska repositoryBeleska = new FileRepositoryBeleska();
            List<Beleska> beleske = repositoryBeleska.GetAll();
            foreach (Beleska beleska in beleske)
            {
                ideovi.Add(beleska.Id);
            }
            return ideovi;
        }
    }
}
