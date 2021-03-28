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
using Model.Pacijenti;

namespace Bolnica.Forms
{
    /// <summary>
    /// Interaction logic for FormIzmeniTerminLekar.xaml
    /// </summary>
    public partial class FormIzmeniTerminLekar : Window
    {
        private Lekar lekarTrenutni = new Lekar();
        private FileStoragePacijenti sviPacijenti = new FileStoragePacijenti();
        private FileStorageProstorija sveProstorije = new FileStorageProstorija();
        private FileStoragePregledi sviPregledi = new FileStoragePregledi();
        private List<Pacijent> pacijentiZa = new List<Pacijent>();
        private List<Prostorija> prostorijaZa = new List<Prostorija>();
        private bool dozvolaIme = true;
        private bool dozvolaPrezime = true;
        private bool dozvolaJmbg = true;
        
        private Pregled trenutniPregled = new Pregled();
        private Operacija trenutnaOperacija = new Operacija();
        private Pregled stariPregled= new Pregled();
        private Operacija staraOperacija = new Operacija();
        
        

        public FormIzmeniTerminLekar(Pregled p1,Lekar l1)
        {
            pacijentiZa = sviPacijenti.GetAll();
            prostorijaZa = sveProstorije.GetAllProstorije();
            trenutniPregled = p1;
            lekarTrenutni = l1;
            stariPregled = p1;
            
            
            InitializeComponent();
          /*  WindowStartupLocation = WindowStartupLocation.CenterOwner;
            Owner = Application.Current.MainWindow;*/


            textDatum.Text = trenutniPregled.Datum.ToString();
            textTrajanje.Text = trenutniPregled.Trajanje.ToString();
            for (int i = 0; i < pacijentiZa.Count; i++)
            {
                textIme.Items.Add(pacijentiZa[i].Ime);
                textPrezime.Items.Add(pacijentiZa[i].Prezime);
                textJmbg.Items.Add(pacijentiZa[i].Jmbg);
            }

            textIme.Text = trenutniPregled.Pacijent.Ime;
            textPrezime.Text = trenutniPregled.Pacijent.Prezime;
            textJmbg.Text = trenutniPregled.Pacijent.Jmbg;

            for (int pr = 0; pr < prostorijaZa.Count; pr++)
            {
                if (prostorijaZa[pr].Obrisana == false && prostorijaZa[pr].Zauzeta == false)
                {
                    textProstorija.Items.Add(prostorijaZa[pr].BrojProstorije);
                }
            }
            textProstorija.Text = trenutniPregled.Prostorija.BrojProstorije.ToString();
            checkOperacija.IsChecked = false;
            checkOperacija.IsEnabled = false;
            
            
           




        }

        public FormIzmeniTerminLekar(Operacija op,Lekar l1)
        {
            pacijentiZa = sviPacijenti.GetAll();
            prostorijaZa = sveProstorije.GetAllProstorije();
            trenutnaOperacija = op;
            lekarTrenutni = l1;
            staraOperacija = op;

            List<TipOperacije> tipOperacije = new List<TipOperacije>();
            tipOperacije.Add(TipOperacije.teška);
            tipOperacije.Add(TipOperacije.laka);
            tipOperacije.Add(TipOperacije.srednja);


            InitializeComponent();
            /*WindowStartupLocation = WindowStartupLocation.CenterOwner;
            Owner = Application.Current.MainWindow; */


            checkOperacija.IsChecked = true;
            checkOperacija.IsEnabled = false;
            labelTextOperacija.Visibility = Visibility.Visible;
            textOperacija.Visibility = Visibility.Visible;
            textDatum.Text = trenutnaOperacija.Datum.ToString();
            textTrajanje.Text = trenutnaOperacija.Trajanje.ToString();
            for (int i = 0; i <pacijentiZa.Count; i++)
            {
                textIme.Items.Add(pacijentiZa[i].Ime);
                textPrezime.Items.Add(pacijentiZa[i].Prezime);
                textJmbg.Items.Add(pacijentiZa[i].Jmbg);
            }

            textIme.Text = trenutnaOperacija.Pacijent.Ime;
            textPrezime.Text = trenutnaOperacija.Pacijent.Prezime;
            textJmbg.Text = trenutnaOperacija.Pacijent.Jmbg;

            for (int pr = 0; pr < prostorijaZa.Count; pr++)
            {
                if (prostorijaZa[pr].Obrisana == false && prostorijaZa[pr].Zauzeta == false)
                {
                    textProstorija.Items.Add(prostorijaZa[pr].BrojProstorije);
                }
            }

            textProstorija.Text = trenutnaOperacija.Prostorija.BrojProstorije.ToString();
            textOperacija.ItemsSource = tipOperacije;
            textOperacija.Text = trenutnaOperacija.TipOperacije.ToString();
           




        }


