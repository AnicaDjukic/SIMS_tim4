using Bolnica.DTO;
using Bolnica.Forms;
using Bolnica.Model.Pregledi;
using Bolnica.Repository.Pregledi;
using Bolnica.ViewModel;
using Model.Pregledi;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace Bolnica.Services
{
    public class LekLekarService
    {
        private FileRepositorySastojak skladisteSastojaka = new FileRepositorySastojak();
        private FileRepositoryLek skladisteLekova = new FileRepositoryLek();

        public List<Lek> DobijLekove()
        {
            return skladisteLekova.GetAll();
        }
        public List<Sastojak> DobijSastojke()
        {
            return skladisteSastojaka.GetAll();
        }
        public List<int> LekComboNaTab(LekLekarDTO lekDTO,ref String stariLek)
        {
                if (lekDTO.lek.Length > 2)
                {
                    List<int> dozeLeka = new List<int>();
                    if (!stariLek.Equals(lekDTO.lek))
                    {
                        stariLek = lekDTO.lek;
                    
                        for (int i = 0; i < lekDTO.sviLekovi.Count; i++)
                        {
                            if (lekDTO.lek.Equals(lekDTO.sviLekovi[i].Naziv) && !dozeLeka.Contains(lekDTO.sviLekovi[i].KolicinaUMg))
                            {
                            dozeLeka.Add(lekDTO.sviLekovi[i].KolicinaUMg);
                            }
                        }
                    return dozeLeka;
                }
                    
                }
                return lekDTO.ItemSourceDozaLeka;
        }

        public void SelektujSastojakNaEnter(LekLekarDTO lekDTO)
        {
                for (int i = 0; i < lekDTO.sastojciKutija.Items.Count; i++)
                {
                    ListBoxItem item = lekDTO.sastojciKutija.ItemContainerGenerator.ContainerFromIndex(i) as ListBoxItem;
                    if (item.IsFocused.Equals(true))
                    {
                        if (item.IsSelected.Equals(true))
                        {
                        item.IsSelected = false;
                        }
                        else
                        {
                        item.IsSelected = true;
                    }

                    }
                }
        }

        public void SelektujZameneNaEnter(LekLekarDTO lekDTO)
        {
                for (int i = 0; i < lekDTO.zameneKutija.Items.Count; i++)
                {
                    ListBoxItem item = lekDTO.zameneKutija.ItemContainerGenerator.ContainerFromIndex(i) as ListBoxItem;
                    if (item.IsFocused.Equals(true))
                    {
                        if (item.IsSelected.Equals(true))
                        {
                        item.IsSelected = false;
                    }
                        else
                        {
                        item.IsSelected = true;
                    }

                    }
                }
        }


        public List<string> ProizvodjacComboNaTab(LekLekarDTO lekDTO,ref String stariProizvodjac)
        {
                if (lekDTO.proizvodjac.Length > 2)
                {
                List<string> naziviLekova = new List<string>();
                if (!stariProizvodjac.Equals(lekDTO.proizvodjac))
                    {
                        stariProizvodjac = lekDTO.proizvodjac;
                    
                        for (int i = 0; i < lekDTO.sviLekovi.Count; i++)
                        {
                            if (lekDTO.proizvodjac.Equals(lekDTO.sviLekovi[i].Proizvodjac) && !naziviLekova.Contains(lekDTO.sviLekovi[i].Naziv))
                            {
                            naziviLekova.Add(lekDTO.sviLekovi[i].Naziv);
                            }
                        }
                    return naziviLekova;
                }
                
                }
            return lekDTO.ItemSourceNazivLeka;
        }

        public void Potvrdi(LekLekarDTO lekDTO)
        {
            Lek izmenjeniLek = new Lek(lekDTO.izabraniLek.Id, lekDTO.lek, lekDTO.proizvodjac, int.Parse(lekDTO.doza), lekDTO.izabraniLek.Status, lekDTO.izabraniLek.Zalihe);
            PrikazLek lekZaPrikaz = new PrikazLek(lekDTO.izabraniLek.Id, lekDTO.lek, lekDTO.proizvodjac, int.Parse(lekDTO.doza), lekDTO.izabraniLek.Status, lekDTO.izabraniLek.Zalihe);
            lekZaPrikaz.Sastojak = "";
            lekZaPrikaz = PopuniStringSastojaka(new LekLekarDTO(lekDTO.sastojciKutija, lekDTO.sviSastojci, izmenjeniLek, lekZaPrikaz));
            lekZaPrikaz = PopuniStringZamena(new LekLekarDTO(lekDTO.zameneKutija, lekDTO.sviLekovi, izmenjeniLek, lekZaPrikaz));
            AzurirajTabelu(new LekLekarDTO(lekZaPrikaz));
            AzurirajSkladiste(new LekLekarDTO(izmenjeniLek));
            
        }
        private void AzurirajTabelu(LekLekarDTO lekDTO)
        {
            for (int j = 0; j < LekarViewModel.lekoviPrikaz.Count; j++)
            {
                if (LekarViewModel.lekoviPrikaz[j].Id.Equals(lekDTO.lekZaPrikaz.Id))
                {
                    LekarViewModel.lekoviPrikaz[j] = lekDTO.lekZaPrikaz;
                }
            }
        }
        private void AzurirajSkladiste(LekLekarDTO lekDTO)
        {
            skladisteLekova.Delete(lekDTO.izmenjeniLek);
            skladisteLekova.Save(lekDTO.izmenjeniLek);
        }
        private PrikazLek PopuniStringSastojaka(LekLekarDTO lekDTO)
        {
            bool dozvolaZaStringSastojak = true;
            for (int i = 0; i < lekDTO.sastojciKutija.Items.Count; i++)
            {
                ListBoxItem item = lekDTO.sastojciKutija.ItemContainerGenerator.ContainerFromIndex(i) as ListBoxItem;
                if (item.IsSelected.Equals(true))
                {
                    for (int h = 0; h < lekDTO.sviSastojci.Count; h++)
                    {
                        if (lekDTO.sviSastojci[h].Naziv.Equals(item.Content as string))
                        {
                            lekDTO.izmenjeniLek.Sastojak.Add(lekDTO.sviSastojci[h]);
                            if (dozvolaZaStringSastojak)
                            {
                                lekDTO.lekZaPrikaz.Sastojak = lekDTO.lekZaPrikaz.Sastojak + " " + lekDTO.sviSastojci[h].Naziv;
                                dozvolaZaStringSastojak = false;
                            }
                            else
                            {
                                lekDTO.lekZaPrikaz.Sastojak = lekDTO.lekZaPrikaz.Sastojak + "," + lekDTO.sviSastojci[h].Naziv;
                            }
                        }
                    }
                }
            }
            return lekDTO.lekZaPrikaz;
        }
        private PrikazLek PopuniStringZamena(LekLekarDTO lekDTO)
        {
            bool dozvolaZaStringZamena = true;
            for (int i = 0; i < lekDTO.zameneKutija.Items.Count; i++)
            {
                ListBoxItem item = lekDTO.zameneKutija.ItemContainerGenerator.ContainerFromIndex(i) as ListBoxItem;
                if (item.IsSelected.Equals(true))
                {
                    for (int h = 0; h < lekDTO.sviLekovi.Count; h++)
                    {
                        string k = lekDTO.sviLekovi[h].Proizvodjac + ", " + lekDTO.sviLekovi[h].Naziv + ", " + lekDTO.sviLekovi[h].KolicinaUMg;
                        if (k.Equals(item.Content as string))
                        {
                            lekDTO.izmenjeniLek.IdZamena.Add(lekDTO.sviLekovi[h].Id);
                            if (dozvolaZaStringZamena)
                            {
                                lekDTO.lekZaPrikaz.Zamena = lekDTO.lekZaPrikaz.Zamena + " " + lekDTO.sviLekovi[h].Naziv;
                                dozvolaZaStringZamena = false;
                            }
                            else
                            {
                                lekDTO.lekZaPrikaz.Zamena = lekDTO.lekZaPrikaz.Zamena + "," + lekDTO.sviLekovi[h].Naziv;
                            }
                        }
                    }
                }
            }
            return lekDTO.lekZaPrikaz;
        }




    }
}
