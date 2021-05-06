using Bolnica.Model.Pregledi;
using Model.Pacijenti;
using System.Collections.Generic;

namespace Model.Korisnici
{
   public class Pacijent : Osoba
   {
        public bool Guest { get; set; }

        public bool Obrisan { get; set; }

        public Pol Pol { get; set; }

        public ZdravstveniKarton ZdravstveniKarton { get; set; }

        public List<Sastojak> Alergeni { get; set; }
   
   }
}