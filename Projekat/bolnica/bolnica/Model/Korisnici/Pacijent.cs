using Bolnica.Model.Pregledi;
using Model.Pacijenti;
using Model.Pregledi;
using System.Collections.Generic;

namespace Model.Korisnici
{
   public class Pacijent : Osoba
   {
        public bool Guest { get; set; }
        public bool ShouldSerializeGuest()
        {

            return FileStoragePregledi.serializeKorisnik;
        }


        public bool Obrisan { get; set; }

        public bool ShouldSerializeObrisan()
        {

            return FileStoragePregledi.serializeKorisnik;
        }


        public Pol Pol { get; set; }
        public bool ShouldSerializePol()
        {

            return FileStoragePregledi.serializeKorisnik;
        }


        public ZdravstveniKarton ZdravstveniKarton { get; set; }
        public bool ShouldSerializeZdravstveniKarton()
        {

            return FileStoragePregledi.serializeKorisnik;
        }

        public List<Sastojak> Alergeni { get; set; }
        public bool ShouldSerializeAlergeni()
        {

            return FileStoragePregledi.serializeKorisnik;
        }

    }
}