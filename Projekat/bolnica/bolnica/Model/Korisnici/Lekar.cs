using Bolnica.Model.Korisnici;
using Model.Pregledi;

namespace Model.Korisnici
{
    public class Lekar : Zaposleni
    {
        public bool PostavljenaSmena { get; set; }
        public bool ShouldSerializePostavljenaSmena()
        {
            return FileRepositoryPregled.serializeKorisnik;
        }
        public Smena Smena { get; set; }
        public bool ShouldSerializeSmena() 
        {
            return FileRepositoryPregled.serializeKorisnik;
        }

        public Specijalizacija Specijalizacija { get; set; }
        public bool ShouldSerializeSpecijalizacija()
        {
            return FileRepositoryPregled.serializeKorisnik;
        }

    }
}