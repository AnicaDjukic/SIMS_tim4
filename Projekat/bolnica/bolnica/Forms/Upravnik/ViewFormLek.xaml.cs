using bolnica.Forms;
using Bolnica.Localization;
using Bolnica.Model.Pregledi;
using Bolnica.Services.Pregledi;
using Model.Pregledi;
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
    /// Interaction logic for ViewFormLek.xaml
    /// </summary>
    public partial class ViewFormLek : Window
    {
        public static ObservableCollection<Sastojak> Sastojci
        {
            get;
            set;
        }

        public static ObservableCollection<Lek> LekoviZamene
        {
            get;
            set;
        }

        private Injector inject;
        public Injector Inject
        {
            get { return inject; }
            set
            {
                inject = value;
            }
        }
        public ViewFormLek(int idLeka)
        {
            InitializeComponent();
            this.DataContext = this;
            Inject = new Injector();
            Title = LocalizedStrings.Instance["Prikaz leka"];
            PrikaziLek(idLeka);
        }

        private void PrikaziLek(int idLeka)
        {
            PrikaziInformacijeLeka(idLeka);
            PrikaziZameneLeka(idLeka);
            PrikaziSastojkeLeka(idLeka);
        }

        private void PrikaziInformacijeLeka(int idLeka)
        {
            Lek lek = Inject.ControllerLek.DobaviLek(idLeka);
            lblId.Content = lek.Id;
            lblNaziv.Content = lek.Naziv;
            lblKolicinaUMg.Content = lek.KolicinaUMg;
            lblProizvodjac.Content = lek.Proizvodjac;
            if (lek.Status == StatusLeka.odobren)
                lblStatus.Content = LocalizedStrings.Instance["Odobren"];
            else if (lek.Status == StatusLeka.odbijen)
                lblStatus.Content = LocalizedStrings.Instance["Odbijen"];
            else
                lblStatus.Content = LocalizedStrings.Instance["Čeka validaciju"];

            lblZalihe.Content = lek.Zalihe;
        }

        private void PrikaziSastojkeLeka(int idLeka)
        {
            Lek lek = Inject.ControllerLek.DobaviLek(idLeka);
            Sastojci = new ObservableCollection<Sastojak>();
            foreach (Sastojak s in Inject.ControllerLek.DobaviSastojkeLeka(lek))
            {
                Sastojci.Add(s);
            }
        }

        private void PrikaziZameneLeka(int idLeka)
        {
            Lek lek = Inject.ControllerLek.DobaviLek(idLeka);
            LekoviZamene = new ObservableCollection<Lek>();
            foreach (Lek l in Inject.ControllerLek.DobaviSveZameneLeka(lek))
            {
                LekoviZamene.Add(l);
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

