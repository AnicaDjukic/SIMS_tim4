using bolnica.Forms;
using Bolnica.Localization;
using Bolnica.Model.Prostorije;
using Bolnica.Services.Prostorije;
using Model.Prostorije;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace Bolnica.Forms.Upravnik
{
    /// <summary>
    /// Interaction logic for FormSkladiste.xaml
    /// </summary>
    public partial class FormSkladiste : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public ObservableCollection<Prostorija> ProstorijeZaSkladistenje
        {
            get;
            set;
        }

        public static ObservableCollection<Zaliha> Zalihe
        {
            get;
            set;
        }

        private Zaliha magacin;

        private Oprema opremaZaSkladistenje;

        private int kolicinaZaPremestanje;
        public int KolicinaZaPremestanje
        {
            get
            {
                return kolicinaZaPremestanje;
            }
            set
            {
                if (value != kolicinaZaPremestanje)
                {
                    kolicinaZaPremestanje = value;
                    OnPropertyChanged("KolicinaZaPremestanje");
                }
            }
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
        public FormSkladiste(Oprema oprema)
        {
            InitializeComponent();
            Inject = new Injector();
            Title = LocalizedStrings.Instance["Skladištenje"];
            this.DataContext = this;
            opremaZaSkladistenje = oprema;
            List<Zaliha> zaliheOpreme = PronadjiZaliheOpreme();
            InicijalizujPrikazZalihe(zaliheOpreme);
            InicijalizujPrikazProstorijeZaSkladistenje(zaliheOpreme);
        }

        private List<Zaliha> PronadjiZaliheOpreme()
        {
            List<BuducaZaliha> buduceZalihe = Inject.ControllerBuducaZaliha.DobaviBuduceZaliheOpreme(opremaZaSkladistenje.Sifra);
            if (buduceZalihe.Count > 0)
            {
                ObrisiStareZalihe();
                SacuvajNoveZalihe(buduceZalihe);
            }

            List<Zaliha> zaliheOpreme = UcitajZalihe();

            return zaliheOpreme;
        }

        private void ObrisiStareZalihe()
        {
            Inject.ControllerZaliha.ObrisiZaliheOpreme(opremaZaSkladistenje.Sifra);
        }

        private void SacuvajNoveZalihe(List<BuducaZaliha> buduceZalihe)
        {
            Inject.ControllerZaliha.PrebaciBuduceZaliheUZalihe(buduceZalihe);
        }

        private List<Zaliha> UcitajZalihe()
        {
            return Inject.ControllerZaliha.DobaviZaliheOpreme(opremaZaSkladistenje.Sifra);
        }

        private void InicijalizujPrikazZalihe(List<Zaliha> zaliheOpreme)
        {
            Zalihe = new ObservableCollection<Zaliha>();
            if (zaliheOpreme.Count > 0)
            {
                PrikaziZalihe(zaliheOpreme);
            }
            else
            {
                magacin = NapraviMagacin();
                Zalihe.Add(magacin);
            }
        }

        private Zaliha NapraviMagacin()
        {
            Prostorija prostorijaMagacin = Inject.ControllerProstorija.NapraviProstoriju("magacin");
            return new Zaliha { Prostorija = prostorijaMagacin, Oprema = opremaZaSkladistenje, Kolicina = opremaZaSkladistenje.Kolicina };
        }

        private void PrikaziZalihe(List<Zaliha> zaliheOpreme)
        {
            int kolicinaUMagacinu = opremaZaSkladistenje.Kolicina;
            foreach (Zaliha z in zaliheOpreme)
            {
                if (z.Prostorija.BrojProstorije == "magacin")
                {
                    magacin = z;
                }
                else
                {
                    kolicinaUMagacinu -= z.Kolicina;
                    Zalihe.Add(z);
                }
            }

            magacin.Kolicina = kolicinaUMagacinu;
            Zalihe.Add(magacin);
        }

        private void InicijalizujPrikazProstorijeZaSkladistenje(List<Zaliha> zaliheOpreme)
        {
            ProstorijeZaSkladistenje = new ObservableCollection<Prostorija>();
            if (zaliheOpreme.Count == 0)
            {
                PrikaziSveProstorije();
                PrikaziSveBolnickeSobe();
            }   
            else
            {
                PrikaziSlobodneProstorije(zaliheOpreme);
                PrikaziSlobodneBolnickeSobe(zaliheOpreme); 
            }
        }

        private void PrikaziSveProstorije()
        {
            List<Prostorija> prostorije = Inject.ControllerProstorija.DobaviSveProstorije();
            foreach (Prostorija p in prostorije)
            {
                if (!p.Obrisana)
                    ProstorijeZaSkladistenje.Add(p);
            }
        }

        private void PrikaziSveBolnickeSobe()
        {
            List<BolnickaSoba> bolnickeSobe = Inject.ControllerBolnickaSoba.DobaviSveBolnickeSobe();
            foreach (BolnickaSoba b in bolnickeSobe)
            {
                if (!b.Obrisana)
                    ProstorijeZaSkladistenje.Add(b);
            }
        }

        private void PrikaziSlobodneProstorije(List<Zaliha> zaliheOpreme)
        {
            List<Prostorija> slobodneProstorije = Inject.ControllerProstorija.DobaviProstorijeBezOpreme(zaliheOpreme);
            foreach (Prostorija prostorija in slobodneProstorije)
            {
                ProstorijeZaSkladistenje.Add(prostorija);
            }
        }

        private void PrikaziSlobodneBolnickeSobe(List<Zaliha> zaliheOpreme)
        {
            List<BolnickaSoba> slobodneBolnickeSobe = Inject.ControllerBolnickaSoba.DobaviBolnickeSobeBezOpreme(zaliheOpreme);
            foreach (BolnickaSoba bolnickaSoba in slobodneBolnickeSobe)
            {
                ProstorijeZaSkladistenje.Add(bolnickaSoba);
            }
        }

        private void Button_Click_Prebaci(object sender, RoutedEventArgs e)
        {
            if (GridProstorije.SelectedCells.Count > 0)
            {
                if (Inject.ControllerZaliha.ValidnaKolicina(kolicinaZaPremestanje, opremaZaSkladistenje))
                {
                    Prostorija izabranaProstorija = (Prostorija)GridProstorije.SelectedItem;
                    UkloniProstorijuIzPrikaza(izabranaProstorija);

                    Zaliha zaliha = new Zaliha { Prostorija = izabranaProstorija, Oprema = opremaZaSkladistenje, Kolicina = kolicinaZaPremestanje };
                    Zalihe.Add(zaliha);
                    SmanjiKolicinuUMagacinu(kolicinaZaPremestanje);
                }
                else
                {
                    MessageBox.Show(LocalizedStrings.Instance["Unesite validnu količinu. Količina mora biti veća od 0 i manja ili jednaka od količine opreme u magacinu."]);
                }
            }
        }

        private void UkloniProstorijuIzPrikaza(Prostorija prostorija)
        {
            for (int i = 0; i < ProstorijeZaSkladistenje.Count; i++)
            {
                if (ProstorijeZaSkladistenje[i].BrojProstorije == prostorija.BrojProstorije)
                    ProstorijeZaSkladistenje.Remove(ProstorijeZaSkladistenje[i]);
            }
        }

        private void SmanjiKolicinuUMagacinu(int kolicina)
        {
            Zalihe.Remove(magacin);
            magacin.Kolicina -= kolicina;
            Zalihe.Add(magacin);
        }

        private void Button_Click_Vrati(object sender, RoutedEventArgs e)
        {
            if (GridZalihe.SelectedCells.Count > 0 && ((Zaliha)GridZalihe.SelectedItem).Prostorija.BrojProstorije != "magacin")
            {
                Zaliha izabranaZaliha = (Zaliha)GridZalihe.SelectedItem;
                UkloniZalihuIzPrikaza(izabranaZaliha);
                ProstorijeZaSkladistenje.Add(izabranaZaliha.Prostorija);
                KolicinaZaPremestanje = izabranaZaliha.Kolicina;
                PovecajKolicinuUMagacinu(KolicinaZaPremestanje);
            }
        }

        private static void UkloniZalihuIzPrikaza(Zaliha zaliha)
        {
            for (int i = 0; i < Zalihe.Count; i++)
            {
                if (Zalihe[i].Prostorija.BrojProstorije == zaliha.Prostorija.BrojProstorije)
                {
                    Zalihe.Remove(Zalihe[i]);
                }
            }
        }

        private void PovecajKolicinuUMagacinu(int kolicina)
        {
            Zalihe.Remove(magacin);
            magacin.Kolicina += kolicina;
            Zalihe.Add(magacin);
        }

        public void Button_Click_Potvrdi(object sender, RoutedEventArgs e)
        {
            if (opremaZaSkladistenje.TipOpreme != TipOpreme.staticka)
            {
                ObrisiStareZalihe();
                Inject.ControllerZaliha.SacuvajZalihe(Zalihe);
            }
            else
            {
                FormDatumPremestanja formPremestanje = new FormDatumPremestanja();
                formPremestanje.datePickerDatum.DisplayDateStart = DateTime.Now;
                formPremestanje.Show();
            }

            Close();
        }

        public void Button_Click_Odustani(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
