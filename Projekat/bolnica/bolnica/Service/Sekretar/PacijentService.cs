using Bolnica.DTO;
using Bolnica.DTO.Sekretar;
using Bolnica.Forms;
using Bolnica.Model.Korisnici;
using Bolnica.Model.Pacijenti;
using Bolnica.Model.Pregledi;
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
        private IRepositorySastojak skladisteAlergena;

        public PacijentService() 
        {
            skladistePacijenata = new FileRepositoryPacijent();
            skladisteZdravstvenihKartona = new FileRepositoryZdravstveniKarton();
            skladisteKorisnika = new FileRepositoryKorisnik();
            skladistePregleda = new FileRepositoryPregled();
            skladisteOperacija = new FileRepositoryOperacija();
            skladisteAlergena = new FileRepositorySastojak();
        }

        public PacijentDTO GetPacijentByID(string jmbg) 
        {
            Pacijent pacijent = skladistePacijenata.GetById(jmbg);
            ZdravstveniKarton zdravstveniKarton = skladisteZdravstvenihKartona.GetById(pacijent.ZdravstveniKarton.BrojKartona);
            ZdravstveniKartonDTO zdravstveniKartonDTO = new ZdravstveniKartonDTO(zdravstveniKarton.BrojKartona, zdravstveniKarton.Zanimanje, zdravstveniKarton.BracniStatus, zdravstveniKarton.Osiguranje);
            List<SastojakDTO> alergeniDTO = new List<SastojakDTO>();
            if (pacijent.Alergeni != null)
                foreach (Sastojak alergen in pacijent.Alergeni)
                    alergeniDTO.Add(new SastojakDTO { Id = alergen.Id, Naziv = alergen.Naziv });

            PacijentDTO pacijentDTO = new PacijentDTO(pacijent.Jmbg, pacijent.Ime, pacijent.Prezime, pacijent.DatumRodjenja, pacijent.BrojTelefona, pacijent.AdresaStanovanja, pacijent.Email, pacijent.KorisnickoIme, pacijent.Lozinka, pacijent.TipKorisnika, pacijent.Guest, pacijent.Obrisan, pacijent.Pol, zdravstveniKartonDTO, alergeniDTO);
            return pacijentDTO;
        }

        public List<PacijentDTO> GetAllPacijente()
        {
            List<Pacijent> sviPacijenti = skladistePacijenata.GetAll();
            List<PacijentDTO> sviPacijentiDTO = new List<PacijentDTO>();

            foreach (Pacijent p in sviPacijenti)
            {
                ZdravstveniKarton zdravstveniKarton = new ZdravstveniKarton();
                ZdravstveniKartonDTO zdravstveniKartonDTO = new ZdravstveniKartonDTO();
                if (p.ZdravstveniKarton != null)
                {
                    zdravstveniKarton = skladisteZdravstvenihKartona.GetById(p.ZdravstveniKarton.BrojKartona);
                    zdravstveniKartonDTO = new ZdravstveniKartonDTO(zdravstveniKarton.BrojKartona, zdravstveniKarton.Zanimanje, zdravstveniKarton.BracniStatus, zdravstveniKarton.Osiguranje);
                }
                List<SastojakDTO> alergeniDTO = new List<SastojakDTO>();
                if (p.Alergeni != null)
                    foreach (Sastojak alergen in p.Alergeni)
                        alergeniDTO.Add(new SastojakDTO { Id = alergen.Id, Naziv = alergen.Naziv });
                sviPacijentiDTO.Add(new PacijentDTO(p.Jmbg, p.Ime, p.Prezime, p.DatumRodjenja, p.BrojTelefona, p.AdresaStanovanja, p.Email, p.KorisnickoIme, p.Lozinka, p.TipKorisnika, p.Guest, p.Obrisan, p.Pol, zdravstveniKartonDTO, alergeniDTO));
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
                    ZdravstveniKarton zdravstveniKarton = skladisteZdravstvenihKartona.GetById(p.ZdravstveniKarton.BrojKartona);
                    ZdravstveniKartonDTO zdravstveniKartonDTO = new ZdravstveniKartonDTO(zdravstveniKarton.BrojKartona, zdravstveniKarton.Zanimanje, zdravstveniKarton.BracniStatus, zdravstveniKarton.Osiguranje);
                    List<SastojakDTO> alergeniDTO = new List<SastojakDTO>();
                    if (p.Alergeni != null)
                        foreach (Sastojak alergen in p.Alergeni)
                            alergeniDTO.Add(new SastojakDTO { Id = alergen.Id, Naziv = alergen.Naziv });
                    redovniPacijenti.Add(new PacijentDTO(p.Jmbg, p.Ime, p.Prezime, p.DatumRodjenja, p.BrojTelefona, p.AdresaStanovanja, p.Email, p.KorisnickoIme, p.Lozinka, p.TipKorisnika, p.Guest, p.Obrisan, p.Pol, zdravstveniKartonDTO, alergeniDTO));
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
                    List<SastojakDTO> alergeniDTO = new List<SastojakDTO>();
                    if (p.Alergeni != null)
                        foreach (Sastojak alergen in p.Alergeni)
                            alergeniDTO.Add(new SastojakDTO { Id = alergen.Id, Naziv = alergen.Naziv });
                    gostPacijenti.Add(new PacijentDTO(p.Jmbg, p.Ime, p.Prezime, p.DatumRodjenja, p.BrojTelefona, p.AdresaStanovanja, p.Email, p.KorisnickoIme, p.Lozinka, p.TipKorisnika, p.Guest, p.Obrisan, p.Pol, alergeniDTO));
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
                    ZdravstveniKarton zdravstveniKarton = new ZdravstveniKarton();
                    ZdravstveniKartonDTO zdravstveniKartonDTO = new ZdravstveniKartonDTO();
                    if (p.ZdravstveniKarton != null) { 
                        zdravstveniKarton = skladisteZdravstvenihKartona.GetById(p.ZdravstveniKarton.BrojKartona);
                        zdravstveniKartonDTO = new ZdravstveniKartonDTO(zdravstveniKarton.BrojKartona, zdravstveniKarton.Zanimanje, zdravstveniKarton.BracniStatus, zdravstveniKarton.Osiguranje);
                    }
                    List<SastojakDTO> alergeniDTO = new List<SastojakDTO>();
                    if (p.Alergeni != null)
                        foreach (Sastojak alergen in p.Alergeni)
                            alergeniDTO.Add(new SastojakDTO { Id = alergen.Id, Naziv = alergen.Naziv });
                    obrisaniPacijenti.Add(new PacijentDTO(p.Jmbg, p.Ime, p.Prezime, p.DatumRodjenja, p.BrojTelefona, p.AdresaStanovanja, p.Email, p.KorisnickoIme, p.Lozinka, p.TipKorisnika, p.Guest, p.Obrisan, p.Pol, zdravstveniKartonDTO, alergeniDTO));
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
            ZdravstveniKarton zdravstveniKarton = skladisteZdravstvenihKartona.GetById(pacijentDTO.ZdravstveniKarton.BrojKartona);
            pacijent.ZdravstveniKarton = new ZdravstveniKarton { BrojKartona = zdravstveniKarton.BrojKartona, Zanimanje = zdravstveniKarton.Zanimanje, BracniStatus = zdravstveniKarton.BracniStatus, Osiguranje = zdravstveniKarton.Osiguranje };
            pacijent.Alergeni = new List<Sastojak>();
            if (pacijentDTO.Alergeni != null)
                foreach (SastojakDTO alergen in pacijentDTO.Alergeni)
                    pacijent.Alergeni.Add(new Sastojak { Id = alergen.Id, Naziv = alergen.Naziv });

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
