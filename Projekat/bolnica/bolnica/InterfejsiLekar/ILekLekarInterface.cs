using Bolnica.DTO;
using Bolnica.Model.Pregledi;
using Model.Pregledi;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.InterfejsiLekar
{
    public interface ILekLekarInterface
    {
        List<Lek> DobijLekove();
        List<Sastojak> DobijSastojke();
        List<int> LekComboNaTab(LekLekarDTO lekDTO, ref String stariLek);
        void SelektujSastojakNaEnter(LekLekarDTO lekDTO);
        void SelektujZameneNaEnter(LekLekarDTO lekDTO);
        List<string> ProizvodjacComboNaTab(LekLekarDTO lekDTO, ref String stariProizvodjac);
        void Potvrdi(LekLekarDTO lekDTO);
        void AzurirajTabelu(LekLekarDTO lekDTO);
        void AzurirajSkladiste(LekLekarDTO lekDTO);
        PrikazLek PopuniStringSastojaka(LekLekarDTO lekDTO);
        PrikazLek PopuniStringZamena(LekLekarDTO lekDTO);
        
    }
}
