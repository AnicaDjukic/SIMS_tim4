using Model.Pacijenti;

namespace Model.Korisnici
{
   public class Pacijent : Osoba
   {
        private bool guest;
        private bool obrisan;
        public bool Guest { get; set; }

        public bool Obrisan { get; set; }

        public Pol Pol { get; set; }

        public ZdravstveniKarton ZdravstveniKarton { get; set; }
   
   }
}