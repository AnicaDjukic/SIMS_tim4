using System;

namespace Model.Korisnici
{
   public abstract class Korisnik
   {
      private string korisnickoIme;
      private string lozinka;
      private TipKorisnika tipKorisnika;

      public string KorisnickoIme { get; set; }
      public string Lozinka { get; set; }
      public TipKorisnika TipKorisnika { get; set; }
    }
}