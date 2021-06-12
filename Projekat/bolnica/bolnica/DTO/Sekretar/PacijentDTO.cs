using Bolnica.DTO.Sekretar;
using Bolnica.Model.Pregledi;
using Model.Korisnici;
using Model.Pacijenti;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace Bolnica.DTO
{
    public class PacijentDTO
    {
        public string Jmbg { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public string BrojTelefona { get; set; }
        public string AdresaStanovanja { get; set; }
        public string Email { get; set; }
        public string KorisnickoIme { get; set; }
        public string Lozinka { get; set; }
        public TipKorisnika TipKorisnika { get; set; }
        public bool Guest { get; set; }
        public bool Obrisan { get; set; }
        public Pol Pol { get; set; }
        public int BrojKartona { get; set; }
        public string Zanimanje { get; set; }
        public BracniStatus BracniStatus { get; set; }
        public bool Osiguranje { get; set; }
        public List<int> IdsAlergena { get; set; }

        public PacijentDTO() 
        {
        }

        public PacijentDTO(string jmbg, string ime, string prezime, DateTime datumRodjenja, string brojTelefona, string adresaStanovanja, string email, string korisnickoIme, string lozinka, TipKorisnika tipKorisnika, bool guest, bool obrisan, Pol pol, int brojKartona, string zanimanje, BracniStatus bracniStatus, bool osiguranje, List<int> idsAlergena)
        {
            Jmbg = jmbg;
            Ime = ime;
            Prezime = prezime;
            DatumRodjenja = datumRodjenja;
            BrojTelefona = brojTelefona;
            AdresaStanovanja = adresaStanovanja;
            Email = email;
            KorisnickoIme = korisnickoIme;
            Lozinka = lozinka;
            TipKorisnika = tipKorisnika;
            Guest = guest;
            Obrisan = obrisan;
            Pol = pol;
            BrojKartona = brojKartona;
            Zanimanje = zanimanje;
            BracniStatus = bracniStatus;
            Osiguranje = osiguranje;
            IdsAlergena = idsAlergena;
        }

        public PacijentDTO(string jmbg, string ime, string prezime, DateTime datumRodjenja, string brojTelefona, string adresaStanovanja, string email, TipKorisnika tipKorisnika, bool guest, bool obrisan, Pol pol, List<int> idsAlergena)
        {
            Jmbg = jmbg;
            Ime = ime;
            Prezime = prezime;
            DatumRodjenja = datumRodjenja;
            BrojTelefona = brojTelefona;
            AdresaStanovanja = adresaStanovanja;
            Email = email;
            TipKorisnika = tipKorisnika;
            Guest = guest;
            Obrisan = obrisan;
            Pol = pol;
            IdsAlergena = idsAlergena;
        }
    }
}
