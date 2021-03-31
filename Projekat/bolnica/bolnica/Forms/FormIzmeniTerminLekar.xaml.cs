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
        private int dozvolaIme = 0;
        private int dozvolaPrezime = 0;
        private int dozvolaJmbg = 0;
        private int dozvola = 0;
        private Lekar ulogovaniLekar = new Lekar();
        private Pregled trenutniPregled = new Pregled();
        private Operacija trenutnaOperacija = new Operacija();
        private Pregled stariPregled = new Pregled();
        private Operacija staraOperacija = new Operacija();
        private string zaFilLek = "";
        private DateTime zaFilLekDat = new DateTime();
        public string imeB { get; set; }
        public string prezimeB { get; set; }
        public string jmbgB { get; set; }

        public DateTime datumB { get; set; }

        public string vremeB { get; set; }

        public string brojProstorijeB { get; set; }

        public string tipOperacijeB { get; set; }

        public string trajanjeB { get; set; }



        public FormIzmeniTerminLekar(Pregled p1, List<Lekar> l1, Lekar neki)
        {

            trenutniPregled = p1;
            lekariTrenutni = l1;
            stariPregled = p1;
            ulogovaniLekar = neki;



            InitializeComponent();

            this.DataContext = this;
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

        public FormIzmeniTerminLekar(Operacija op, List<Lekar> l1, Lekar neki)
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
            for (int i = 0; i < pacijentiZa.Count; i++)
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
                            if (ulogovaniLekar.Mbr.Equals(trenutnaOperacija.Lekar.Mbr))
                            {
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
                    if (dozvolaIme == 0)
                    {
                        dozvola++;
                        dozvolaIme = dozvola;
                    }
                    if (dozvolaIme <= 3)
                    {
                        if (dozvolaIme == 1)
                        {
                            textJmbg.Items.Clear();
                            textPrezime.Items.Clear();

                            for (int i = 0; i < pacijentiZa.Count; i++)
                            {
                                if (textIme.Text.Equals(pacijentiZa[i].Ime))
                                {
                                    textJmbg.Items.Add(pacijentiZa[i].Jmbg);
                                    textPrezime.Items.Add(pacijentiZa[i].Prezime);
                                }
                            }
                            if (textPrezime.Items.Count == 1)
                            {
                                textPrezime.SelectedItem = textPrezime.Items[0];
                            }
                            if (textJmbg.Items.Count == 1)
                            {
                                textJmbg.SelectedItem = textJmbg.Items[0];
                            }
                        }
                        else if (dozvolaIme == 2)
                        {
                            if (dozvolaJmbg == 1)
                            {
                                textPrezime.Items.Clear();
                                for (int i = 0; i < pacijentiZa.Count; i++)
                                {
                                    if (textIme.Text.Equals(pacijentiZa[i].Ime) && textJmbg.Text.Equals(pacijentiZa[i].Jmbg))
                                    {
                                        textPrezime.Items.Add(pacijentiZa[i].Prezime);
                                    }
                                }

                                if (textPrezime.Items.Count == 1)
                                {
                                    textPrezime.SelectedItem = textPrezime.Items[0];
                                }
                            }
                            else if (dozvolaPrezime == 1)
                            {
                                textJmbg.Items.Clear();


                                for (int i = 0; i < pacijentiZa.Count; i++)
                                {
                                    if (textIme.Text.Equals(pacijentiZa[i].Ime) && textPrezime.Text.Equals(pacijentiZa[i].Prezime))
                                    {
                                        textJmbg.Items.Add(pacijentiZa[i].Jmbg);

                                    }
                                }
                                if (textJmbg.Items.Count == 1)
                                {
                                    textJmbg.SelectedItem = textJmbg.Items[0];
                                }
                            }
                        }

                    }
                }


            }
        }

        public void filterPrezime()
        {
            for (int filt = 0; filt < pacijentiZa.Count; filt++)
            {
                if (pacijentiZa[filt].Prezime.Equals(textPrezime.Text))
                {
                    if (dozvolaPrezime == 0)
                    {
                        dozvola++;
                        dozvolaPrezime = dozvola;
                    }
                    if (dozvolaPrezime <= 3)
                    {
                        if (dozvolaPrezime == 1)
                        {
                            textIme.Items.Clear();
                            textJmbg.Items.Clear();

                            for (int i = 0; i < pacijentiZa.Count; i++)
                            {
                                if (textPrezime.Text.Equals(pacijentiZa[i].Prezime))
                                {
                                    textIme.Items.Add(pacijentiZa[i].Ime);
                                    textJmbg.Items.Add(pacijentiZa[i].Jmbg);
                                }
                            }
                            if (textJmbg.Items.Count == 1)
                            {
                                textJmbg.SelectedItem = textJmbg.Items[0];
                            }
                            if (textIme.Items.Count == 1)
                            {
                                textIme.SelectedItem = textIme.Items[0];
                            }
                        }
                        else if (dozvolaPrezime == 2)
                        {
                            if (dozvolaIme == 1)
                            {
                                textJmbg.Items.Clear();
                                for (int i = 0; i < pacijentiZa.Count; i++)
                                {
                                    if (textPrezime.Text.Equals(pacijentiZa[i].Prezime) && textIme.Text.Equals(pacijentiZa[i].Ime))
                                    {
                                        textJmbg.Items.Add(pacijentiZa[i].Jmbg);
                                    }
                                }

                                if (textJmbg.Items.Count == 1)
                                {
                                    textJmbg.SelectedItem = textJmbg.Items[0];
                                }
                            }
                            else if (dozvolaJmbg == 1)
                            {
                                textIme.Items.Clear();


                                for (int i = 0; i < pacijentiZa.Count; i++)
                                {
                                    if (textPrezime.Text.Equals(pacijentiZa[i].Prezime) && textJmbg.Text.Equals(pacijentiZa[i].Jmbg))
                                    {
                                        textIme.Items.Add(pacijentiZa[i].Ime);
                                    }
                                }
                                if (textIme.Items.Count == 1)
                                {
                                    textIme.SelectedItem = textIme.Items[0];
                                }
                            }
                        }

                    }
                }
            }
        }

        public void filterJMBG()
        {

            for (int filt = 0; filt < pacijentiZa.Count; filt++)
            {
                if (pacijentiZa[filt].Jmbg.Equals(textJmbg.Text))
                {
                    if (dozvolaJmbg == 0)
                    {
                        dozvola++;
                        dozvolaJmbg = dozvola;
                    }
                    if (dozvolaJmbg <= 3)
                    {
                        if (dozvolaJmbg == 1)
                        {
                            textIme.Items.Clear();
                            textPrezime.Items.Clear();

                            for (int i = 0; i < pacijentiZa.Count; i++)
                            {
                                if (textJmbg.Text.Equals(pacijentiZa[i].Jmbg))
                                {
                                    textIme.Items.Add(pacijentiZa[i].Ime);
                                    textPrezime.Items.Add(pacijentiZa[i].Prezime);
                                }
                            }
                            if (textPrezime.Items.Count == 1)
                            {
                                textPrezime.SelectedItem = textPrezime.Items[0];
                            }
                            if (textIme.Items.Count == 1)
                            {
                                textIme.SelectedItem = textIme.Items[0];
                            }
                        }
                        else if (dozvolaJmbg == 2)
                        {
                            if (dozvolaIme == 1)
                            {
                                textPrezime.Items.Clear();
                                for (int i = 0; i < pacijentiZa.Count; i++)
                                {
                                    if (textJmbg.Text.Equals(pacijentiZa[i].Jmbg) && textIme.Text.Equals(pacijentiZa[i].Ime))
                                    {
                                        textPrezime.Items.Add(pacijentiZa[i].Prezime);
                                    }
                                }

                                if (textPrezime.Items.Count == 1)
                                {
                                    textPrezime.SelectedItem = textPrezime.Items[0];
                                }
                            }
                            else if (dozvolaPrezime == 1)
                            {
                                textIme.Items.Clear();


                                for (int i = 0; i < pacijentiZa.Count; i++)
                                {
                                    if (textJmbg.Text.Equals(pacijentiZa[i].Jmbg) && textPrezime.Text.Equals(pacijentiZa[i].Prezime))
                                    {
                                        textIme.Items.Add(pacijentiZa[i].Ime);

                                    }
                                }
                                if (textIme.Items.Count == 1)
                                {
                                    textIme.SelectedItem = textIme.Items[0];
                                }
                            }
                        }

                    }
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
            if (e.Key == Key.Tab)
            {
                if (zaFilLek != textLekar.Text)
                {
                    filterLekar();
                    zaFilLek = textLekar.Text;

                }
            }

            else if (e.Key == Key.Enter)
            {
                textLekar.IsDropDownOpen = true;

            }
        }
        public void filterLekar()
        {
            textVreme.Items.Clear();
            for (int vre = 0; vre < 24; vre++)
            {
                for (int min = 0; min < 59;)
                {
                    TimeSpan ts = new TimeSpan(vre, min, 0);
                    min = min + 15;
                    textVreme.Items.Add(ts);
                }

            }


            for (int lek = 0; lek < lekariTrenutni.Count; lek++)
            {
                if (lekariTrenutni[lek].Prezime.Equals(textLekar.Text) && lekariTrenutni[lek].Specijalizacija.Naziv != null)
                {

                    List<TimeSpan> zauzetiTermini = new List<TimeSpan>();
                    List<Pregled> preglediLekara = sviPregledi.GetAllPregledi();
                    List<Operacija> operacijeLekara = sviPregledi.GetAllOperacije();
                    for (int da = 0; da < preglediLekara.Count; da++)
                    {
                        if (!preglediLekara[da].Lekar.Prezime.Equals(textLekar.Text))
                        {
                            preglediLekara.RemoveAt(da);
                            da = da - 1;
                        }
                    }
                    for (int ad = 0; ad < operacijeLekara.Count; ad++)
                    {
                        if (!operacijeLekara[ad].Lekar.Prezime.Equals(textLekar.Text))
                        {
                            operacijeLekara.RemoveAt(ad);
                            ad = ad - 1;
                        }
                    }
                    for (int pre = 0; pre < preglediLekara.Count; pre++)
                    {
                        if (preglediLekara[pre].Datum.Date.Equals(textDatum.SelectedDate.Value.Date) && preglediLekara[pre].Id!=stariPregled.Id)
                        {
                            string[] div = preglediLekara[pre].Datum.ToString().Split(" ");
                            string v = div[1];
                            TimeSpan pocetni = TimeSpan.Parse(v);
                            for (int jos = 0; jos <= preglediLekara[pre].Trajanje; jos++)
                            {
                                TimeSpan dodatni = new TimeSpan(0, jos, 0);
                                zauzetiTermini.Add(pocetni + dodatni);
                            }
                        }
                    }
                    for (int ope = 0; ope < operacijeLekara.Count; ope++)
                    {
                        if (operacijeLekara[ope].Datum.Date.Equals(textDatum.SelectedDate.Value.Date) && operacijeLekara[ope].Id!=staraOperacija.Id)
                        {
                            string[] div = operacijeLekara[ope].Datum.ToString().Split(" ");
                            string v = div[1];
                            TimeSpan pocetni = TimeSpan.Parse(v);
                            for (int jos = 0; jos <= operacijeLekara[ope].Trajanje; jos++)
                            {
                                TimeSpan dodatni = new TimeSpan(0, jos, 0);
                                zauzetiTermini.Add(pocetni + dodatni);
                            }
                        }



                    }

                    for (int tm = 0; tm < zauzetiTermini.Count; tm++)
                    {
                        textVreme.Items.Remove(zauzetiTermini[tm]);
                    }



                    break;
                }



            }
        }

        private void DatumDateKey(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                if (zaFilLekDat != textDatum.SelectedDate)
                {
                    filterLekar();
                    zaFilLekDat = (DateTime)textDatum.SelectedDate;
                }
            }
        }
    }
}
