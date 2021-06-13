using bolnica.Forms;
using Bolnica.Localization;
using Bolnica.Model.Prostorije;
using System;
using System.ComponentModel;
using System.Windows;

namespace Bolnica.Forms.Upravnik
{
    /// <summary>
    /// Interaction logic for CreateFormOprema.xaml
    /// </summary>
    public partial class CreateFormOprema : Window
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private string sifra;
        private string naziv;
        private int kolicina;

        public string Sifra
        {
            get
            {
                return sifra;
            }
            set
            {
                if (value != sifra)
                {
                    sifra = value;
                    OnPropertyChanged("Sifra");
                }
            }
        }

        public string Naziv
        {
            get
            {
                return naziv;
            }
            set
            {
                if (value != naziv)
                {
                    naziv = value;
                    OnPropertyChanged("Naziv");
                }
            }
        }

        public int Kolicina
        {
            get
            {
                return kolicina;
            }
            set
            {
                if (value != kolicina)
                {
                    kolicina = value;
                    OnPropertyChanged("Kolicina");
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
        public CreateFormOprema(string sifra)
        {
            InitializeComponent();
            DataContext = this;
            Inject = new Injector();
            if (!FormUpravnik.clickedDodaj)
            {
                Title = LocalizedStrings.Instance["Izmena opreme"];
                PopuniPolja(sifra);
            }
            else
                Title = LocalizedStrings.Instance["Dodavanje opreme"];
        }

        private void PopuniPolja(string sifra)
        {
            Oprema o = Inject.ControllerOprema.DobaviOpremu(sifra);
            Sifra = o.Sifra;
            Naziv = o.Naziv;
            Kolicina = o.Kolicina;
            if (o.TipOpreme == TipOpreme.staticka)
                ComboTipOpreme.SelectedIndex = 0;
            else
                ComboTipOpreme.SelectedIndex = 1;
        }

        private void Button_Click_Potvrdi(object sender, RoutedEventArgs e)
        {
            if (Sifra != null && Naziv != null)
            {
                if (!Inject.ControllerOprema.OpremaPostoji(sifra) || !FormUpravnik.clickedDodaj)
                {
                    if(UkKolicinaValidna(kolicina))
                    {
                        if (!FormUpravnik.clickedDodaj)
                        {
                            Inject.ControllerOprema.ObrisiOpremu(sifra);
                            UkloniIzPrikaza(sifra);
                        }

                        Oprema oprema = NapraviOpremu();

                        Inject.ControllerOprema.SacuvajOpremu(oprema);
                        FormUpravnik.Oprema.Add(oprema);
                        Close();
                    }
                }
                else
                {
                    MessageBox.Show(LocalizedStrings.Instance["Oprema sa istom šifrom već postoji!"]);
                }
            }
        }

        private void UkloniIzPrikaza(string sifra)
        {
            for (int i = 0; i < FormUpravnik.Oprema.Count; i++)
            {
                if (FormUpravnik.Oprema[i].Sifra == sifra)
                {
                    FormUpravnik.Oprema.Remove(FormUpravnik.Oprema[i]);
                    break;
                }

            }
        }
        private Oprema NapraviOpremu()
        {
            Oprema oprema = new Oprema { Sifra = sifra, Naziv = naziv, Kolicina = kolicina };
            if (ComboTipOpreme.SelectedIndex == 0)
                oprema.TipOpreme = TipOpreme.staticka;
            else
                oprema.TipOpreme = TipOpreme.dinamicka;

            return oprema;
        }

        private void Button_Click_Skladisti(object sender, RoutedEventArgs e)
        {
            Oprema oprema = NapraviOpremu();
            if (UkKolicinaValidna(kolicina))
            {
                if (!FormUpravnik.clickedDodaj || !Inject.ControllerOprema.OpremaPostoji(sifra))
                {
                    var s = new FormSkladiste(oprema);
                    s.Show();
                }
                else
                {
                    MessageBox.Show(LocalizedStrings.Instance["Oprema sa istom šifrom već postoji!"]);
                    return;
                }
            }
        }

        private bool UkKolicinaValidna(int ukKolicina)
        {
            bool validna = true;
            if (ukKolicina <= 0)
            {
                MessageBox.Show(LocalizedStrings.Instance["Unesite validnu količinu! Količina mora biti veća od 0."]);
                validna = false;
            }
            else
            {
                validna = UkupnaKolicinaVecaOdRezervisane(ukKolicina);
            }

            return validna;
        }

        private bool UkupnaKolicinaVecaOdRezervisane(int ukKolicina)
        {
            int rezervisanaKolicina = Inject.ControllerZaliha.DobaviRezervisanuKolicinu(sifra);
            if (ukKolicina < rezervisanaKolicina)
            {
                MessageBox.Show(LocalizedStrings.Instance["Nije moguće toliko smajiti količinu. Količina ne sme biti manja od"] + " " + rezervisanaKolicina);
                return false;
            }

            return true;
        }

        private void Button_Click_Odustani(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
