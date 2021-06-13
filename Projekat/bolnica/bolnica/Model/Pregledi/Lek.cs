using Bolnica.Model.Pregledi;
using Bolnica.Repository.Pregledi;
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

            return FileRepositoryLek.serializeLek;
        }
        public string Proizvodjac { get; set; }
        public bool ShouldSerializeProizvodjac()
        {

            return FileRepositoryLek.serializeLek;
        }
        public int KolicinaUMg { get; set; }
        public bool ShouldSerializeKolicinaUMg()
        {

            return FileRepositoryLek.serializeLek;
        }
        public StatusLeka Status { get; set; }
        public bool ShouldSerializeStatus()
        {

            return FileRepositoryLek.serializeLek;
        }
        public int Zalihe { get; set; }

        public bool ShouldSerializeZalihe()
        {

            return FileRepositoryLek.serializeLek;
        }

        public bool Obrisan { get; set; }
        public bool ShouldSerializeObrisan()
        {

            return FileRepositoryLek.serializeLek;
        }

        public List<int> IdZamena { get; set; }

        public bool ShouldSerializeIdZamena()
        {

            return FileRepositoryLek.serializeLek;
        }

        [JsonIgnore]
        public List<Lek> Zamena { get; set; }

        public List<Sastojak> Sastojak { get; set; }

        public bool ShouldSerializeSastojak()
        {

            return FileRepositoryLek.serializeLek;
        }

        public Lek()
        {
            Sastojak = new List<Sastojak>();
            Zamena = new List<Lek>();
            IdZamena = new List<int>();
            Obrisan = false;
        }

        public Lek(int Id,string Naziv, string Proizvodjac, int KolicinaUMg, StatusLeka Status, int Zalihe)

        {
            Sastojak = new List<Sastojak>();
            Zamena = new List<Lek>();
            IdZamena = new List<int>();
            Obrisan = false;
            this.Id = Id;
            this.Naziv = Naziv;
            this.Proizvodjac = Proizvodjac;
            this.KolicinaUMg = KolicinaUMg;
            this.Status = Status;
            this.Zalihe = Zalihe;
        }
    }
}

