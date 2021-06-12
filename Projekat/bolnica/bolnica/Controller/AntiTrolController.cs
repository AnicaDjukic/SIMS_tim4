using Bolnica.Service;
using Model.Korisnici;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Controller
{
    public class AntiTrolController
    {
        private AntiTrolService service = new AntiTrolService();

        public int DobijBrojAktivnosti(Pacijent trenutniPacijent)
        {
            return service.DobijBrojAktivnosti(trenutniPacijent);
        }
    }
}
