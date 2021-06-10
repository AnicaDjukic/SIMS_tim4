using Bolnica.Controller;
using Bolnica.Controller.Sekretar;
using Bolnica.DTO;
using Bolnica.DTO.Sekretar;
using Bolnica.Forms;
using Bolnica.Model.Pregledi;
using Bolnica.Repository.Pregledi;
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
        public static ObservableCollection<SastojakDTO> SviAlergeni { get; set; }
        public static ObservableCollection<SastojakDTO> DodatiAlergeni { get; set; }
        private SastojakController sastojakController;

        public FormAlergeniDodaj(TextBox txtJMBG)
        {
            InitializeComponent();
            sastojakController = new SastojakController();
            InicijalizujGUI(txtJMBG);
        }

        private void InicijalizujGUI(TextBox txtJMBG) 
        {
            this.DataContext = this;
            DodatiAlergeni = new ObservableCollection<SastojakDTO>(sastojakController.GetDodatiAlergeni(txtJMBG.Text));
            List<SastojakDTO> dodatiAlergeni = new List<SastojakDTO>(sastojakController.GetDodatiAlergeni(txtJMBG.Text));
            SviAlergeni = new ObservableCollection<SastojakDTO>(sastojakController.GetSviAlergeni(dodatiAlergeni));
        }

        private void Button_Clicked_Dodaj(object sender, RoutedEventArgs e)
        {
            if (dataGridAlergeniSvi.SelectedItems.Count == 0)
                MessageBox.Show("Niste odabrali alergen za dodavanje.", "Dodavanje alergena", MessageBoxButton.OK, MessageBoxImage.Information);
            else
                DodajAlergene();
        }

        private void DodajAlergene() 
        {
            for (int i = 0; i < dataGridAlergeniSvi.SelectedItems.Count; i++)
            {
                SastojakDTO s = (SastojakDTO)dataGridAlergeniSvi.SelectedItems[i];
                SviAlergeni.Remove(s);
                DodatiAlergeni.Add(s);
            }

            btnUkloni.IsEnabled = true;
            if (SviAlergeni.Count == 0)
                btnDodaj.IsEnabled = false;
        }

        private void Button_Clicked_Ukloni(object sender, RoutedEventArgs e)
        {
            if (dataGridAlergeniDodati.SelectedItems.Count == 0)
                MessageBox.Show("Niste odabrali alergen za uklanjanje.", "Uklanjanje alergena", MessageBoxButton.OK, MessageBoxImage.Information);
            else
                UkloniAlergene();
        }

        private void UkloniAlergene() 
        {
            for (int i = 0; i < dataGridAlergeniDodati.SelectedItems.Count; i++)
            {
                SastojakDTO s = (SastojakDTO)dataGridAlergeniDodati.SelectedItems[i];
                DodatiAlergeni.Remove(s);
                SviAlergeni.Add(s);
            }

            btnDodaj.IsEnabled = true;
            if (DodatiAlergeni.Count == 0)
                btnUkloni.IsEnabled = false;
        }

        private void Potvrdi(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Zatvori(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
