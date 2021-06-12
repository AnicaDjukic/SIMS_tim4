using Bolnica.Controller;
using Bolnica.Model.Pregledi;
using Bolnica.ViewModel;
using Model.Korisnici;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Bolnica.Forms
{
    /// <summary>
    /// Interaction logic for FormNasiPredloziPage.xaml
    /// </summary>
    public partial class FormNasiPredloziPage : Page
    {
        private Pacijent trenutniPacijent = new Pacijent();
        public static ObservableCollection<PrikazPregleda> PredlozeniTermini
        {
            get;
            set;
        }

        private NasiPredloziController nasiPredloziController = new NasiPredloziController();

        public FormNasiPredloziPage(Pacijent pacijent, DateTime datum, int sat, int minut, Lekar lekar)
        {
            InitializeComponent();
            this.DataContext = this;
            trenutniPacijent = pacijent;

            nasiPredloziController.DobijPredlozeneTermine(pacijent, datum, sat, minut, lekar);
        }

        private void PotvrdiIzabraniTermin(object sender, RoutedEventArgs e)
        {
            var objekat = nasiPredloziGrid.SelectedValue;

            if (objekat != null)
            {
                PrikazPregleda prikazPregleda = (PrikazPregleda)nasiPredloziGrid.SelectedItem;
                nasiPredloziController.OdaberiTermin(prikazPregleda);
            }
            else
            {
                MessageBox.Show("Morate odabrati pregled koji zelite da zakazete!", "Upozorenje");
            }
        }

        private void Odustani(object sender, RoutedEventArgs e)
        {
            PacijentPageViewModel pacijentPageViewModel = new PacijentPageViewModel(trenutniPacijent);
            FormPacijentWeb.Forma.Pocetna.Content = new FormPacijentPage(pacijentPageViewModel);
        }
    }
}
