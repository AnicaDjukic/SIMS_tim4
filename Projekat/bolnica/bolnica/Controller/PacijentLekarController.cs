using Bolnica.Model.Pregledi;
using Bolnica.Services;
using Model.Korisnici;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Controller
{
    public class PacijentLekarController
    {
        PacijentLekarService service = new PacijentLekarService();

        public List<Sastojak> DobijSastojke()
        {
            return service.DobijSastojke();
        }
        public void HospitalizacijaPacijenta(Pacijent pacijent,int akcija)
        {
            service.HospitalizacijaPacijenta(pacijent,akcija);
        }
        public List<Hospitalizacija> DobijHospitalizacije()
        {
            return service.DobijHospitalizacije();
        }
    }
}
