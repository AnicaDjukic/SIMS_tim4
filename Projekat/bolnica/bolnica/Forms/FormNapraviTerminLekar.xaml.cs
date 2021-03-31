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
using Bolnica.Validation;


namespace Bolnica.Forms
{

    public partial class FormNapraviTerminLekar : Window
    {



        private List<Lekar> lekariTrenutni = new List<Lekar>();
        private Lekar ulogovaniLekar = new Lekar();
        private bool jeOpe = false;
        private bool dozvolaIme = true;
        private bool dozvolaPrezime = true;
        private bool dozvolaJmbg = true;
        private FileStoragePacijenti sviPacijenti = new FileStoragePacijenti();
        private FileStorageProstorija sveProstorije = new FileStorageProstorija();
        private FileStoragePregledi sviPregledi = new FileStoragePregledi();
        private List<Pacijent> pacijentiZa = new List<Pacijent>();
        private List<Prostorija> prostorijaZa = new List<Prostorija>();

        public string imeB { get; set; }
        public string prezimeB { get; set; }
        public string jmbgB { get; set; }

        public DateTime datumB { get; set; }

        public string vremeB { get; set; }

        public string brojProstorijeB { get; set; }

        public string tipOperacijeB { get; set; }

        public string trajanjeB { get; set; }
        public FormNapraviTerminLekar(List<Lekar> l1, Lekar neki)
        {


            ulogovaniLekar = neki;
            lekariTrenutni = l1;
            InitializeComponent();
            datumB = DateTime.Now;
     
            this.DataContext = this;

            pacijentiZa = sviPacijenti.GetAll();
            prostorijaZa = sveProstorije.GetAllProstorije();

            for (int le = 0; le < lekariTrenutni.Count; le++)
            {
                if (lekariTrenutni[le].Specijalizacija.Naziv != null)
                {
                    textLekar.Items.Add(lekariTrenutni[le].Prezime);
                }
            }

            for (int vre = 0; vre < 24; vre++)
            {
                for (int min = 0; min < 59;)
                {
                    TimeSpan ts = new TimeSpan(vre, min, 0);
                    min = min + 15;
                    textVreme.Items.Add(ts);
                }

            }

            if (ulogovaniLekar.Specijalizacija.Naziv == null)
            {
                textOperacija.IsEnabled = false;
            }

            trajanjeB = "30";
            textTrajanje.IsEnabled = false;

           

            /* WindowStartupLocation = WindowStartupLocation.CenterOwner;
             Owner = Application.Current.MainWindow; */
            for (int i = 0; i < pacijentiZa.Count; i++)
            {
                textIme.Items.Add(pacijentiZa[i].Ime);
                textPrezime.Items.Add(pacijentiZa[i].Prezime);
                textJmbg.Items.Add(pacijentiZa[i].Jmbg);

            }
            for (int pr = 0; pr < prostorijaZa.Count; pr++)
            {
                if (prostorijaZa[pr].Obrisana == false && prostorijaZa[pr].Zauzeta == false && prostorijaZa[pr].TipProstorije.Equals(TipProstorije.salaZaPreglede))
                {
                    textProstorija.Items.Add(prostorijaZa[pr].BrojProstorije);
                }
            }


        }




        public void PotvrdiIzmenu(object sender, RoutedEventArgs e)
        {

            if (CheckFields())
            {
                
                bool ope = false;
                Pregled trenutniPregled = new Pregled();
                Operacija trenutnaOperacija = new Operacija();
                trenutnaOperacija.Zavrsen = false;
                trenutnaOperacija.Datum = DateTime.Parse(textDatum.Text + TimeSpan.Parse(textVreme.Text));

                trenutnaOperacija.Trajanje = int.Parse(textTrajanje.Text);
                trenutniPregled.Datum = DateTime.Parse(textDatum.Text + TimeSpan.Parse(textVreme.Text));

                trenutniPregled.Trajanje = int.Parse(textTrajanje.Text);
                trenutniPregled.Zavrsen = false;

                for (int le = 0; le < lekariTrenutni.Count; le++)
                {
                    if (lekariTrenutni[le].Prezime.Equals(textLekar.Text))
                    {
                        trenutnaOperacija.Lekar = lekariTrenutni[le];
                        trenutniPregled.Lekar = lekariTrenutni[le];
                    }
                }


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
                    if (prostorijaZa[pp].BrojProstorije.ToString().Equals(textProstorija.Text))
                    {
                        trenutnaOperacija.Prostorija = prostorijaZa[pp];
                        trenutniPregled.Prostorija = prostorijaZa[pp];
                        break;
                    }
                }
                if (checkOperacija.IsChecked.Equals(true))
                {
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
                }
                if (ope)
                {
                    List<Operacija> zaId = new List<Operacija>();
                    zaId = sviPregledi.GetAllOperacije();
                    int max = 0;
                    for (int i = 0; i < zaId.Count; i++)
                    {
                        if (zaId[i].Id > max)
                            max = zaId[i].Id;
                    }
                    trenutnaOperacija.Id = max + 1;

                    if (ulogovaniLekar.Mbr.Equals(trenutnaOperacija.Lekar.Mbr))
                    {
                        FormLekar.listaOperacija.Add(trenutnaOperacija);
                        FormLekar.dataList.Items.Add(trenutnaOperacija);
                        FormLekar.data();
                    }
                    sviPregledi.Save(trenutnaOperacija);

                }
                else
                {
                    List<Pregled> zaId = new List<Pregled>();
                    zaId = sviPregledi.GetAllPregledi();
                    int max = 0;
                    for (int i = 0; i < zaId.Count; i++)
                    {
                        if (zaId[i].Id > max)
                            max = zaId[i].Id;
                    }
                    trenutniPregled.Id = max + 1;

                    if (ulogovaniLekar.Mbr.Equals(trenutniPregled.Lekar.Mbr))
                    {
                        FormLekar.listaPregleda.Add(trenutniPregled);
                        FormLekar.dataList.Items.Add(trenutniPregled);
                        FormLekar.data();
                    }
                    sviPregledi.Save(trenutniPregled);

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
                textTrajanje.Text = "30";
                textTrajanje.IsEnabled = false;
                textProstorija.Items.Clear();
                for (int pr = 0; pr < prostorijaZa.Count; pr++)
                {
                    if (prostorijaZa[pr].Obrisana == false && prostorijaZa[pr].Zauzeta == false && prostorijaZa[pr].TipProstorije.Equals(TipProstorije.salaZaPreglede))
                    {
                        textProstorija.Items.Add(prostorijaZa[pr].BrojProstorije);
                    }
                }
                jeOpe = false;
                labelTextOperacija.Visibility = Visibility.Hidden;
                textOperacija.Visibility = Visibility.Hidden;
                List<TipOperacije> tipOperacije = new List<TipOperacije>();
                textOperacija.ItemsSource = tipOperacije;
                textOperacija.IsEnabled = false;
            }
            else
            {
                textTrajanje.Text = "";
                textTrajanje.IsEnabled = true;
                textProstorija.Items.Clear();
                for (int pr = 0; pr < prostorijaZa.Count; pr++)
                {
                    if (prostorijaZa[pr].Obrisana == false && prostorijaZa[pr].Zauzeta == false && prostorijaZa[pr].TipProstorije.Equals(TipProstorije.operacionaSala))
                    {
                        textProstorija.Items.Add(prostorijaZa[pr].BrojProstorije);
                    }
                }
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

        public void filterIme()
        {
            for (int filt = 0; filt < pacijentiZa.Count; filt++)
                {
                if (pacijentiZa[filt].Ime.Equals(textIme.Text))
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
                        for (int i = 0; i < pacijentiZa.Count; i++)
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
                    break;
                }

            }


        }

        public void filterPrezime()
        {
            for (int filt = 0; filt < pacijentiZa.Count; filt++)
            {
                if (pacijentiZa[filt].Prezime.Equals(textPrezime.Text))
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
                    break;
                }

            }
        }

