using Bolnica.Model.Pregledi;
using Bolnica.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Controller
{
    public class InformacijeOPacijentuLekarController
    {
        InformacijeOPacijentuLekarService service = new InformacijeOPacijentuLekarService();

        public List<Sastojak> DobijSastojke()
        {
            return service.DobijSastojke();
        }
    }
}
