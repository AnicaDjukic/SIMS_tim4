using Model.Korisnici;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.DTO.Sekretar
{
    public class KorisnikDTO
    {
        public string KorisnickoIme { get; set; }
        public string Lozinka { get; set; }
        public TipKorisnika TipKorisnika { get; set; }

        public KorisnikDTO() 
        {
        }

        public KorisnikDTO(string korisnickoIme, string lozinka, TipKorisnika tipKorisnika)
        {
            KorisnickoIme = korisnickoIme;
            Lozinka = lozinka;
            TipKorisnika = tipKorisnika;
        }
    }
}
