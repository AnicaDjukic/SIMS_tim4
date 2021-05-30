using Bolnica.Model.Pregledi;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Services
{
    public class InformacijeOPacijentuLekarService
    {
        FileRepositorySastojak skladisteSastojaka = new FileRepositorySastojak();
        public List<Sastojak> DobijSastojke()
        {
            return skladisteSastojaka.GetAll();
        }
    }
}
