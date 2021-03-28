using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Model.Korisnici;
using Model.Pregledi;
using Model.Prostorije;


namespace Bolnica.Forms
{
    /// <summary>
    /// Interaction logic for FormLekar.xaml
    /// </summary>
    
    public partial class FormLekar : Window
    {
        public static List<Pregled> listaPregleda= new List<Pregled>();
        public static List<Operacija> listaOperacija = new List<Operacija>();
        public static DataGrid dataList = new DataGrid();
        
        public static List<Lekar> listaLekara = new List<Lekar>();
        private Lekar lekarTrenutni = new Lekar();
        private Lekar lekarPomocni = new Lekar();
        private Lekar l3 = new Lekar();
        private Lekar l4 = new Lekar();
        private FileStoragePregledi sviPregledi = new FileStoragePregledi();
       



        public FormLekar()
        {
            InitializeComponent();
            //WindowStartupLocation = WindowStartupLocation.CenterOwner;
            //Owner = Application.Current.MainWindow;
            this.WindowState = WindowState.Maximized;
            
            
            lekarTrenutni.AdresaStanovanja = "AAA";
            lekarTrenutni.BrojSlobodnihDana = 15;
            lekarTrenutni.BrojTelefona = "111111";
            lekarTrenutni.DatumRodjenja = new DateTime();
            lekarTrenutni.Email = "dada@dada.com";
            lekarTrenutni.GodineStaza = 11;
            lekarTrenutni.Ime = "Mico";
            lekarTrenutni.Prezime = "Govedarica";
            lekarTrenutni.Jmbg = "342425";
            lekarTrenutni.KorisnickoIme = "Pero";
            lekarTrenutni.Lozinka = "Admin";
            lekarTrenutni.Mbr = 21312;
            lekarTrenutni.Plata = 1000;
            Specijalizacija sp = new Specijalizacija();
            sp.Id = 121;
            sp.Naziv = "neka";
            sp.OblastMedicine = "nekaa";
            lekarTrenutni.Specijalizacija = sp;
            lekarTrenutni.TipKorisnika = TipKorisnika.lekar;
            lekarTrenutni.Zaposlen = true;

            lekarPomocni.AdresaStanovanja = "BBB";
            lekarPomocni.BrojSlobodnihDana = 15;
            lekarPomocni.BrojTelefona = "22222";
            lekarPomocni.DatumRodjenja = new DateTime();
            lekarPomocni.Email = "bada@dada.com";
            lekarPomocni.GodineStaza = 7;
            lekarPomocni.Ime = "Mio";
            lekarPomocni.Prezime = "Prodano";
            lekarPomocni.Jmbg = "222222";
            lekarPomocni.KorisnickoIme = "Peki";
            lekarPomocni.Lozinka = "Baja";
            lekarPomocni.Mbr = 3232;
            lekarPomocni.Plata = 10000;
            Specijalizacija spa = new Specijalizacija();
            spa.Id = 1211;
            spa.Naziv = "neeka";
            spa.OblastMedicine = "nekaaa";
            lekarPomocni.Specijalizacija = spa;
            lekarPomocni.TipKorisnika = TipKorisnika.lekar;
            lekarPomocni.Zaposlen = true;

            l3.AdresaStanovanja = "Tolstojeva 1";
            l3.BrojSlobodnihDana = 20;
            l3.BrojTelefona = "0642354578";
            l3.DatumRodjenja = new DateTime(1965, 3, 3);
            l3.Email = "pap@gmail.com";
            l3.GodineStaza = 30;
            l3.Ime = "Vatroslav";
            l3.Prezime = "Pap";
            l3.Jmbg = "0303965123456";
            l3.KorisnickoIme = "vatro";
            l3.Lozinka = "vatro";
            l3.Mbr = 123123;
            l3.Plata = 15000;
            Specijalizacija sp3 = new Specijalizacija();
            sp3.Id = 1251;
            sp3.Naziv = "kardioloski majstor";
            sp3.OblastMedicine = "kardiologija";
            l3.Specijalizacija = sp3;
            l3.TipKorisnika = TipKorisnika.lekar;
            l3.Zaposlen = true;

            l4.AdresaStanovanja = "Balzakova 21";
            l4.BrojSlobodnihDana = 17;
            l4.BrojTelefona = "0613579624";
            l4.DatumRodjenja = new DateTime(1988, 9, 9);
            l4.Email = "bodi@gmail.com";
            l4.GodineStaza = 6;
            l4.Ime = "Radmilo";
            l4.Prezime = "Bodiroga";
            l4.Jmbg = "090988131533";
            l4.KorisnickoIme = "bodi";
            l4.Lozinka = "bodi";
            l4.Mbr = 123456;
            l4.Plata = 8000;
            Specijalizacija sp4 = new Specijalizacija();
            sp4.Id = 1251;
            sp4.Naziv = "slusni specijalista";
            sp4.OblastMedicine = "otorinolaringologija";
            l4.Specijalizacija = sp3;
            l4.TipKorisnika = TipKorisnika.lekar;
            l4.Zaposlen = true;


            
            listaLekara.Add(lekarTrenutni);
            listaLekara.Add(lekarPomocni);
            listaLekara.Add(l3);
            listaLekara.Add(l4);


            listaPregleda = sviPregledi.GetAllPregledi();
            listaOperacija = sviPregledi.GetAllOperacije();

            
            for (int l=0; l < listaPregleda.Count; l++)
            {
                if (!listaPregleda[l].Lekar.KorisnickoIme.Equals(lekarTrenutni.KorisnickoIme))
                {
                    listaPregleda.RemoveAt(l);
                    l = l - 1;
                }
            }
            for (int l = 0; l < listaOperacija.Count; l++)
            {
                if (!listaOperacija[l].Lekar.KorisnickoIme.Equals(lekarTrenutni.KorisnickoIme))
                {
                    listaOperacija.RemoveAt(l);
                    l = l - 1;
                }
            } 

            dataList.AddingNewItem += dataListAddingNewItemEventArgs;

            dataList.Items.SortDescriptions.Clear();
            dataList.Items.SortDescriptions.Add(new SortDescription("Datum", ListSortDirection.Ascending));
            for (int i = 0; i < listaPregleda.Count; i++)
            {
                if (listaPregleda[i].Zavrsen.Equals(false))
                {
                    dataList.Items.Add(listaPregleda[i]);
                }
            }
            for (int i = 0; i < listaOperacija.Count; i++)
            {
                if (listaOperacija[i].Zavrsen.Equals(false))
                {
                    dataList.Items.Add(listaOperacija[i]);
                }
            }
            data();
            lekarGrid.ItemsSource = dataList.Items;
        }

