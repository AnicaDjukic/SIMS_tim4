using Bolnica.Model.Pregledi;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Services
{
    public class InformacijeOPacijentuLekarService
    {
        FileStorageSastojak skladisteSastojaka = new FileStorageSastojak();
        public List<Sastojak> DobijSastojke()
        {
            return skladisteSastojaka.GetAll();
        }
    }
}
