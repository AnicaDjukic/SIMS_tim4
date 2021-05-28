using Bolnica.DTO;
using Bolnica.Forms;
using Bolnica.Model.Pregledi;
using Bolnica.Services;
using Bolnica.ViewModel;
using Model.Pregledi;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace Bolnica.Controller
{
    public class IzmeniLekLekarController
    {
        private IzmeniLekLekarService service = new IzmeniLekLekarService();
        public List<int> LekComboNaTab(IzmeniLekLekarServiceDTO lekDTO, ref String stariLek)
        {
            return service.LekComboNaTab(lekDTO,ref stariLek);
        }
        public void SelektujSastojakNaEnter(IzmeniLekLekarServiceDTO lekDTO)
        {
            service.SelektujSastojakNaEnter(lekDTO);
        }
        public void SelektujZameneNaEnter(IzmeniLekLekarServiceDTO lekDTO)
        {
            service.SelektujZameneNaEnter(lekDTO);
        }
        public List<string> ProizvodjacComboNaTab(IzmeniLekLekarServiceDTO lekDTO, ref String stariProizvodjac)
        {
            return service.ProizvodjacComboNaTab(lekDTO, ref stariProizvodjac);
        }
        public void Potvrdi(IzmeniLekLekarServiceDTO lekDTO)
        {
            service.Potvrdi(lekDTO);
        }

    }
}
