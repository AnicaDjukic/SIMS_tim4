using Bolnica.DTO.Sekretar;
using Bolnica.Service.Sekretar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Controller.Sekretar
{
    public class LekarController
    {
        private LekarService service = new LekarService();

        public LekarDTO GetLekarById(string korisnickoIme) 
        {
            return service.GetLekarById(korisnickoIme);
        }

        public void UpdateLekara(LekarDTO lekarDTO)
        {
            service.UpdateLekara(lekarDTO);
        }
    }
}
