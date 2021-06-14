using Bolnica.DTO;
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

        public List<SastojakDTO> GetDodatiAlergeni(PacijentDTO pacijentDTO) 
        {
            return service.GetDodatiAlergeni(pacijentDTO);
        }

        public List<SastojakDTO> GetSviAlergeni(List<SastojakDTO> dodatiAlergeni) 
        {
            return service.GetSviAlergeni(dodatiAlergeni);
        }

        public SastojakDTO GetAlergenById(int id) 
        {
            return service.GetAlergenById(id);
        } 
    }
}
