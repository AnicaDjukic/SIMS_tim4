using Bolnica.Repository.Pregledi;
using Model.Pregledi;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Service
{
    public class BeleskaService
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
            int max = 0;
            List<Beleska> beleske = DobaviSveBeleske();
            foreach (Beleska beleska in beleske)
            {
                if (beleska.Id > max)
                {
                    max = beleska.Id;
                }
            }
            return max + 1;
        }
    }
}
