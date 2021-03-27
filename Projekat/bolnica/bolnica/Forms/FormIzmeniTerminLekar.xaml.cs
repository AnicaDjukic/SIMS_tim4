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
    /// Interaction logic for FormIzmeniTerminLekar.xaml
    /// </summary>
    public partial class FormIzmeniTerminLekar : Window
    {
        private DateTime datum;
        private int trajanje;
        private string ime;
        private string prezime;
        private TipOperacije operacija;
        
        private Pregled p1;
        private Operacija op;
        

        public FormIzmeniTerminLekar(Pregled p1)
        {
            this.datum = p1.Datum;
            this.trajanje = p1.Trajanje;
            this.ime = p1.Pacijent.Ime;
            this.prezime = p1.Pacijent.Prezime;
            
            
            this.p1 = p1;
            
            
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            Owner = Application.Current.MainWindow;
            textDatum.Text = datum.ToString();
            textTrajanje.Text = trajanje.ToString();
            textIme.Text = ime;
            textPrezime.Text = prezime;
            checkOperacija.IsChecked = false;
            checkOperacija.IsEnabled = false;
            
            
           




        }

        public FormIzmeniTerminLekar(Operacija op)
        {
            this.datum = op.Datum;
            this.trajanje = op.Trajanje;
            this.ime = op.Pacijent.Ime;
            this.prezime = op.Pacijent.Prezime;
            this.operacija = op.TipOperacije;
            
            this.op = op;
            
            List<TipOperacije> tipOperacije = new List<TipOperacije>();
            tipOperacije.Add(TipOperacije.teška);
            tipOperacije.Add(TipOperacije.laka);
            tipOperacije.Add(TipOperacije.srednja);
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            Owner = Application.Current.MainWindow;
            checkOperacija.IsChecked = true;
            checkOperacija.IsEnabled = false;
            labelTextOperacija.Visibility = Visibility.Visible;
            textOperacija.Visibility = Visibility.Visible;
            textDatum.Text = datum.ToString();
            textTrajanje.Text = trajanje.ToString();
            textIme.Text = ime;
            textPrezime.Text = prezime;
            textOperacija.ItemsSource = tipOperacije;
            textOperacija.Text = operacija.ToString();
           




        }


        public void PotvrdiIzmenu(object sender, RoutedEventArgs e)
        {
            bool ope = false;
            if (CheckFields())
            {
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
                    
                    for (int i = 0; i < FormLekar.listaOperacija.Count; i++)
                    {
                        if (FormLekar.listaOperacija[i].Equals(op))
                        {
                            FormLekar.listaOperacija[i] = oper;

                        }
                    }
                    for (int p = 0; p < FormLekar.dataList.Items.Count; p++)
                    {
                        if (FormLekar.dataList.Items[p].Equals(op))
                        {
                            FormLekar.dataList.Items[p] = oper;
                            FormLekar.data();
                        }
                    }
                    this.Close();
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
                    
                    for (int i = 0; i < FormLekar.listaPregleda.Count; i++)
                    {
                        if (FormLekar.listaPregleda[i].Equals(p1))
                        {
                            Pregled pp = FormLekar.listaPregleda[i];
                            FormLekar.listaPregleda[i] = p12;



                        }
                    }
                    for (int p = 0; p < FormLekar.dataList.Items.Count; p++)
                    {
                        if (FormLekar.dataList.Items[p].Equals(p1))
                        {
                            FormLekar.dataList.Items[p] = p12;
                            FormLekar.data();
                        }

                    }
                    this.Close();

                }
            }
        }

        private void OtkaziIzmenu(object sender, RoutedEventArgs e)
        {
            this.Close();

        }

        private bool CheckFields()
        {
            return true;
        }

        
           
        
    }
}
