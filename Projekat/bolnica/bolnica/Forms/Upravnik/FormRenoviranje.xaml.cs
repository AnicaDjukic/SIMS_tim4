using Bolnica.Model.Prostorije;
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
            if (datumPocetka != null) {
                novoRenoviranje.PocetakRenoviranja = (DateTime)datumPocetka;
                datePickerKraj.DisplayDateStart = datumPocetka;
            } 
            else
            {
                datePickerKraj.DisplayDateStart = DateTime.Now;
            }
        }

        private void datePickerKraj_SelectedDateChanged_Kraj(object sender, SelectionChangedEventArgs e)
        {
            if(datumPocetka != null)
            novoRenoviranje.KrajRenoviranja = (DateTime)datumKraja;
        }

        private void Button_Click_Zakazi(object sender, RoutedEventArgs e)
        {
            List<Renoviranje> renoviranja = storage.GetAll();
            int maxId = 0;
            foreach(Renoviranje r in renoviranja)
            {
                if (r.Id > maxId)
                    maxId = r.Id;
            }
            novoRenoviranje.Opis = txtOpis.Text;
            novoRenoviranje.Id = maxId + 1;
            storage.Save(novoRenoviranje);
            Close();
        }
    }
}
