using Model.Korisnici;
using Model.Pregledi;
using Model.Prostorije;
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
    /// Interaction logic for FormIzmeniTerminPacijent.xaml
    /// </summary>
    public partial class FormIzmeniTerminPacijent : Window
    {
        private Pregled pregled;

        public FormIzmeniTerminPacijent(Pregled p)
        {
            pregled = p;

            DateTime datPre = p.Datum;
            string[] div = datPre.ToString().Split(" ");
            string[] d = div[0].Split(".");
            string[] v = div[1].Split(":");

            string imeL = p.Lekar.Ime;
            string prezimeL = p.Lekar.Prezime;
           
            InitializeComponent();

            DateTime dat = new DateTime(Int32.Parse(d[2]), Int32.Parse(d[1]), Int32.Parse(d[0]));
            datumPicker.SelectedDate = dat;
            comboSat.Text = v[0];
            comboMinut.Text = v[1];
            comboLekar.Text = imeL + " " + prezimeL;
        }

        private void PotvrdiIzmenu(object sender, RoutedEventArgs e)
        {
            DateTime datum = (DateTime)datumPicker.SelectedDate;
            int dan = datum.Day;
            int mesec = datum.Month;
            int godina = datum.Year;
            int sat = comboSat.SelectedIndex;
            int minut = comboMinut.SelectedIndex * 15;
            DateTime datumPregleda = new DateTime(godina, mesec, dan, sat, minut, 0);

            string imeLekara = comboLekar.Text;
            String[] splited = imeLekara.Split(" ");
            string ime = splited[0];
            string prezime = splited[1];

            Lekar l = new Lekar();
            l.Ime = ime;
            l.Prezime = prezime;

            Pregled pre = new Pregled();
            pre.Datum = datumPregleda;
            pre.Lekar = l;
            pre.Trajanje = 15;
            pre.Prostorija = new Prostorija();

            FormPacijent.Pregledi.Remove(pregled);
            FormPacijent.Pregledi.Add(pre);
            this.Close();
        }

        private void OtkaziIzmenu(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
