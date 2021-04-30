using Model.Prostorije;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Model.Prostorije
{
    public class Renoviranje
    {
        public DateTime PocetakRenoviranja { get; set; }
        public DateTime KrajRenoviranja { get; set; }
        public string Opis { get; set; }
        public string BrojProstorije { get; set; }
        [JsonIgnore]
        public Prostorija Prostorija { get; set; }
    }
}
