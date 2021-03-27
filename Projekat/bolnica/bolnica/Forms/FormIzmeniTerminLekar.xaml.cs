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
        private string jmbg;
        private TipOperacije operacija;
        private bool dozvolaIme = true;
        private bool dozvolaPrezime = true;
        private bool dozvolaJmbg = true;
        
        private Pregled p1;
        private Operacija op;
        

        public FormIzmeniTerminLekar(Pregled p1)
        {
            this.datum = p1.Datum;
            this.trajanje = p1.Trajanje;
            this.ime = p1.Pacijent.Ime;
            this.prezime = p1.Pacijent.Prezime;
            this.jmbg = p1.Pacijent.Jmbg;



            this.p1 = p1;
            
            
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            Owner = Application.Current.MainWindow;
            textDatum.Text = datum.ToString();
            textTrajanje.Text = trajanje.ToString();
            for (int i = 0; i < FormLekar.listaPacijenata.Count; i++)
            {
                textIme.Items.Add(FormLekar.listaPacijenata[i].Ime);
                textPrezime.Items.Add(FormLekar.listaPacijenata[i].Prezime);
                textJmbg.Items.Add(FormLekar.listaPacijenata[i].Jmbg);
            }

            textIme.Text = ime;
            textPrezime.Text = prezime;
            textJmbg.Text = jmbg;

            checkOperacija.IsChecked = false;
            checkOperacija.IsEnabled = false;
            
            
           




        }

        public FormIzmeniTerminLekar(Operacija op)
        {
            this.datum = op.Datum;
            this.trajanje = op.Trajanje;
            this.ime = op.Pacijent.Ime;
            this.jmbg = op.Pacijent.Jmbg;
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
            for (int i = 0; i < FormLekar.listaPacijenata.Count; i++)
            {
                textIme.Items.Add(FormLekar.listaPacijenata[i].Ime);
                textPrezime.Items.Add(FormLekar.listaPacijenata[i].Prezime);
                textJmbg.Items.Add(FormLekar.listaPacijenata[i].Jmbg);
            }

            textIme.Text = ime;
            textPrezime.Text = prezime;
            textJmbg.Text = jmbg;

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
                jmbg = textJmbg.Text;
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
                    oper.Pacijent.Prezime = prezime;
                    oper.Pacijent.Jmbg = jmbg;
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
                    p12.Pacijent.Prezime = prezime;
                    p12.Pacijent.Jmbg = jmbg;
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



        private void filterIme(object sender, EventArgs e)
        {
            if (dozvolaJmbg)
            {
                textJmbg.Items.Clear();
            }
            if (dozvolaPrezime)
            {
                textPrezime.Items.Clear();
            }
            if ((dozvolaJmbg || dozvolaPrezime))
            {
                for (int i = 0; i < FormLekar.listaPacijenata.Count; i++)
                {
                    if (textIme.Text.Equals(FormLekar.listaPacijenata[i].Ime))
                    {
                        if (dozvolaJmbg)
                        {
                            textJmbg.Items.Add(FormLekar.listaPacijenata[i].Jmbg);
                        }
                        if (dozvolaPrezime)
                        {
                            textPrezime.Items.Add(FormLekar.listaPacijenata[i].Prezime);
                        }
                    }

                }
            }
            if (dozvolaJmbg)
            {
                if (textJmbg.Items.Count == 1)
                {
                    textJmbg.SelectedItem = textJmbg.Items[0];
                }
            }
            if (dozvolaPrezime)
            {
                if (textPrezime.Items.Count == 1)
                {
                    textPrezime.SelectedItem = textPrezime.Items[0];
                }
            }
            dozvolaIme = false;
        }

        private void filterPrezime(object sender, EventArgs e)
        {
            if (dozvolaJmbg)
            {
                textJmbg.Items.Clear();
            }
            if (dozvolaIme)
            {
                textIme.Items.Clear();
            }
            if ((dozvolaJmbg || dozvolaIme))
            {
                for (int i = 0; i < FormLekar.listaPacijenata.Count; i++)
                {
                    if (textPrezime.Text.Equals(FormLekar.listaPacijenata[i].Prezime))
                    {
                        if (dozvolaJmbg)
                        {
                            textJmbg.Items.Add(FormLekar.listaPacijenata[i].Jmbg);
                        }
                        if (dozvolaIme)
                        {
                            textIme.Items.Add(FormLekar.listaPacijenata[i].Ime);
                        }
                    }

                }
            }
            if (dozvolaJmbg)
            {
                if (textJmbg.Items.Count == 1)
                {
                    textJmbg.SelectedItem = textJmbg.Items[0];
                }
            }
            if (dozvolaIme)
            {
                if (textIme.Items.Count == 1)
                {
                    textIme.SelectedItem = textIme.Items[0];
                }
            }
            dozvolaPrezime = false;
        }

        private void filterJMBG(object sender, EventArgs e)
        {
            if (dozvolaIme)
            {
                textIme.Items.Clear();
            }
            if (dozvolaPrezime)
            {
                textPrezime.Items.Clear();
            }
            if ((dozvolaIme || dozvolaPrezime))
            {
                for (int i = 0; i < FormLekar.listaPacijenata.Count; i++)
                {
                    if (textJmbg.Text.Equals(FormLekar.listaPacijenata[i].Jmbg))
                    {
                        if (dozvolaIme)
                        {
                            textIme.Items.Add(FormLekar.listaPacijenata[i].Ime);
                        }
                        if (dozvolaPrezime)
                        {
                            textPrezime.Items.Add(FormLekar.listaPacijenata[i].Prezime);
                        }
                    }

                }
            }
            if (dozvolaPrezime)
            {
                if (textPrezime.Items.Count == 1)
                {
                    textPrezime.SelectedItem = textPrezime.Items[0];
                }
            }
            if (dozvolaIme)
            {
                if (textIme.Items.Count == 1)
                {
                    textIme.SelectedItem = textIme.Items[0];
                }
            }
            dozvolaJmbg = false;
        }

        

        private void OpenComboIme(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                textIme.IsDropDownOpen = true;
            }
        }

        private void OpenComboPrezime(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                textPrezime.IsDropDownOpen = true;
            }
        }

        private void OpenComboJmbg(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                textJmbg.IsDropDownOpen = true;
            }
        }
    }
}
