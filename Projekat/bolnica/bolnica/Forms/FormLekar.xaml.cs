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



            listaLekara.Add(lekarTrenutni);
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
            FormNapraviTerminLekar forma = new FormNapraviTerminLekar(lekarTrenutni);
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
                FormIzmeniTerminLekar forma = new FormIzmeniTerminLekar(p1,lekarTrenutni);
                forma.Show();


            }
            else if(op.Pacijent.Ime!=null)
            {
                FormIzmeniTerminLekar forma = new FormIzmeniTerminLekar(op,lekarTrenutni);
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
