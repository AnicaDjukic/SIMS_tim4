using Bolnica.DTO;
using Bolnica.Model.Pregledi;
using Model.Korisnici;
using Model.Pregledi;
using Model.Prostorije;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Pregledi
{
    public class PrikazOperacije : PrikazPregleda
    {
        public TipOperacije TipOperacije { get; set; }

        public PrikazOperacije()
        {
            this.Anamneza = new Anamneza();
            this.Lekar = new Lekar();
            this.Prostorija = new Prostorija();
            this.Pacijent = new Pacijent();
        }

        public PrikazOperacije(int Id, DateTime Datum, int Trajanje, bool Zavrsen, bool Hitan, Anamneza Anamneza, TipOperacije TipOperacije, int i, LekarServiceDTO lekarServiceDTO)
        {
            this.Anamneza = new Anamneza();
            this.Lekar = new Lekar();
            this.Prostorija = new Prostorija();
            this.Pacijent = new Pacijent();
            this.Id = Id;
            this.Datum = Datum;
            this.Trajanje = Trajanje;
            this.Zavrsen = Zavrsen;
            this.Hitan = Hitan;
            this.Anamneza = Anamneza;
            this.TipOperacije = TipOperacije;
            this.Lekar = dobijLekaraZaOperaciju(i, lekarServiceDTO);
            this.Pacijent = dobijPacijentaZaOperaciju(i, lekarServiceDTO);
            this.Prostorija = dobijProstorijuZaOperaciju(i, lekarServiceDTO);

        }

        public PrikazOperacije(Operacija o)
        {
            this.Anamneza = new Anamneza();
            this.Lekar = new Lekar();
            this.Prostorija = new Prostorija();
            this.Pacijent = new Pacijent();
            this.Id = o.Id;
            this.Datum = o.Datum;
            this.Trajanje = o.Trajanje;
            this.Zavrsen = o.Zavrsen;
            this.Hitan = o.Hitan;
            this.Anamneza = o.Anamneza;
            this.TipOperacije = o.TipOperacije;
            this.Lekar = o.Lekar;
            this.Pacijent = o.Pacijent;
            this.Prostorija = o.Prostorija;

        }
        public PrikazOperacije(int Id, DateTime Datum, int Trajanje, bool Zavrsen, bool Hitan, Anamneza Anamneza)
        {
            this.Anamneza = new Anamneza();
            this.Lekar = new Lekar();
            this.Prostorija = new Prostorija();
            this.Pacijent = new Pacijent();
            this.Id = Id;
            this.Datum = Datum;
            this.Trajanje = Trajanje;
            this.Zavrsen = Zavrsen;
            this.Hitan = Hitan;
            this.Anamneza = Anamneza;
        }
        public PrikazOperacije(int Id, Anamneza Anamneza)
        {
            this.Id = Id;

            this.Anamneza = Anamneza;

        }

        public Pacijent dobijPacijentaZaOperaciju(int i, LekarServiceDTO lekarServiceDTO)
        {
            for (int p = 0; p < lekarServiceDTO.listaPacijenata.Count; p++)
            {
                if (lekarServiceDTO.listaOperacija[i].Pacijent.Jmbg.Equals(lekarServiceDTO.listaPacijenata[p].Jmbg) && lekarServiceDTO.listaPacijenata[p].Obrisan == false)
                {
                    return lekarServiceDTO.listaPacijenata[p];

                }
            }
            return null;
        }
        public Prostorija dobijProstorijuZaOperaciju(int i, LekarServiceDTO lekarServiceDTO)
        {
            for (int p = 0; p < lekarServiceDTO.listaProstorija.Count; p++)
            {
                if (lekarServiceDTO.listaOperacija[i].Prostorija.BrojProstorije.Equals(lekarServiceDTO.listaProstorija[p].BrojProstorije) && lekarServiceDTO.listaProstorija[p].Obrisana == false)
                {
                    return lekarServiceDTO.listaProstorija[p];

                }
            }
            return null;
        }
        public Lekar dobijLekaraZaOperaciju(int i, LekarServiceDTO lekarServiceDTO)
        {
            for (int p = 0; p < lekarServiceDTO.listaLekara.Count; p++)
            {
                if (lekarServiceDTO.listaOperacija[i].Lekar.Jmbg.Equals(lekarServiceDTO.listaLekara[p].Jmbg))
                {
                    return lekarServiceDTO.listaLekara[p];
                }
            }
            return null;
        }
    }
    
}
