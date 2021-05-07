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
using Bolnica.Model.Pregledi;
using Bolnica.Model.Korisnici;

namespace Bolnica.Forms
{

    public partial class FormNapraviTerminLekar : Window
    {
        private List<Lekar> lekariTrenutni = new List<Lekar>();
        private Lekar ulogovaniLekar = new Lekar();
        private bool jeOpe = false;
        private FileStoragePacijenti sviPacijenti = new FileStoragePacijenti();
        private FileStorageProstorija sveProstorije = new FileStorageProstorija();
        private FileStoragePregledi sviPregledi = new FileStoragePregledi();
        private List<Pacijent> pacijentiZa = new List<Pacijent>();
        private List<Prostorija> prostorijaZa = new List<Prostorija>();
        private FileStorageLekar sviLekari = new FileStorageLekar();
        private string zaFilLek = "";
        private DateTime zaFilLekDat = new DateTime();
        public List<String> specijalizacija { get; set; }
        private List<Pregled> pregledi;
        private List<Operacija> operacije;
        
        public List<String> specijalizacije{get; set;}

        public string prezimeB { get; set; }

        public string lekarB { get; set; }

        public DateTime datumB { get; set; }

        public string vremeB { get; set; }

        public string brojProstorijeB { get; set; }

        public string tipOperacijeB { get; set; }

        public string trajanjeB { get; set; }

