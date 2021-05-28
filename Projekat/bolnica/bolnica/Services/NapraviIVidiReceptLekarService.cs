using Bolnica.DTO;
using Bolnica.Forms;
using Bolnica.Model.Pregledi;
using Bolnica.ViewModel;
using Model.Korisnici;
using Model.Pregledi;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Bolnica.Services
{
    public class NapraviIVidiReceptLekarService
    {


        public void Potvrdi(NapraviIVidiReceptLekarServiceDTO receptDTO)
        {
           
            NapraviAnamnezuLekarViewModel.Recepti.Add(NapraviRecept(receptDTO));
            
        }

        private Lek DobijIzabraniLek(NapraviIVidiReceptLekarServiceDTO receptDTO)
        {
            Lek izabraniLek = new Lek();
            for (int i = 0; i < receptDTO.sviLekovi.Count; i++)
            {
                if (receptDTO.proizvodjac.Equals(receptDTO.sviLekovi[i].Proizvodjac) && receptDTO.nazivLeka.Equals(receptDTO.sviLekovi[i].Naziv) && int.Parse(receptDTO.dozaLeka).Equals(receptDTO.sviLekovi[i].KolicinaUMg))
                {
                    izabraniLek = receptDTO.sviLekovi[i];
                }
            }
            return izabraniLek;
        }

        public bool PacijentAlergicanNaLek(NapraviIVidiReceptLekarServiceDTO receptDTO)
        {
            Lek izabraniLek = DobijIzabraniLek(receptDTO);
            List<Sastojak>? alergeniPacijenta = receptDTO.trenutniPacijent?.Alergeni;
            if (alergeniPacijenta != null)
            {
                for (int o = 0; o < alergeniPacijenta.Count; o++)
                {
                    for (int m = 0; m < izabraniLek.Sastojak.Count; m++)
                    {
                        if (alergeniPacijenta[o].Id.Equals(izabraniLek.Sastojak[m].Id))
                        {
                            MessageBox.Show("Pacijent je alergican na izabrani lek");
                            return true;
                        }

                    }
                }
            }
            return false;

        }

        private PrikazRecepta NapraviRecept(NapraviIVidiReceptLekarServiceDTO receptDTO)
        {
            PrikazRecepta noviPrikazRecepta = new PrikazRecepta(DateTime.Parse(receptDTO.datumIzdavanja), int.Parse(receptDTO.brojKutijaLeka), TimeSpan.Parse(receptDTO.vremeUzimanjaLeka), DateTime.Parse(receptDTO.datumPrekida));
            for (int i = 0; i < receptDTO.sviLekovi.Count; i++)
            {
                if (receptDTO.sviLekovi[i].Naziv.Equals(receptDTO.nazivLeka) && receptDTO.sviLekovi[i].KolicinaUMg.Equals(int.Parse(receptDTO.dozaLeka)))
                {
                    noviPrikazRecepta.lek = receptDTO.sviLekovi[i];
                    break;
                }
            }
            return noviPrikazRecepta;
        }

        public List<int> OtvoriIFiltirajNaTabLek(NapraviIVidiReceptLekarServiceDTO receptDTO)
        {
           
                if (receptDTO.nazivLeka?.Length > 2 && receptDTO.proizvodjac?.Length > 2)
                {
                List<int> dozeLekova = new List<int>();
                    for (int i = 0; i < receptDTO.sviLekovi.Count; i++)
                    {
                        if (receptDTO.nazivLeka.Equals(receptDTO.sviLekovi[i].Naziv) && receptDTO.proizvodjac.Equals(receptDTO.sviLekovi[i].Proizvodjac) && !dozeLekova.Contains(receptDTO.sviLekovi[i].KolicinaUMg))
                        {
                            dozeLekova.Add(receptDTO.sviLekovi[i].KolicinaUMg);
                        }
                    }
                return dozeLekova;
                }

            return null;
            
        }



        public List<string> OtvoriIFiltirajNaTabProizvodjac(NapraviIVidiReceptLekarServiceDTO receptDTO)
        {
            
                
            if (receptDTO.proizvodjac?.Length > 2)
                {
                List<string> naziviLekova = new List<string>();
               
                    for (int i = 0; i < receptDTO.sviLekovi.Count; i++)
                    {
                        if (receptDTO.proizvodjac.Equals(receptDTO.sviLekovi[i].Proizvodjac) && !naziviLekova.Contains(receptDTO.sviLekovi[i].Naziv) && LekVecDodat(i, receptDTO) == 0)
                        {
                        naziviLekova.Add(receptDTO.sviLekovi[i].Naziv);
                        }
                    }
                return naziviLekova;
                }
            return null;
           
        }

        private int LekVecDodat(int i, NapraviIVidiReceptLekarServiceDTO receptDTO)
        {
            int lekVecDodat = 0;
            for (int p = 0; p < NapraviAnamnezuLekarViewModel.Recepti.Count; p++)
            {
                if (NapraviAnamnezuLekarViewModel.Recepti[p].lek.Id.Equals(receptDTO.sviLekovi[i].Id))
                {
                    lekVecDodat = 1;
                }
            }
            return lekVecDodat;
        }


    }
}
