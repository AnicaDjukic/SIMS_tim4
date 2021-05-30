using Bolnica.DTO;
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

            return FileRepositoryPregled.serializeKorisnik;
        }


        public bool Obrisan { get; set; }

        public bool ShouldSerializeObrisan()
        {

            return FileRepositoryPregled.serializeKorisnik;
        }


        public Pol Pol { get; set; }
        public bool ShouldSerializePol()
        {

            return FileRepositoryPregled.serializeKorisnik;
        }


        public ZdravstveniKarton ZdravstveniKarton { get; set; }
        public bool ShouldSerializeZdravstveniKarton()
        {

            return FileRepositoryPregled.serializeKorisnik;
        }

        public List<Sastojak> Alergeni { get; set; }
        public bool ShouldSerializeAlergeni()
        {

            return FileRepositoryPregled.serializeKorisnik;
        }
    }
}