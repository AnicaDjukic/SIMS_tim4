using Bolnica.Model.Korisnici;
using Model.Pregledi;

namespace Model.Korisnici
{
    public class Lekar : Zaposleni
    {
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