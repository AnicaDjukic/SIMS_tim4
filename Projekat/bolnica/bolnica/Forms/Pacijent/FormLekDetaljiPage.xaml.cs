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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Bolnica.Forms
{
    /// <summary>
    /// Interaction logic for FormLekDetaljiPage.xaml
    /// </summary>
    public partial class FormLekDetaljiPage : Page
    {
        public string Naziv { get; set; }
        public DateTime DatumPrepisivanja { get; set; }
        public DateTime DatumPrekida { get; set; }
        public int Doza { get; set; }
        public int BrojKutija { get; set; }
        public string UputstvoZaKoriscenje { get; set; }

        public FormLekDetaljiPage(PrikazRecepta prikazRecepta)
        {
            InitializeComponent();
            this.DataContext = this;

            NamjestiPodatkeLeka(prikazRecepta);
            NapisiUputstvoZaKoriscenje(prikazRecepta);
        }

        private void NamjestiPodatkeLeka(PrikazRecepta prikazRecepta)
        {
            Naziv = prikazRecepta.lek.Naziv;
            DatumPrepisivanja = prikazRecepta.DatumIzdavanja;
            DatumPrekida = prikazRecepta.Trajanje;
            Doza = prikazRecepta.lek.KolicinaUMg;
            BrojKutija = prikazRecepta.Kolicina;
        }

        private void NapisiUputstvoZaKoriscenje(PrikazRecepta prikazRecepta)
        {
            if (prikazRecepta.VremeUzimanja.Hours == 0)
            {
                UputstvoZaKoriscenje = "- Lek piti jednom dnevno.\r";
            }
            else
            {
                UputstvoZaKoriscenje = "- Lek piti " + 24 / prikazRecepta.VremeUzimanja.Hours + " puta dnevno u razmacima od po " + prikazRecepta.VremeUzimanja.Hours + " sati.\r";
            }
            UputstvoZaKoriscenje += "- Tokom upotrebe ovog leka strogo je zabranjeno konzumiranje alkohola.\r" +
                 "- Za informacije o nezeljenim dejstvima posavetujte se sa Vasim lekarom ili farmaceutom.";
        }
    }
}
