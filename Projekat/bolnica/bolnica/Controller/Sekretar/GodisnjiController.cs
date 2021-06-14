using Bolnica.DTO.Sekretar;
using Bolnica.Service.Sekretar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Controller.Sekretar
{
    public class GodisnjiController
    {
        private GodisnjiService service = new GodisnjiService();

        public List<GodisnjiDTO> GetAllGodisnji()
        {
            return service.GetAllGodisnji();
        }

        public void ZakaziGodisnji(GodisnjiDTO godisnjiDTO, LekarDTO lekarDTO, int daniNaGodisnjem, bool pomeriTermine)
        {
            service.ZakaziGodisnji(godisnjiDTO, lekarDTO, daniNaGodisnjem, pomeriTermine);
        }
    }
}
