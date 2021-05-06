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
    /// Interaction logic for FormDodajPrimaoce.xaml
    /// </summary>
    public partial class FormDodajPrimaoce : Window
    {
        public static ObservableCollection<Primalac> DodatiPrimaoci { get; set; }
        public static ObservableCollection<Primalac> SviPrimaoci { get; set; }
        private Button btnDodajPacijente;
        private FileStoragePacijenti storagePacijenti;
        private List<Pacijent> pacijenti;
        public FormDodajPrimaoce(Button btnDodajPacijente, int id)
        {
            InitializeComponent();
            dataGridPrimaociSvi.DataContext = this;
            dataGridPrimaociDodati.DataContext = this;
            DodatiPrimaoci = new ObservableCollection<Primalac>();
            SviPrimaoci = new ObservableCollection<Primalac>();
            this.btnDodajPacijente = btnDodajPacijente;
            List<Primalac> dodati = new List<Primalac>();
            storagePacijenti = new FileStoragePacijenti();
            pacijenti = storagePacijenti.GetAll();
            Primalac p1 = new Primalac("Upravnik");
            Primalac p2 = new Primalac("Svi lekari");
            Primalac p3 = new Primalac("Svi pacijenti");
            List<Primalac> primaoci = new List<Primalac>();
            primaoci.Add(p1);
            primaoci.Add(p2);
            primaoci.Add(p3);

            for (int i = 0; i < FormObavestenja.Obavestenja.Count; i++)
                if (id == FormObavestenja.Obavestenja[i].Id)
                {
                    if (FormObavestenja.Obavestenja[i].KorisnickaImena.Contains("upravnik")) 
                    {
                        DodatiPrimaoci.Add(p1);
                        dodati.Add(p1);
                    }

                    if (FormObavestenja.Obavestenja[i].KorisnickaImena.Contains("lekar"))
                    {
                        DodatiPrimaoci.Add(p2);
                        dodati.Add(p2);
                    }

                    bool svi = true;
                    foreach (Pacijent p in pacijenti)
                    {
                        if (!p.Guest) 
                        {
                            if (!FormObavestenja.Obavestenja[i].KorisnickaImena.Contains(p.KorisnickoIme))
                                svi = false;
                        }
                    }

                    if (svi) 
                    {
                        DodatiPrimaoci.Add(p3);
                        dodati.Add(p3);
                    }

                    break;
                }

            SviPrimaoci.Add(p1);
            SviPrimaoci.Add(p2);
            SviPrimaoci.Add(p3);

            for (int i = primaoci.Count - 1; i >= 0; i--)
                for (int j = 0; j < dodati.Count; j++)
                    if (String.Equals(primaoci[i].Naziv, dodati[j].Naziv))
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
            List<Primalac> primaoci = new List<Primalac>();
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
                    Primalac p = (Primalac)dataGridPrimaociSvi.SelectedItems[i];
                    primaoci.Add(p);
                }
                foreach (Primalac p in primaoci) 
                {
                    SviPrimaoci.Remove(p);
                    DodatiPrimaoci.Add(p);
                }
            }
        }

        private void Button_Clicked_Ukloni(object sender, RoutedEventArgs e)
        {
            List<Primalac> primaoci = new List<Primalac>();
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
                    Primalac p = (Primalac)dataGridPrimaociDodati.SelectedItems[i];
                    primaoci.Add(p);
                }
                foreach (Primalac p in primaoci)
                {
                    DodatiPrimaoci.Remove(p);
                    SviPrimaoci.Add(p);
                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            bool dodat = false;

            foreach (Primalac p in DodatiPrimaoci)
                if (p.Naziv.Equals("Svi pacijenti"))
                {
                    dodat = true;
                    btnDodajPacijente.IsEnabled = false;
                    break;
                }

            if (!dodat)
                btnDodajPacijente.IsEnabled = true;
        }
    }

    public class Primalac
    {
        public string Naziv { get; set; }

        public Primalac(string naziv) { Naziv = naziv; }
    }
}
