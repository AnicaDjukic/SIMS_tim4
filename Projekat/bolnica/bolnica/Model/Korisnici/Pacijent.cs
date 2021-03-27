using Model.Pacijenti;
using System;

namespace Model.Korisnici
{
   public class Pacijent : Osoba
   {
        private bool guest;
        private bool obrisan;
        private Pol pol;

        public bool Guest { get; set; }

        public bool Obrisan { get; set; }

        public Pol Pol { get; set; }

        public ZdravstveniKarton ZdravstveniKarton { get; set; }
   
   }
}