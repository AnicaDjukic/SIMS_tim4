using Bolnica.Localization;
using Bolnica.Model.Prostorije;
using Bolnica.Services.Prostorije;
using Model.Prostorije;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace Bolnica.Forms.Upravnik
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class FormSpajanjeProstorija : Window
    {
        public static ObservableCollection<Prostorija> ProstorijeZaSpajanje
        {
            get;
            set;
        }

        Renoviranje renoviranje;

        private Injector inject;
        public Injector Inject
        {
            get { return inject; }
            set
            {
                inject = value;
            }
        }
        public FormSpajanjeProstorija(Renoviranje novoRenoviranje)
        {
            InitializeComponent();
            DataContext = this;
            Inject = new Injector();
            Title = LocalizedStrings.Instance["Izbor prostorija za spajanje"];
            renoviranje = novoRenoviranje;
            NadjiProstorijuKojaSeRenovira();
            novoRenoviranje.BrojNovihProstorija = 0;
            PrikaziProstorijeZaSpajanje();
        }

        private void NadjiProstorijuKojaSeRenovira()
        {
            Prostorija prostorija = Inject.ControllerProstorija.DobaviProstoriju(renoviranje.Prostorija.BrojProstorije);
            if (prostorija != null)
                renoviranje.Prostorija = prostorija;
            else
                renoviranje.Prostorija = Inject.ControllerBolnickaSoba.DobaviBolnickuSobu(renoviranje.Prostorija.BrojProstorije);
        }

        private void PrikaziProstorijeZaSpajanje()
        {
            ProstorijeZaSpajanje = new ObservableCollection<Prostorija>();

            foreach (Prostorija p in Inject.ControllerProstorija.DobaviProstorijeNaIstomSpratu(renoviranje.Prostorija.Sprat))
            {
                if (p.BrojProstorije != renoviranje.Prostorija.BrojProstorije)
                    ProstorijeZaSpajanje.Add(p);
            }

            foreach(BolnickaSoba b in Inject.ControllerBolnickaSoba.DobaviBolnickeSobeNaIstomSpratu(renoviranje.Prostorija.Sprat))
            {
                if (b.BrojProstorije != renoviranje.Prostorija.BrojProstorije)
                    ProstorijeZaSpajanje.Add(b);
            }
        }

        private void Button_Click_Potvrdi(object sender, RoutedEventArgs e)
        {
            if(GridProstorijeZaSpajanje.SelectedCells.Count > 0)
            {
                renoviranje.ProstorijeZaSpajanje.Clear();
                var izabraneProstorije = GridProstorijeZaSpajanje.SelectedItems;
                foreach (Prostorija p in izabraneProstorije)
                {
                    if (p.TipProstorije != TipProstorije.bolnickaSoba) {

                        if (!ProstorijaZauzeta(p))
                            renoviranje.ProstorijeZaSpajanje.Add(p);
                        else
                        {
                            MessageBox.Show(LocalizedStrings.Instance["Prostorija"] + " " + p.BrojProstorije + " " + LocalizedStrings.Instance["je zauzeta u narednom periodu!"]);
                            return;
                        }
                    } 
                    else
                    {
                        if(!BolnickaSobaZauzeta(p))
                            renoviranje.ProstorijeZaSpajanje.Add(p);
                        else
                        {
                            MessageBox.Show(LocalizedStrings.Instance["Bolnička soba"] + " " + p.BrojProstorije + " " + LocalizedStrings.Instance["je zauzeta u narednom periodu!"]);
                            return;
                        }
                    }
                }
                Close();
            }
        }

        private bool ProstorijaZauzeta(Prostorija prostorija)
        {
            if (Inject.ControllerProstorija.ZauzetaOdDatuma(prostorija.BrojProstorije, renoviranje.PocetakRenoviranja))
            {
                return true;
            }
            return false;
        }

        private bool BolnickaSobaZauzeta(Prostorija prostorija)
        {
            if (Inject.ControllerBolnickaSoba.PostojeZauzetiKreveti(prostorija.BrojProstorije))
            {
                return true;
            }
            return false;
        }

        private void Button_Click_Odustani(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
