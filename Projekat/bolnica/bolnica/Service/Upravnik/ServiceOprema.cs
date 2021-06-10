using Bolnica.Model.Prostorije;
using Bolnica.Repository.Prostorije;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Services.Prostorije
{
    public class ServiceOprema
    {
        private RepositoryOprema repository;
        public ServiceOprema()
        {
            repository = new FileRepositoryOprema();
        }
        public bool OpremaPostoji(string sifra)
        {
            bool postoji = false;
            foreach (Oprema o in repository.GetAll())
            {
                if (o.Sifra == sifra)
                {
                    postoji = true;
                    break;
                }
            }
            return postoji;
        }

        public void ObrisiOpremu(string sifra)
        {
            repository.DeleteById(sifra);
        }

        public void SacuvajOpremu(Oprema oprema)
        {
            repository.Save(oprema);
        }

        public Oprema DobaviOpremu(string sifra)
        {
            return repository.GetById(sifra);
        }
    }
}
