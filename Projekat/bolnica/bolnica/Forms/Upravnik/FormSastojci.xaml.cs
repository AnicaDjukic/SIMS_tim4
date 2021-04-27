using Bolnica.Model.Pregledi;
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
        FileStorageSastojak storage = new FileStorageSastojak();
        public FormSastojci(Lek lek)
        {
            InitializeComponent();
            DataContext = this;
            noviLek = lek;
            Sastojci = new ObservableCollection<Sastojak>();
            List<Sastojak> sastojci = storage.GetAll();
            foreach(Sastojak s in sastojci)
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
                var selectedItems = dataGridSastojci.SelectedItems;
                List<Sastojak> sastojciZaBrisanje = new List<Sastojak>();
                foreach (Sastojak s in selectedItems)
                {
                    sastojciZaBrisanje.Add(s);
                    storage.Delete(s);
                }
                foreach (Sastojak s in sastojciZaBrisanje)
                {
                    Sastojci.Remove(s);
                }
            }
        }
    }
}
