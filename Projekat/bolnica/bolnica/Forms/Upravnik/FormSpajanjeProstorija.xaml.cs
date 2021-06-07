using Bolnica.Localization;
using Bolnica.Model.Prostorije;
using Bolnica.Services.Prostorije;
using Model.Prostorije;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
        private ServiceProstorija serviceProstorija = new ServiceProstorija();
        Renoviranje renoviranje;
        public FormSpajanjeProstorija(Renoviranje novoRenoviranje)
        {
            InitializeComponent();
            Title = LocalizedStrings.Instance["Izbor prostorija za spajanje"];
            DataContext = this;
            novoRenoviranje.BrojNovihProstorija = 0;
            renoviranje = novoRenoviranje;
            PrikaziProstorijeZaSpajanje();
        }

        private void PrikaziProstorijeZaSpajanje()
        {
            ProstorijeZaSpajanje = new ObservableCollection<Prostorija>();
            foreach(Prostorija p in serviceProstorija.DobaviProstorijeNaIstomSpratu(renoviranje.Prostorija.BrojProstorije)){
                if(p.BrojProstorije != renoviranje.Prostorija.BrojProstorije)
                    ProstorijeZaSpajanje.Add(p);
            }
        }

        private void Button_Click_Potvrdi(object sender, RoutedEventArgs e)
        {
            if(GridProstorije.SelectedCells.Count > 0)
            {
                renoviranje.ProstorijeZaSpajanje.Clear();
                var izabraneProstorije = GridProstorije.SelectedItems;
                foreach(Prostorija p in izabraneProstorije)
                {
                    renoviranje.ProstorijeZaSpajanje.Add(p);
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
