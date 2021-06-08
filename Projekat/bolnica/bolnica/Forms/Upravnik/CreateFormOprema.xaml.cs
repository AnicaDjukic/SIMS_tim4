using bolnica.Forms;
using Bolnica.Localization;
using Bolnica.Model.Prostorije;
using Bolnica.Services.Prostorije;
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

        private ServiceOprema serviceOprema = new ServiceOprema();
        public CreateFormOprema()
        {
            InitializeComponent();
            if (!FormUpravnik.clickedDodaj)
                Title = LocalizedStrings.Instance["Izmena opreme"];
            else
                Title = LocalizedStrings.Instance["Dodavanje opreme"];
            this.DataContext = this;
        }

        private void Button_Click_Potvrdi(object sender, RoutedEventArgs e)
        {
            if (Sifra != null && Naziv != null)
            {
                if (!serviceOprema.OpremaPostoji(sifra) || !FormUpravnik.clickedDodaj)
                {
                    if (!FormUpravnik.clickedDodaj)
                    {
                        serviceOprema.ObrisiOpremu(sifra);
                        UkloniIzPrikaza(sifra);
                    }

                    Oprema oprema = NapraviOpremu();

                    serviceOprema.SacuvajOpremu(oprema);
                    FormUpravnik.Oprema.Add(oprema);
                }
                else
                {
                    MessageBox.Show(LocalizedStrings.Instance["Oprema sa istom šifrom već postoji!"]);
                    return;
                }
                Close();
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
                if (!FormUpravnik.clickedDodaj || !serviceOprema.OpremaPostoji(sifra))
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
                int rezervisanaKolicina = serviceOprema.IzracunajRezervisanuKolicinu(sifra);
                if (!serviceOprema.MoguceSmanjitiKolicinu(ukKolicina, rezervisanaKolicina))
                {
                    MessageBox.Show(LocalizedStrings.Instance["Nije moguće toliko smajiti količinu. Količina ne sme biti manja od"] + " " + rezervisanaKolicina);
                    validna = false;
                }
            }

            return validna;
        }

        private void Button_Click_Odustani(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
