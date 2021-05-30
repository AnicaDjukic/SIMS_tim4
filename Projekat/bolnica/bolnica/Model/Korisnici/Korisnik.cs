using Model.Pregledi;

namespace Model.Korisnici
{
    public class Korisnik
    {

        public string KorisnickoIme { get; set; }

        public string Lozinka { get; set; }
        public bool ShouldSerializeLozinka()
        {

            return FileRepositoryPregled.serializeKorisnik;
        }
        public TipKorisnika TipKorisnika { get; set; }
        public bool ShouldSerializeTipKorisnika()
        {

            return FileRepositoryPregled.serializeKorisnik;
        }
    }
}