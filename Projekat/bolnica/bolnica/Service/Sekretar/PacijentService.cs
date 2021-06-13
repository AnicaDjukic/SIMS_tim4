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
        private SastojakService sastojakService;
        private KorisnikService korisnikService;
        private ZdravstveniKartonService zdravstveniKartonService;
        private PregledService pregledService;
        private OperacijaService operacijaService;

        public PacijentService() 
        {
            skladistePacijenata = new FileRepositoryPacijent();
            sastojakService = new SastojakService();
            korisnikService = new KorisnikService();
            zdravstveniKartonService = new ZdravstveniKartonService();
            pregledService = new PregledService();
            operacijaService = new OperacijaService();
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
            List<PacijentDTO> sviPacijentiDTO = new List<PacijentDTO>();
            foreach (Pacijent p in skladistePacijenata.GetAll())
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
            List<PacijentDTO> redovniPacijenti = new List<PacijentDTO>();
            foreach (Pacijent p in skladistePacijenata.GetAll())
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
            List<PacijentDTO> gostPacijenti = new List<PacijentDTO>();
            foreach (Pacijent p in skladistePacijenata.GetAll())
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
            List<PacijentDTO> obrisaniPacijenti = new List<PacijentDTO>();
            foreach (Pacijent p in skladistePacijenata.GetAll())
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
                KorisnikDTO korisnikDTO = new KorisnikDTO { KorisnickoIme = pacijent.KorisnickoIme, Lozinka = pacijent.Lozinka, TipKorisnika = TipKorisnika.pacijent };
                korisnikService.DeleteKorisnika(korisnikDTO);
            }
        }

        private void UpdateTerminaPacijentaBlokiranje(PacijentDTO pacijentDTO) 
        {
            foreach (PrikazPregleda pp in pregledService.GetAllPregledi())
                if (pp.Pacijent.Jmbg == pacijentDTO.Jmbg)
                    pregledService.DeletePregled(pp);
            foreach (PrikazOperacije po in operacijaService.GetAllOperacije())
                if (po.Pacijent.Jmbg == pacijentDTO.Jmbg)
                    operacijaService.DeleteOperacija(po);
        }

        public void OdblokirajPacijenta(PacijentDTO pacijentDTO) 
        {
            Pacijent pacijent = skladistePacijenata.GetById(pacijentDTO.Jmbg);
            pacijent.Obrisan = false;
            skladistePacijenata.Update(pacijent);

            if (!pacijent.Guest)
            {
                KorisnikDTO korisnikDTO = new KorisnikDTO { KorisnickoIme = pacijent.KorisnickoIme, Lozinka = pacijent.Lozinka, TipKorisnika = TipKorisnika.pacijent };
                korisnikService.SaveKorisnika(korisnikDTO);
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
            Pacijent pacijent = new Pacijent { Guest = pacijentDTO.Guest, Obrisan = pacijentDTO.Obrisan, Pol = pacijentDTO.Pol, Jmbg = pacijentDTO.Jmbg, Ime = pacijentDTO.Ime, Prezime = pacijentDTO.Prezime, DatumRodjenja = pacijentDTO.DatumRodjenja, BrojTelefona = pacijentDTO.BrojTelefona, AdresaStanovanja = pacijentDTO.AdresaStanovanja, Email = pacijentDTO.Email, KorisnickoIme = pacijentDTO.KorisnickoIme, Lozinka = pacijentDTO.Lozinka, TipKorisnika = pacijentDTO.TipKorisnika };
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
            ZdravstveniKartonDTO zdravstveniKarton = new ZdravstveniKartonDTO { BrojKartona = pacijentDTO.BrojKartona, Zanimanje = pacijentDTO.Zanimanje, Osiguranje = pacijentDTO.Osiguranje, BracniStatus = pacijentDTO.BracniStatus };
            if (FormSekretar.clickedDodaj)
                zdravstveniKartonService.SaveZdravstveniKarton(zdravstveniKarton);
            else
                zdravstveniKartonService.UpdateZdravstveniKarton(zdravstveniKarton);
        }

        private void SnimiKorisnikaDodavanjeIliIzmena(PacijentDTO pacijentDTO) 
        {
            KorisnikDTO korisnikDTO = new KorisnikDTO { KorisnickoIme = pacijentDTO.KorisnickoIme, Lozinka = pacijentDTO.Lozinka, TipKorisnika = TipKorisnika.pacijent };
            if (FormSekretar.clickedDodaj)
                korisnikService.SaveKorisnika(korisnikDTO);
            else
                korisnikService.UpdateKorisnika(korisnikDTO);
        }
    }
}
