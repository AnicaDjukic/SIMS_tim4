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
        private bool dozvolaIme = true;
        private bool dozvolaPrezime = true;
        private bool dozvolaJmbg = true;
        public FormNapraviTerminLekar()
        {
            
            InitializeComponent();
            
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            Owner = Application.Current.MainWindow;
            for (int i = 0; i < FormLekar.listaPacijenata.Count; i++)
            {
                textIme.Items.Add(FormLekar.listaPacijenata[i].Ime);
                textPrezime.Items.Add(FormLekar.listaPacijenata[i].Prezime);
                textJmbg.Items.Add(FormLekar.listaPacijenata[i].Jmbg);
            }


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
