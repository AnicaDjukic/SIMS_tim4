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

            return FileStoragePregledi.serializeKorisnik;
        }

        public string Prezime { get; set; }

        public bool ShouldSerializePrezime()
        {

            return FileStoragePregledi.serializeKorisnik;
        }
        public DateTime DatumRodjenja { get; set; }
        public bool ShouldSerializeDatumRodjenja()
        {

            return FileStoragePregledi.serializeKorisnik;
        }
        public string BrojTelefona { get; set; }
        public bool ShouldSerializeBrojTelefona()
        {

            return FileStoragePregledi.serializeKorisnik;
        }
        public string AdresaStanovanja { get; set; }
        public bool ShouldSerializeAdresaStanovanja()
        {

            return FileStoragePregledi.serializeKorisnik;
        }
        public string Email { get; set; }
        public bool ShouldSerializeEmail()
        {

            return FileStoragePregledi.serializeKorisnik;
        }
    }
}