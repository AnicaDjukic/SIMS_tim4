using Bolnica.Localization;
using Bolnica.Model.Prostorije;
using Bolnica.Services.Prostorije;
using Model.Prostorije;
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
            novoRenoviranje.BrojNovihProstorija = 0;
            renoviranje = novoRenoviranje;
            PrikaziProstorijeZaSpajanje();
        }

        private void PrikaziProstorijeZaSpajanje()
        {
            ProstorijeZaSpajanje = new ObservableCollection<Prostorija>();

            foreach(Prostorija p in Inject.ControllerProstorija.DobaviProstorijeNaIstomSpratu(renoviranje.Prostorija.BrojProstorije)){
                if(p.BrojProstorije != renoviranje.Prostorija.BrojProstorije)
                    ProstorijeZaSpajanje.Add(p);
            }
        }

        private void Button_Click_Potvrdi(object sender, RoutedEventArgs e)
        {
            if(GridProstorijeZaSpajanje.SelectedCells.Count > 0)
            {
                renoviranje.ProstorijeZaSpajanje.Clear();
                var izabraneProstorije = GridProstorijeZaSpajanje.SelectedItems;
                foreach(Prostorija p in izabraneProstorije)
                {
                    if (!Inject.ControllerProstorija.ZauzetaOdDatuma(p.BrojProstorije, renoviranje.PocetakRenoviranja))
                    {
                        renoviranje.ProstorijeZaSpajanje.Add(p);
                    }
                    else
                    {
                        MessageBox.Show("Prostorija je " + p.BrojProstorije + " je zauzeta u narednom periodu!");
                        return;
                    }
                }
                Close();
            }
        }

        private void Button_Click_Odustani(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
