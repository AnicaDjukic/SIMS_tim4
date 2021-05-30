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
        public PrikazRecepta PrikazRecepta { get; set; }
        public string UputstvoZaKoriscenje { get; set; }

        public FormLekDetaljiPage(PrikazRecepta prikazRecepta)
        {
            InitializeComponent();
            this.DataContext = this;

            NamestiPodatkeLeka(prikazRecepta);
            NapisiUputstvoZaKoriscenje(prikazRecepta);
        }

        private void NamestiPodatkeLeka(PrikazRecepta prikazRecepta)
        {
            PrikazRecepta = prikazRecepta;
        }

        private void NapisiUputstvoZaKoriscenje(PrikazRecepta prikazRecepta)
        {
            if (prikazRecepta.VremeUzimanja == 0)
            {
                UputstvoZaKoriscenje = "- Lek piti jednom dnevno.\r";
            }
            else
            {
                UputstvoZaKoriscenje = "- Lek piti " + 24 / prikazRecepta.VremeUzimanja + " puta dnevno u razmacima od po " + prikazRecepta.VremeUzimanja + " sati.\r";
            }
            UputstvoZaKoriscenje += "- Tokom upotrebe ovog leka strogo je zabranjeno konzumiranje alkohola.\r" +
                 "- Za informacije o nezeljenim dejstvima posavetujte se sa Vasim lekarom ili farmaceutom.";
        }
    }
}
