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

        public int BrojProstorija
        {
            get;
            set;
        }
        public FormPodelaProstorije(Renoviranje novoRenoviranje)
        {
            InitializeComponent();
            DataContext = this;
            Title = LocalizedStrings.Instance["Podela prostorije"];
            novoRenoviranje.ProstorijeZaSpajanje.Clear();
            renoviranje = novoRenoviranje;
        }

        private void Button_Click_Potvrdi(object sender, RoutedEventArgs e)
        {
            if (BrojProstorija != 0)
            {
                renoviranje.BrojNovihProstorija = BrojProstorija;
                Close();
            }
        }

        private void Button_Click_Odustani(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
