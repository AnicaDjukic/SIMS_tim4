using Bolnica.Model.Korisnici;
using Bolnica.Model.Pregledi;
using Model.Korisnici;
using Model.Pregledi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Bolnica.Forms
{
    /// <summary>
    /// Interaction logic for FormNapraviAnamnezuLekar.xaml
    /// </summary>
    public partial class FormNapraviAnamnezuLekar : Window
    {

        public string simptomi { get; set; }
        public string dijagnoza { get; set; }

        public List<PrikazRecepta> recepti { get; set; }
        private int dozvola = 0;
        private List<Lek> lekovi = new List<Lek>();

        private FileStoragePregledi sviPregledi = new FileStoragePregledi();
        private FileStorageLek sviLekovi = new FileStorageLek();
        private List<Pregled> sviPreg = new List<Pregled>();
        private List<Operacija> sveOpe = new List<Operacija>();
        private List<Lekar> lekariTrenutni = new List<Lekar>();
        private Lekar ulogovaniLekar = new Lekar();
        private Pacijent trenturniPacijent = new Pacijent();
        private PrikazPregleda trenutniPregled = new PrikazPregleda();
        private PrikazOperacije trenutnaOperacija = new PrikazOperacije();
        private PrikazPregleda stariPregled = new PrikazPregleda();
        private PrikazOperacije staraOperacija = new PrikazOperacije();
        private List<Anamneza> listaAnamneza = new List<Anamneza>();
        private FileStorageAnamneza anam = new FileStorageAnamneza();
        private FileStorageLekar sviLekari = new FileStorageLekar();
        private int idAnamneze;
        private int jePregled = 0;

        public static ObservableCollection<PrikazRecepta> Recepti
        {
            get;
            set;
        }


        public FormNapraviAnamnezuLekar(PrikazPregleda p1, Lekar neki)
        {
            jePregled = 1;


            lekovi = sviLekovi.GetAll();

            for (int i = 0; i < lekovi.Count; i++)
            {
                if (lekovi[i].Status.Equals(StatusLeka.odbijen))
                {
                    lekovi.RemoveAt(i);
                    i--;
                }
            }
            trenturniPacijent = p1.Pacijent;

            listaAnamneza = anam.GetAll();

            Recepti = new ObservableCollection<PrikazRecepta>();

            simptomi = "";
            dijagnoza = "";
            
            trenutniPregled = p1;
            lekariTrenutni = sviLekari.GetAll();
            stariPregled = p1;
            ulogovaniLekar = neki;
            
            


            InitializeComponent();
            this.DataContext = this;

            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            Owner = Application.Current.MainWindow;


            for (int i = 0; i < listaAnamneza.Count; i++)
            {
                if (p1.AnamnezaId == listaAnamneza[i].Id)
                {
                    if ((p1.Datum > DateTime.Now) || (p1.Datum.AddDays(7) < DateTime.Now))
                    {
                        textDijagnoza.IsEnabled = false;
                        textSimptomi.IsEnabled = false;
                        DodajButton.IsEnabled = false;
                        IzbrisiButton.IsEnabled = false;
                        PotvrdiButton.IsEnabled = false;
                    }

                    idAnamneze = p1.AnamnezaId;
                    simptomi = listaAnamneza[i].Simptomi;
                    dijagnoza = listaAnamneza[i].Dijagnoza;
                    PrikazRecepta pr = new PrikazRecepta(); 


                    for (int r = 0; r < listaAnamneza[i].Recept.Count; r++)
                    {
                        pr = new PrikazRecepta();
                        pr.DatumIzdavanja = listaAnamneza[i].Recept[r].DatumIzdavanja;
                        pr.Id = listaAnamneza[i].Recept[r].Id;
                        pr.Kolicina = listaAnamneza[i].Recept[r].Kolicina;
                    
                        pr.Trajanje = listaAnamneza[i].Recept[r].Trajanje;
                        pr.VremeUzimanja = listaAnamneza[i].Recept[r].VremeUzimanja;
                        for (int le = 0; le < lekovi.Count; le++)
                        {
                            if (listaAnamneza[i].Recept[r].Lek_id.Equals(lekovi[le].Id))
                            {
                                pr.lek = lekovi[le];
                                break;
                            }
                        }
                        Recepti.Add(pr);

                    }
                    dozvola = 1;
                    break;
                }
            }

            if (dozvola == 0)
            {



                textSimptomi.Text = simptomi;
                textDijagnoza.Text = dijagnoza;
                Recepti = new ObservableCollection<PrikazRecepta>();
            }

        }

        public FormNapraviAnamnezuLekar(PrikazOperacije op, Lekar neki)
        {
           

            trenturniPacijent = op.Pacijent;

            listaAnamneza = anam.GetAll();
            simptomi = "";
            dijagnoza = "";

            lekovi = sviLekovi.GetAll();


            trenutnaOperacija = op;
            lekariTrenutni = sviLekari.GetAll();
            staraOperacija = op;
            ulogovaniLekar = neki;

            Recepti = new ObservableCollection<PrikazRecepta>();


            InitializeComponent();

            this.DataContext = this;

            



            for (int i = 0; i < listaAnamneza.Count; i++)
            {
                if (op.AnamnezaId == listaAnamneza[i].Id)
                {
                    if ((op.Datum > DateTime.Now) || (op.Datum.AddDays(7) < DateTime.Now))
                    {
                        textDijagnoza.IsEnabled = false;
                        textSimptomi.IsEnabled = false;
                        DodajButton.IsEnabled = false;
                        IzbrisiButton.IsEnabled = false;
                        PotvrdiButton.IsEnabled = false;
                    }
                    idAnamneze = op.AnamnezaId;
                    simptomi = listaAnamneza[i].Simptomi;
                    dijagnoza = listaAnamneza[i].Dijagnoza;
                    PrikazRecepta pr = new PrikazRecepta();


                    for (int r = 0; r < listaAnamneza[i].Recept.Count; r++)
                    {
                        pr = new PrikazRecepta();
                        pr.DatumIzdavanja = listaAnamneza[i].Recept[r].DatumIzdavanja;
                        pr.Id = listaAnamneza[i].Recept[r].Id;
                        pr.Kolicina = listaAnamneza[i].Recept[r].Kolicina;
                        pr.Trajanje = listaAnamneza[i].Recept[r].Trajanje;
                        pr.VremeUzimanja = listaAnamneza[i].Recept[r].VremeUzimanja;
                        for (int le = 0; le < lekovi.Count; le++)
                        {
                            if (listaAnamneza[i].Recept[r].Lek_id.Equals(lekovi[le].Id))
                            {
                                pr.lek = lekovi[le];
                                break;
                            }
                        }
                        Recepti.Add(pr);

                    }

                    dozvola = 1;
                    break;
                }
            }

                if (dozvola == 0)
                {



                   textSimptomi.Text = simptomi;
                   textDijagnoza.Text = dijagnoza;
                Recepti = new ObservableCollection<PrikazRecepta>();
            }

            

        }



        private void OtkaziDugme(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DodajLek(object sender, RoutedEventArgs e)
        {
            FormNapraviReceptLekar form = new FormNapraviReceptLekar(trenturniPacijent);
            form.Show();
        }

        private void Potvrdi(object sender, RoutedEventArgs e)
        {
            Anamneza a = new Anamneza();
           
            a.Simptomi = textSimptomi.Text;
            a.Dijagnoza = textDijagnoza.Text;
            a.Recept = new List<Recept>();
            for (int i = 0; i < Recepti.Count; i++)
            {
                Recept n = new Recept();
                n.DatumIzdavanja = Recepti[i].DatumIzdavanja;
                n.Id = Recepti[i].Id;
                n.Kolicina = Recepti[i].Kolicina;
                n.Lek_id = Recepti[i].lek.Id;
                n.Trajanje = Recepti[i].Trajanje;
                n.VremeUzimanja = Recepti[i].VremeUzimanja;
                a.Recept.Add(n);
            }

            if (dozvola == 0)
            {
                int max = 0;
                for (int id = 0; id < listaAnamneza.Count; id++)
                {
                    if (listaAnamneza[id].Id > max)
                    {
                        max = listaAnamneza[id].Id;
                    }
                }
                a.Id = max + 1;
                anam.Save(a);
                if (jePregled == 1)
                {
                   
                       
                        for (int p = 0; p < FormLekar.dataList.Items.Count; p++)
                        {
                            if (FormLekar.dataList.Items[p].Equals(stariPregled))
                            {
                                trenutniPregled.AnamnezaId = a.Id;
                                
                                trenutniPregled.Zavrsen = true;
                                FormLekar.dataListIstorija.Items.Add(trenutniPregled);
                                FormLekar.dataList.Items.RemoveAt(p);

                                FormLekar.dataIstorija();
                                FormLekar.data();
                                
                                Pregled o = new Pregled();
                                o.Id = trenutniPregled.Id;
                                 o.Hitan = trenutniPregled.Hitan;
                                o.lekarJmbg = trenutniPregled.Lekar.Jmbg;
                                o.pacijentJmbg = trenutniPregled.Pacijent.Jmbg;
                                o.Trajanje = trenutniPregled.Trajanje;
                                o.Zavrsen = trenutniPregled.Zavrsen;
                                o.AnamnezaId = a.Id;
                                o.brojProstorije = trenutniPregled.Prostorija.BrojProstorije;
                                o.Datum = trenutniPregled.Datum;
                                o.Zavrsen = true;
                                sviPregledi.Izmeni(o);
                            }

                        }

                    
                    
                }
                else
                {
                    
                        
                        for (int p = 0; p < FormLekar.dataList.Items.Count; p++)
                        {
                            if (FormLekar.dataList.Items[p].Equals(staraOperacija))
                            {
                                trenutnaOperacija.AnamnezaId = a.Id;



                                trenutnaOperacija.Zavrsen = true;
                                FormLekar.dataListIstorija.Items.Add(trenutnaOperacija);
                                FormLekar.dataList.Items.RemoveAt(p);

                                FormLekar.dataIstorija();
                                FormLekar.data();

                                Operacija o = new Operacija();
                                o.Id = trenutnaOperacija.Id;
                            o.Hitan = trenutnaOperacija.Hitan;
                                o.lekarJmbg = trenutnaOperacija.Lekar.Jmbg;
                                o.pacijentJmbg = trenutnaOperacija.Pacijent.Jmbg;
                                o.TipOperacije = trenutnaOperacija.TipOperacije;
                                o.Trajanje = trenutnaOperacija.Trajanje;
                                o.Zavrsen = trenutnaOperacija.Zavrsen;
                                o.AnamnezaId = a.Id;
                                o.brojProstorije = trenutnaOperacija.Prostorija.BrojProstorije;
                                o.Datum = trenutnaOperacija.Datum;
                                o.Zavrsen = true;
                                sviPregledi.Izmeni(o);
                            }
                        }
                    
                    
                }

            }
            else {
                a.Id = idAnamneze;
                anam.Izmeni(a);
                if(jePregled == 1)
                {
                    for (int p = 0; p < FormLekar.dataListIstorija.Items.Count; p++)
                    {
                        if (FormLekar.dataListIstorija.Items[p].Equals(stariPregled))
                        {
                            stariPregled.AnamnezaId = a.Id;



                            stariPregled.Zavrsen = true;
                            FormLekar.dataListIstorija.Items[p] = stariPregled;


                            FormLekar.dataIstorija();


                            Pregled o = new Pregled();
                            o.Id = trenutniPregled.Id;
                            o.Hitan = trenutniPregled.Hitan;
                            o.lekarJmbg = trenutniPregled.Lekar.Jmbg;
                            o.pacijentJmbg = trenutniPregled.Pacijent.Jmbg;
                            o.Trajanje = trenutniPregled.Trajanje;
                            o.Zavrsen = trenutniPregled.Zavrsen;
                            o.AnamnezaId = a.Id;
                            o.brojProstorije = trenutniPregled.Prostorija.BrojProstorije;
                            o.Datum = trenutniPregled.Datum;
                            o.Zavrsen = true;
                            sviPregledi.Izmeni(o);
                        }
                    }
                }
                else
                {
                    for (int p = 0; p < FormLekar.dataListIstorija.Items.Count; p++)
                    {
                        if (FormLekar.dataListIstorija.Items[p].Equals(staraOperacija))
                        {
                            trenutnaOperacija.AnamnezaId = a.Id;



                            trenutnaOperacija.Zavrsen = true;
                            FormLekar.dataListIstorija.Items[p] = trenutnaOperacija;


                            FormLekar.dataIstorija();


                            Operacija o = new Operacija();
                            o.Id = trenutnaOperacija.Id;
                            o.Hitan = trenutnaOperacija.Hitan;
                            o.lekarJmbg = trenutnaOperacija.Lekar.Jmbg;
                            o.pacijentJmbg = trenutnaOperacija.Pacijent.Jmbg;
                            o.TipOperacije = trenutnaOperacija.TipOperacije;
                            o.Trajanje = trenutnaOperacija.Trajanje;
                            o.Zavrsen = trenutnaOperacija.Zavrsen;
                            o.AnamnezaId = a.Id;
                            o.brojProstorije = trenutnaOperacija.Prostorija.BrojProstorije;
                            o.Datum = trenutnaOperacija.Datum;
                            o.Zavrsen = true;
                            sviPregledi.Izmeni(o);
                        }
                    }
                }
                
            }
            
            this.Close();
        }

        private void Obrisi(object sender, RoutedEventArgs e)
        {
            if (dataGridLekovi.SelectedCells.Count > 0)
            {
                int index = dataGridLekovi.SelectedIndex;
                Recepti.RemoveAt(index);
            }
        }

        private void ZakaziPregled(object sender, RoutedEventArgs e)
        {
            FormNapraviTerminLekar form = new FormNapraviTerminLekar(ulogovaniLekar,trenturniPacijent);
            form.Show();
        }

        private void otkazi(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void VidiDetalje(object sender, RoutedEventArgs e)
        {
            if (dataGridLekovi.SelectedCells.Count > 0)
            {
                PrikazRecepta r = new PrikazRecepta();
                r = dataGridLekovi.SelectedItem as PrikazRecepta;
                Recept a = new Recept();
                a.DatumIzdavanja = r.DatumIzdavanja;
                a.Id = r.Id;
                a.Kolicina = r.Kolicina;
                a.Lek_id = r.lek.Id;
                a.Trajanje = r.Trajanje;
                a.VremeUzimanja = r.VremeUzimanja;
               
                FormVidiReceptLekar form = new FormVidiReceptLekar(a);
                form.Show();  
            }
        }
    }
}
