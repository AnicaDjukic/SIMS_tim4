using Bolnica.Model.Pregledi;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Model.Pregledi
{
    public class Lek
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public bool ShouldSerializeNaziv()
        {

            return FileStorageLek.serializeLek;
        }
        public string Proizvodjac { get; set; }
        public bool ShouldSerializeProizvodjac()
        {

            return FileStorageLek.serializeLek;
        }
        public int KolicinaUMg { get; set; }
        public bool ShouldSerializeKolicinaUMg()
        {

            return FileStorageLek.serializeLek;
        }
        public StatusLeka Status { get; set; }
        public bool ShouldSerializeStatus()
        {

            return FileStorageLek.serializeLek;
        }
        public int Zalihe { get; set; }
        public bool ShouldSerializeZalihe()
        {

            return FileStorageLek.serializeLek;
        }

        public bool Obrisan { get; set; }
        public bool ShouldSerializeObrisan()
        {

            return FileStorageLek.serializeLek;
        }

        public List<int> IdZamena { get; set; }

        public bool ShouldSerializeIdZamena()
        {

            return FileStorageLek.serializeLek;
        }

        [JsonIgnore]
        public List<Lek> Zamena { get; set; }
       

        public List<Sastojak> Sastojak { get; set; }

        public bool ShouldSerializeSastojak()
        {

            return FileStorageLek.serializeLek;
        }

        public Lek()
        {
            Sastojak = new List<Sastojak>();
            Zamena = new List<Lek>();
            IdZamena = new List<int>();
            Obrisan = false;
        }
    }
}

