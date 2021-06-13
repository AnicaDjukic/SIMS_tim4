using Bolnica.Model.Korisnici;
using Bolnica.Model.Pregledi;
using Model.Korisnici;
using Model.Pacijenti;
using Model.Pregledi;
using Model.Prostorije;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace Bolnica.DTO
{
    public class LekarServiceDTO
    {
        public List<Pregled> listaPregleda { get; set; }
        public List<Operacija> listaOperacija { get; set; }
        public PrikazPregleda prikazPregleda { get; set; }
        public PrikazOperacije prikazOperacije { get; set; }
        public Lekar lekarTrenutni { get; set; }
        public DataGrid tabela { get; set; }
        public List<Lek> lekovi { get; set; }
        public Button dugme { get; set; }
        public TabItem tab { get; set; }

        public List<Lekar> listaLekara { get; set; }

        public List<Prostorija> listaProstorija { get; set; }

        public List<Pacijent> listaPacijenata { get; set; }
        public LekarServiceDTO(DataGrid lekoviTabela)
        {
            this.tabela = lekoviTabela;
        }
        public LekarServiceDTO(DataGrid lekoviTabela,List<Lek> lekovi)
        {
            this.tabela = lekoviTabela;
            this.lekovi = lekovi;
        }
        public LekarServiceDTO(DataGrid Tabela, List<Operacija> listaOperacija, List<Pregled> listaPregleda, Lekar lekarTrenutni, PrikazPregleda prikazPregleda, PrikazOperacije prikazOperacije)
        {
            this.tabela = Tabela;
            this.listaOperacija = listaOperacija;
            this.listaPregleda = listaPregleda;
            this.lekarTrenutni = lekarTrenutni;
            this.prikazPregleda = prikazPregleda;
            this.prikazOperacije = prikazOperacije;
        }
        public LekarServiceDTO(Button dugme)
        {
            this.dugme = dugme;
        }
        public LekarServiceDTO(TabItem tab)
        {
            this.tab = tab;
        }
        public LekarServiceDTO(DataGrid lekoviTabela, List<Operacija> listaOperacija)
        {
            this.tabela = lekoviTabela;
            this.listaOperacija = listaOperacija;
        }
        public LekarServiceDTO(DataGrid lekoviTabela, List<Pregled> listaPregleda)
        {
            this.tabela = lekoviTabela;
            this.listaPregleda = listaPregleda;
        }
        public LekarServiceDTO(DataGrid lekoviTabela, List<Operacija> listaOperacija, Lekar lekarTrenutni)
        {
            this.tabela = lekoviTabela;
            this.listaOperacija = listaOperacija;
            this.lekarTrenutni = lekarTrenutni;
        }
        public LekarServiceDTO(DataGrid lekoviTabela, List<Pregled> listaPregleda, Lekar lekarTrenutni)
        {
            this.tabela = lekoviTabela;
            this.listaPregleda = listaPregleda;
            this.lekarTrenutni = lekarTrenutni;
        }
        public LekarServiceDTO(Lekar lekarTrenutni)
        {
            this.lekarTrenutni = lekarTrenutni;
        }
        public LekarServiceDTO(List<Lekar> listaLekara, List<Prostorija> listaProstorija, List<Pacijent> listaPacijenata, List<Pregled> listaPregleda)
        {
            this.listaLekara = listaLekara;
            this.listaProstorija = listaProstorija;
            this.listaPregleda = listaPregleda;
            this.listaPacijenata = listaPacijenata;
        }
        public LekarServiceDTO(List<Lekar> listaLekara, List<Prostorija> listaProstorija, List<Pacijent> listaPacijenata, List<Operacija> listaOperacija)
        {
            this.listaLekara = listaLekara;
            this.listaProstorija = listaProstorija;
            this.listaOperacija = listaOperacija;
            this.listaPacijenata = listaPacijenata;
        }
        public LekarServiceDTO(List<Lek> lekovi)
        {
            this.lekovi = lekovi;
        }
    }
}
