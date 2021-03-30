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
        private List<Lekar> lekariTrenutni = new List<Lekar>();
        private FileStoragePacijenti sviPacijenti = new FileStoragePacijenti();
        private FileStorageProstorija sveProstorije = new FileStorageProstorija();
        private FileStoragePregledi sviPregledi = new FileStoragePregledi();
        private List<Pacijent> pacijentiZa = new List<Pacijent>();
        private List<Prostorija> prostorijaZa = new List<Prostorija>();
        private bool dozvolaIme = true;
        private bool dozvolaPrezime = true;
        private bool dozvolaJmbg = true;
        private Lekar ulogovaniLekar = new Lekar();
        private Pregled trenutniPregled = new Pregled();
        private Operacija trenutnaOperacija = new Operacija();
        private Pregled stariPregled= new Pregled();
        private Operacija staraOperacija = new Operacija();
        public string imeB { get; set; }
        public string prezimeB { get; set; }
        public string jmbgB { get; set; }

        public DateTime datumB { get; set; }

        public string vremeB { get; set; }

        public string brojProstorijeB { get; set; }

        public string tipOperacijeB { get; set; }

        public string trajanjeB { get; set; }



        public FormIzmeniTerminLekar(Pregled p1,List<Lekar> l1, Lekar neki)
        {
           
            trenutniPregled = p1;
            lekariTrenutni = l1;
            stariPregled = p1;
            ulogovaniLekar = neki;
           
            
            
            InitializeComponent();
           
            this.DataContext=this;
            /*  WindowStartupLocation = WindowStartupLocation.CenterOwner;
              Owner = Application.Current.MainWindow;*/

            pacijentiZa = sviPacijenti.GetAll();
            prostorijaZa = sveProstorije.GetAllProstorije();

            trajanjeB = trenutniPregled.Trajanje.ToString();
            textTrajanje.IsEnabled = false;

            for (int le = 0; le < lekariTrenutni.Count; le++)
            {
                if (lekariTrenutni[le].Specijalizacija.Naziv != null)
                {
                    textLekar.Items.Add(lekariTrenutni[le].Prezime);
                }
            }

            for (int vre = 1; vre < 25; vre++)
            {
                for (int min = 0; min < 61;)
                {
                    TimeSpan ts = new TimeSpan(vre, min, 0);
                    min = min + 15;
                    textVreme.Items.Add(ts);
                }

            }

            string[] div = trenutniPregled.Datum.ToString().Split(" ");
            string[] d = div[0].Split(".");
            string v = div[1];
            DateTime dat = new DateTime(Int32.Parse(d[2]), Int32.Parse(d[1]), Int32.Parse(d[0]));
            textLekar.Text = trenutniPregled.Lekar.Prezime;

            datumB = dat;
            vremeB = v;
            
            for (int i = 0; i < pacijentiZa.Count; i++)
            {
                textIme.Items.Add(pacijentiZa[i].Ime);
                textPrezime.Items.Add(pacijentiZa[i].Prezime);
                textJmbg.Items.Add(pacijentiZa[i].Jmbg);
            }
            imeB = trenutniPregled.Pacijent.Ime;
            prezimeB = trenutniPregled.Pacijent.Prezime;
            jmbgB = trenutniPregled.Pacijent.Jmbg;
            

            for (int pr = 0; pr < prostorijaZa.Count; pr++)
            {
                if (prostorijaZa[pr].Obrisana == false && prostorijaZa[pr].Zauzeta == false)
                {
                    textProstorija.Items.Add(prostorijaZa[pr].BrojProstorije);
                }
            }
            brojProstorijeB = trenutniPregled.Prostorija.BrojProstorije.ToString();
            checkOperacija.IsChecked = false;
            checkOperacija.IsEnabled = false;
            
            
           




        }

        public FormIzmeniTerminLekar(Operacija op,List<Lekar> l1, Lekar neki)
        {
            trenutnaOperacija = op;
            lekariTrenutni = l1;
            staraOperacija = op;
            ulogovaniLekar = neki;

            List<TipOperacije> tipOperacije = new List<TipOperacije>();
            tipOperacije.Add(TipOperacije.teška);
            tipOperacije.Add(TipOperacije.laka);
            tipOperacije.Add(TipOperacije.srednja);

           

            InitializeComponent();
            this.DataContext = this;


            pacijentiZa = sviPacijenti.GetAll();
            prostorijaZa = sveProstorije.GetAllProstorije();
            /*WindowStartupLocation = WindowStartupLocation.CenterOwner;
            Owner = Application.Current.MainWindow; */


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

            string[] div = trenutnaOperacija.Datum.ToString().Split(" ");
            string[] d = div[0].Split(".");
            string v = div[1];
            DateTime dat = new DateTime(Int32.Parse(d[2]), Int32.Parse(d[1]), Int32.Parse(d[0]));
            textLekar.Text = trenutnaOperacija.Lekar.Prezime;

            datumB = dat;
            vremeB = v;

            checkOperacija.IsChecked = true;
            checkOperacija.IsEnabled = false;
            labelTextOperacija.Visibility = Visibility.Visible;
            textOperacija.Visibility = Visibility.Visible;
            
            trajanjeB = trenutnaOperacija.Trajanje.ToString();
            for (int i = 0; i <pacijentiZa.Count; i++)
            {
                textIme.Items.Add(pacijentiZa[i].Ime);
                textPrezime.Items.Add(pacijentiZa[i].Prezime);
                textJmbg.Items.Add(pacijentiZa[i].Jmbg);
            }

            imeB = trenutnaOperacija.Pacijent.Ime;
            prezimeB = trenutnaOperacija.Pacijent.Prezime;
            jmbgB = trenutnaOperacija.Pacijent.Jmbg;

            for (int pr = 0; pr < prostorijaZa.Count; pr++)
            {
                if (prostorijaZa[pr].Obrisana == false && prostorijaZa[pr].Zauzeta == false)
                {
                    textProstorija.Items.Add(prostorijaZa[pr].BrojProstorije);
                }
            }

            brojProstorijeB = trenutnaOperacija.Prostorija.BrojProstorije.ToString();
            textOperacija.ItemsSource = tipOperacije;
            tipOperacijeB = trenutnaOperacija.TipOperacije.ToString();
           




        }


        public void PotvrdiIzmenu(object sender, RoutedEventArgs e)
        {
            bool ope = false;
            if (CheckFields())
            {
                
                trenutnaOperacija.Datum = DateTime.Parse(textDatum.Text + TimeSpan.Parse(textVreme.Text));
                trenutnaOperacija.Trajanje = int.Parse(textTrajanje.Text);
                
                trenutniPregled.Datum = DateTime.Parse(textDatum.Text + TimeSpan.Parse(textVreme.Text));
                trenutniPregled.Trajanje = int.Parse(textTrajanje.Text);


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
                   
                    
                    
                    for (int i = 0; i < FormLekar.listaOperacija.Count; i++)
                    {
                        if (FormLekar.listaOperacija[i].Equals(staraOperacija))
                        {
                            if (ulogovaniLekar.Mbr.Equals(trenutnaOperacija.Lekar.Mbr)){
                                FormLekar.listaOperacija[i] = trenutnaOperacija; 
                            }
                            else
                            {
                                FormLekar.listaOperacija.RemoveAt(i);
                            }

                        }
                    }
                    for (int p = 0; p < FormLekar.dataList.Items.Count; p++)
                    {
                        if (FormLekar.dataList.Items[p].Equals(staraOperacija))
                        {
                            if (ulogovaniLekar.Mbr.Equals(trenutnaOperacija.Lekar.Mbr))
                            {
                                FormLekar.dataList.Items[p] = trenutnaOperacija;
                                FormLekar.data();
                            }
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
                            if (ulogovaniLekar.Mbr.Equals(trenutniPregled.Lekar.Mbr))
                            {
                                FormLekar.listaPregleda[i] = trenutniPregled;
                            }
                            else
                            {
                                FormLekar.listaOperacija.RemoveAt(i);
                            }


                        }
                    }
                    for (int p = 0; p < FormLekar.dataList.Items.Count; p++)
                    {
                        if (FormLekar.dataList.Items[p].Equals(stariPregled))
                        {
                            if (ulogovaniLekar.Mbr.Equals(trenutniPregled.Lekar.Mbr))
                            {
                                FormLekar.dataList.Items[p] = trenutniPregled;
                                FormLekar.data();
                            }
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
