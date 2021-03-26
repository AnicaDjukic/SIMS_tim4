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
using Model.Korisnici;
using Model.Pregledi;

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
        
        
        
        public FormLekar()
        {
            InitializeComponent();
            
            Pregled p1 = new Pregled();
            p1.Datum = new DateTime(2008, 3, 1, 7, 0, 0);
            // p1.Lekar = new Lekar();
            
            p1.Pacijent = new Pacijent();
            p1.Pacijent.Ime = "Milan";
            p1.Pacijent.Prezime = "Govedarica";
            p1.Zavrsen = true;
            p1.Prostorija = new Model.Prostorije.Prostorija();
            p1.Trajanje = 100;
            p1.Zavrsen = true;
            listaPregleda.Add(p1);
            Operacija op = new Operacija();
            op.Datum = new DateTime(2008, 3, 1, 7, 0, 0);
            // p1.Lekar = new Lekar();

            op.Pacijent = new Pacijent();
            op.Pacijent.Ime = "Milan";
            op.Pacijent.Prezime = "Govedarica";
            op.Zavrsen = true;
            op.Prostorija = new Model.Prostorije.Prostorija();
            op.Trajanje = 100;
            op.Zavrsen = true;
            op.TipOperacije = 0;
            listaOperacija.Add(op);
            
           
            dataList.Items.Add(listaPregleda[0]);
            dataList.Items.Add(listaOperacija[0]);
            lekarGrid.ItemsSource = dataList.Items;
        }

        private void ZakaziPregled(object sender, RoutedEventArgs e)
        {
            FormNapraviTerminLekar forma = new FormNapraviTerminLekar();
            forma.Show();
        }

        private void OtkaziPregled(object sender, RoutedEventArgs e)
        {
            
            var objekat = lekarGrid.SelectedValue;
            for(int i = 0; i < listaPregleda.Count; i++)
            {
                if (objekat.Equals(listaPregleda[i]))
                {
                    listaPregleda.RemoveAt(i);
                }
            }
            for (int i = 0; i < listaOperacija.Count; i++)
            {
                if (objekat.Equals(listaOperacija[i]))
                {
                    listaOperacija.RemoveAt(i);
                }
            }
            int index = lekarGrid.SelectedIndex;
            lekarGrid.Items.RemoveAt(index);
         
            
        }

        private void IzmeniPregled(object sender, RoutedEventArgs e)
        {
            
            var objekat = lekarGrid.SelectedValue;
            Pregled p1 = new Pregled();
            p1.Pacijent = new Pacijent();
            Operacija op = new Operacija();
            op.Pacijent = new Pacijent();
            DateTime datum=new DateTime();
            int trajanje=0;
            string ime="";
            string prezime="";
            bool zavrsen=false;

            for (int i = 0; i < listaPregleda.Count; i++)
            {
                if (objekat.Equals(listaPregleda[i]))
                {
                    
                     p1 = lekarGrid.SelectedItem as Pregled;
                }
            }
            for (int i = 0; i < listaOperacija.Count; i++)
            {
                if (objekat.Equals(listaOperacija[i]))
                {
                    
                     op = lekarGrid.SelectedItem as Operacija;
                }
            }

            if (p1.Pacijent.Ime!=null)
            {
                datum = p1.Datum;
                trajanje = p1.Trajanje;
                ime = p1.Pacijent.Ime;
                prezime = p1.Pacijent.Prezime;
                
                zavrsen = p1.Zavrsen;
                FormIzmeniTerminLekar forma = new FormIzmeniTerminLekar(datum, trajanje, ime, prezime, zavrsen,p1);
                forma.Show();


            }
            else if(op.Pacijent.Ime!=null)
            {
                datum = op.Datum;
                trajanje = op.Trajanje;
                ime = op.Pacijent.Ime;
                prezime = op.Pacijent.Prezime;
                TipOperacije operacija = op.TipOperacije;
                zavrsen = op.Zavrsen;
                FormIzmeniTerminLekar forma = new FormIzmeniTerminLekar(datum, trajanje, ime, prezime, operacija, zavrsen,op);
                forma.Show();
            }
            
            
            

        }

       public void Refresh()
        {
            lekarGrid.Items.Refresh();
        }

       
       
    }
}
