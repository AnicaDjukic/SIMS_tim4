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
        public List<Sastojak> DobaviSastojkeLeka(int id)
        {
            return serviceSastojak.DobaviSastojkeLeka(id);
        }
    }
}