        private void ZakaziPregled(object sender, RoutedEventArgs e)
        {
            FormNapraviTerminLekar forma = new FormNapraviTerminLekar(listaLekara,lekarTrenutni);
            forma.Show();
        }

        private void OtkaziPregled(object sender, RoutedEventArgs e)
        {
            
            var objekat = lekarGrid.SelectedValue;
            for(int i = 0; i < listaPregleda.Count; i++)
            {
                if (objekat.Equals(listaPregleda[i]))
                {
                    
                    sviPregledi.Delete(listaPregleda[i]);
                    listaPregleda.RemoveAt(i);
                    break;
                }
            }
            for (int i = 0; i < listaOperacija.Count; i++)
            {
                if (objekat.Equals(listaOperacija[i]))
                {
                    sviPregledi.Delete(listaOperacija[i]);
                    listaOperacija.RemoveAt(i);
                    break;
                }
            }
            int index = lekarGrid.SelectedIndex;
            dataList.Items.RemoveAt(index);
            data();


        }

        private void IzmeniPregled(object sender, RoutedEventArgs e)
        {
            bool dozvolaZaFor = true;
            var objekat = lekarGrid.SelectedValue;
            Pregled p1 = new Pregled();
            Operacija op = new Operacija();
            p1.Pacijent = new Pacijent();
            op.Pacijent = new Pacijent();
           
            for (int i = 0; i < listaPregleda.Count; i++)
            {
                if (objekat.Equals(listaPregleda[i]))
                {
                    
                     p1 = lekarGrid.SelectedItem as Pregled;
                    dozvolaZaFor = false;
                     break;
                }
            }
            if (dozvolaZaFor)
            {
                for (int i = 0; i < listaOperacija.Count; i++)
                {
                    if (objekat.Equals(listaOperacija[i]))
                    {

                        op = lekarGrid.SelectedItem as Operacija;
                    }
                }
            }
            if (p1.Pacijent.Ime!=null)
            {
                FormIzmeniTerminLekar forma = new FormIzmeniTerminLekar(p1, listaLekara, lekarTrenutni);
                forma.Show();


            }
            else if(op.Pacijent.Ime!=null)
            {
                FormIzmeniTerminLekar forma = new FormIzmeniTerminLekar(op, listaLekara, lekarTrenutni);
                forma.Show();
            }

        }

       public void Refresh()
        {
            lekarGrid.Items.Refresh();
        }

        public static void data()
        {
            dataList.Items.Refresh();
        }
        private void dataListAddingNewItemEventArgs(object sender, AddingNewItemEventArgs e)
        {
            lekarGrid.Items.Refresh();
        }

        
    }
}
