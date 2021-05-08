using Model.Korisnici;
using Model.Pregledi;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Model.Pregledi
{
    class Ocena
    {
        public int IdOcene { get; set; }
        public DateTime Datum { get; set; }
        public int BrojOcene { get; set; }
        public string Sadrzaj { get; set; }
        public string PosiljalacJMBG { get; set; }
        public String Primalac { get; set; }
    }
}
