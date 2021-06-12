using Bolnica.Service;
using Model.Korisnici;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Controller
{
    public class IstorijaOcenaController
    {
        private IstorijaOcenaService service = new IstorijaOcenaService();

        public void PopuniTabeluOcena(Pacijent trenutniPacijent)
        {
            service.PopuniTabeluOcena(trenutniPacijent);
        }
    }
}
