using Bolnica.DTO.Sekretar;
using Bolnica.Service.Sekretar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Controller.Sekretar
{
    public class SastojakController
    {
        private SastojakService service = new SastojakService();

        public List<SastojakDTO> GetDodatiAlergeni(string jmbg) 
        {
            return service.GetDodatiAlergeni(jmbg);
        }

        public List<SastojakDTO> GetSviAlergeni(List<SastojakDTO> dodatiAlergeni) 
        {
            return service.GetSviAlergeni(dodatiAlergeni);
        }
    }
}
