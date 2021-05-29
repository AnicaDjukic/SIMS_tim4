using Bolnica.Forms;
using Bolnica.Model.Pregledi;
using Bolnica.ViewModel;
using Model.Pregledi;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace Bolnica.Services
{
    public class IzmeniLekLekarService
    {
        private FileRepositorySastojak sviSastojci = new FileRepositorySastojak();
        private FileStorageLek sviLekovi = new FileStorageLek();

        public List<int> NazivLekaOpenTab(string nazivLeka,ref String stariLek,List<Lek> lekovi,List<int> ItemSourceDozaLeka)
        {
           
                if (nazivLeka.Length > 2)
                {
                    List<int> dozeLeka = new List<int>();
                    if (!stariLek.Equals(nazivLeka))
                    {
                        stariLek = nazivLeka;
                    
                        for (int i = 0; i < lekovi.Count; i++)
                        {
                            if (nazivLeka.Equals(lekovi[i].Naziv) && !dozeLeka.Contains(lekovi[i].KolicinaUMg))
                            {
                            dozeLeka.Add(lekovi[i].KolicinaUMg);
                            }
                        }
                    return dozeLeka;
                }
                    
                }
                return ItemSourceDozaLeka;
        }

        public void SelectSastojakNaEnter(ListBox textSastojci)
        {
                for (int i = 0; i < textSastojci.Items.Count; i++)
                {
                    ListBoxItem item = textSastojci.ItemContainerGenerator.ContainerFromIndex(i) as ListBoxItem;
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

        public void SelectZamenaNaEnter(ListBox textZamene)
        {
                for (int i = 0; i < textZamene.Items.Count; i++)
                {
                    ListBoxItem item = textZamene.ItemContainerGenerator.ContainerFromIndex(i) as ListBoxItem;
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

      
        public List<string> ProizvodjacOpenTab(string proizvodjac,ref String stariProizvodjac,List<Lek> lekovi,List<string> ItemSourceNazivLeka)
        {
                if (proizvodjac.Length > 2)
                {
                List<string> naziviLekova = new List<string>();
                if (!stariProizvodjac.Equals(proizvodjac))
                    {
                        stariProizvodjac = proizvodjac;
                    
                        for (int i = 0; i < lekovi.Count; i++)
                        {
                            if (proizvodjac.Equals(lekovi[i].Proizvodjac) && !naziviLekova.Contains(lekovi[i].Naziv))
                            {
                            naziviLekova.Add(lekovi[i].Naziv);
                            }
                        }
                    return naziviLekova;
                }
                
                }
            return ItemSourceNazivLeka;
        }

        public void Potvrdi(Lek l,string doza,string lek,string proizvodjac,ListBox textSastojci,List<Sastojak> sas,ListBox textZamene,List<Lek> lekovi)
        {
            Lek lekk = new Lek();
            lekk.Id = l.Id;
            lekk.KolicinaUMg = int.Parse(doza);
            lekk.Naziv = lek;
            lekk.Proizvodjac = proizvodjac;
            lekk.Zalihe = l.Zalihe;
            lekk.Sastojak = new List<Sastojak>();
            lekk.IdZamena = new List<int>();
            lekk.Status = l.Status;
            PrikazLek lekpri = new PrikazLek();
            lekpri.Sastojak = "";
            int doz = 0;
            int dozz = 0;
            for (int i = 0; i < textSastojci.Items.Count; i++)
            {
                ListBoxItem item = textSastojci.ItemContainerGenerator.ContainerFromIndex(i) as ListBoxItem;
                if (item.IsSelected.Equals(true))
                {
                    for (int h = 0; h < sas.Count; h++)
                    {
                        if (sas[h].Naziv.Equals(item.Content as string))
                        {
                            lekk.Sastojak.Add(sas[h]);
                            if (doz == 0)
                            {
                                lekpri.Sastojak = lekpri.Sastojak + " " + sas[h].Naziv;
                                doz = 1;
                            }
                            else
                            {
                                lekpri.Sastojak = lekpri.Sastojak + "," + sas[h].Naziv;
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < textZamene.Items.Count; i++)
            {
                ListBoxItem item = textZamene.ItemContainerGenerator.ContainerFromIndex(i) as ListBoxItem;
                if (item.IsSelected.Equals(true))
                {
                    for (int h = 0; h < lekovi.Count; h++)
                    {
                        string k = lekovi[h].Proizvodjac + ", " + lekovi[h].Naziv + ", " + lekovi[h].KolicinaUMg;
                        if (k.Equals(item.Content as string))
                        {
                            lekk.IdZamena.Add(lekovi[h].Id);
                            if (dozz == 0)
                            {
                                lekpri.Zamena = lekpri.Zamena + " " + lekovi[h].Naziv;
                                dozz = 1;
                            }
                            else
                            {
                                lekpri.Zamena = lekpri.Zamena + "," + lekovi[h].Naziv;
                            }
                        }
                    }
                }
            }


            lekpri.Id = l.Id;
            lekpri.KolicinaUMg = int.Parse(doza);
            lekpri.Naziv = lek;
            lekpri.Zalihe = l.Zalihe;
            lekpri.Proizvodjac = proizvodjac;
            lekpri.Status = l.Status;

            for (int j = 0; j < LekarViewModel.lekoviPrikaz.Count; j++)
            {
                if (LekarViewModel.lekoviPrikaz[j].Id.Equals(lekpri.Id))
                {
                    LekarViewModel.lekoviPrikaz[j] = lekpri;
                }
            }
            sviLekovi.Delete(lekk);
            sviLekovi.Save(lekk);

        }




    }
}