        public void PotvrdiIzmenu(object sender, RoutedEventArgs e)
        {
            bool ope = false;
            if (CheckFields())
            {
                trenutnaOperacija.Datum = DateTime.Parse(textDatum.Text);
                trenutnaOperacija.Trajanje = int.Parse(textTrajanje.Text);
                trenutnaOperacija.Lekar = lekarTrenutni;
                trenutniPregled.Datum = DateTime.Parse(textDatum.Text);
                trenutniPregled.Trajanje = int.Parse(textTrajanje.Text);
                trenutniPregled.Lekar = lekarTrenutni;
                for (int i = 0; i < pacijentiZa.Count; i++)
                {
                    if (pacijentiZa[i].Jmbg == textJmbg.Text)
                    {
                        trenutnaOperacija.Pacijent = pacijentiZa[i];
                        trenutniPregled.Pacijent = pacijentiZa[i];
                        break;

                    }
                }
                for (int pp = 0; pp < prostorijaZa.Count; pp++)
                {
                    if (prostorijaZa[pp].BrojProstorije.ToString().Equals(textOperacija.Text))
                    {
                        trenutnaOperacija.Prostorija = prostorijaZa[pp];
                        trenutniPregled.Prostorija = prostorijaZa[pp];
                        break;
                    }
                }

                if (textOperacija.Text.Equals("teška"))
                {
                    ope = true;
                    trenutnaOperacija.TipOperacije = TipOperacije.teška;
                }
                else if (textOperacija.Text.Equals("laka"))
                {
                    ope = true;
                    trenutnaOperacija.TipOperacije = TipOperacije.laka;
                }
                else if (textOperacija.Text.Equals("srednja"))
                {
                    ope = true;
                    trenutnaOperacija.TipOperacije = TipOperacije.srednja;
                }
               
                if (ope)
                {
                   
                    
                    
                    for (int i = 0; i < FormLekar.listaOperacija.Count; i++)
                    {
                        if (FormLekar.listaOperacija[i].Equals(staraOperacija))
                        {
                            FormLekar.listaOperacija[i] = trenutnaOperacija;

                        }
                    }
                    for (int p = 0; p < FormLekar.dataList.Items.Count; p++)
                    {
                        if (FormLekar.dataList.Items[p].Equals(staraOperacija))
                        {
                            FormLekar.dataList.Items[p] = trenutnaOperacija;
                            FormLekar.data();
                            sviPregledi.Izmeni(trenutnaOperacija);
                        }
                    }
                    this.Close();
                }
                else
                {
                    
                    
                    for (int i = 0; i < FormLekar.listaPregleda.Count; i++)
                    {
                        if (FormLekar.listaPregleda[i].Equals(stariPregled))
                        {
                            Pregled pp = FormLekar.listaPregleda[i];
                            FormLekar.listaPregleda[i] = trenutniPregled;



                        }
                    }
                    for (int p = 0; p < FormLekar.dataList.Items.Count; p++)
                    {
                        if (FormLekar.dataList.Items[p].Equals(stariPregled))
                        {
                            FormLekar.dataList.Items[p] = trenutniPregled;
                            FormLekar.data();
                            sviPregledi.Izmeni(trenutniPregled);
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
                for (int i = 0; i <pacijentiZa.Count; i++)
                {
                    if (textIme.Text.Equals(pacijentiZa[i].Ime))
                    {
                        if (dozvolaJmbg)
                        {
                            textJmbg.Items.Add(pacijentiZa[i].Jmbg);
                        }
                        if (dozvolaPrezime)
                        {
                            textPrezime.Items.Add(pacijentiZa[i].Prezime);
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
                for (int i = 0; i < pacijentiZa.Count; i++)
                {
                    if (textPrezime.Text.Equals(pacijentiZa[i].Prezime))
                    {
                        if (dozvolaJmbg)
                        {
                            textJmbg.Items.Add(pacijentiZa[i].Jmbg);
                        }
                        if (dozvolaIme)
                        {
                            textIme.Items.Add(pacijentiZa[i].Ime);
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
                for (int i = 0; i < pacijentiZa.Count; i++)
                {
                    if (textJmbg.Text.Equals(pacijentiZa[i].Jmbg))
                    {
                        if (dozvolaIme)
                        {
                            textIme.Items.Add(pacijentiZa[i].Ime);
                        }
                        if (dozvolaPrezime)
                        {
                            textPrezime.Items.Add(pacijentiZa[i].Prezime);
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
