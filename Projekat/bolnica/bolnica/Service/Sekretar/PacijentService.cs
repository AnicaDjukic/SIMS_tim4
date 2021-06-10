using Bolnica.DTO;
using Bolnica.Forms;
using Bolnica.Model.Korisnici;
using Bolnica.Model.Pacijenti;
using Bolnica.Repository.Korisnici;
using Bolnica.Repository.Pregledi;
using Bolnica.Sekretar;
using Model.Korisnici;
using Model.Pacijenti;
using Model.Pregledi;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;

namespace Bolnica.Services
{
    public class PacijentService
    {
        private IRepositoryPacijent skladistePacijenata;
        private IRepositoryZdravstveniKarton skladisteZdravstvenihKartona;
        private IRepositoryKorisnik skladisteKorisnika;
        private IRepositoryPregled skladistePregleda;
        private IRepositoryOperacija skladisteOperacija;

        public PacijentService() 
        {
            skladistePacijenata = new FileRepositoryPacijent();
            skladisteZdravstvenihKartona = new FileRepositoryZdravstveniKarton();
            skladisteKorisnika = new FileRepositoryKorisnik();
            skladistePregleda = new FileRepositoryPregled();
            skladisteOperacija = new FileRepositoryOperacija();
        }

        public PacijentDTO GetPacijentByID(string jmbg) 
        {
            Pacijent pacijent = skladistePacijenata.GetById(jmbg);
            PacijentDTO pacijentDTO = new PacijentDTO(pacijent.Jmbg, pacijent.Ime, pacijent.Prezime, pacijent.DatumRodjenja, pacijent.BrojTelefona, pacijent.AdresaStanovanja, pacijent.Email, pacijent.KorisnickoIme, pacijent.Lozinka, pacijent.TipKorisnika, pacijent.Guest, pacijent.Obrisan, pacijent.Pol, pacijent.ZdravstveniKarton, pacijent.Alergeni);
            return pacijentDTO;
        }

        public List<PacijentDTO> GetAllPacijente()
        {
            List<Pacijent> sviPacijenti = skladistePacijenata.GetAll();
            List<PacijentDTO> sviPacijentiDTO = new List<PacijentDTO>();

            foreach (Pacijent p in sviPacijenti)
                sviPacijentiDTO.Add(new PacijentDTO(p.Jmbg, p.Ime, p.Prezime, p.DatumRodjenja, p.BrojTelefona, p.AdresaStanovanja, p.Email, p.KorisnickoIme, p.Lozinka, p.TipKorisnika, p.Guest, p.Obrisan, p.Pol, p.ZdravstveniKarton, p.Alergeni));

            return sviPacijentiDTO;
        }

        public List<PacijentDTO> GetRedovnePacijente() 
        {
            List<Pacijent> sviPacijenti = skladistePacijenata.GetAll();
            List<PacijentDTO> redovniPacijenti = new List<PacijentDTO>();

            foreach (Pacijent p in sviPacijenti)
                if (p.Obrisan == false && !p.Guest)
                    redovniPacijenti.Add(new PacijentDTO(p.Jmbg, p.Ime, p.Prezime, p.DatumRodjenja, p.BrojTelefona, p.AdresaStanovanja, p.Email, p.KorisnickoIme, p.Lozinka, p.TipKorisnika, p.Guest, p.Obrisan, p.Pol, p.ZdravstveniKarton, p.Alergeni));

            return redovniPacijenti;
        }

        public List<PacijentDTO> GetGostPacijente()
        {
            List<Pacijent> sviPacijenti = skladistePacijenata.GetAll();
            List<PacijentDTO> gostPacijenti = new List<PacijentDTO>();

            foreach (Pacijent p in sviPacijenti)
                if (p.Obrisan == false && p.Guest)
                    gostPacijenti.Add(new PacijentDTO(p.Jmbg, p.Ime, p.Prezime, p.DatumRodjenja, p.BrojTelefona, p.AdresaStanovanja, p.Email, p.KorisnickoIme, p.Lozinka, p.TipKorisnika, p.Guest, p.Obrisan, p.Pol, p.Alergeni));

            return gostPacijenti;
        }

        public List<PacijentDTO> GetObrisanePacijente()
        {
            List<Pacijent> sviPacijenti = skladistePacijenata.GetAll();
            List<PacijentDTO> obrisaniPacijenti = new List<PacijentDTO>();

            foreach (Pacijent p in sviPacijenti)
                if (p.Obrisan)
                    obrisaniPacijenti.Add(new PacijentDTO(p.Jmbg, p.Ime, p.Prezime, p.DatumRodjenja, p.BrojTelefona, p.AdresaStanovanja, p.Email, p.KorisnickoIme, p.Lozinka, p.TipKorisnika, p.Guest, p.Obrisan, p.Pol, p.ZdravstveniKarton, p.Alergeni));

            return obrisaniPacijenti;
        }

