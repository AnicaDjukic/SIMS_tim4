using Bolnica.Sekretar;
using Model.Korisnici;
using Model.Pacijenti;
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

namespace Bolnica.Forms.Sekretar
{
    /// <summary>
    /// Interaction logic for FormDodajPrimaocePacijente.xaml
    /// </summary>
    public partial class FormDodajPrimaocePacijente : Window
    {
        public static ObservableCollection<Pacijent> DodatiPrimaoci { get; set; }
        public static ObservableCollection<Pacijent> SviPrimaoci { get; set; }
        private FileStoragePacijenti storage;
        public FormDodajPrimaocePacijente(int id)
        {
            InitializeComponent();
            dataGridPrimaociSvi.DataContext = this;
            dataGridPrimaociDodati.DataContext = this;
            DodatiPrimaoci = new ObservableCollection<Pacijent>();
            SviPrimaoci = new ObservableCollection<Pacijent>();
            storage = new FileStoragePacijenti();
            List<Pacijent> dodati = new List<Pacijent>();
            List<Pacijent> pacijenti = storage.GetAll();
            List<Pacijent> redovniPacijenti = new List<Pacijent>();

            foreach (Pacijent p in pacijenti)
                if (!p.Guest)
                    redovniPacijenti.Add(p);

            for (int i = 0; i < FormObavestenja.Obavestenja.Count; i++)
                if (id == FormObavestenja.Obavestenja[i].Id)
                {
                    List<string> korImena = new List<string>();
                    foreach (Korisnik k in FormObavestenja.Obavestenja[i].Korisnici)
                        korImena.Add(k.KorisnickoIme);

                    foreach (Pacijent p in pacijenti)
                    {
                        if (!p.Guest)
                        {
                            if (korImena.Contains(p.KorisnickoIme)) 
                            {
                                DodatiPrimaoci.Add(p);
                                dodati.Add(p);
                            }
                        }
                    }
                }

            foreach (Pacijent p in pacijenti)
                if (!p.Guest)
                    SviPrimaoci.Add(p);

            for (int i = redovniPacijenti.Count - 1; i >= 0; i--)
                for (int j = 0; j < dodati.Count; j++)
                    if (String.Equals(redovniPacijenti[i].Jmbg, dodati[j].Jmbg))
                        SviPrimaoci.RemoveAt(i);
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ti1.IsSelected)
            {
                btnUkloni.IsEnabled = false;
                if (SviPrimaoci.Count != 0)
                    btnDodaj.IsEnabled = true;
                else
                    btnDodaj.IsEnabled = false;
            }
            else if (ti2.IsSelected)
            {
                btnDodaj.IsEnabled = false;
                if (DodatiPrimaoci.Count != 0)
                    btnUkloni.IsEnabled = true;
                else
                    btnUkloni.IsEnabled = false;
            }
        }

        private void Button_Clicked_Dodaj(object sender, RoutedEventArgs e)
        {
            List<Pacijent> primaoci = new List<Pacijent>();
            if (dataGridPrimaociSvi.SelectedItems.Count == 0)
            {
                MessageBoxResult result = MessageBox.Show("Niste odabrali primaoca za dodavanje.",
                                          "Dodavanje primaoca",
                                          MessageBoxButton.OK,
                                          MessageBoxImage.Information);
            }
            else
            {
                for (int i = 0; i < dataGridPrimaociSvi.SelectedItems.Count; i++)
                {
                    Pacijent p = (Pacijent)dataGridPrimaociSvi.SelectedItems[i];
                    primaoci.Add(p);
                }
                foreach (Pacijent p in primaoci)
                {
                    SviPrimaoci.Remove(p);
                    DodatiPrimaoci.Add(p);
                }
            }
        }

        private void Button_Clicked_Ukloni(object sender, RoutedEventArgs e)
        {
            List<Pacijent> primaoci = new List<Pacijent>();
            if (dataGridPrimaociDodati.SelectedItems.Count == 0)
            {
                MessageBoxResult result = MessageBox.Show("Niste odabrali primaoca za uklanjanje.",
                                          "Uklanjanje primaoca",
                                          MessageBoxButton.OK,
                                          MessageBoxImage.Information);
            }
            else
            {
                for (int i = 0; i < dataGridPrimaociDodati.SelectedItems.Count; i++)
                {
                    Pacijent p = (Pacijent)dataGridPrimaociDodati.SelectedItems[i];
                    primaoci.Add(p);
                }
                foreach (Pacijent p in primaoci)
                {
                    DodatiPrimaoci.Remove(p);
                    SviPrimaoci.Add(p);
                }
            }
        }
    }
}
