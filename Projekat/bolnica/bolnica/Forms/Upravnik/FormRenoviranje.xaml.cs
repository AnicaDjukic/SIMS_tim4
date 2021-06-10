using Bolnica.Localization;
using Bolnica.Model.Prostorije;
using Bolnica.Services.Prostorije;
using Model.Prostorije;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Bolnica.Forms.Upravnik
{
    /// <summary>
    /// Interaction logic for FormRenoviranje.xaml
    /// </summary>
    public partial class FormRenoviranje : Window
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public ObservableCollection<Renoviranje> Renoviranja
        {
            get;
            set;
        }

        private DateTime? datumPocetka;
        private DateTime? datumKraja;

        public DateTime? DatumPocetka
        {
            get
            {
                return datumPocetka;
            }
            set
            {
                if (value != datumPocetka)
                {
                    datumPocetka = value;
                    OnPropertyChanged("DatumPocetka");
                }
            }
        }

        public DateTime? DatumKraja
        {
            get
            {
                return datumKraja;
            }
            set
            {
                if (value != datumKraja)
                {
                    datumKraja = value;
                    OnPropertyChanged("DatumKraja");
                }
            }
        }

        public static Renoviranje novoRenoviranje = new Renoviranje();

        private Injector inject;
        public Injector Inject
        {
            get { return inject; }
            set
            {
                inject = value;
            }
        }
        public FormRenoviranje(string brojProstorije)
        {
            InitializeComponent();
            DataContext = this;
            Inject = new Injector();
            Title = LocalizedStrings.Instance["Renoviranje prostorije"];
            PrikaziRenoviranja(brojProstorije);
            novoRenoviranje.Prostorija = new Prostorija();
            novoRenoviranje.Prostorija.BrojProstorije = brojProstorije;
        }

        private void PrikaziRenoviranja(string brojProstorije)
        {
            Renoviranja = new ObservableCollection<Renoviranje>();
            foreach (Renoviranje r in Inject.ControllerRenoviranje.DobaviRenoviranjaProstorije(brojProstorije))
            {
                Renoviranja.Add(r);
            }
        }

        private void Button_Click_Zatvori(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void DatePicker_SelectedDateChanged_Pocetak(object sender, SelectionChangedEventArgs e)
        {
            if (datumPocetka != null && datePickerPocetak.SelectedDate != null)
            {
                novoRenoviranje.PocetakRenoviranja = (DateTime)datumPocetka;
                datePickerKraj.DisplayDateStart = datumPocetka;
                if (datumKraja != null)
                    btnZakazi.IsEnabled = true;
                else
                    btnZakazi.IsEnabled = false;
            }
            else
            {
                btnZakazi.IsEnabled = false;
            }
        }

        private void datePickerKraj_SelectedDateChanged_Kraj(object sender, SelectionChangedEventArgs e)
        {
            if (datumKraja != null && datePickerKraj.SelectedDate != null)
            {
                novoRenoviranje.KrajRenoviranja = (DateTime)datumKraja;
                if (datumPocetka != null)
                    btnZakazi.IsEnabled = true;
                else
                    btnZakazi.IsEnabled = false;
            }
            else
            {
                btnZakazi.IsEnabled = false;
            }
        }

        private void Button_Click_Zakazi(object sender, RoutedEventArgs e)
        {
            if (DatumiRenoviranjaValidni())
            {
                novoRenoviranje.Opis = txtOpis.Text;
                Inject.ControllerRenoviranje.SacuvajRenoviranje(novoRenoviranje);
                Close();
            }
        }

        private bool DatumiRenoviranjaValidni()
        {
            bool validni = true;
            if (datumPocetka > datumKraja)
            {
                MessageBox.Show(LocalizedStrings.Instance["Datum kraja renoviranja mora biti posle datuma početka renoviranja!"]);
                validni = false;
            }
            if (DatumNijeSlobodan(datumPocetka) || DatumNijeSlobodan(datumKraja))
            {
                btnZakazi.IsEnabled = false;
                validni = false;
            }

            if(PostojeZauzetiDatumiIzmedju(datumPocetka, datumKraja))
            {
                MessageBox.Show(LocalizedStrings.Instance["Između datuma početka i datuma kraja renoviranja postoje zauzeti datumi!"]);
                validni = false;
            }
           
            return validni;
        }

        private bool PostojeZauzetiDatumiIzmedju(DateTime? datumPocetka, DateTime? datumKraja)
        {
            bool postoje = false;
            for (DateTime? date = datumPocetka; date < datumKraja; date = date.Value.AddDays(1.0))
            {
                if (Calendar.BlackoutDates.Contains((DateTime)date))
                {
                    postoje = true;
                    break;
                }
            }
            return postoje;
        }

        private bool DatumNijeSlobodan(DateTime? datum)
        {
            return Calendar.BlackoutDates.Contains((DateTime)datum);
        }

        private void btnSpoji_Click(object sender, RoutedEventArgs e)
        {
            FormSpajanjeProstorija formSpajanje = new FormSpajanjeProstorija(novoRenoviranje);
            formSpajanje.Show();
        }

        private void btnPodeli_Click(object sender, RoutedEventArgs e)
        {
            FormPodelaProstorije formPodela = new FormPodelaProstorije(novoRenoviranje);
            formPodela.Show();
        }
    }
}
