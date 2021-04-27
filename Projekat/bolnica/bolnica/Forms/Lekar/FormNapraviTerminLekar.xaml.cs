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
        private int dozvolaIme = 0;
        private int dozvolaPrezime = 0;
        private int dozvolaJmbg = 0;
        private FileStoragePacijenti sviPacijenti = new FileStoragePacijenti();
        private FileStorageProstorija sveProstorije = new FileStorageProstorija();
        private FileStoragePregledi sviPregledi = new FileStoragePregledi();
        private List<Pacijent> pacijentiZa = new List<Pacijent>();
        private List<Prostorija> prostorijaZa = new List<Prostorija>();
        private FileStorageLekar sviLekari = new FileStorageLekar();

        private int dozvola = 0;
        private string zaFilLek = "";
        private DateTime zaFilLekDat = new DateTime();


        public List<String> specijalizacija { get; set; }

        private List<Pregled> pregledi;
        private List<Operacija> operacije;
        
        public List<String> specijalizacije{get; set;}
        public string imeB { get; set; }
        public string prezimeB { get; set; }
        public string jmbgB { get; set; }

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
                    textLekar.Items.Add(lekariTrenutni[le].Prezime);
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
                    textIme.Items.Add(pacijentiZa[i].Ime);
                    textPrezime.Items.Add(pacijentiZa[i].Prezime);
                    textJmbg.Items.Add(pacijentiZa[i].Jmbg);
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
                    textLekar.Items.Add(lekariTrenutni[le].Prezime);
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
                    textIme.Items.Add(pacijentiZa[i].Ime);
                    textPrezime.Items.Add(pacijentiZa[i].Prezime);
                    textJmbg.Items.Add(pacijentiZa[i].Jmbg);
                }
            }
            for (int pr = 0; pr < prostorijaZa.Count; pr++)
            {
                if (prostorijaZa[pr].Obrisana == false && prostorijaZa[pr].Zauzeta == false && prostorijaZa[pr].TipProstorije.Equals(TipProstorije.salaZaPreglede))
                {
                    textProstorija.Items.Add(prostorijaZa[pr].BrojProstorije);
                }
            }
            imeB = pacij.Ime;
            prezimeB = pacij.Prezime;
            jmbgB = pacij.Jmbg;
            textIme.IsEnabled = false;
            textPrezime.IsEnabled = false;
            textJmbg.IsEnabled = false;


        }




        public void PotvrdiIzmenu(object sender, RoutedEventArgs e)
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
                    Operacija o = new Operacija();
                    trenutnaOperacija.AnamnezaId = -1;
                    trenutnaOperacija.Hitan = (bool)checkHitan.IsChecked;
                    o.Id = trenutnaOperacija.Id;
                    o.Hitan = (bool)checkHitan.IsChecked;
                    o.lekarJmbg = trenutnaOperacija.Lekar.Jmbg;
                    o.pacijentJmbg = trenutnaOperacija.Pacijent.Jmbg;
                    o.TipOperacije = trenutnaOperacija.TipOperacije;
                    o.Trajanje = trenutnaOperacija.Trajanje;
                    o.Zavrsen = trenutnaOperacija.Zavrsen;
                    o.AnamnezaId = -1;
                    o.brojProstorije = trenutnaOperacija.Prostorija.BrojProstorije;
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
                    trenutniPregled.AnamnezaId = -1;
                    trenutniPregled.Hitan = false;
                    o.Id = trenutniPregled.Id;
                    o.Hitan = false;
                    o.lekarJmbg = trenutniPregled.Lekar.Jmbg;
                    o.pacijentJmbg = trenutniPregled.Pacijent.Jmbg;
                    o.Trajanje = trenutniPregled.Trajanje;
                    o.Zavrsen = trenutniPregled.Zavrsen;
                    o.AnamnezaId = -1;
                    o.brojProstorije = trenutniPregled.Prostorija.BrojProstorije;
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

        private bool PostojiLekar()
        {
            
            FileStorageLekar ProveraL = new FileStorageLekar();
            List<Lekar> lekari = new List<Lekar>();
            lekari = ProveraL.GetAll();
            for(int i = 0; i < lekari.Count; i++)
            {
                if (lekari[i].Specijalizacija.OblastMedicine.Equals(textSpecijalizacija.Text) && lekari[i].Prezime.Equals(textLekar.Text))
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
                if (lekari[lek].Prezime.Equals(textLekar.Text) && lekari[lek].Specijalizacija.OblastMedicine != null)
                {
                   
                    
                    List<Lekar> lekar = new List<Lekar>();
                    lekar = sviLekari.GetAll();
                    string jmbgLekar = "";
                    for (int l = 0; l < lekar.Count; l++)
                    {
                        if (lekar[l].Prezime.Equals(textLekar.Text))
                        {
                            jmbgLekar = lekar[l].Jmbg;
                            break;
                        }
                    }
                    for (int da = 0; da < preglediLekara1.Count; da++)
                    {
                        if (!preglediLekara1[da].lekarJmbg.Equals(jmbgLekar))
                        {
                            preglediLekara1.RemoveAt(da);
                            da = da - 1;
                        }
                    }
                    for (int ad = 0; ad < operacijeLekara1.Count; ad++)
                    {
                        if (!operacijeLekara1[ad].lekarJmbg.Equals(jmbgLekar))
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
            if (zauzetiTermini.Contains(TimeSpan.Parse(textVreme.Text)))
            {
                return false;
            }
            else
            {
                return true;
            }
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
                if (pacijenti[lek].Prezime.Equals(textPrezime.Text) && pacijenti[lek].Ime.Equals(textIme.Text) && pacijenti[lek].Jmbg.Equals(textJmbg.Text))
                {


                    List<Pacijent> pacijent = new List<Pacijent>();
                    pacijent = sviPacijenti.GetAll();
                    string jmbgPacijent = "";
                    for (int l = 0; l < pacijent.Count; l++)
                    {
                        if (pacijent[l].Jmbg.Equals(textJmbg.Text))
                        {
                            jmbgPacijent = pacijent[l].Jmbg;
                            break;
                        }
                    }
                    for (int da = 0; da < preglediPacijenta1.Count; da++)
                    {
                        if (!preglediPacijenta1[da].pacijentJmbg.Equals(jmbgPacijent))
                        {
                            preglediPacijenta1.RemoveAt(da);
                            da = da - 1;
                        }
                    }
                    for (int ad = 0; ad < operacijePacijenta1.Count; ad++)
                    {
                        if (!operacijePacijenta1[ad].pacijentJmbg.Equals(jmbgPacijent))
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
                if (zauzetiTermini.Contains(TimeSpan.Parse(textVreme.Text)))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            if (zauzetiTermini.Contains(TimeSpan.Parse(textVreme.Text)))
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        private bool PostojiProstorija()
        {
           
            for(int i = 0; i < prostorijaZa.Count; i++)
            {

                if (prostorijaZa[i].BrojProstorije.Equals(int.Parse(textProstorija.Text))&&!prostorijaZa[i].Obrisana)
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

                if (prostorijaZa[i].BrojProstorije.Equals(int.Parse(textProstorija.Text)) && !prostorijaZa[i].Obrisana)
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
                tipOperacije.Add(TipOperacije.teška);
                tipOperacije.Add(TipOperacije.laka);
                tipOperacije.Add(TipOperacije.srednja);
            
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
                if (lekariTrenutni[lek].Prezime.Equals(textLekar.Text) && lekariTrenutni[lek].Specijalizacija.OblastMedicine != null)
                {

                    List<TimeSpan> zauzetiTermini = new List<TimeSpan>();
                    List<Pregled> preglediLekara = sviPregledi.GetAllPregledi();
                    List<Operacija> operacijeLekara = sviPregledi.GetAllOperacije();
                    List<Lekar> lekar = new List<Lekar>();
                    lekar = lekariTrenutni;
                    string jmbgLekar = "";
                    for (int l = 0; l < lekar.Count; l++)
                    {
                        if (lekar[l].Prezime.Equals(textLekar.Text))
                        {
                            jmbgLekar = lekar[l].Jmbg;
                            break;
                        }
                    }
                    for (int da = 0; da < preglediLekara.Count; da++)
                    {
                        if (!preglediLekara[da].lekarJmbg.Equals(jmbgLekar))
                        {
                            preglediLekara.RemoveAt(da);
                            da = da - 1;
                        }
                    }
                    for (int ad = 0; ad < operacijeLekara.Count; ad++)
                    {
                        if (!operacijeLekara[ad].lekarJmbg.Equals(jmbgLekar))
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
                            textLekar.Items.Add(lekariTrenutni[le].Prezime);
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
    }
}
