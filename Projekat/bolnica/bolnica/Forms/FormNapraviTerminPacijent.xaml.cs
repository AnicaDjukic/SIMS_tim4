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

namespace Bolnica.Forms
{
    /// <summary>
    /// Interaction logic for FormNapraviTerminPacijent.xaml
    /// </summary>
    public partial class FormNapraviTerminPacijent : Window
    {
        public FormNapraviTerminPacijent()
        {
            InitializeComponent();
        }

        private void PotvrdiIzmenu(object sender, RoutedEventArgs e)
        {
            DateTime datum = (DateTime) datumPicker.SelectedDate;
            int dan = datum.Day;
            int mesec = datum.Month;
            int godina = datum.Year;
            int sat = comboSat.SelectedIndex;
            int minut = comboMinut.SelectedIndex * 15;
            DateTime datumPregleda = new DateTime(godina, mesec, dan, sat, minut, 0);

            string imeLekara = textIme.Text;
            string prezimeLekara = textPrezime.Text;

            this.Close();
        }

        private void OtkaziIzmenu(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
