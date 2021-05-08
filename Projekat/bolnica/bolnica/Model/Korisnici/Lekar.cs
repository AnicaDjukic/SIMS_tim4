using Model.Pregledi;

namespace Model.Korisnici
{
    public class Lekar : Zaposleni
    {

        public Specijalizacija Specijalizacija { get; set; }
        public bool ShouldSerializeSpecijalizacija()
        {

            return FileStoragePregledi.serializeKorisnik;
        }

    }
}