using Model.Pregledi;

namespace Model.Korisnici
{
   public class Zaposleni : Osoba
   {
      public int Mbr { get; set; }
      public bool ShouldSerializeMbr()
      {

          return FileStoragePregledi.serializeKorisnik;
      }
      public double Plata { get; set; }
      public bool ShouldSerializePlata()
      {

          return FileStoragePregledi.serializeKorisnik;
      }
      public int BrojSlobodnihDana { get; set; }
      public bool ShouldSerializeBrojSlobodnihDana()
      {

          return FileStoragePregledi.serializeKorisnik;
      }
      public int GodineStaza { get; set; }
      public bool ShouldSerializeGodineStaza()
      {

          return FileStoragePregledi.serializeKorisnik;
      }
      public bool Zaposlen { get; set; }
      public bool ShouldSerializeZaposlen()
      {

          return FileStoragePregledi.serializeKorisnik;
      }
    }
}