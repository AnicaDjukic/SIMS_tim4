using Model.Pregledi;
using System.Windows.Controls;

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
            if (prikazRecepta.VremeUzimanja == 24)
            {
                UputstvoZaKoriscenje = "- Lek piti jednom dnevno.\r";
            }
            else
            {
                UputstvoZaKoriscenje = "- Lek piti " + 24 / prikazRecepta.VremeUzimanja + " puta dnevno u razmacima od po " + prikazRecepta.VremeUzimanja + " sati.\r";
            }
            UputstvoZaKoriscenje += "- Za informacije o nezeljenim dejstvima posavetujte se sa Vasim lekarom ili farmaceutom.";
        }
    }
}
