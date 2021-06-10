using Bolnica.Model.Korisnici;
using Bolnica.Repository.Korisnici;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Controller.Korisnici
{
    public class ControllerOcenaAplikacije
    {
        private IRepositoryOcenaAplikacije repository;

        public ControllerOcenaAplikacije()
        {
            repository = new FileRepositoryOcenaAplikacije();
        }

        public void SacuvajOcenuAplikacije(OcenaAplikacije ocena)
        {
            repository.Save(ocena);
        }
    }
}
