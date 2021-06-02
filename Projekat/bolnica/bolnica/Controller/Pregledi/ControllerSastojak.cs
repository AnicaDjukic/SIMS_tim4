using Bolnica.DTO;
using Bolnica.Model.Pregledi;
using Bolnica.Services.Pregledi;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Controller.Pregledi
{
    public class ControllerSastojak
    {
        ServiceSastojak serviceSastojak = new ServiceSastojak();
        public List<SastojakDTO> DobaviSastojkeLeka(int id)
        {
            List<SastojakDTO> sastojci = new List<SastojakDTO>();
            foreach (Sastojak s in serviceSastojak.DobaviSastojkeLeka(id))
            {
                SastojakDTO sastojak = new SastojakDTO(s);
                sastojci.Add(sastojak);
            }

            return sastojci;
        }

        internal List<SastojakDTO> DobaviSveSastojke()
        {
            List<SastojakDTO> sastojci = new List<SastojakDTO>();
            foreach(Sastojak s in serviceSastojak.DobaviSveSastojke())
            {
                SastojakDTO sastojak = new SastojakDTO(s);
                sastojci.Add(sastojak);
            }

            return sastojci;
        }

        internal void ObrisiSastojak(int id)
        {
            serviceSastojak.ObrisiSastojak(id);
        }

        internal bool SastojakPostoji(string naziv)
        {
            return serviceSastojak.SastojakPostoji(naziv);
        }

        internal void SacuvajSastojak(SastojakDTO noviSastojak)
        {
            Sastojak sastojak = new Sastojak { Id = noviSastojak.Id, Naziv = noviSastojak.Naziv };
            serviceSastojak.SacuvajSastojak(sastojak);
        }

        internal int DobaviNoviId()
        {
            return serviceSastojak.MaxId() + 1;
        }
    }
}
