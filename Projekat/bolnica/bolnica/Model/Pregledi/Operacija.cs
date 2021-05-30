using Model.Korisnici;
using Model.Prostorije;

namespace Model.Pregledi
{
   public class Operacija : Pregled
   {

        public TipOperacije TipOperacije { get; set; }


        public Operacija()
        {
            this.Anamneza = new Anamneza();
            this.Lekar = new Lekar();
            this.Prostorija = new Prostorija();
            this.Pacijent = new Pacijent();
        }
        public Operacija(PrikazOperacije p)
        {
            this.Id = p.Id;
            this.Datum = p.Datum;
            this.Trajanje = p.Trajanje;
            this.Zavrsen = p.Zavrsen;
            this.Hitan = p.Hitan;
            this.Anamneza = p.Anamneza;
            this.Lekar = p.Lekar;
            this.Prostorija = p.Prostorija;
            this.Pacijent = p.Pacijent;
            this.TipOperacije = p.TipOperacije;
        }

    }

}