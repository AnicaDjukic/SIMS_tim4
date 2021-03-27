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
        
        private DateTime datum;
        private int trajanje;
        private string jmbg;
        private string ime;
        private string prezime;
        private TipOperacije operacija;
        private Pregled p1;
        private Operacija op;
        private bool jeOpe = false;
        public FormNapraviTerminLekar()
        {
            
            InitializeComponent();
            
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            Owner = Application.Current.MainWindow;


        }

       


        public void PotvrdiIzmenu(object sender, RoutedEventArgs e)
        {
            
            if (CheckFields())
            {
                bool ope = false;
                datum = DateTime.Parse(textDatum.Text);
                trajanje = int.Parse(textTrajanje.Text);
                ime = textIme.Text;
                jmbg = textJmbg.Text;
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
                
                if (ope)
                {
                    Operacija oper = new Operacija();
                    oper.Datum = datum;
                    oper.Trajanje = trajanje;
                    oper.Pacijent = new Pacijent();
                    oper.Pacijent.Ime = ime;
                    oper.Pacijent.Jmbg = jmbg;
                    oper.pacijent.Prezime = prezime;
                    oper.Prostorija = new Prostorija();
                    oper.TipOperacije = operacija;
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
                    p12.Pacijent.Jmbg = jmbg;
                    p12.pacijent.Prezime = prezime;
                    p12.Prostorija = new Prostorija();
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

        private void isOperacija(object sender, RoutedEventArgs e)
        {
            if (jeOpe)
            {
                jeOpe = false;
                labelTextOperacija.Visibility = Visibility.Hidden;
                textOperacija.Visibility = Visibility.Hidden;
                List<TipOperacije> tipOperacije = new List<TipOperacije>();
                textOperacija.ItemsSource = tipOperacije;
            }
            else
            {
                jeOpe = true;
                labelTextOperacija.Visibility = Visibility.Visible;
                textOperacija.Visibility = Visibility.Visible;
                List<TipOperacije> tipOperacije = new List<TipOperacije>();
                tipOperacije.Add(TipOperacije.teška);
                tipOperacije.Add(TipOperacije.laka);
                tipOperacije.Add(TipOperacije.srednja);
                textOperacija.ItemsSource = tipOperacije;
            }

        }

       
    }
}
