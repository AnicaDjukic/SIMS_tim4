using Model.Pacijenti;
using System;

namespace Model.Korisnici
{
   public class Pacijent : Osoba
   {
      private int brojKartona;
      private bool guest;
      private bool obrisan;
      
      public ZdravstveniKarton zdravstveniKarton;

      public int BrojKartona { get; set; }
      public bool Guest { get; set; }

      public bool Obrisan { get; set; }

      public ZdravstveniKarton ZdravstveniKarton { get; set; }


      

    }
}