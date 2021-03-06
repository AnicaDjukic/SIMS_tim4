using Model.Korisnici;
using Model.Pregledi;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.DTO
{
    public class ReceptLekarDTO
    {
        public string nazivLeka { get; set; }
        public string dozaLeka { get; set; }
        public List<Lek> sviLekovi { get; set; }
        public string datumIzdavanja { get; set; }
        public string brojKutijaLeka { get; set; }
        public string vremeUzimanjaLeka { get; set; }
        public string datumPrekida { get; set; }
        public Pacijent trenutniPacijent { get; set; }
        public string proizvodjac { get; set; }

        public ReceptLekarDTO(string nazivLeka, string dozaLeka, List<Lek> sviLekovi, string datumIzdavanja, string brojKutijaLeka, string vremeUzimanjaLeka, string datumPrekida)
        {
           this.nazivLeka = nazivLeka;
           this.dozaLeka = dozaLeka;
           this.sviLekovi = sviLekovi;
           this.datumIzdavanja = datumIzdavanja;
           this.brojKutijaLeka = brojKutijaLeka;
           this.vremeUzimanjaLeka = vremeUzimanjaLeka;
           this.datumPrekida = datumPrekida;
        }

        public ReceptLekarDTO(string proizvodjac, string nazivLeka, string dozaLeka, List<Lek> sviLekovi)
        {
            this.nazivLeka = nazivLeka;
            this.dozaLeka = dozaLeka;
            this.sviLekovi = sviLekovi;
            this.proizvodjac = proizvodjac;
        }
        public ReceptLekarDTO(Pacijent trenutniPacijent, string proizvodjac, string nazivLeka, string dozaLeka, List<Lek> sviLekovi)
        {
            this.nazivLeka = nazivLeka;
            this.dozaLeka = dozaLeka;
            this.sviLekovi = sviLekovi;
            this.proizvodjac = proizvodjac;
            this.trenutniPacijent = trenutniPacijent;
        }
        public ReceptLekarDTO(string nazivLeka, List<Lek> sviLekovi, string proizvodjac)
        {
            this.nazivLeka = nazivLeka;
            this.sviLekovi = sviLekovi;
            this.proizvodjac = proizvodjac;
        }
        public ReceptLekarDTO(string proizvodjac, List<Lek> sviLekovi)
        {
            this.sviLekovi = sviLekovi;
            this.proizvodjac = proizvodjac;
        }
        public ReceptLekarDTO(List<Lek> sviLekovi)
        {
            this.sviLekovi = sviLekovi;
        }
    }
}
