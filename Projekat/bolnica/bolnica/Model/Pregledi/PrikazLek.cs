using Bolnica.DTO;

using Bolnica.Model.Korisnici;
using Bolnica.Repository.Pregledi;
using Bolnica.ViewModel;
using Model.Pacijenti;

using Model.Pregledi;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Model.Pregledi
{
    public class PrikazLek
    {
        private FileRepositorySastojak skladisteSastojaka = new FileRepositorySastojak();
        public PrikazLek(int Id, string Naziv, int KolicinaUMg, StatusLeka Status, int Zalihe, string Proizvodjac, int i,LekarServiceDTO lekarServceDTO)
        {
            this.Id = Id;
            this.Naziv = Naziv;
            this.KolicinaUMg = KolicinaUMg;
            this.Status = Status;
            this.Zalihe = Zalihe;
            this.Proizvodjac = Proizvodjac;
            this.Sastojak = DobijSastojkeZaLek(i, lekarServceDTO);
            this.Zamena = DobijZameneZaLek(i, lekarServceDTO);

        }

        public PrikazLek(int Id, string Naziv, string Proizvodjac, int KolicinaUMg, StatusLeka Status, int Zalihe)
        {
            this.Id = Id;
            this.Naziv = Naziv;
            this.Proizvodjac = Proizvodjac;
            this.KolicinaUMg = KolicinaUMg;
            this.Status = Status;
            this.Zalihe = Zalihe;
        }

        public PrikazLek() { }  
        public int Id { get; set; }
        public string Naziv { get; set; }
        public int KolicinaUMg { get; set; }
        public StatusLeka Status { get; set; }
        public int Zalihe { get; set; }
        public string Proizvodjac { get; set; }
        public String Zamena { get; set; }
        public string Sastojak { get; set; }

        public string DobijSastojkeZaLek(int i, LekarServiceDTO lekarServiceDTO)
        {
            string sastojci = "";
            for (int m = 0; m < lekarServiceDTO.lekovi[i].Sastojak.Count; m++)
            {
                foreach (Sastojak s in skladisteSastojaka.GetAll())
                {
                    if (m == 0)
                    {
                        if (lekarServiceDTO.lekovi[i].Sastojak[m].Id == s.Id)
                            sastojci = sastojci + " " + s.Naziv;
                    }
                    else
                    {
                        if (lekarServiceDTO.lekovi[i].Sastojak[m].Id == s.Id)
                            sastojci = sastojci + ", " + s.Naziv;
                    }
                }
            }
            return sastojci;
        }
        public string DobijZameneZaLek(int i, LekarServiceDTO lekarServiceDTO)
        {
            string h = "";
            for (int m = 0; m < lekarServiceDTO.lekovi[i].IdZamena.Count; m++)
            {
                Lek novi = new Lek();
                for (int mo = 0; mo < lekarServiceDTO.lekovi.Count; mo++)
                {
                    if (lekarServiceDTO.lekovi[i].IdZamena[m].Equals(lekarServiceDTO.lekovi[mo].Id))
                    {
                        novi = lekarServiceDTO.lekovi[mo];
                        break;
                    }
                }
                if (m == 0)
                {
                    h = h + " " + novi.Naziv;
                }
                else
                {
                    h = h + ", " + novi.Naziv;
                }
            }
            return h;
        }
    }
}
