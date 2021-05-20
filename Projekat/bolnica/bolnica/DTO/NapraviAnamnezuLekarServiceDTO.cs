using Bolnica.Model.Pregledi;
using Model.Korisnici;
using Model.Pregledi;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace Bolnica.DTO
{
    public class NapraviAnamnezuLekarServiceDTO
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
        public DataGrid dataGridLekovi { get; set; }
        public ScrollViewer ScroolBar { get; set; }
        public Button IzbrisiButton { get; set; }

        public Lekar ulogovaniLekar { get; set; }

        public NapraviAnamnezuLekarServiceDTO(Pacijent trenutniPacijent)
        {
            this.trenutniPacijent = trenutniPacijent;
        }
        public NapraviAnamnezuLekarServiceDTO(bool DaLiPostojiAnamneza, bool DaLiJePregled, int idAnamneze, string simptomi, string dijagnoza, PrikazPregleda stariPregled, PrikazPregleda trenutniPregled, PrikazOperacije staraOperacija, PrikazOperacije trenutnaOperacija, List<Anamneza> sveAnamneze)
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

        public NapraviAnamnezuLekarServiceDTO(List<Anamneza> sveAnamneze)
        {
            this.sveAnamneze = sveAnamneze;
        }

        public NapraviAnamnezuLekarServiceDTO(string simptomi, string dijagnoza)
        {
            this.simptomi = simptomi;
            this.dijagnoza = dijagnoza;
        }
        public NapraviAnamnezuLekarServiceDTO(Anamneza novaAnamneza, PrikazPregleda stariPregled, PrikazPregleda trenutniPregled)
        {
            this.novaAnamneza = novaAnamneza;
            this.stariPregled = stariPregled;
            this.trenutniPregled = trenutniPregled;
        }
        public NapraviAnamnezuLekarServiceDTO(Anamneza novaAnamneza, PrikazOperacije staraOperacija, PrikazOperacije trenutnaOperacija)
        {
            this.novaAnamneza = novaAnamneza;
            this.staraOperacija = staraOperacija;
            this.trenutnaOperacija = trenutnaOperacija;
        }
        public NapraviAnamnezuLekarServiceDTO(Anamneza novaAnamneza, PrikazOperacije trenutnaOperacija)
        {
            this.novaAnamneza = novaAnamneza;
            this.trenutnaOperacija = trenutnaOperacija;
        }
        public NapraviAnamnezuLekarServiceDTO(Anamneza novaAnamneza, PrikazPregleda trenutniPregled)
        {
            this.novaAnamneza = novaAnamneza;
            this.trenutniPregled = trenutniPregled;
        }
        public NapraviAnamnezuLekarServiceDTO(DataGrid dataGridLekovi)
        {
            this.dataGridLekovi = dataGridLekovi;
        }
        public NapraviAnamnezuLekarServiceDTO(Lekar ulogovaniLekar, Pacijent trenutniPacijent)
        {
            this.ulogovaniLekar = ulogovaniLekar;
            this.trenutniPacijent = trenutniPacijent;
        }
        public NapraviAnamnezuLekarServiceDTO(DataGrid dataGridLekovi, Pacijent trenutniPacijent)
        {
            this.dataGridLekovi = dataGridLekovi;
            this.trenutniPacijent = trenutniPacijent;
        }
        public NapraviAnamnezuLekarServiceDTO(Button IzbrisiButton)
        {
            this.IzbrisiButton = IzbrisiButton;
        }
        public NapraviAnamnezuLekarServiceDTO(ScrollViewer ScroolBar)
        {
            this.ScroolBar = ScroolBar;
        }
    }
}
