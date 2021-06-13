using Bolnica.DTO.Sekretar;
using Bolnica.Model.Korisnici;
using Bolnica.Repository.Korisnici;
using Model.Korisnici;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Service.Sekretar
{
    public class KorisnikService
    {
        private IRepositoryKorisnik skladisteKorisnika;

        public KorisnikService()
        {
            skladisteKorisnika = new FileRepositoryKorisnik();
        }

        public void SaveKorisnika(KorisnikDTO korisnikDTO) 
        {
            Korisnik korisnik = new Korisnik { KorisnickoIme = korisnikDTO.KorisnickoIme, Lozinka = korisnikDTO.Lozinka, TipKorisnika = korisnikDTO.TipKorisnika };
            skladisteKorisnika.Save(korisnik);
        }

        public void DeleteKorisnika(KorisnikDTO korisnikDTO)
        {
            Korisnik korisnik = new Korisnik { KorisnickoIme = korisnikDTO.KorisnickoIme, Lozinka = korisnikDTO.Lozinka, TipKorisnika = korisnikDTO.TipKorisnika };
            skladisteKorisnika.Delete(korisnik);
        }

        public void UpdateKorisnika(KorisnikDTO korisnikDTO)
        {
            Korisnik korisnik = new Korisnik { KorisnickoIme = korisnikDTO.KorisnickoIme, Lozinka = korisnikDTO.Lozinka, TipKorisnika = korisnikDTO.TipKorisnika };
            skladisteKorisnika.Update(korisnik);
        }
    }
}
