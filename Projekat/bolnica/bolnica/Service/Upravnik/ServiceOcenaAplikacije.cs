using Bolnica.Model.Korisnici;
using Bolnica.Repository.Korisnici;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Service.Upravnik
{
    public class ServiceOcenaAplikacije
    {
        private IRepositoryOcenaAplikacije repository;

        public ServiceOcenaAplikacije()
        {
            repository = new FileRepositoryOcenaAplikacije();
        }

        public void SacuvajOcenuAplikacije(OcenaAplikacije ocena)
        {
            repository.Save(ocena);
        }
    }
}