        public void BlokirajPacijenta(PacijentDTO pacijentDTO)
        {
            UpdatePacijentaBlokiranje(pacijentDTO);
            UpdateTerminaPacijentaBlokiranje(pacijentDTO);
        }

        private void UpdatePacijentaBlokiranje(PacijentDTO pacijentDTO) 
        {
            Pacijent pacijent = skladistePacijenata.GetById(pacijentDTO.Jmbg);
            pacijent.Obrisan = true;
            skladistePacijenata.Update(pacijent);

            if (!pacijent.Guest)
            {
                Korisnik korisnik = new Korisnik() { KorisnickoIme = pacijent.KorisnickoIme, Lozinka = pacijent.Lozinka, TipKorisnika = TipKorisnika.pacijent };
                skladisteKorisnika.Delete(korisnik);
            }
        }

        private void UpdateTerminaPacijentaBlokiranje(PacijentDTO pacijentDTO) 
        {
            List<Pregled> pregledi = skladistePregleda.GetAll();
            List<Operacija> operacije = skladisteOperacija.GetAll();
            foreach (Pregled p in pregledi)
                if (p.Pacijent.Jmbg == pacijentDTO.Jmbg)
                    skladistePregleda.Delete(p);
            foreach (Operacija o in operacije)
                if (o.Pacijent.Jmbg == pacijentDTO.Jmbg)
                    skladisteOperacija.Delete(o);
        }

        public void OdblokirajPacijenta(PacijentDTO pacijentDTO) 
        {
            Pacijent pacijent = skladistePacijenata.GetById(pacijentDTO.Jmbg);
            pacijent.Obrisan = false;
            skladistePacijenata.Update(pacijent);

            if (!pacijent.Guest)
            {
                Korisnik korisnik = new Korisnik() { KorisnickoIme = pacijent.KorisnickoIme, Lozinka = pacijent.Lozinka, TipKorisnika = TipKorisnika.pacijent };
                skladisteKorisnika.Save(korisnik);
            }
        }

        public void DodajIliIzmeniRedovnogPacijenta(PacijentDTO pacijentDTO) 
        {
            SnimiPacijentaDodavanjeIliIzmena(pacijentDTO);
            SnimiZdravstveniKartonDodavanjeIliIzmena(pacijentDTO);
            SnimiKorisnikaDodavanjeIliIzmena(pacijentDTO);
        }

        private void SnimiPacijentaDodavanjeIliIzmena(PacijentDTO pacijentDTO) 
        {
            Pacijent pacijent = new Pacijent
            {
                Guest = pacijentDTO.Guest,
                Obrisan = pacijentDTO.Obrisan,
                Pol = pacijentDTO.Pol,
                ZdravstveniKarton = pacijentDTO.ZdravstveniKarton,
                Alergeni = pacijentDTO.Alergeni,
                Jmbg = pacijentDTO.Jmbg,
                Ime = pacijentDTO.Ime,
                Prezime = pacijentDTO.Prezime,
                DatumRodjenja = pacijentDTO.DatumRodjenja,
                BrojTelefona = pacijentDTO.BrojTelefona,
                AdresaStanovanja = pacijentDTO.AdresaStanovanja,
                Email = pacijentDTO.Email,
                KorisnickoIme = pacijentDTO.KorisnickoIme,
                Lozinka = pacijentDTO.Lozinka,
                TipKorisnika = pacijentDTO.TipKorisnika
            };
            if (FormSekretar.clickedDodaj)
                skladistePacijenata.Save(pacijent);
            else
                skladistePacijenata.Update(pacijent);
        }

        private void SnimiZdravstveniKartonDodavanjeIliIzmena(PacijentDTO pacijentDTO) 
        {
            ZdravstveniKarton zdravstveniKarton = new ZdravstveniKarton
            {
                BrojKartona = pacijentDTO.ZdravstveniKarton.BrojKartona,
                Zanimanje = pacijentDTO.ZdravstveniKarton.Zanimanje,
                Osiguranje = pacijentDTO.ZdravstveniKarton.Osiguranje,
                BracniStatus = pacijentDTO.ZdravstveniKarton.BracniStatus
            };
            if (FormSekretar.clickedDodaj)
                skladisteZdravstvenihKartona.Save(zdravstveniKarton);
            else
                skladisteZdravstvenihKartona.Update(zdravstveniKarton);
        }

        private void SnimiKorisnikaDodavanjeIliIzmena(PacijentDTO pacijentDTO) 
        {
            Korisnik korisnik = new Korisnik
            {
                KorisnickoIme = pacijentDTO.KorisnickoIme,
                Lozinka = pacijentDTO.Lozinka,
                TipKorisnika = TipKorisnika.pacijent
            };
            if (FormSekretar.clickedDodaj)
                skladisteKorisnika.Save(korisnik);
            else
                skladisteKorisnika.Update(korisnik);
        }
    }
}
