using Bolnica.DTO.Sekretar;
using Bolnica.Model.Korisnici;
using Bolnica.Model.Pregledi;
using Bolnica.Repository.Korisnici;
using Model.Korisnici;
using Model.Pregledi;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Service.Sekretar
{
    public class GodisnjiService
    {
        private IRepositoryGodisnji skladisteGodisnji;
        private LekarService lekarService;
        private PregledService pregledService;
        private OperacijaService operacijaService;

        public GodisnjiService() 
        {
            skladisteGodisnji = new FileRepositoryGodisnji();
            lekarService = new LekarService();
            pregledService = new PregledService();
            operacijaService = new OperacijaService();
        }

        public List<GodisnjiDTO> GetAllGodisnji() 
        {
            List<GodisnjiDTO> sviGodisnjiDTO = new List<GodisnjiDTO>();
            foreach (Godisnji g in skladisteGodisnji.GetAll())
                sviGodisnjiDTO.Add(new GodisnjiDTO(g.PocetakGodisnjeg, g.KrajGodisnjeg, g.Lekar.KorisnickoIme));

            return sviGodisnjiDTO;
        }

        private void SaveGodisnji(GodisnjiDTO godisnjiDTO)
        {
            LekarDTO lekarDTO = lekarService.GetLekarById(godisnjiDTO.KorisnickoImeLekara);
            Lekar lekar = new Lekar { AdresaStanovanja = lekarDTO.AdresaStanovanja, BrojSlobodnihDana = lekarDTO.BrojSlobodnihDana, BrojTelefona = lekarDTO.BrojTelefona, DatumRodjenja = lekarDTO.DatumRodjenja, Email = lekarDTO.Email, GodineStaza = lekarDTO.GodineStaza, Ime = lekarDTO.Ime, Jmbg = lekarDTO.Jmbg, KorisnickoIme = lekarDTO.KorisnickoIme, Lozinka = lekarDTO.Lozinka, Mbr = lekarDTO.Mbr, Plata = lekarDTO.Plata, Prezime = lekarDTO.Prezime, Smena = lekarDTO.Smena, Specijalizacija = lekarDTO.Specijalizacija, TipKorisnika = lekarDTO.TipKorisnika, Zaposlen = lekarDTO.Zaposlen };
            Godisnji godisnji = new Godisnji { PocetakGodisnjeg = godisnjiDTO.PocetakGodisnjeg, KrajGodisnjeg = godisnjiDTO.KrajGodisnjeg, Lekar = lekar};
            skladisteGodisnji.Save(godisnji);
        }

        public void ZakaziGodisnji(GodisnjiDTO godisnji, LekarDTO lekar, int daniNaGodisnjem, bool pomeriTermine)
        {
            lekarService.UpdateLekaraPoBrojuSlobodnihDana(lekar, daniNaGodisnjem);
            SaveGodisnji(godisnji);
            IzvrsiManipulacijuTerminima(godisnji, daniNaGodisnjem, pomeriTermine);
        }

        private void IzvrsiManipulacijuTerminima(GodisnjiDTO godisnji, int daniNaGodisnjem, bool pomeriTermine) 
        {
            Context context = new Context();
            if (!pomeriTermine)
            {
                context.PostaviStrategiju(new StrategyDeleteTermine());
                context.IzvrsiPoslovnuLogiku(godisnji, daniNaGodisnjem);
            }
            else
            {
                context.PostaviStrategiju(new StrategyPomeriTermine());
                context.IzvrsiPoslovnuLogiku(godisnji, daniNaGodisnjem);
            }
        }
    }
}
