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
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            Owner = Application.Current.MainWindow;
            this.WindowState = WindowState.Maximized;

            //
            Pregled p1 = new Pregled();
            p1.Datum = new DateTime(2008, 3, 1, 7, 0, 0);
            p1.Pacijent = new Pacijent();
            p1.Pacijent.Ime = "Milan";
            p1.Pacijent.Prezime = "Govedarica";
            p1.Zavrsen = true;
            p1.Prostorija = new Model.Prostorije.Prostorija();
            p1.Trajanje = 100;
            p1.Zavrsen = false;
            
            Operacija op = new Operacija();
            op.Datum = new DateTime(2008, 3, 1, 6, 0, 0);
            op.Pacijent = new Pacijent();
            op.Pacijent.Ime = "Milan";
            op.Pacijent.Prezime = "Govedarica";
            op.Zavrsen = true;
            op.Prostorija = new Model.Prostorije.Prostorija();
            op.Trajanje = 100;
            op.Zavrsen = false;
            op.TipOperacije = 0;

            listaPregleda.Add(p1);
            listaOperacija.Add(op);
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
            dataList.Items.RemoveAt(index);
            data();


        }

        private void IzmeniPregled(object sender, RoutedEventArgs e)
        {
            
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
                FormIzmeniTerminLekar forma = new FormIzmeniTerminLekar(p1);
                forma.Show();


            }
            else if(op.Pacijent.Ime!=null)
            {
                FormIzmeniTerminLekar forma = new FormIzmeniTerminLekar(op);
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
