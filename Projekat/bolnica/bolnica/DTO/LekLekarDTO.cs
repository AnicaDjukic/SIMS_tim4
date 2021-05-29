using Bolnica.Model.Pregledi;
using Model.Pregledi;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace Bolnica.DTO
{
    public class LekLekarDTO
    {
        public List<Lek> sviLekovi { get; set; }
        public List<int> ItemSourceDozaLeka { get; set; }
        public ListBox sastojciKutija { get; set; }
        public ListBox zameneKutija { get; set; }
        public string proizvodjac { get; set; }
        public List<string> ItemSourceNazivLeka { get; set; }
        public Lek izabraniLek { get; set; }
        public string doza { get; set; }
        public string lek { get; set; }
        public List<Sastojak> sviSastojci { get; set; }
        public PrikazLek lekZaPrikaz { get; set; }
        public Lek izmenjeniLek { get; set; }

        public LekLekarDTO(string nazivLeka, List<Lek> sviLekovi, List<int> ItemSourceDozaLeka)
        {
            this.lek = nazivLeka;
            this.sviLekovi = sviLekovi;
            this.ItemSourceDozaLeka = ItemSourceDozaLeka;

        }
        public LekLekarDTO(ListBox kutija)
        {
            this.sastojciKutija = kutija;
            this.zameneKutija = kutija;
        }
        public LekLekarDTO(string proizvodjac, List<Lek> sviLekovi, List<string> ItemSourceNazivLeka)
        {
            this.proizvodjac = proizvodjac;
            this.sviLekovi = sviLekovi;
            this.ItemSourceNazivLeka = ItemSourceNazivLeka;
        }
        public LekLekarDTO(Lek izabraniLek, string doza, string lek, string proizvodjac, ListBox sastojciKutija, List<Sastojak> sviSastojci, ListBox zameneKutija, List<Lek> sviLekovi)
        {
            this.izabraniLek = izabraniLek;
            this.doza = doza;
            this.lek = lek;
            this.proizvodjac = proizvodjac;
            this.sastojciKutija = sastojciKutija;
            this.sviSastojci = sviSastojci;
            this.zameneKutija = zameneKutija;
            this.sviLekovi = sviLekovi;
        }
        public LekLekarDTO(PrikazLek lekZaPrikaz)
        {
            this.lekZaPrikaz = lekZaPrikaz;
        }
        public LekLekarDTO(Lek izmenjeniLek)
        {
            this.izmenjeniLek = izmenjeniLek;
        }
        public LekLekarDTO(ListBox sastojciKutija, List<Sastojak> sviSastojci, Lek izmenjeniLek, PrikazLek lekZaPrikaz)
        {
            this.sastojciKutija = sastojciKutija;
            this.sviSastojci = sviSastojci;
            this.izmenjeniLek = izmenjeniLek;
            this.lekZaPrikaz = lekZaPrikaz;
        }
        public LekLekarDTO(ListBox zameneKutija, List<Lek> sviLekovi, Lek izmenjeniLek, PrikazLek lekZaPrikaz)
        {
            this.zameneKutija = zameneKutija;
            this.sviLekovi = sviLekovi;
            this.izmenjeniLek = izmenjeniLek;
            this.lekZaPrikaz = lekZaPrikaz;
        }
    }
}
