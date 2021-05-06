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
    /// Interaction logic for FormIzaberiPacijenta.xaml
    /// </summary>
    public partial class FormIzaberiPacijenta : Window
    {
        public static ObservableCollection<Pacijent> Pacijenti { get; set; }
        public static string Jmbg;
        private FileStoragePacijenti storage;
        private TextBox txtPacijent;
        public FormIzaberiPacijenta(TextBox txtPacijent)
        {
            InitializeComponent();
            dataGridPacijenti.DataContext = this;
            Pacijenti = new ObservableCollection<Pacijent>();
            this.txtPacijent = txtPacijent;
            storage = new FileStoragePacijenti();
            List<Pacijent> pacijenti = storage.GetAll();

            foreach (Pacijent p in pacijenti)
                if (!p.Guest)
                    Pacijenti.Add(p);
        }

        private void Zatvori(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void DodajRedovnogPacijenta(object sender, RoutedEventArgs e)
        {
            Pacijent pacijent = (Pacijent)dataGridPacijenti.SelectedItem;
            if (pacijent != null)
            {
                Jmbg = pacijent.Jmbg;
                txtPacijent.Text = pacijent.Ime + " " + pacijent.Prezime;
                FormZakaziHitanTermin.guest = false;
                Close();
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Odaberite pacijenta za dodavanje.",
                                          "Dodavanje pacijenta",
                                          MessageBoxButton.OK,
                                          MessageBoxImage.Information);
            }
        }
    }
}
