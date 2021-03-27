using System;

namespace Model.Korisnici
{
   public abstract class Osoba : Korisnik
   {

        private string ime;
      public string Jmbg { get; set; }
      public string Ime { get; set; }
      public string Prezime { get; set; }
      public DateTime DatumRodjenja { get; set; }
      public string BrojTelefona { get; set; }
      public string AdresaStanovanja { get; set; }
      public string Email { get; set; }
    }
}