using Bolnica.DTO.Sekretar;
using Bolnica.Model.Korisnici;
using Bolnica.Repository.Korisnici;
using Model.Korisnici;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Service.Sekretar
{
    public class LekarService
    {
        private IRepositoryLekar skladisteLekara;

        public LekarService() 
        {
            skladisteLekara = new FileRepositoryLekar();
        }

        public LekarDTO GetLekarById(string korisnickoIme) 
        {
            Lekar lekar = skladisteLekara.GetById(korisnickoIme);
            LekarDTO lekarDTO = new LekarDTO(lekar.KorisnickoIme, lekar.Lozinka, lekar.TipKorisnika, lekar.Jmbg, lekar.Ime, lekar.Prezime, lekar.DatumRodjenja, lekar.BrojTelefona, lekar.AdresaStanovanja, lekar.Email, lekar.Mbr, lekar.Plata, lekar.BrojSlobodnihDana, lekar.GodineStaza, lekar.Zaposlen, lekar.Smena, lekar.Specijalizacija);
            return lekarDTO;
        }

        public void UpdateLekara(LekarDTO lekarDTO) 
        {
            Lekar lekar = new Lekar { AdresaStanovanja = lekarDTO.AdresaStanovanja, BrojSlobodnihDana = lekarDTO.BrojSlobodnihDana, BrojTelefona = lekarDTO.BrojTelefona, DatumRodjenja = lekarDTO.DatumRodjenja, Email = lekarDTO.Email, GodineStaza = lekarDTO.GodineStaza, Ime = lekarDTO.Ime, Jmbg = lekarDTO.Jmbg, KorisnickoIme = lekarDTO.KorisnickoIme, Lozinka = lekarDTO.Lozinka, Mbr = lekarDTO.Mbr, Plata = lekarDTO.Plata, Prezime = lekarDTO.Prezime, Smena = lekarDTO.Smena, Specijalizacija = lekarDTO.Specijalizacija, TipKorisnika = lekarDTO.TipKorisnika, Zaposlen = lekarDTO.Zaposlen };
            skladisteLekara.Update(lekar);
        }
    }
}
