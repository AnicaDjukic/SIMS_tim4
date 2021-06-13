using Bolnica.Repository.Pregledi;
using Bolnica.Template;
using Model.Pregledi;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Service
{
    public class BeleskaService : RacunajId
    {
        private FileRepositoryBeleska repositoryBeleska = new FileRepositoryBeleska();

        public List<Beleska> DobaviSveBeleske()
        {
            return repositoryBeleska.GetAll();
        }

        public void SacuvajBelesku(Beleska novaBeleska)
        {
            repositoryBeleska.Save(novaBeleska);
        }

        public void IzmeniBelesku(Beleska novaBeleske)
        {
            repositoryBeleska.Update(novaBeleske);
        }

        public int IzracunajIdBeleske()
        {
            return IzracunajId();
        }

        public override List<int> DobijListu()
        {
            List<int> ideovi = new List<int>();
            List<Beleska> beleske = DobaviSveBeleske();
            foreach (Beleska beleska in beleske)
            {
                ideovi.Add(beleska.Id);
            }
            return ideovi;
        }
    }
}
