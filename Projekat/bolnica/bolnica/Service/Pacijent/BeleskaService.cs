using Bolnica.Repository.Pregledi;
using Model.Pregledi;
using System.Collections.Generic;

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
    }
}
