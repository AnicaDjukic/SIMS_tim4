using Bolnica.DTO;
//using Bolnica.ViewModel;
using Model.Korisnici;
using Model.Pregledi;
using Model.Prostorije;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Bolnica.Model.Pregledi
{
    public class PrikazPregleda
    {
        public int Id { get; set; }
        public DateTime Datum { get; set; }
        public int Trajanje { get; set; }
        public bool Zavrsen { get; set; }
        public bool Hitan { get; set; }
        public Anamneza Anamneza { get; set; }
        public Lekar Lekar { get; set; }
        public Prostorija Prostorija { get; set; }
        public Pacijent Pacijent { get; set; }

        public PrikazPregleda()
        {
            this.Anamneza = new Anamneza();
            this.Lekar = new Lekar();
            this.Prostorija = new Prostorija();
            this.Pacijent = new Pacijent();
        }
        public PrikazPregleda(int Id, DateTime Datum,int Trajanje,bool Zavrsen,bool Hitan,Anamneza Anamneza, int i,LekarServiceDTO lekarServiceDTO)
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
            this.Lekar = dobijLekaraZaPregled(i, lekarServiceDTO);
            this.Pacijent = dobijPacijentaZaPregled(i, lekarServiceDTO);
            this.Prostorija = dobijProstorijuZaPregled(i, lekarServiceDTO);
            
        }

        public PrikazPregleda(int Id, Anamneza Anamneza)
        {
            this.Id = Id;

            this.Anamneza = Anamneza;

        }

        public PrikazPregleda(int Id, DateTime Datum, int Trajanje, bool Zavrsen, bool Hitan, Anamneza Anamneza)
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

        public Pacijent dobijPacijentaZaPregled(int i, LekarServiceDTO lekarServiceDTO)
        {
            for (int p = 0; p < lekarServiceDTO.listaPacijenata.Count; p++)
            {
                if (lekarServiceDTO.listaPregleda[i].Pacijent.Jmbg.Equals(lekarServiceDTO.listaPacijenata[p].Jmbg) && lekarServiceDTO.listaPacijenata[p].Obrisan == false)
                {
                    return lekarServiceDTO.listaPacijenata[p];

                }
            }
            return null;
        }
        public Prostorija dobijProstorijuZaPregled(int i, LekarServiceDTO lekarServiceDTO)
        {
            for (int p = 0; p < lekarServiceDTO.listaProstorija.Count; p++)
            {
                if (lekarServiceDTO.listaPregleda[i].Prostorija.BrojProstorije.Equals(lekarServiceDTO.listaProstorija[p].BrojProstorije) && lekarServiceDTO.listaProstorija[p].Obrisana == false)
                {
                    return lekarServiceDTO.listaProstorija[p];

                }
            }
            return null;
        }
        public Lekar dobijLekaraZaPregled(int i, LekarServiceDTO lekarServiceDTO)
        {
            for (int p = 0; p < lekarServiceDTO.listaLekara.Count; p++)
            {
                if (lekarServiceDTO.listaPregleda[i].Lekar.Jmbg.Equals(lekarServiceDTO.listaLekara[p].Jmbg))
                {
                    return lekarServiceDTO.listaLekara[p];
                }
            }
            return null;
        }

        public PrikazPregleda(int id, DateTime datum, int trajanje, bool zavrsen, bool hitan, Anamneza anamneza, Lekar lekar, Prostorija prostorija, Pacijent pacijent) : this(id, datum, trajanje, zavrsen, hitan, anamneza)
        {
            Lekar = lekar;
            Prostorija = prostorija;
            Pacijent = pacijent;
        }
    }
}
