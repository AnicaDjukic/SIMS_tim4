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
                if (lekovi[i].Status.Equals(StatusLeka.odbijen) || lekovi[i].Obrisan)
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
                if (lekovi[i].Id.Equals(r.Lek.Id))
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

        public void Potvrdi()
        {
            this.Close();
        }
        private void Potvrdi(object sender, RoutedEventArgs e)
        {

            Potvrdi();

        }

        private void isAkcelerator(object sender, KeyEventArgs e)
        {
            if (e.KeyboardDevice.IsKeyDown(Key.LeftCtrl) || e.KeyboardDevice.IsKeyDown(Key.RightCtrl))
            {
                switch (e.Key)
                {
                    case Key.Q:
                        Potvrdi();
                        break;



                }
            }
        }
    }
}