        public void filterJMBG()
        {
            for (int filt = 0; filt < pacijentiZa.Count; filt++)
            {
                if (pacijentiZa[filt].Jmbg.Equals(textJmbg.Text))
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
                    break;
                }

            }
        }



        private void OpenComboIme(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Tab)
            {
                filterIme();
            }
            else if (e.Key == Key.Enter)
            {
                textIme.IsDropDownOpen = true;
            }
        }

        private void OpenComboPrezime(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Tab)
            {
                filterPrezime();
            }
            else if (e.Key == Key.Enter)
            {
                textPrezime.IsDropDownOpen = true;

            }
        }

        private void OpenComboJmbg(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Tab)
            {
                filterJMBG();
            }
            else if (e.Key == Key.Enter)
            {
                textJmbg.IsDropDownOpen = true;
            }
        }

        private void OpenComboProstorija(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                textProstorija.IsDropDownOpen = true;
            }
        }

        private void OpenComboOperacija(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                textOperacija.IsDropDownOpen = true;
            }
        }

        private void CheckOnEnter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (jeOpe)
                {
                    textTrajanje.Text = "30";
                    textTrajanje.IsEnabled = false;
                    textProstorija.Items.Clear();
                    for (int pr = 0; pr < prostorijaZa.Count; pr++)
                    {
                        if (prostorijaZa[pr].Obrisana == false && prostorijaZa[pr].Zauzeta == false && prostorijaZa[pr].TipProstorije.Equals(TipProstorije.salaZaPreglede))
                        {
                            textProstorija.Items.Add(prostorijaZa[pr].BrojProstorije);
                        }
                    }
                    jeOpe = false;
                    labelTextOperacija.Visibility = Visibility.Hidden;
                    textOperacija.Visibility = Visibility.Hidden;
                    List<TipOperacije> tipOperacije = new List<TipOperacije>();
                    textOperacija.ItemsSource = tipOperacije;

                }
                else
                {
                    textTrajanje.Text = "";
                    textTrajanje.IsEnabled = true;
                    textProstorija.Items.Clear();
                    for (int pr = 0; pr < prostorijaZa.Count; pr++)
                    {
                        if (prostorijaZa[pr].Obrisana == false && prostorijaZa[pr].Zauzeta == false && prostorijaZa[pr].TipProstorije.Equals(TipProstorije.operacionaSala))
                        {
                            textProstorija.Items.Add(prostorijaZa[pr].BrojProstorije);
                        }
                    }
                    jeOpe = true;
                    labelTextOperacija.Visibility = Visibility.Visible;
                    textOperacija.Visibility = Visibility.Visible;
                    List<TipOperacije> tipOperacije = new List<TipOperacije>();
                    tipOperacije.Add(TipOperacije.teška);
                    tipOperacije.Add(TipOperacije.laka);
                    tipOperacije.Add(TipOperacije.srednja);
                    textOperacija.ItemsSource = tipOperacije;
                    textOperacija.SelectedItem = TipOperacije.laka;
                }
                bool jeste = (bool)checkOperacija.IsChecked;
                if (jeste)
                {
                    jeste = false;
                    checkOperacija.IsChecked = jeste;

                }
                else
                {
                    jeste = true;
                    checkOperacija.IsChecked = jeste;

                }
            }
        }

        

        private void VremeComboOpen(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                textVreme.IsDropDownOpen = true;
            }
        }

        private void LekarComboOpen(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                textLekar.IsDropDownOpen = true;
                
            }
        }

        

       

       
    }
}
