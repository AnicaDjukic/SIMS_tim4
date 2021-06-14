using Bolnica.Model.Korisnici;
using Model.Korisnici;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.DTO.Sekretar
{
    public class LekarDTO
    {
        public string KorisnickoIme { get; set; }
        public string Lozinka { get; set; }
        public TipKorisnika TipKorisnika { get; set; }
        public string Jmbg { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public string BrojTelefona { get; set; }
        public string AdresaStanovanja { get; set; }
        public string Email { get; set; }
        public int Mbr { get; set; }
        public double Plata { get; set; }
        public int BrojSlobodnihDana { get; set; }
        public int GodineStaza { get; set; }
        public bool Zaposlen { get; set; }
        public Smena Smena { get; set; }
        public Specijalizacija Specijalizacija { get; set; }

        public LekarDTO() 
        {
        }

        public LekarDTO(string korisnickoIme, string lozinka, TipKorisnika tipKorisnika, string jmbg, string ime, string prezime, DateTime datumRodjenja, string brojTelefona, string adresaStanovanja, string email, int mbr, double plata, int brojSlobodnihDana, int godineStaza, bool zaposlen, Smena smena, Specijalizacija specijalizacija)
        {
            KorisnickoIme = korisnickoIme;
            Lozinka = lozinka;
            TipKorisnika = tipKorisnika;
            Jmbg = jmbg;
            Ime = ime;
            Prezime = prezime;
            DatumRodjenja = datumRodjenja;
            BrojTelefona = brojTelefona;
            AdresaStanovanja = adresaStanovanja;
            Email = email;
            Mbr = mbr;
            Plata = plata;
            BrojSlobodnihDana = brojSlobodnihDana;
            GodineStaza = godineStaza;
            Zaposlen = zaposlen;
            Smena = smena;
            Specijalizacija = specijalizacija;
        }
    }
}
