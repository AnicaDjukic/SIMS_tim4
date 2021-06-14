using Bolnica.Model.Pregledi;
using Model.Korisnici;
using Model.Pregledi;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace Bolnica.DTO
{
    public class AnamnezaLekarDTO
    {
        public Pacijent trenutniPacijent { get; set; }
        public bool DaLiPostojiAnamneza { get; set; }
        public bool DaLiJePregled { get; set; }
        public int idAnamneze { get; set; }
        public string simptomi { get; set; }
        public string dijagnoza { get; set; }
        public PrikazPregleda stariPregled { get; set; }
        public PrikazPregleda trenutniPregled { get; set; }
        public PrikazOperacije staraOperacija { get; set; }
        public PrikazOperacije trenutnaOperacija { get; set; }
        public List<Anamneza> sveAnamneze { get; set; }
        public Anamneza novaAnamneza { get; set; }
        
        public int selektovaniIndeks { get; set; }

        public PrikazRecepta selektovaniItem { get; set; }

        public Lekar ulogovaniLekar { get; set; }

        public AnamnezaLekarDTO(Pacijent trenutniPacijent)
        {
            this.trenutniPacijent = trenutniPacijent;
        }
        public AnamnezaLekarDTO(bool DaLiPostojiAnamneza, bool DaLiJePregled, int idAnamneze, string simptomi, string dijagnoza, PrikazPregleda stariPregled, PrikazPregleda trenutniPregled, PrikazOperacije staraOperacija, PrikazOperacije trenutnaOperacija, List<Anamneza> sveAnamneze)
        {
            this.DaLiPostojiAnamneza = DaLiPostojiAnamneza;
            this.DaLiJePregled = DaLiJePregled;
            this.idAnamneze = idAnamneze;
            this.simptomi = simptomi;
            this.dijagnoza = dijagnoza;
            this.stariPregled = stariPregled;
            this.trenutniPregled = trenutniPregled;
            this.staraOperacija = staraOperacija;
            this.trenutnaOperacija = trenutnaOperacija;
            this.sveAnamneze = sveAnamneze;

        }

        public AnamnezaLekarDTO(List<Anamneza> sveAnamneze)
        {
            this.sveAnamneze = sveAnamneze;
        }

        public AnamnezaLekarDTO(string simptomi, string dijagnoza)
        {
            this.simptomi = simptomi;
            this.dijagnoza = dijagnoza;
        }
        public AnamnezaLekarDTO(Anamneza novaAnamneza, PrikazPregleda stariPregled, PrikazPregleda trenutniPregled)
        {
            this.novaAnamneza = novaAnamneza;
            this.stariPregled = stariPregled;
            this.trenutniPregled = trenutniPregled;
        }
        public AnamnezaLekarDTO(Anamneza novaAnamneza, PrikazOperacije staraOperacija, PrikazOperacije trenutnaOperacija)
        {
            this.novaAnamneza = novaAnamneza;
            this.staraOperacija = staraOperacija;
            this.trenutnaOperacija = trenutnaOperacija;
        }
        public AnamnezaLekarDTO(Anamneza novaAnamneza, PrikazOperacije trenutnaOperacija)
        {
            this.novaAnamneza = novaAnamneza;
            this.trenutnaOperacija = trenutnaOperacija;
        }
        public AnamnezaLekarDTO(Anamneza novaAnamneza, PrikazPregleda trenutniPregled)
        {
            this.novaAnamneza = novaAnamneza;
            this.trenutniPregled = trenutniPregled;
        }
        public AnamnezaLekarDTO(int selektovaniIndeks)
        {
            this.selektovaniIndeks = selektovaniIndeks;
        }
        public AnamnezaLekarDTO(Lekar ulogovaniLekar, Pacijent trenutniPacijent)
        {
            this.ulogovaniLekar = ulogovaniLekar;
            this.trenutniPacijent = trenutniPacijent;
        }
        public AnamnezaLekarDTO(int selektovaniIndeks, Pacijent trenutniPacijent, PrikazRecepta selektovaniItem )
        {
            this.selektovaniIndeks = selektovaniIndeks;
            this.trenutniPacijent = trenutniPacijent;
            this.selektovaniItem = selektovaniItem;
        }
      
    }
}
