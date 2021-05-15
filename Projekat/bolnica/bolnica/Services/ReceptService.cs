using Bolnica.Forms;
using Bolnica.Model.Pregledi;
using Model.Korisnici;
using Model.Pregledi;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Bolnica.Services
{
    public class ReceptService
    {
        private object sviLekovi;

        public void Potvrdi(string nazivLeka, string dozaLeka, List<Lek> sviLekovi, string datumIzdavanja, string brojKutijaLeka, string vremeUzimanjaLeka, string datumPrekida)
        {
           
            FormNapraviAnamnezuLekar.Recepti.Add(NapraviRecept(datumIzdavanja, sviLekovi, nazivLeka, dozaLeka, brojKutijaLeka, vremeUzimanjaLeka, datumPrekida));
            
        }

        public Lek DobijIzabraniLek(string proizvodjac, string nazivLeka, string dozaLeka,List<Lek> sviLekovi)
        {
            Lek izabraniLek = new Lek();
            for (int i = 0; i < sviLekovi.Count; i++)
            {
                if (proizvodjac.Equals(sviLekovi[i].Proizvodjac) && nazivLeka.Equals(sviLekovi[i].Naziv) && int.Parse(dozaLeka).Equals(sviLekovi[i].KolicinaUMg))
                {
                    izabraniLek = sviLekovi[i];
                }
            }
            return izabraniLek;
        }

        public bool PacijentAlergicanNaLek(Pacijent trenutniPacijent, string proizvodjac, string nazivLeka, string dozaLeka, List<Lek> sviLekovi)
        {
            Lek izabraniLek = DobijIzabraniLek(proizvodjac, nazivLeka, dozaLeka, sviLekovi);
            List<Sastojak>? alergeniPacijenta = trenutniPacijent?.Alergeni;
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

        public PrikazRecepta NapraviRecept(string datumIzdavanja,List<Lek> sviLekovi,string nazivLeka,string dozaLeka,string brojKutijaLeka,string vremeUzimanjaLeka,string datumPrekida)
        {
            PrikazRecepta noviPrikazRecepta = new PrikazRecepta();
            noviPrikazRecepta.DatumIzdavanja = DateTime.Parse(datumIzdavanja);
            for (int i = 0; i < sviLekovi.Count; i++)
            {
                if (sviLekovi[i].Naziv.Equals(nazivLeka) && sviLekovi[i].KolicinaUMg.Equals(int.Parse(dozaLeka)))
                {
                    noviPrikazRecepta.lek = sviLekovi[i];
                    break;
                }
            }
            noviPrikazRecepta.Kolicina = int.Parse(brojKutijaLeka);
            noviPrikazRecepta.VremeUzimanja = TimeSpan.Parse(vremeUzimanjaLeka);
            noviPrikazRecepta.Trajanje = DateTime.Parse(datumPrekida);
            return noviPrikazRecepta;
        }

        public List<int> OtvoriIFiltirajNaTabLek(string nazivLeka,List<Lek> sviLekovi,string proizvodjac)
        {
           
                if (nazivLeka?.Length > 2 && proizvodjac?.Length > 2)
                {
                List<int> dozeLekova = new List<int>();
                    for (int i = 0; i < sviLekovi.Count; i++)
                    {
                        if (nazivLeka.Equals(sviLekovi[i].Naziv) && proizvodjac.Equals(sviLekovi[i].Proizvodjac) && !dozeLekova.Contains(sviLekovi[i].KolicinaUMg))
                        {
                            dozeLekova.Add(sviLekovi[i].KolicinaUMg);
                        }
                    }
                return dozeLekova;
                }

            return null;
            
        }

      

        public List<string> OtvoriIFiltirajNaTabProizvodjac(string proizvodjacLeka,List<Lek> sviLekovi)
        {
            
                if (proizvodjacLeka?.Length > 2)
                {
                List<string> naziviLekova = new List<string>();
               
                    for (int i = 0; i < sviLekovi.Count; i++)
                    {
                        if (proizvodjacLeka.Equals(sviLekovi[i].Proizvodjac) && !naziviLekova.Contains(sviLekovi[i].Naziv) && LekVecDodat(i,sviLekovi) == 0)
                        {
                        naziviLekova.Add(sviLekovi[i].Naziv);
                        }
                    }
                return naziviLekova;
                }
            return null;
           
        }

        public int LekVecDodat(int i,List<Lek> sviLekovi)
        {
            int lekVecDodat = 0;
            for (int p = 0; p < FormNapraviAnamnezuLekar.Recepti.Count; p++)
            {
                if (FormNapraviAnamnezuLekar.Recepti[p].lek.Id.Equals(sviLekovi[i].Id))
                {
                    lekVecDodat = 1;
                }
            }
            return lekVecDodat;
        }


    }
}
