using Bolnica.Model.Prostorije;
using Bolnica.Repository.Prostorije;
using System;
using System.Collections.Generic;

namespace Bolnica.Services.Prostorije
{
    public class ServiceRenoviranje
    {
        private RepositoryRenoviranje repository;

        public ServiceRenoviranje()
        {
            repository = new FileRepositoryRenoviranje();
        }
        public List<Renoviranje> DobaviRenoviranjaProstorije(string brojProstorije)
        {
            List<Renoviranje> renoviranja = new List<Renoviranje>();
            foreach (Renoviranje r in repository.GetAll())
            {
                if (r.Prostorija.BrojProstorije == brojProstorije)
                    renoviranja.Add(r);
            }
            return renoviranja;
        }

        internal void SacuvajRenoviranje(Renoviranje novoRenoviranje)
        {
            repository.Save(novoRenoviranje);
        }
    }
}
