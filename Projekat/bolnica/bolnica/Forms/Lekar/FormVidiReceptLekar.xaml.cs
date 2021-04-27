using Bolnica.Model.Pregledi;
using Model.Pregledi;
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

namespace Bolnica.Forms
{
    /// <summary>
    /// Interaction logic for FormVidiReceptLekar.xaml
    /// </summary>
    public partial class FormVidiReceptLekar : Window
    {
        public DateTime datumIzdavanja { get; set; }
        public string lek { get; set; }
        public string doza { get; set; }
        public string brojKutija { get; set; }
        public string vremeUzimanja { get; set; }

        public string proizvodjac { get; set; }
        public DateTime datumPrekida { get; set; }

        private List<Lek> lekovi;

        private FileStorageLek sviLekovi = new FileStorageLek();
        public FormVidiReceptLekar(Recept r)
        {
            lekovi = sviLekovi.GetAll();
            for (int i = 0; i < lekovi.Count; i++)
            {
                if (lekovi[i].Status.Equals(StatusLeka.Odbijen))
                {
                    lekovi.RemoveAt(i);
                    i--;
                }
            }

            InitializeComponent();

            this.DataContext = this;

            datumIzdavanja = r.DatumIzdavanja;

            for (int i = 0;i < lekovi.Count; i++)
            {
                if (lekovi[i].Id.Equals(r.Lek_id))
                {
                    lek = lekovi[i].Naziv;
                    doza = lekovi[i].KolicinaUMg.ToString();
                    proizvodjac = lekovi[i].Proizvodjac;
                }
            }
            brojKutija = r.Kolicina.ToString();
            vremeUzimanja = r.VremeUzimanja.ToString();
            datumPrekida = r.Trajanje;
           


        }

        private void Potvrdi(object sender, RoutedEventArgs e)
        {
            
            this.Close();

        }

        

       

       
    }
}
