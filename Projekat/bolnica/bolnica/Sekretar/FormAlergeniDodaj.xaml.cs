using Bolnica.Forms;
using Bolnica.Model.Pregledi;
using Model.Korisnici;
using Model.Pacijenti;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Bolnica.Sekretar
{
    /// <summary>
    /// Interaction logic for FormAlergeniDodaj.xaml
    /// </summary>
    public partial class FormAlergeniDodaj : Window
    {
        public static ObservableCollection<Sastojak> SviAlergeni { get; set; }
        public static ObservableCollection<Sastojak> DodatiAlergeni { get; set; }
        private FileStorageSastojak storage;

        public FormAlergeniDodaj(TextBox txtJMBG)
        {
            InitializeComponent();
            dataGridAlergeniDodati.DataContext = this;
            dataGridAlergeniSvi.DataContext = this;
            SviAlergeni = new ObservableCollection<Sastojak>();
            DodatiAlergeni = new ObservableCollection<Sastojak>();
            storage = new FileStorageSastojak();
            
            List<Sastojak> alergeni = storage.GetAll();
            List<Sastojak> dodati = new List<Sastojak>();

            for (int i = 0; i < FormSekretar.Pacijenti.Count; i++)
                if(String.Equals(txtJMBG.Text, FormSekretar.Pacijenti[i].Jmbg))
                {
                    if (FormSekretar.Pacijenti[i].Alergeni != null)
                        foreach (Sastojak s in FormSekretar.Pacijenti[i].Alergeni)
                        {
                            DodatiAlergeni.Add(s);
                            dodati.Add(s);
                        }
                    break;
                }

            foreach (Sastojak s in alergeni)
                SviAlergeni.Add(s);

            for (int i = alergeni.Count - 1; i >= 0; i--)
                for (int j = 0; j < dodati.Count; j++)
                    if (String.Equals(alergeni[i].Naziv, dodati[j].Naziv))
                        SviAlergeni.RemoveAt(i);
        }

        private void Button_Clicked_Dodaj(object sender, RoutedEventArgs e)
        {
            List<Sastojak> alergeni = new List<Sastojak>();
            if (dataGridAlergeniSvi.SelectedItems.Count == 0)
            {
                MessageBoxResult result = MessageBox.Show("Niste odabrali alergen za dodavanje.",
                                          "Dodavanje alergena",
                                          MessageBoxButton.OK,
                                          MessageBoxImage.Information);
            }
            else
            {
                for (int i = 0; i < dataGridAlergeniSvi.SelectedItems.Count; i++)
                {
                    Sastojak s = (Sastojak)dataGridAlergeniSvi.SelectedItems[i];
                    alergeni.Add(s);
                }
                foreach (Sastojak s in alergeni) 
                {
                    SviAlergeni.Remove(s);
                    DodatiAlergeni.Add(s);
                }
            }
        }

        private void Button_Clicked_Ukloni(object sender, RoutedEventArgs e)
        {
            List<Sastojak> alergeni = new List<Sastojak>();
            if (dataGridAlergeniDodati.SelectedItems.Count == 0) 
            {
                MessageBoxResult result = MessageBox.Show("Niste odabrali alergen za uklanjanje.",
                                          "Uklanjanje alergena",
                                          MessageBoxButton.OK,
                                          MessageBoxImage.Information);
            }
            else
            {
                for (int i = 0; i < dataGridAlergeniDodati.SelectedItems.Count; i++)
                {
                    Sastojak s = (Sastojak)dataGridAlergeniDodati.SelectedItems[i];
                    alergeni.Add(s);
                }
                foreach (Sastojak s in alergeni)
                {
                    DodatiAlergeni.Remove(s);
                    SviAlergeni.Add(s);
                }
            }
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ti1.IsSelected)
            {
                btnUkloni.IsEnabled = false;
                btnDodaj.IsEnabled = true;
            }
            else if (ti2.IsSelected) 
            {
                btnDodaj.IsEnabled = false;
                if (DodatiAlergeni.Count != 0)
                    btnUkloni.IsEnabled = true;
                else
                    btnUkloni.IsEnabled = false;
            }
        }
    }
}
