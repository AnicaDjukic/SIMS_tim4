using Bolnica.Localization;
using Bolnica.Model.Prostorije;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for FormPodelaProstorije.xaml
    /// </summary>
    public partial class FormPodelaProstorije : Window
    {
        private Renoviranje renoviranje;
        public FormPodelaProstorije(Renoviranje novoRenoviranje)
        {
            InitializeComponent();
            Title = LocalizedStrings.Instance["Podela prostorije"];
            novoRenoviranje.ProstorijeZaSpajanje.Clear();
            renoviranje = novoRenoviranje;
        }

        private void Button_Click_Potvrdi(object sender, RoutedEventArgs e)
        {
            if (txtBrojProstorija.Text != "")
            {
                renoviranje.BrojNovihProstorija = Int32.Parse(txtBrojProstorija.Text);
                Close();
            }
            else
            {
                MessageBox.Show("Morate uneti broj prostorija.");
            }
        }

        private void Button_Click_Odustani(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
