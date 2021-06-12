using Bolnica.DTO;
using Bolnica.DTO.Sekretar;
using Bolnica.Forms;
using Bolnica.Model.Korisnici;
using Bolnica.Model.Pacijenti;
using Bolnica.Model.Pregledi;
using Bolnica.Repository.Korisnici;
using Bolnica.Repository.Pregledi;
using Bolnica.Sekretar;
using Bolnica.Service.Sekretar;
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
        private IRepositoryKorisnik skladisteKorisnika;
        private IRepositoryPregled skladistePregleda;
        private IRepositoryOperacija skladisteOperacija;
        private SastojakService sastojakService;
        private ZdravstveniKartonService zdravstveniKartonService;

        public PacijentService() 
        {
            skladistePacijenata = new FileRepositoryPacijent();
            skladisteKorisnika = new FileRepositoryKorisnik();
            skladistePregleda = new FileRepositoryPregled();
            skladisteOperacija = new FileRepositoryOperacija();
            sastojakService = new SastojakService();
            zdravstveniKartonService = new ZdravstveniKartonService();
        }

        public PacijentDTO GetPacijentByID(string jmbg) 
        {
            Pacijent pacijent = skladistePacijenata.GetById(jmbg);
            ZdravstveniKartonDTO zdravstveniKarton = zdravstveniKartonService.GetZdravstveniKartonByID(pacijent.ZdravstveniKarton.BrojKartona);
            List<int> idsAlergena = new List<int>();
            if (pacijent.Alergeni != null)
                foreach (Sastojak sastojak in pacijent.Alergeni)
                    idsAlergena.Add(sastojak.Id);
            PacijentDTO pacijentDTO = new PacijentDTO(pacijent.Jmbg, pacijent.Ime, pacijent.Prezime, pacijent.DatumRodjenja, pacijent.BrojTelefona, pacijent.AdresaStanovanja, pacijent.Email, pacijent.KorisnickoIme, pacijent.Lozinka, pacijent.TipKorisnika, pacijent.Guest, pacijent.Obrisan, pacijent.Pol, zdravstveniKarton.BrojKartona, zdravstveniKarton.Zanimanje, zdravstveniKarton.BracniStatus, zdravstveniKarton.Osiguranje, idsAlergena);
            return pacijentDTO;
        }

        public List<PacijentDTO> GetAllPacijente()
        {
            List<Pacijent> sviPacijenti = skladistePacijenata.GetAll();
            List<PacijentDTO> sviPacijentiDTO = new List<PacijentDTO>();

            foreach (Pacijent p in sviPacijenti)
            {
                List<int> idsAlergena = new List<int>();
                if (p.Alergeni != null)
                    foreach (Sastojak sastojak in p.Alergeni)
                        idsAlergena.Add(sastojak.Id);
                if (!p.Guest)
                {
                    ZdravstveniKartonDTO zdravstveniKarton = zdravstveniKartonService.GetZdravstveniKartonByID(p.ZdravstveniKarton.BrojKartona);
                    sviPacijentiDTO.Add(new PacijentDTO(p.Jmbg, p.Ime, p.Prezime, p.DatumRodjenja, p.BrojTelefona, p.AdresaStanovanja, p.Email, p.KorisnickoIme, p.Lozinka, p.TipKorisnika, p.Guest, p.Obrisan, p.Pol, zdravstveniKarton.BrojKartona, zdravstveniKarton.Zanimanje, zdravstveniKarton.BracniStatus, zdravstveniKarton.Osiguranje, idsAlergena));
                }
                else
                    sviPacijentiDTO.Add(new PacijentDTO(p.Jmbg, p.Ime, p.Prezime, p.DatumRodjenja, p.BrojTelefona, p.AdresaStanovanja, p.Email, p.TipKorisnika, p.Guest, p.Obrisan, p.Pol, idsAlergena));
            }
            return sviPacijentiDTO;
        }

        public List<PacijentDTO> GetRedovnePacijente() 
        {
            List<Pacijent> sviPacijenti = skladistePacijenata.GetAll();
            List<PacijentDTO> redovniPacijenti = new List<PacijentDTO>();

            foreach (Pacijent p in sviPacijenti)
                if (p.Obrisan == false && !p.Guest)
                {
                    ZdravstveniKartonDTO zdravstveniKarton = zdravstveniKartonService.GetZdravstveniKartonByID(p.ZdravstveniKarton.BrojKartona);
                    List<int> idsAlergena = new List<int>();
                    if (p.Alergeni != null)
                        foreach (Sastojak sastojak in p.Alergeni)
                            idsAlergena.Add(sastojak.Id);
                    redovniPacijenti.Add(new PacijentDTO(p.Jmbg, p.Ime, p.Prezime, p.DatumRodjenja, p.BrojTelefona, p.AdresaStanovanja, p.Email, p.KorisnickoIme, p.Lozinka, p.TipKorisnika, p.Guest, p.Obrisan, p.Pol, zdravstveniKarton.BrojKartona, zdravstveniKarton.Zanimanje, zdravstveniKarton.BracniStatus, zdravstveniKarton.Osiguranje, idsAlergena));
                }
            return redovniPacijenti;
        }

        public List<PacijentDTO> GetGostPacijente()
        {
            List<Pacijent> sviPacijenti = skladistePacijenata.GetAll();
            List<PacijentDTO> gostPacijenti = new List<PacijentDTO>();

            foreach (Pacijent p in sviPacijenti)
                if (p.Obrisan == false && p.Guest)
                {
                    List<int> idsAlergena = new List<int>();
                    if (p.Alergeni != null)
                        foreach (Sastojak sastojak in p.Alergeni)
                            idsAlergena.Add(sastojak.Id);
                    gostPacijenti.Add(new PacijentDTO(p.Jmbg, p.Ime, p.Prezime, p.DatumRodjenja, p.BrojTelefona, p.AdresaStanovanja, p.Email, p.TipKorisnika, p.Guest, p.Obrisan, p.Pol, idsAlergena));
                }
            return gostPacijenti;
        }

        public List<PacijentDTO> GetObrisanePacijente()
        {
            List<Pacijent> sviPacijenti = skladistePacijenata.GetAll();
            List<PacijentDTO> obrisaniPacijenti = new List<PacijentDTO>();

            foreach (Pacijent p in sviPacijenti)
                if (p.Obrisan)
                {
                    List<int> idsAlergena = new List<int>();
                    if (p.Alergeni != null)
                        foreach (Sastojak sastojak in p.Alergeni)
                            idsAlergena.Add(sastojak.Id);
                    if (!p.Guest)
                    {
                        ZdravstveniKartonDTO zdravstveniKarton = zdravstveniKartonService.GetZdravstveniKartonByID(p.ZdravstveniKarton.BrojKartona);
                        obrisaniPacijenti.Add(new PacijentDTO(p.Jmbg, p.Ime, p.Prezime, p.DatumRodjenja, p.BrojTelefona, p.AdresaStanovanja, p.Email, p.KorisnickoIme, p.Lozinka, p.TipKorisnika, p.Guest, p.Obrisan, p.Pol, zdravstveniKarton.BrojKartona, zdravstveniKarton.Zanimanje, zdravstveniKarton.BracniStatus, zdravstveniKarton.Osiguranje, idsAlergena));
                    }
                    else
                        obrisaniPacijenti.Add(new PacijentDTO(p.Jmbg, p.Ime, p.Prezime, p.DatumRodjenja, p.BrojTelefona, p.AdresaStanovanja, p.Email, p.TipKorisnika, p.Guest, p.Obrisan, p.Pol, idsAlergena));
                }
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
            SnimiZdravstveniKartonDodavanjeIliIzmena(pacijentDTO);
            SnimiKorisnikaDodavanjeIliIzmena(pacijentDTO);
            SnimiPacijentaDodavanjeIliIzmena(pacijentDTO);
        }

        private void SnimiPacijentaDodavanjeIliIzmena(PacijentDTO pacijentDTO) 
        {
            Pacijent pacijent = new Pacijent
            {
                Guest = pacijentDTO.Guest,
                Obrisan = pacijentDTO.Obrisan,
                Pol = pacijentDTO.Pol,
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
            pacijent.ZdravstveniKarton = new ZdravstveniKarton { BrojKartona = pacijentDTO.BrojKartona, Zanimanje = pacijentDTO.Zanimanje, BracniStatus = pacijentDTO.BracniStatus, Osiguranje = pacijentDTO.Osiguranje };
            pacijent.Alergeni = new List<Sastojak>();
            if (pacijentDTO.IdsAlergena != null)
                foreach (int id in pacijentDTO.IdsAlergena) 
                {
                    SastojakDTO sastojakDTO = sastojakService.GetAlergenById(id);
                    pacijent.Alergeni.Add(new Sastojak { Id = sastojakDTO.Id, Naziv = sastojakDTO.Naziv});
                }
                    
            if (FormSekretar.clickedDodaj)
                skladistePacijenata.Save(pacijent);
            else
                skladistePacijenata.Update(pacijent);
        }

        private void SnimiZdravstveniKartonDodavanjeIliIzmena(PacijentDTO pacijentDTO) 
        {
            ZdravstveniKartonDTO zdravstveniKarton = new ZdravstveniKartonDTO
            {
                BrojKartona = pacijentDTO.BrojKartona,
                Zanimanje = pacijentDTO.Zanimanje,
                Osiguranje = pacijentDTO.Osiguranje,
                BracniStatus = pacijentDTO.BracniStatus
            };

            if (FormSekretar.clickedDodaj)
                zdravstveniKartonService.SaveZdravstveniKarton(zdravstveniKarton);
            else
                zdravstveniKartonService.UpdateZdravstveniKarton(zdravstveniKarton);
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
