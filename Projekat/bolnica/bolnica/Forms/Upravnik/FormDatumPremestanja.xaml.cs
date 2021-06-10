using Bolnica.Localization;
using Bolnica.Model.Prostorije;
using Bolnica.Services.Prostorije;
using Model.Prostorije;
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

namespace Bolnica.Forms.Upravnik
{
    /// <summary>
    /// Interaction logic for FormDatumPremestanja.xaml
    /// </summary>
    public partial class FormDatumPremestanja : Window
    {
        private Injector inject;
        public Injector Inject
        {
            get { return inject; }
            set
            {
                inject = value;
            }
        }
        public FormDatumPremestanja()
        {
            Inject = new Injector();
            InitializeComponent();
            Title = LocalizedStrings.Instance["Unos datuma premeštanja statičke opreme"];
        }

        private void Button_Click_Potvrdi(object sender, RoutedEventArgs e)
        {
            List<BuducaZaliha> buduceZalihe = new List<BuducaZaliha>();
            string sifraOpreme = "";
            foreach (Zaliha z in FormSkladiste.Zalihe)
            {
                BuducaZaliha bz = NapraviBuducuZalihu(z);
                sifraOpreme = z.Oprema.Sifra;
                buduceZalihe.Add(bz);
            }

            ObrisiBuduceZaliheIzabranogDatuma(sifraOpreme, datePickerDatum.SelectedDate.Value);
            Inject.ControllerBuducaZaliha.SacuvajBuduceZalihe(buduceZalihe);

            Close();
        }

        private void ObrisiBuduceZaliheIzabranogDatuma(string sifraOpreme, DateTime datum)
        {
            Inject.ControllerBuducaZaliha.ObrisiBuduceZaliheOpremeZaDatum(sifraOpreme, datum);
        }

        private BuducaZaliha NapraviBuducuZalihu(Zaliha z)
        {
            return new BuducaZaliha { Kolicina = z.Kolicina, Prostorija = z.Prostorija, Oprema = z.Oprema, Datum = datePickerDatum.SelectedDate.Value };
        }

        private void Button_Click_Odustani(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