        public FormNapraviTerminLekar(Lekar neki)
        {
           
            

            specijalizacije = new List<String>();
            ulogovaniLekar = neki;
            lekariTrenutni = sviLekari.GetAll();
            InitializeComponent();

            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            Owner = Application.Current.MainWindow;
            datumB = DateTime.Now;

            this.DataContext = this;

            pacijentiZa = sviPacijenti.GetAll();
            pregledi = sviPregledi.GetAllPregledi();
            operacije = sviPregledi.GetAllOperacije();
            prostorijaZa = sveProstorije.GetAllProstorije();
            

            for (int le = 0; le < lekariTrenutni.Count; le++)
            {
                if (lekariTrenutni[le].Specijalizacija.OblastMedicine != null)
                {
                    string s = lekariTrenutni[le].Prezime + ' ' + lekariTrenutni[le].Ime + ' ' + lekariTrenutni[le].Jmbg;
                    textLekar.Items.Add(s);
                }
            }

            for (int le = 0; le < lekariTrenutni.Count; le++)
            {
                if (lekariTrenutni[le].Specijalizacija.OblastMedicine != null && !specijalizacije.Contains(lekariTrenutni[le].Specijalizacija.OblastMedicine))
                {
                    specijalizacije.Add(lekariTrenutni[le].Specijalizacija.OblastMedicine);
                    textSpecijalizacija.Items.Add(lekariTrenutni[le].Specijalizacija.OblastMedicine);
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

            if (ulogovaniLekar.Specijalizacija.OblastMedicine.Equals("Opsta"))
            {
                checkOperacija.IsEnabled = false;
            }

            trajanjeB = "30";
            textTrajanje.IsEnabled = false;



            /* WindowStartupLocation = WindowStartupLocation.CenterOwner;
             Owner = Application.Current.MainWindow; */
            for (int i = 0; i < pacijentiZa.Count; i++)
            {
                if (pacijentiZa[i].Obrisan == false)
                {
                    string s;
                    s = pacijentiZa[i].Prezime + ' ' + pacijentiZa[i].Ime + ' ' + pacijentiZa[i].Jmbg;
                    
                    textPrezime.Items.Add(s);
                    
                }
            }
            for (int pr = 0; pr < prostorijaZa.Count; pr++)
            {
                if (prostorijaZa[pr].Obrisana == false && prostorijaZa[pr].Zauzeta == false && prostorijaZa[pr].TipProstorije.Equals(TipProstorije.salaZaPreglede))
                {
                    textProstorija.Items.Add(prostorijaZa[pr].BrojProstorije);
                }
            }


        }

        public FormNapraviTerminLekar(Lekar neki,Pacijent pacij)
        {
           
           
            specijalizacije = new List<String>();
            ulogovaniLekar = neki;
            lekariTrenutni = sviLekari.GetAll();
            InitializeComponent();

            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            Owner = Application.Current.MainWindow;
            datumB = DateTime.Now;

            this.DataContext = this;

            pacijentiZa = sviPacijenti.GetAll();
            pregledi = sviPregledi.GetAllPregledi();
            operacije = sviPregledi.GetAllOperacije();
            prostorijaZa = sveProstorije.GetAllProstorije();
           

            for (int le = 0; le < lekariTrenutni.Count; le++)
            {
                if (lekariTrenutni[le].Specijalizacija.OblastMedicine != null)
                {
                    string s = lekariTrenutni[le].Prezime + ' ' + lekariTrenutni[le].Ime + ' ' + lekariTrenutni[le].Jmbg;
                    textLekar.Items.Add(s);
                }
            }

            for (int le = 0; le < lekariTrenutni.Count; le++)
            {
                if (lekariTrenutni[le].Specijalizacija.OblastMedicine != null && !specijalizacije.Contains(lekariTrenutni[le].Specijalizacija.OblastMedicine))
                {
                    specijalizacije.Add(lekariTrenutni[le].Specijalizacija.OblastMedicine);
                    textSpecijalizacija.Items.Add(lekariTrenutni[le].Specijalizacija.OblastMedicine);
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

            if (ulogovaniLekar.Specijalizacija.OblastMedicine.Equals("Opsta"))
            {
                checkOperacija.IsEnabled = false;
            }

            trajanjeB = "30";
            textTrajanje.IsEnabled = false;



            /* WindowStartupLocation = WindowStartupLocation.CenterOwner;
             Owner = Application.Current.MainWindow; */
            for (int i = 0; i < pacijentiZa.Count; i++)
            {
                string t;
                t = pacij.Prezime + ' ' + pacij.Ime + ' ' + pacij.Jmbg;
                if (pacijentiZa[i].Obrisan == false)
                {
                    string s;
                    s = pacijentiZa[i].Prezime + ' ' + pacijentiZa[i].Ime + ' ' + pacijentiZa[i].Jmbg;

                    textPrezime.Items.Add(s);
                    if (s.Equals(t))
                    {
                        prezimeB = s;

                    }
                }
            }
            for (int pr = 0; pr < prostorijaZa.Count; pr++)
            {
                if (prostorijaZa[pr].Obrisana == false && prostorijaZa[pr].Zauzeta == false && prostorijaZa[pr].TipProstorije.Equals(TipProstorije.salaZaPreglede))
                {
                    textProstorija.Items.Add(prostorijaZa[pr].BrojProstorije);
                }
            }
           
          
            textPrezime.IsEnabled = false;
            


        }

        public void PotvrdiIzmenu()
        {
            if (!PostojiLekar())
            {
                MessageBox.Show("Ne postoji lekar");
                return;
            }
            if (!LekarSlobodanUToVreme())
            {
                MessageBox.Show("Lekar nije slobodan u to vreme");
                return;
            }
            if (!PacijentSlobodanUToVreme())
            {
                MessageBox.Show("Pacijent nije slobodan u to vreme");
                return;
            }
            if (!PostojiProstorija())
            {
                MessageBox.Show("Prostorija ne postoji");
                return;
            }
            if (!ProstorijaSlobodna())
            {
                MessageBox.Show("Prostorija nije slobodna");
                return;
            }

            if (CheckFields())
            {

                bool ope = false;
                PrikazPregleda trenutniPregled = new PrikazPregleda();
                PrikazOperacije trenutnaOperacija = new PrikazOperacije();
                trenutnaOperacija.Zavrsen = false;
                trenutnaOperacija.Datum = DateTime.Parse(textDatum.Text + TimeSpan.Parse(textVreme.Text));

                trenutnaOperacija.Trajanje = int.Parse(textTrajanje.Text);
                trenutniPregled.Datum = DateTime.Parse(textDatum.Text + TimeSpan.Parse(textVreme.Text));

                trenutniPregled.Trajanje = int.Parse(textTrajanje.Text);
                trenutniPregled.Zavrsen = false;

                for (int le = 0; le < lekariTrenutni.Count; le++)
                {
                    string s = lekariTrenutni[le].Prezime + ' ' + lekariTrenutni[le].Ime + ' ' + lekariTrenutni[le].Jmbg;
                    if (s.Equals(textLekar.Text))
                    {
                        trenutnaOperacija.Lekar = lekariTrenutni[le];
                        trenutniPregled.Lekar = lekariTrenutni[le];
                    }
                }


                for (int i = 0; i < pacijentiZa.Count; i++)
                {
                    string s;
                    s = pacijentiZa[i].Prezime + ' ' + pacijentiZa[i].Ime + ' ' + pacijentiZa[i].Jmbg;


                    if (s.Equals(textPrezime.SelectedItem))
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
                    if (textOperacija.Text.Equals("prvaKat"))
                    {
                        ope = true;
                        trenutnaOperacija.TipOperacije = TipOperacije.prvaKat;
                    }
                    else if (textOperacija.Text.Equals("drugaKat"))
                    {
                        ope = true;
                        trenutnaOperacija.TipOperacije = TipOperacije.drugaKat;
                    }
                    else if (textOperacija.Text.Equals("trecaKat"))
                    {
                        ope = true;
                        trenutnaOperacija.TipOperacije = TipOperacije.trecaKat;
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
                    Operacija o = new Operacija();
                    trenutnaOperacija.Anamneza.Id = -1;
                    trenutnaOperacija.Hitan = (bool)checkHitan.IsChecked;
                    o.Id = trenutnaOperacija.Id;
                    o.Hitan = (bool)checkHitan.IsChecked;
                    o.Lekar = trenutnaOperacija.Lekar;
                    o.Pacijent = trenutnaOperacija.Pacijent;
                    o.TipOperacije = trenutnaOperacija.TipOperacije;
                    o.Trajanje = trenutnaOperacija.Trajanje;
                    o.Zavrsen = trenutnaOperacija.Zavrsen;
                    o.Anamneza.Id = -1;
                    o.Prostorija = trenutnaOperacija.Prostorija;
                    o.Datum = trenutnaOperacija.Datum;
                    if (ulogovaniLekar.Mbr.Equals(trenutnaOperacija.Lekar.Mbr))
                    {

                        FormLekar.listaOperacija.Add(o);
                        FormLekar.dataList.Items.Add(trenutnaOperacija);
                        FormLekar.data();
                    }
                    sviPregledi.Save(o);

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
                    Pregled o = new Pregled();
                    trenutniPregled.Anamneza.Id = -1;
                    trenutniPregled.Hitan = false;
                    o.Id = trenutniPregled.Id;
                    o.Hitan = false;
                    o.Lekar = trenutniPregled.Lekar;
                    o.Pacijent = trenutniPregled.Pacijent;
                    o.Trajanje = trenutniPregled.Trajanje;
                    o.Zavrsen = trenutniPregled.Zavrsen;
                    o.Anamneza.Id = -1;
                    o.Prostorija = trenutniPregled.Prostorija;
                    o.Datum = trenutniPregled.Datum;
                    if (ulogovaniLekar.Mbr.Equals(trenutniPregled.Lekar.Mbr))
                    {

                        FormLekar.listaPregleda.Add(o);
                        FormLekar.dataList.Items.Add(trenutniPregled);
                        FormLekar.data();
                    }
                    sviPregledi.Save(o);

                }
                this.Close();

            }
        }

        public void PotvrdiIzmenu(object sender, RoutedEventArgs e)
        {
            PotvrdiIzmenu();
        }

        private bool PostojiLekar()
        {
            
            FileStorageLekar ProveraL = new FileStorageLekar();
            List<Lekar> lekari = new List<Lekar>();
            lekari = ProveraL.GetAll();
            for(int i = 0; i < lekari.Count; i++)
            {
                string ss = lekari[i].Prezime + ' ' + lekari[i].Ime + ' ' + lekari[i].Jmbg;
                if (lekari[i].Specijalizacija.OblastMedicine.Equals(textSpecijalizacija.Text) && ss.Equals(textLekar.Text))
                {
                    return true;
                }
                
            }
            return false;
            
        }

        private bool LekarSlobodanUToVreme()
        {
            FileStorageLekar ProveraL = new FileStorageLekar();
            List<Lekar> lekari = new List<Lekar>();
            lekari = ProveraL.GetAll();
            List<TimeSpan> zauzetiTermini = new List<TimeSpan>();
            List<Pregled> preglediLekara1 = pregledi;
            List<Operacija> operacijeLekara1 = operacije;
            for (int lek = 0; lek < lekari.Count; lek++)
            {
                string ss = lekari[lek].Prezime + ' ' + lekari[lek].Ime + ' ' + lekari[lek].Jmbg;
                if (ss.Equals(textLekar.Text) && lekari[lek].Specijalizacija.OblastMedicine != null)
                {
                   
                    
                    List<Lekar> lekar = new List<Lekar>();
                    lekar = sviLekari.GetAll();
                    string jmbgLekar = "";
                    for (int l = 0; l < lekar.Count; l++)
                    {
                        string pp = lekar[l].Prezime + ' ' + lekar[l].Ime + ' ' + lekar[l].Jmbg;
                        if (pp.Equals(textLekar.Text))
                        {
                            jmbgLekar = lekar[l].Jmbg;
                            break;
                        }
                    }
                    for (int da = 0; da < preglediLekara1.Count; da++)
                    {
                        if (!preglediLekara1[da].Lekar.Jmbg.Equals(jmbgLekar))
                        {
                            preglediLekara1.RemoveAt(da);
                            da = da - 1;
                        }
                    }
                    for (int ad = 0; ad < operacijeLekara1.Count; ad++)
                    {
                        if (!operacijeLekara1[ad].Lekar.Jmbg.Equals(jmbgLekar))
                        {
                            operacijeLekara1.RemoveAt(ad);
                            ad = ad - 1;
                        }
                    }
                    for (int pre = 0; pre < preglediLekara1.Count; pre++)
                    {
                        if (preglediLekara1[pre].Datum.Date.Equals(textDatum.SelectedDate.Value.Date))
                        {
                            string[] div = preglediLekara1[pre].Datum.ToString().Split(" ");
                            string v = div[1];
                            TimeSpan pocetni = TimeSpan.Parse(v);
                            for (int jos = 0; jos <= preglediLekara1[pre].Trajanje; jos++)
                            {
                                TimeSpan dodatni = new TimeSpan(0, jos, 0);
                                zauzetiTermini.Add(pocetni + dodatni);
                            }
                        }
                    }
                    for (int ope = 0; ope < operacijeLekara1.Count; ope++)
                    {
                        if (operacijeLekara1[ope].Datum.Date.Equals(textDatum.SelectedDate.Value.Date))
                        {
                            string[] div = operacijeLekara1[ope].Datum.ToString().Split(" ");
                            string v = div[1];
                            TimeSpan pocetni = TimeSpan.Parse(v);
                            for (int jos = 0; jos <= operacijeLekara1[ope].Trajanje; jos++)
                            {
                                TimeSpan dodatni = new TimeSpan(0, jos, 0);
                                zauzetiTermini.Add(pocetni + dodatni);
                            }
                        }



                    }

                }
            }
            pregledi = sviPregledi.GetAllPregledi();
            operacije = sviPregledi.GetAllOperacije();
            for (int mek = 0; mek < int.Parse(textTrajanje.Text); mek++)
            {
                TimeSpan jes = new TimeSpan(0, mek, 0);
                if (zauzetiTermini.Contains(TimeSpan.Parse(textVreme.Text)+jes))
                {
                    return false;
                }

            }
            return true;
            
        }

        private bool PacijentSlobodanUToVreme()
        {
            FileStoragePacijenti ProveraP = new FileStoragePacijenti();
            List<Pacijent> pacijenti = pacijentiZa;
            List<TimeSpan> zauzetiTermini = new List<TimeSpan>();
            List<Pregled> preglediPacijenta1 = pregledi;
            List<Operacija> operacijePacijenta1 = operacije;
            for (int lek = 0; lek < pacijenti.Count; lek++)
            {
                string s;
                s = pacijentiZa[lek].Prezime + ' ' + pacijentiZa[lek].Ime + ' ' + pacijentiZa[lek].Jmbg;

              
                if (s.Equals(textPrezime.SelectedItem) )
                {


                    List<Pacijent> pacijent = new List<Pacijent>();
                    pacijent = sviPacijenti.GetAll();
                    string jmbgPacijent = "";
                    for (int l = 0; l < pacijent.Count; l++)
                    {
                        string d;
                        d = pacijent[l].Prezime + ' ' + pacijent[l].Ime + ' ' + pacijent[l].Jmbg;

                        if (d.Equals(textPrezime.SelectedItem))
                        {
                            jmbgPacijent = pacijent[l].Jmbg;
                            break;
                        }
                    }
                    for (int da = 0; da < preglediPacijenta1.Count; da++)
                    {
                        if (!preglediPacijenta1[da].Pacijent.Jmbg.Equals(jmbgPacijent))
                        {
                            preglediPacijenta1.RemoveAt(da);
                            da = da - 1;
                        }
                    }
                    for (int ad = 0; ad < operacijePacijenta1.Count; ad++)
                    {
                        if (!operacijePacijenta1[ad].Pacijent.Jmbg.Equals(jmbgPacijent))
                        {
                            operacijePacijenta1.RemoveAt(ad);
                            ad = ad - 1;
                        }
                    }
                    for (int pre = 0; pre < preglediPacijenta1.Count; pre++)
                    {
                        if (preglediPacijenta1[pre].Datum.Date.Equals(textDatum.SelectedDate.Value.Date))
                        {
                            string[] div = preglediPacijenta1[pre].Datum.ToString().Split(" ");
                            string v = div[1];
                            TimeSpan pocetni = TimeSpan.Parse(v);
                            for (int jos = 0; jos <= preglediPacijenta1[pre].Trajanje; jos++)
                            {
                                TimeSpan dodatni = new TimeSpan(0, jos, 0);
                                zauzetiTermini.Add(pocetni + dodatni);
                            }
                        }
                    }
                    for (int ope = 0; ope < operacijePacijenta1.Count; ope++)
                    {
                        if (operacijePacijenta1[ope].Datum.Date.Equals(textDatum.SelectedDate.Value.Date))
                        {
                            string[] div = operacijePacijenta1[ope].Datum.ToString().Split(" ");
                            string v = div[1];
                            TimeSpan pocetni = TimeSpan.Parse(v);
                            for (int jos = 0; jos <= operacijePacijenta1[ope].Trajanje; jos++)
                            {
                                TimeSpan dodatni = new TimeSpan(0, jos, 0);
                                zauzetiTermini.Add(pocetni + dodatni);
                            }
                        }



                    }

                }
               
            }
            pregledi = sviPregledi.GetAllPregledi();
            operacije = sviPregledi.GetAllOperacije();
            for (int mek = 0; mek < int.Parse(textTrajanje.Text); mek++)
            {
                TimeSpan jes = new TimeSpan(0, mek, 0);
                if (zauzetiTermini.Contains(TimeSpan.Parse(textVreme.Text)+jes))
                {
                    return false;
                }
            }
             return true;
            

        }

        private bool PostojiProstorija()
        {
           
            for(int i = 0; i < prostorijaZa.Count; i++)
            {

                if (prostorijaZa[i].BrojProstorije.ToString().Equals(textProstorija.Text)&&!prostorijaZa[i].Obrisana)
                {
                    return true;
                }
                
            }
            return false;
        }

        private bool ProstorijaSlobodna()
        {
            for (int i = 0; i < prostorijaZa.Count; i++)
            {

                if (prostorijaZa[i].BrojProstorije.ToString().Equals(textProstorija.Text) && !prostorijaZa[i].Obrisana)
                {
                    if (prostorijaZa[i].Zauzeta)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }

            }
            return false;
            
        }
            
        public void OtkaziIzmenu()
        {
            this.Close();
        }

        private void OtkaziIzmenu(object sender, RoutedEventArgs e)
        {
            OtkaziIzmenu();

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
                checkHitan.Visibility = Visibility.Hidden;
                textHitna.Visibility = Visibility.Hidden;
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
                checkHitan.Visibility = Visibility.Visible;
                textHitna.Visibility = Visibility.Visible;
                labelTextOperacija.Visibility = Visibility.Visible;
                textOperacija.Visibility = Visibility.Visible;

                List<TipOperacije> tipOperacije = new List<TipOperacije>();
                tipOperacije.Add(TipOperacije.prvaKat);
                tipOperacije.Add(TipOperacije.drugaKat);
                tipOperacije.Add(TipOperacije.trecaKat);
            
                textOperacija.ItemsSource = tipOperacije;
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
                string h = lekariTrenutni[lek].Prezime + ' ' + lekariTrenutni[lek].Ime + ' ' + lekariTrenutni[lek].Jmbg;
                if (h.Equals(textLekar.Text) && lekariTrenutni[lek].Specijalizacija.OblastMedicine != null)
                {

                    List<TimeSpan> zauzetiTermini = new List<TimeSpan>();
                    List<Pregled> preglediLekara = sviPregledi.GetAllPregledi();
                    List<Operacija> operacijeLekara = sviPregledi.GetAllOperacije();
                    List<Lekar> lekar = new List<Lekar>();
                    lekar = lekariTrenutni;
                    string jmbgLekar = "";
                    for (int l = 0; l < lekar.Count; l++)
                    {
                        string hh = lekar[l].Prezime + ' ' + lekar[l].Ime + ' ' + lekar[l].Jmbg;
                        if (hh.Equals(textLekar.Text))
                        {
                            jmbgLekar = lekar[l].Jmbg;
                            break;
                        }
                    }
                    for (int da = 0; da < preglediLekara.Count; da++)
                    {
                        if (!preglediLekara[da].Lekar.Jmbg.Equals(jmbgLekar))
                        {
                            preglediLekara.RemoveAt(da);
                            da = da - 1;
                        }
                    }
                    for (int ad = 0; ad < operacijeLekara.Count; ad++)
                    {
                        if (!operacijeLekara[ad].Lekar.Jmbg.Equals(jmbgLekar))
                        {
                            operacijeLekara.RemoveAt(ad);
                            ad = ad - 1;
                        }
                    }
                    for (int pre = 0; pre < preglediLekara.Count; pre++)
                    {
                        if (preglediLekara[pre].Datum.Date.Equals(textDatum.SelectedDate.Value.Date))
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
                        if (operacijeLekara[ope].Datum.Date.Equals(textDatum.SelectedDate.Value.Date))
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

        private void OpenComboPrezime(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Enter)
            {
                textPrezime.IsDropDownOpen = true;

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
                    checkHitan.Visibility = Visibility.Hidden;
                    textHitna.Visibility = Visibility.Hidden;
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
                    checkHitan.Visibility = Visibility.Visible;
                    textHitna.Visibility = Visibility.Visible;
                    labelTextOperacija.Visibility = Visibility.Visible;
                    textOperacija.Visibility = Visibility.Visible;
                    List<TipOperacije> tipOperacije = new List<TipOperacije>();
                    tipOperacije.Add(TipOperacije.prvaKat);
                    tipOperacije.Add(TipOperacije.drugaKat);
                    tipOperacije.Add(TipOperacije.trecaKat);
                    
                    textOperacija.ItemsSource = tipOperacije;
                    textOperacija.SelectedItem = TipOperacije.drugaKat;
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

        private void LekarComboOpeen(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                if (zaFilLek != textLekar.Text)
                {
                    filterLekar();
                    zaFilLek = textLekar.Text;
                    for (int le = 0; le < lekariTrenutni.Count; le++)
                    { 
                    string h = lekariTrenutni[le].Prezime + ' ' + lekariTrenutni[le].Ime + ' ' + lekariTrenutni[le].Jmbg;
                        if (h.Equals(textLekar.Text))
                        {
                            textSpecijalizacija.Text = lekariTrenutni[le].Specijalizacija.OblastMedicine;
                        }

                    }

                    }
            }

            else if (e.Key == Key.Enter)
            {
                textLekar.IsDropDownOpen = true;

            }
        }

        private void SpecijalizacijaComboOpeen(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                if (specijalizacije.Contains(textSpecijalizacija.Text))
                {
                    textLekar.Items.Clear();
                    for (int le = 0; le < lekariTrenutni.Count; le++)
                    {
                        if (lekariTrenutni[le].Specijalizacija.OblastMedicine.Equals(textSpecijalizacija.Text))
                        {
                            string ss = lekariTrenutni[le].Prezime + ' ' + lekariTrenutni[le].Ime + ' ' + lekariTrenutni[le].Jmbg;
                            textLekar.Items.Add(ss);
                        }
                    }
                }
            }

            else if (e.Key == Key.Enter)
            {
                textSpecijalizacija.IsDropDownOpen = true;

            }
        }

        private void check(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (checkHitan.IsChecked.Equals(false))
                {
                    checkHitan.IsChecked = true;
                }
                else
                {
                    checkHitan.IsChecked = false;
                }
            }
        }

        private void isAkcelerator(object sender, KeyEventArgs e)
        {
            if (e.KeyboardDevice.IsKeyDown(Key.LeftCtrl) || e.KeyboardDevice.IsKeyDown(Key.RightCtrl))
            {
                switch (e.Key)
                {
                    case Key.Q:
                        PotvrdiIzmenu();
                        break;
                    case Key.W:
                        OtkaziIzmenu();
                        break;


                }
            }
        }
    }
}
