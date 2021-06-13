using Bolnica.Model.Pregledi;
using Bolnica.Repository.Pregledi;
using Bolnica.Services.Pregledi;
using Model.Pregledi;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Bolnica.Forms.Upravnik
{
    /// <summary>
    /// Interaction logic for FormSastojci.xaml
    /// </summary>
    public partial class FormSastojci : Window
    {
        public static ObservableCollection<Sastojak> Sastojci
        {
            get;
            set;
        }
        private Lek noviLek;
        FileRepositorySastojak storage = new FileRepositorySastojak();
        private ServiceSastojak serviceSastojak = new ServiceSastojak();
        public FormSastojci(Lek lek)
        {
            InitializeComponent();
            DataContext = this;
            noviLek = lek;
            PrikaziSveSastojke();
        }

        private void PrikaziSveSastojke()
        {
            Sastojci = new ObservableCollection<Sastojak>();
            foreach (Sastojak s in serviceSastojak.DobaviSveSastojke())
            {
                Sastojci.Add(s);
            }
        }

        private void Button_Click_Potvrdi(object sender, RoutedEventArgs e)
        {
            if(dataGridSastojci.SelectedItems.Count > 0)
            {
               var selected = dataGridSastojci.SelectedItems;

                foreach(Sastojak s in selected)
                {
                    noviLek.Sastojak.Add(s);
                    CreateFormLekovi.Sastojci.Add(s);
                }
                Close();
            }
        }

        private void Button_Click_Odustani(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click_Dodaj_Novi(object sender, RoutedEventArgs e)
        {
            CreateFormSastojak createForm = new CreateFormSastojak();
            createForm.Show();
        }

        private void Button_Click_Obrisi(object sender, RoutedEventArgs e)
        {
            if (dataGridSastojci.SelectedItems.Count > 0)
            {
                if (UpitZaBrisanjeLeka() == MessageBoxResult.Yes)
                {
                    var selectedItems = dataGridSastojci.SelectedItems;
                    List<Sastojak> sastojciZaBrisanje = new List<Sastojak>();
                    ObrisiSastojke(selectedItems);
                }
            }
        }

        private void ObrisiSastojke(IList selectedItems)
        {
            List<Sastojak> sastojciZaBrisanje = new List<Sastojak>();
            foreach (Sastojak s in selectedItems)
            {
                sastojciZaBrisanje.Add(s);
                serviceSastojak.ObrisiSastojak(s);
            }
            foreach (Sastojak s in sastojciZaBrisanje)
            {
                Sastojci.Remove(s);
            }
        }

        private MessageBoxResult UpitZaBrisanjeLeka()
        {
            string sMessageBoxText = "";
            if (dataGridSastojci.SelectedItems.Count > 1)
                sMessageBoxText = "Da li ste sigurni da želite da obrišete izabrane sastojke?";
            else
                sMessageBoxText = "Da li ste sigurni da želite da obrišete sastojak " + ((Sastojak)dataGridSastojci.SelectedItem).Naziv + "?";
            string sCaption = "Brisanje opreme";

            MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
            MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

            MessageBoxResult rsltMessageBox = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);

            return rsltMessageBox;
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                PretraziSastojke();
            }
        }

        private void PretraziSastojke()
        {
            Sastojci.Clear();
            foreach (Sastojak s in storage.GetAll())
            {
                if (s.Naziv.ToLower().StartsWith(txtSearch.Text.ToLower()))
                {
                    Sastojci.Add(s);
                }
            }
        }
    }
}
