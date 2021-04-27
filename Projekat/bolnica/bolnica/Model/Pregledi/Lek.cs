using Bolnica.Model.Pregledi;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Model.Pregledi
{
    public class Lek
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Proizvodjac { get; set; }
        public int KolicinaUMg { get; set; }
        public StatusLeka Status { get; set; }
        public int Zalihe { get; set; }

        public bool Odobren { get; set; }

        public bool Obrisan { get; set; }

        public List<int> IdZamena { get; set; }
        [JsonIgnore]
        public List<Lek> Zamena { get; set; }

        public List<Sastojak> Sastojak { get; set; }

        public Lek()
        {
            Sastojak = new List<Sastojak>();
            Zamena = new List<Lek>();
            IdZamena = new List<int>();
            Obrisan = false;
        }
    }
}

