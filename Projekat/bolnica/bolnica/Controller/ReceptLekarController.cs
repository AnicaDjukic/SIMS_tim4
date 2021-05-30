using Bolnica.DTO;
using Bolnica.Forms;
using Bolnica.Model.Pregledi;
using Bolnica.Services;
using Bolnica.ViewModel;
using Model.Korisnici;
using Model.Pregledi;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Bolnica.Controller
{
    public class ReceptLekarController
    {
        private ReceptLekarService service = new ReceptLekarService();

        public List<Lek> DobijLekove()
        {
            return service.DobijLekove();
        }
        public void Potvrdi(ReceptLekarDTO receptDTO)
        {
            service.Potvrdi(receptDTO);
        }
        public bool PacijentAlergicanNaLek(ReceptLekarDTO receptDTO)
        {
            return service.PacijentAlergicanNaLek(receptDTO);
        }
        public List<int> OtvoriIFiltirajNaTabLek(ReceptLekarDTO receptDTO)
        {
            return service.OtvoriIFiltirajNaTabLek(receptDTO);
        }
        public List<string> OtvoriIFiltirajNaTabProizvodjac(ReceptLekarDTO receptDTO)
        {
            return service.OtvoriIFiltirajNaTabProizvodjac(receptDTO);
        }

    }
}
