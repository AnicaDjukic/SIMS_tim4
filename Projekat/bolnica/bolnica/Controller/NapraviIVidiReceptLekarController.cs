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
    public class NapraviIVidiReceptLekarController
    {
        private NapraviIVidiReceptLekarService service = new NapraviIVidiReceptLekarService();

        public void Potvrdi(NapraviIVidiReceptLekarServiceDTO receptDTO)
        {
            service.Potvrdi(receptDTO);
        }
        public bool PacijentAlergicanNaLek(NapraviIVidiReceptLekarServiceDTO receptDTO)
        {
            return service.PacijentAlergicanNaLek(receptDTO);
        }
        public List<int> OtvoriIFiltirajNaTabLek(NapraviIVidiReceptLekarServiceDTO receptDTO)
        {
            return service.OtvoriIFiltirajNaTabLek(receptDTO);
        }
        public List<string> OtvoriIFiltirajNaTabProizvodjac(NapraviIVidiReceptLekarServiceDTO receptDTO)
        {
            return service.OtvoriIFiltirajNaTabProizvodjac(receptDTO);
        }

    }
}
