using Bolnica.Model.Prostorije;
using Bolnica.Validation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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

        private FileStorageRenoviranje storage;
        private DateTime? datumPocetka;
        private DateTime? datumKraja; 

        [Required(ErrorMessage = "Valid Date is Required")]
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

        public FormRenoviranje(string brojProstorije)
        {
            InitializeComponent();
            DataContext = this;
            Renoviranja = new ObservableCollection<Renoviranje>();
            storage = new FileStorageRenoviranje();
            List<Renoviranje> renoviranja = storage.GetAll();
            foreach (Renoviranje r in renoviranja)
            {
                if (r.BrojProstorije == brojProstorije)
                {
                    Renoviranja.Add(r);
                }
            }
            novoRenoviranje.BrojProstorije = brojProstorije;
        }

        private void Button_Click_Zatvori(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void DatePicker_SelectedDateChanged_Pocetak(object sender, SelectionChangedEventArgs e)
        {
            if (datumPocetka != null && datePickerPocetak.SelectedDate != null) {
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
            if(datumKraja != null && datePickerKraj.SelectedDate != null)
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
           if(datumPocetka > datumKraja)
            {
                MessageBox.Show("Datm kraja renoviranja mora biti posle datuma početka renoviranja!");
                return;
            } 
            if(Calendar.BlackoutDates.Contains(datePickerPocetak.SelectedDate.Value) || Calendar.BlackoutDates.Contains(datePickerKraj.SelectedDate.Value))
            {
                btnZakazi.IsEnabled = false;
                return;
            }

            for (DateTime? date = datumPocetka; date < DatumKraja; date = date.Value.AddDays(1.0))
            {
                if(Calendar.BlackoutDates.Contains((DateTime)date))
                {
                    MessageBox.Show("Između datuma početka i datuma kraja renoviranja postoje zauzeti datumi!");
                    return;
                }
            }

            novoRenoviranje.Opis = txtOpis.Text;
            storage.Save(novoRenoviranje);
            Close();
        }
    }
}
