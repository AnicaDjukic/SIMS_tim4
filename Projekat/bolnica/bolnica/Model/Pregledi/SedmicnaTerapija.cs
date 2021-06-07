using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Model.Pregledi
{
    public class SedmicnaTerapija
    {
        public String Vreme { get; set; }
        public String Ponedeljak { get; set; }
        public String Utorak { get; set; }
        public String Sreda { get; set; }
        public String Cetvrtak { get; set; }
        public String Petak { get; set; }
        public String Subota { get; set; }
        public String Nedelja { get; set; }

        public SedmicnaTerapija() { }

        public SedmicnaTerapija(String vreme, String ponedeljak, String utorak, String sreda, String cetvrtak, String petak, String subota, String nedelja)
        {
            Vreme = vreme;
            Ponedeljak = ponedeljak;
            Utorak = utorak;
            Sreda = sreda;
            Cetvrtak = cetvrtak;
            Petak = petak;
            Subota = subota;
            Nedelja = nedelja;
        }
    }
}
