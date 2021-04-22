using Bolnica.Forms;
using Bolnica.Model.Korisnici;
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

namespace Bolnica.Sekretar
{
    /// <summary>
    /// Interaction logic for FormObavestenja.xaml
    /// </summary>
    public partial class FormObavestenja : Window
    {
        public static ObservableCollection<Obavestenje> Obavestenja
        {
            get;
            set;
        }
        private FileStorageObavestenja storage;
        public static bool clickedDodaj;

        public FormObavestenja()
        {
            InitializeComponent();
            dataGridObavestenja.DataContext = this;
            Obavestenja = new ObservableCollection<Obavestenje>();
            storage = new FileStorageObavestenja();

            List<Obavestenje> obavestenja = storage.GetAll();
            foreach (Obavestenje o in obavestenja)
            {
                if (o.Obrisan == false)
                    Obavestenja.Add(o);
            }
        }

        private void Button_Click_Dodaj(object sender, RoutedEventArgs e)
        {
            clickedDodaj = true;
            FormDodajObavestenje s = new FormDodajObavestenje(-1);
            s.ShowDialog();
        }

        private void Button_Click_Prikazi(object sender, RoutedEventArgs e)
        {
            Obavestenje obavestenje = (Obavestenje)dataGridObavestenja.SelectedItem;
            if (obavestenje != null)
            {
                List<Obavestenje> obavestenja = storage.GetAll();
                var s = new FormPrikazObavestenja();
                foreach (Obavestenje o in obavestenja)
                {
                    if (o.Id == obavestenje.Id)
                    {
                        s.lblDatum.Content = o.Datum.ToShortDateString();
                        s.lblNaslov.Content = o.Naslov;
                        s.tbText.Text = o.Sadrzaj;
                        
                        s.ShowDialog();
                        break;
                    }
                }
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Odaberite obaveštenje za njegov prikaz.",
                                          "Prikaz obaveštenja",
                                          MessageBoxButton.OK,
                                          MessageBoxImage.Information);
            }
        }

        private void Button_Click_Izmeni(object sender, RoutedEventArgs e)
        {
            Obavestenje obavestenje = (Obavestenje)dataGridObavestenja.SelectedItem;
            if (obavestenje != null)
            {
                List<Obavestenje> obavestenja = storage.GetAll();
                FormDodajObavestenje s = new FormDodajObavestenje(obavestenje.Id);

                foreach (Obavestenje o in obavestenja)
                {
                    if (o.Id == obavestenje.Id)
                    {
                        s.dpDatum.SelectedDate = o.Datum;
                        s.dpDatum.IsEnabled = false;
                        s.txtNaslov.Text = o.Naslov;
                        s.txtText.Text = o.Sadrzaj;
                        
                        clickedDodaj = false;
                        s.ShowDialog();
                        break;
                    }
                }
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Odaberite obaveštenje za izmenu njegovih informacija.",
                                          "Izmena obaveštenja",
                                          MessageBoxButton.OK,
                                          MessageBoxImage.Information);
            }
        }

        private void Button_Click_Obrisi(object sender, RoutedEventArgs e)
        {
            Obavestenje obavestenje = (Obavestenje)dataGridObavestenja.SelectedItem;
            if (obavestenje != null)
            {
                MessageBoxResult result = MessageBox.Show("Da li ste sigurni da želite izbrisati ovo obaveštenje?",
                                          "Brisanje obaveštenja",
                                          MessageBoxButton.YesNo,
                                          MessageBoxImage.Exclamation);
                if (result == MessageBoxResult.Yes)
                {
                    Obavestenja.Remove(obavestenje);
                    List<Obavestenje> obavestenja = storage.GetAll();
                    storage.Delete(obavestenje);
                    obavestenje.Obrisan = true;
                    storage.Save(obavestenje);
                }
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Odaberite obaveštenje za brisanje.",
                                          "Brisanje obaveštenja",
                                          MessageBoxButton.OK,
                                          MessageBoxImage.Information);
            }
        }

        private void Button_Click_Pacijenti(object sender, RoutedEventArgs e)
        {
            var s = new FormSekretar();
            s.Show();
            this.Close();
        }

        private void Button_Click_Pregledi(object sender, RoutedEventArgs e)
        {
            var s = new FormPregledi();
            s.Show();
            this.Close();
        }
    }
}
