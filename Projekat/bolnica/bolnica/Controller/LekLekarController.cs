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
    public class LekLekarController
    {
        private LekLekarService service = new LekLekarService();
        public List<int> LekComboNaTab(LekLekarDTO lekDTO, ref String stariLek)
        {
            return service.LekComboNaTab(lekDTO,ref stariLek);
        }
        public void SelektujSastojakNaEnter(LekLekarDTO lekDTO)
        {
            service.SelektujSastojakNaEnter(lekDTO);
        }
        public void SelektujZameneNaEnter(LekLekarDTO lekDTO)
        {
            service.SelektujZameneNaEnter(lekDTO);
        }
        public List<string> ProizvodjacComboNaTab(LekLekarDTO lekDTO, ref String stariProizvodjac)
        {
            return service.ProizvodjacComboNaTab(lekDTO, ref stariProizvodjac);
        }
        public void Potvrdi(LekLekarDTO lekDTO)
        {
            service.Potvrdi(lekDTO);
        }

        public List<Lek> DobijLekove()
        {
            return service.DobijLekove();
        }
        public List<Sastojak> DobijSastojke()
        {
            return service.DobijSastojke();
        }

    }
}
