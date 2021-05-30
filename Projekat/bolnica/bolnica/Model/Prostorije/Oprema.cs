using System;
using System.Collections.Generic;
using System.Text;
using Bolnica.Repository.Prostorije;
using Model.Prostorije;
using Newtonsoft.Json;

namespace Bolnica.Model.Prostorije
{
    public class Oprema
    {
        public string Sifra { get; set; }
        public string Naziv { get; set; }
        public bool ShouldSerializeNaziv()
        {
            
            return FileRepositoryZaliha.serializeOprema;
        }
        public TipOpreme TipOpreme { get; set; }
        public bool ShouldSerializeTipOpreme()
        {

            return FileRepositoryZaliha.serializeOprema;
        }
        public int Kolicina { get; set; }
        public bool ShouldSerializeKolicina()
        {

            return FileRepositoryZaliha.serializeOprema;
        }

    }
}
