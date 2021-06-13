using Bolnica.Service;
using Model.Pregledi;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Controller
{
    public class PregledController
    {
        private PregledService service = new PregledService();

        public void IzmeniPregled(Pregled noviPregled)
        {
            service.IzmeniPregled(noviPregled);
        }
    }
}
