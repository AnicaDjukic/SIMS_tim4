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
        public Prostorija Prostorija { get; set; }
        public int BrojNovihProstorija { get; set; }
        public List<Prostorija> ProstorijeZaSpajanje { get; set; }

        public Renoviranje()
        {
            BrojNovihProstorija = 0;
            ProstorijeZaSpajanje = new List<Prostorija>();
        }
    }
}
