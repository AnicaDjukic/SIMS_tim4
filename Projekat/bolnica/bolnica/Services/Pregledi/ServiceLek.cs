using Bolnica.Repository.Pregledi;
using Model.Pregledi;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Services.Pregledi
{
    class ServiceLek
    {
        private RepositoryLek repository;

        public ServiceLek()
        {
            repository = new FileRepositoryLek();
        }
        public List<Lek> DobaviSveLekove()
        {
            return repository.GetAll();
        }

        internal bool LekPostoji(int id)
        {
            bool postoji = false;
            foreach (Lek l in repository.GetAll())
            {
                if (l.Id == id)
                {
                    postoji = true;
                    break;
                }
            }
            return postoji;
        }

        internal void ObrisiLek(Lek lek)
        {
            repository.Delete(lek);
        }

        internal void SacuvajLek(Lek lek)
        {
            repository.Save(lek);
        }
    }
}
