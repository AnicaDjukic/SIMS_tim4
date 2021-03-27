using Model.Pacijenti;

namespace Model.Korisnici
{
   public class Pacijent : Osoba
   {  
      public ZdravstveniKarton zdravstveniKarton;

      public int BrojKartona { get; set; }
      public bool Guest { get; set; }

      public bool Obrisan { get; set; }

      public ZdravstveniKarton ZdravstveniKarton { get; set; }


      

    }
}