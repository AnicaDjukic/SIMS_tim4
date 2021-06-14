using Bolnica.DTO;
using Bolnica.Service.Sekretar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Controller.Sekretar
{
    public class ZdravstveniKartonController
    {
        private ZdravstveniKartonService service = new ZdravstveniKartonService();

        public ZdravstveniKartonDTO GetZdravstveniKartonByID(int brojKartona)
        {
            return service.GetZdravstveniKartonByID(brojKartona);
        }
    }
}
