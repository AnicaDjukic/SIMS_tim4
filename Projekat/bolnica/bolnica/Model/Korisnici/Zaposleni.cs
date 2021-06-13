using Model.Pregledi;

namespace Model.Korisnici
{

   public class Zaposleni : Osoba
   {
      public int Mbr { get; set; }
      public bool ShouldSerializeMbr()
      {

          return FileRepositoryPregled.serializeKorisnik;
      }
      public double Plata { get; set; }
      public bool ShouldSerializePlata()
      {

          return FileRepositoryPregled.serializeKorisnik;
      }
      public int BrojSlobodnihDana { get; set; }
      public bool ShouldSerializeBrojSlobodnihDana()
      {

          return FileRepositoryPregled.serializeKorisnik;
      }
      public int GodineStaza { get; set; }
      public bool ShouldSerializeGodineStaza()
      {

          return FileRepositoryPregled.serializeKorisnik;
      }
      public bool Zaposlen { get; set; }
      public bool ShouldSerializeZaposlen()
      {

          return FileRepositoryPregled.serializeKorisnik;
      }
   }
}