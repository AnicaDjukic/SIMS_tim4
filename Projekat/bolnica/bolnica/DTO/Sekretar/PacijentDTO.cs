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
        public ZdravstveniKarton ZdravstveniKarton { get; set; }
        public List<Sastojak> Alergeni { get; set; }

        public PacijentDTO() 
        {
        }

        public PacijentDTO(string jmbg, string ime, string prezime, DateTime datumRodjenja, string brojTelefona, string adresaStanovanja, string email, string korisnickoIme, string lozinka, TipKorisnika tipKorisnika, bool guest, bool obrisan, Pol pol, List<Sastojak> alergeni)
        {
            this.Jmbg = jmbg;
            this.Ime = ime;
            this.Prezime = prezime;
            this.DatumRodjenja = datumRodjenja;
            this.BrojTelefona = brojTelefona;
            this.AdresaStanovanja = adresaStanovanja;
            this.Email = email;
            this.KorisnickoIme = korisnickoIme;
            this.Lozinka = lozinka;
            this.TipKorisnika = tipKorisnika;
            this.Guest = guest;
            this.Obrisan = obrisan;
            this.Pol = pol;
            this.Alergeni = alergeni;
        }

        public PacijentDTO(string jmbg, string ime, string prezime, DateTime datumRodjenja, string brojTelefona, string adresaStanovanja, string email, string korisnickoIme, string lozinka, TipKorisnika tipKorisnika, bool guest, bool obrisan, Pol pol, ZdravstveniKarton zdravstveniKarton, List<Sastojak> alergeni)
        {
            this.Jmbg = jmbg;
            this.Ime = ime;
            this.Prezime = prezime;
            this.DatumRodjenja = datumRodjenja;
            this.BrojTelefona = brojTelefona;
            this.AdresaStanovanja = adresaStanovanja;
            this.Email = email;
            this.KorisnickoIme = korisnickoIme;
            this.Lozinka = lozinka;
            this.TipKorisnika = tipKorisnika;
            this.Guest = guest;
            this.Obrisan = obrisan;
            this.Pol = pol;
            this.ZdravstveniKarton = zdravstveniKarton;
            this.Alergeni = alergeni;
        }
    }
}
