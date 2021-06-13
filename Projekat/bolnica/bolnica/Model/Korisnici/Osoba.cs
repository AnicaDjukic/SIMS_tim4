using Model.Pregledi;
using System;

namespace Model.Korisnici
{
    public class Osoba : Korisnik
    {
        public string Jmbg { get; set; }
        public string Ime { get; set; }
        public bool ShouldSerializeIme()
        {

            return FileRepositoryPregled.serializeKorisnik;
        }

        public string Prezime { get; set; }

        public bool ShouldSerializePrezime()
        {

            return FileRepositoryPregled.serializeKorisnik;
        }
        public DateTime DatumRodjenja { get; set; }
        public bool ShouldSerializeDatumRodjenja()
        {

            return FileRepositoryPregled.serializeKorisnik;
        }
        public string BrojTelefona { get; set; }
        public bool ShouldSerializeBrojTelefona()
        {

            return FileRepositoryPregled.serializeKorisnik;
        }
        public string AdresaStanovanja { get; set; }
        public bool ShouldSerializeAdresaStanovanja()
        {

            return FileRepositoryPregled.serializeKorisnik;
        }
        public string Email { get; set; }
        public bool ShouldSerializeEmail()
        {

            return FileRepositoryPregled.serializeKorisnik;
        }
    }
}