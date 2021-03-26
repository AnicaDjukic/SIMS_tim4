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
using Model.Pregledi;
using Model.Korisnici;
using Model.Prostorije;

namespace Bolnica.Forms
{
    /// <summary>
    /// Interaction logic for FormNapraviTerminLekar.xaml
    /// </summary>
    public partial class FormNapraviTerminLekar : Window
    {
        DateTime datum;
        int trajanje;
        string ime;
        string prezime;
        TipOperacije operacija;
        bool zavrsen;
        Pregled p1;
        Operacija op;
        public FormNapraviTerminLekar()
        {
            
            InitializeComponent();
           

        }

       


        public void PotvrdiIzmenu(object sender, RoutedEventArgs e)
        {
            
            if (CheckFields())
            {
                bool ope = false;
                datum = DateTime.Parse(textDatum.Text);
                trajanje = int.Parse(textTrajanje.Text);
                ime = textIme.Text;
                prezime = textPrezime.Text;
                if (textOperacija.Text.Equals("teška"))
                {
                    ope = true;
                    operacija = TipOperacije.teška;
                }
                else if (textOperacija.Text.Equals("laka"))
                {
                    ope = true;
                    operacija = TipOperacije.laka;
                }
                else if (textOperacija.Text.Equals("srednja"))
                {
                    ope = true;
                    operacija = TipOperacije.srednja;
                }
                zavrsen = (bool)checkZavrsen.IsChecked;
                if (ope)
                {
                    Operacija oper = new Operacija();
                    oper.Datum = datum;
                    oper.Trajanje = trajanje;
                    oper.Pacijent = new Pacijent();
                    oper.Pacijent.Ime = ime;
                    oper.pacijent.Prezime = prezime;
                    oper.Prostorija = new Prostorija();
                    oper.TipOperacije = operacija;
                    oper.Zavrsen = zavrsen;
                    FormLekar.listaOperacija.Add(oper);
                    FormLekar.dataList.Items.Add(oper);
                    FormLekar.data();
                }
                else
                {
                    Pregled p12 = new Pregled();
                    p12.Datum = datum;
                    p12.Trajanje = trajanje;
                    p12.Pacijent = new Pacijent();
                    p12.Pacijent.Ime = ime;
                    p12.pacijent.Prezime = prezime;
                    p12.Prostorija = new Prostorija();
                    p12.Zavrsen = zavrsen;
                    FormLekar.listaPregleda.Add(p12);
                    FormLekar.dataList.Items.Add(p12);
                    FormLekar.data();

                }
                this.Close();

            }
        }

        private void OtkaziIzmenu(object sender, RoutedEventArgs e)
        {
            this.Close();

        }

        public bool CheckFields()
        {
            return true;
        }
    }
}
