using Bolnica.Localization;
using Bolnica.Model.Prostorije;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace Bolnica.Forms.Upravnik
{
    /// <summary>
    /// Interaction logic for ViewFormOprema.xaml
    /// </summary>
    public partial class ViewFormOprema : Window
    {
        public static ObservableCollection<Zaliha> Zalihe
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
        public ViewFormOprema(string sifraOpreme)
        {
            InitializeComponent();
            this.DataContext = this;
            Inject = new Injector();
            Title = LocalizedStrings.Instance["Prikaz opreme"];
            PrikaziInformacijeOpreme(sifraOpreme);
            List<Zaliha> zalihe = DobaviZaliheOpreme(sifraOpreme);
            PrikaziZalihe(zalihe);
        }

        private void PrikaziInformacijeOpreme(string sifraOpreme)
        {
            Oprema oprema = Inject.ControllerOprema.DobaviOpremu(sifraOpreme);
            lblSifra.Content = oprema.Sifra;
            lblNaziv.Content = oprema.Naziv;
            if (oprema.TipOpreme == TipOpreme.dinamicka)
                lblTipOpreme.Content = LocalizedStrings.Instance["Dinamička"];
            else
                lblTipOpreme.Content = LocalizedStrings.Instance["Statička"];
            lblKolicina.Content = oprema.Kolicina.ToString();
        }

        private List<Zaliha> DobaviZaliheOpreme(string sifraOpreme)
        {
            List<BuducaZaliha> buduceZalihe = Inject.ControllerBuducaZaliha.DobaviBuduceZaliheOpreme(sifraOpreme);

            List<Zaliha> zalihe;
            if (buduceZalihe.Count > 0)
            {
                ObrisiStareZalihe(sifraOpreme);
                zalihe = NapraviNoveZalihe(buduceZalihe);
                inject.ControllerZaliha.SacuvajZalihe(zalihe);
            }
            else
            {
                zalihe = Inject.ControllerZaliha.DobaviZaliheOpreme(sifraOpreme);
            }

            return zalihe;
        }

        private void ObrisiStareZalihe(string sifraOpreme)
        {
            Inject.ControllerZaliha.ObrisiZaliheOpreme(sifraOpreme);
        }

        private List<Zaliha> NapraviNoveZalihe(List<BuducaZaliha> buduceZalihe)
        {
            return Inject.ControllerZaliha.NapraviZaliheOdBuducihZaliha(buduceZalihe);
        }

        private void PrikaziZalihe(List<Zaliha> zalihe)
        {
            Zalihe = new ObservableCollection<Zaliha>();
            foreach (Zaliha z in zalihe)
                Zalihe.Add(z);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
