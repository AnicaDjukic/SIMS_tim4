using Bolnica.Model.Korisnici;
using Bolnica.Repository.Korisnici;
using Bolnica.Service.Upravnik;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Controller.Korisnici
{
    public class ControllerOcenaAplikacije
    {
        private ServiceOcenaAplikacije service;

        public ControllerOcenaAplikacije()
        {
            service = new ServiceOcenaAplikacije();
        }

        public void SacuvajOcenuAplikacije(OcenaAplikacije ocena)
        {
            service.SacuvajOcenuAplikacije(ocena);
        }
    }
}
