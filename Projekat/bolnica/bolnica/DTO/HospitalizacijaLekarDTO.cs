using Model.Korisnici;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.DTO
{
    public class HospitalizacijaLekarDTO
    {
        public DateTime datumPocetka { get; set; }
        public DateTime datumZavrsetka { get; set; }
        public string brojBolnickeSobe { get; set; }
        public Pacijent izabraniPacijent { get; set; }

        public string razlog { get; set; }

        public HospitalizacijaLekarDTO(DateTime datumPocetka, DateTime datumZavrsetka, string brojBolnickeSobe, Pacijent izabraniPacijent, string razlog)
        {
            this.datumPocetka = datumPocetka;
            this.datumZavrsetka = datumZavrsetka;
            this.brojBolnickeSobe = brojBolnickeSobe;
            this.izabraniPacijent = izabraniPacijent;
            this.razlog = razlog;

        }
        
    }
}
