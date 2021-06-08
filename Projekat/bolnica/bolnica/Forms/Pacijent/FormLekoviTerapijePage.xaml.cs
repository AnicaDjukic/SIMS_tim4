using Bolnica.Model.Pregledi;
using Bolnica.Repository.Pregledi;
using Model.Korisnici;
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
    /// Interaction logic for FormLekoviTerapijePage.xaml
    /// </summary>
    public partial class FormLekoviTerapijePage : Page
    {
        public static List<PrikazRecepta> LekoviPacijenta { get; set; }

        private FileRepositoryPregled storagePregledi = new FileRepositoryPregled();
        private FileRepositoryOperacija storageOperacije = new FileRepositoryOperacija();
        private FileRepositoryAnamneza storageAnamneza = new FileRepositoryAnamneza();
        private FileRepositoryLek storageLek = new FileRepositoryLek();

        private List<Pregled> pregledi = new List<Pregled>();
        private List<Operacija> operacije = new List<Operacija>();
        private List<Anamneza> anamneze = new List<Anamneza>();
        private List<Lek> lekovi = new List<Lek>();

        public static string PrviDanSedmice
        {
            get;
            set;
        }

        public static string PoslednjiDanSedmice
        {
            get;
            set;
        }

        public static DateTime Ponedeljak
        {
            get;
            set;
        }

        public static DateTime Nedelja
        {
            get;
            set;
        }

        public static List<SedmicnaTerapija> SedmicnaTerapija
        {
            get;
            set;
        }

        public FormLekoviTerapijePage(Pacijent trenutniPacijent)
        {
            InitializeComponent();
            this.DataContext = this;

            DobijTrenutnuSedmicu();

            PopuniListe();
            NadjiPacijenta(trenutniPacijent);
        }

        private static void DobijTrenutnuSedmicu()
        {
            String danasnjiDan = DateTime.Today.DayOfWeek.ToString();
            switch (danasnjiDan)
            {
                case "Monday":
                    PrviDanSedmice = DateTime.Today.ToShortDateString();
                    PoslednjiDanSedmice = DateTime.Today.AddDays(6).ToShortDateString();
                    Ponedeljak = DateTime.Today;
                    Nedelja = DateTime.Today.AddDays(6);
                    break;
                case "Tuesday":
                    PrviDanSedmice = DateTime.Today.AddDays(-1).ToShortDateString();
                    PoslednjiDanSedmice = DateTime.Today.AddDays(5).ToShortDateString();
                    Ponedeljak = DateTime.Today.AddDays(-1);
                    Nedelja = DateTime.Today.AddDays(5);
                    break;
                case "Wednesday":
                    PrviDanSedmice = DateTime.Today.AddDays(-2).ToShortDateString();
                    PoslednjiDanSedmice = DateTime.Today.AddDays(4).ToShortDateString();
                    Ponedeljak = DateTime.Today.AddDays(-2);
                    Nedelja = DateTime.Today.AddDays(4);
                    break;
                case "Thursday":
                    PrviDanSedmice = DateTime.Today.AddDays(-3).ToShortDateString();
                    PoslednjiDanSedmice = DateTime.Today.AddDays(3).ToShortDateString();
                    Ponedeljak = DateTime.Today.AddDays(-3);
                    Nedelja = DateTime.Today.AddDays(3);
                    break;
                case "Friday":
                    PrviDanSedmice = DateTime.Today.AddDays(-4).ToShortDateString();
                    PoslednjiDanSedmice = DateTime.Today.AddDays(2).ToShortDateString();
                    Ponedeljak = DateTime.Today.AddDays(-4);
                    Nedelja = DateTime.Today.AddDays(2);
                    break;
                case "Sathurday":
                    PrviDanSedmice = DateTime.Today.AddDays(-5).ToShortDateString();
                    PoslednjiDanSedmice = DateTime.Today.AddDays(1).ToShortDateString();
                    Ponedeljak = DateTime.Today.AddDays(-5);
                    Nedelja = DateTime.Today.AddDays(1);
                    break;
                case "Sunday":
                    PrviDanSedmice = DateTime.Today.AddDays(-6).ToShortDateString();
                    PoslednjiDanSedmice = DateTime.Today.ToShortDateString();
                    Ponedeljak = DateTime.Today.AddDays(-6);
                    Nedelja = DateTime.Today;
                    break;
            }
        }

        private void PopuniListe()
        {
            SedmicnaTerapija = new List<SedmicnaTerapija>();
            SedmicnaTerapija.Add(new SedmicnaTerapija("00:00h", "", "", "", "", "", "", ""));
            SedmicnaTerapija.Add(new SedmicnaTerapija("04:00h", "", "", "", "", "", "", ""));
            SedmicnaTerapija.Add(new SedmicnaTerapija("08:00h", "", "", "", "", "", "", ""));
            SedmicnaTerapija.Add(new SedmicnaTerapija("12:00h", "", "", "", "", "", "", ""));
            SedmicnaTerapija.Add(new SedmicnaTerapija("16:00h", "", "", "", "", "", "", ""));
            SedmicnaTerapija.Add(new SedmicnaTerapija("20:00h", "", "", "", "", "", "", ""));
            LekoviPacijenta = new List<PrikazRecepta>();
            pregledi = storagePregledi.GetAll();
            operacije = storageOperacije.GetAll();
            anamneze = storageAnamneza.GetAll();
            lekovi = storageLek.GetAll();
        }

        private void NadjiPacijenta(Pacijent trenutniPacijent)
        {
            foreach (Pregled pregled in pregledi)
            {
                if (trenutniPacijent.Jmbg.Equals(pregled.Pacijent.Jmbg))
                {
                    NadjiAnamnezu(pregled);
                }
            }
            foreach (Operacija operacija in operacije)
            {
                if (trenutniPacijent.Jmbg.Equals(operacija.Pacijent.Jmbg))
                {
                    NadjiAnamnezu(operacija);
                }
            }
        }

        private void NadjiAnamnezu(Pregled pregled)
        {
            foreach (Anamneza anamneza in anamneze)
            {
                if (pregled.Anamneza.Id.Equals(anamneza.Id))
                {
                    DodajLekoveIzRecepta(anamneza);
                    break;
                }
            }
        }

        private void DodajLekoveIzRecepta(Anamneza anamneza)
        {
            foreach (SedmicnaTerapija s in SedmicnaTerapija)
            {
                if (s.Vreme.Equals("00:00h"))
                {
                    foreach (Recept recept in anamneza.Recept)
                    {
                        Lek lek = NadjiLek(recept);
                        if (recept.VremeUzimanja == 4 || recept.VremeUzimanja == 8 || recept.VremeUzimanja == 12)
                        {
                            if (recept.Trajanje.CompareTo(Nedelja) >= 0)
                            {
                                s.Ponedeljak += lek.Naziv + "\n";
                                s.Utorak += lek.Naziv + "\n";
                                s.Sreda += lek.Naziv + "\n";
                                s.Cetvrtak += lek.Naziv + "\n";
                                s.Petak += lek.Naziv + "\n";
                                s.Subota += lek.Naziv + "\n";
                                s.Nedelja += lek.Naziv + "\n";
                            }
                            else if (recept.Trajanje.CompareTo(Nedelja.AddDays(-1)) >= 0)
                            {
                                s.Ponedeljak += lek.Naziv + "\n";
                                s.Utorak += lek.Naziv + "\n";
                                s.Sreda += lek.Naziv + "\n";
                                s.Cetvrtak += lek.Naziv + "\n";
                                s.Petak += lek.Naziv + "\n";
                                s.Subota += lek.Naziv + "\n";
                            }
                            else if (recept.Trajanje.CompareTo(Nedelja.AddDays(-2)) >= 0)
                            {
                                s.Ponedeljak += lek.Naziv + "\n";
                                s.Utorak += lek.Naziv + "\n";
                                s.Sreda += lek.Naziv + "\n";
                                s.Cetvrtak += lek.Naziv + "\n";
                                s.Petak += lek.Naziv + "\n";
                            }
                            else if (recept.Trajanje.CompareTo(Nedelja.AddDays(-3)) >= 0)
                            {
                                s.Ponedeljak += lek.Naziv + "\n";
                                s.Utorak += lek.Naziv + "\n";
                                s.Sreda += lek.Naziv + "\n";
                                s.Cetvrtak += lek.Naziv + "\n";
                            }
                            else if (recept.Trajanje.CompareTo(Nedelja.AddDays(-4)) >= 0)
                            {
                                s.Ponedeljak += lek.Naziv + "\n";
                                s.Utorak += lek.Naziv + "\n";
                                s.Sreda += lek.Naziv + "\n";
                            }
                            else if (recept.Trajanje.CompareTo(Nedelja.AddDays(-5)) >= 0)
                            {
                                s.Ponedeljak += lek.Naziv + "\n";
                                s.Utorak += lek.Naziv + "\n";
                            }
                            else if (recept.Trajanje.CompareTo(Ponedeljak) >= 0)
                            {
                                s.Ponedeljak += lek.Naziv + "\n";
                            }
                        }
                    }
                }
                else if (s.Vreme.Equals("04:00h"))
                {
                    foreach (Recept recept in anamneza.Recept)
                    {
                        Lek lek = NadjiLek(recept);
                        if (recept.VremeUzimanja == 4 || recept.VremeUzimanja == 8)
                        {
                            if (recept.Trajanje.CompareTo(Nedelja) >= 0)
                            {
                                s.Ponedeljak += lek.Naziv + "\n";
                                s.Utorak += lek.Naziv + "\n";
                                s.Sreda += lek.Naziv + "\n";
                                s.Cetvrtak += lek.Naziv + "\n";
                                s.Petak += lek.Naziv + "\n";
                                s.Subota += lek.Naziv + "\n";
                                s.Nedelja += lek.Naziv + "\n";
                            }
                            else if (recept.Trajanje.CompareTo(Nedelja.AddDays(-1)) >= 0)
                            {
                                s.Ponedeljak += lek.Naziv + "\n";
                                s.Utorak += lek.Naziv + "\n";
                                s.Sreda += lek.Naziv + "\n";
                                s.Cetvrtak += lek.Naziv + "\n";
                                s.Petak += lek.Naziv + "\n";
                                s.Subota += lek.Naziv + "\n";
                            }
                            else if (recept.Trajanje.CompareTo(Nedelja.AddDays(-2)) >= 0)
                            {
                                s.Ponedeljak += lek.Naziv + "\n";
                                s.Utorak += lek.Naziv + "\n";
                                s.Sreda += lek.Naziv + "\n";
                                s.Cetvrtak += lek.Naziv + "\n";
                                s.Petak += lek.Naziv + "\n";
                            }
                            else if (recept.Trajanje.CompareTo(Nedelja.AddDays(-3)) >= 0)
                            {
                                s.Ponedeljak += lek.Naziv + "\n";
                                s.Utorak += lek.Naziv + "\n";
                                s.Sreda += lek.Naziv + "\n";
                                s.Cetvrtak += lek.Naziv + "\n";
                            }
                            else if (recept.Trajanje.CompareTo(Nedelja.AddDays(-4)) >= 0)
                            {
                                s.Ponedeljak += lek.Naziv + "\n";
                                s.Utorak += lek.Naziv + "\n";
                                s.Sreda += lek.Naziv + "\n";
                            }
                            else if (recept.Trajanje.CompareTo(Nedelja.AddDays(-5)) >= 0)
                            {
                                s.Ponedeljak += lek.Naziv + "\n";
                                s.Utorak += lek.Naziv + "\n";
                            }
                            else if (recept.Trajanje.CompareTo(Ponedeljak) >= 0)
                            {
                                s.Ponedeljak += lek.Naziv + "\n";
                            }
                        }
                    }
                }
                else if (s.Vreme.Equals("08:00h"))
                {
                    foreach (Recept recept in anamneza.Recept)
                    {
                        Lek lek = NadjiLek(recept);
                        if (recept.VremeUzimanja == 4 || recept.VremeUzimanja == 6)
                        {
                            if (recept.Trajanje.CompareTo(Nedelja) >= 0)
                            {
                                s.Ponedeljak += lek.Naziv + "\n";
                                s.Utorak += lek.Naziv + "\n";
                                s.Sreda += lek.Naziv + "\n";
                                s.Cetvrtak += lek.Naziv + "\n";
                                s.Petak += lek.Naziv + "\n";
                                s.Subota += lek.Naziv + "\n";
                                s.Nedelja += lek.Naziv + "\n";
                            }
                            else if (recept.Trajanje.CompareTo(Nedelja.AddDays(-1)) >= 0)
                            {
                                s.Ponedeljak += lek.Naziv + "\n";
                                s.Utorak += lek.Naziv + "\n";
                                s.Sreda += lek.Naziv + "\n";
                                s.Cetvrtak += lek.Naziv + "\n";
                                s.Petak += lek.Naziv + "\n";
                                s.Subota += lek.Naziv + "\n";
                            }
                            else if (recept.Trajanje.CompareTo(Nedelja.AddDays(-2)) >= 0)
                            {
                                s.Ponedeljak += lek.Naziv + "\n";
                                s.Utorak += lek.Naziv + "\n";
                                s.Sreda += lek.Naziv + "\n";
                                s.Cetvrtak += lek.Naziv + "\n";
                                s.Petak += lek.Naziv + "\n";
                            }
                            else if (recept.Trajanje.CompareTo(Nedelja.AddDays(-3)) >= 0)
                            {
                                s.Ponedeljak += lek.Naziv + "\n";
                                s.Utorak += lek.Naziv + "\n";
                                s.Sreda += lek.Naziv + "\n";
                                s.Cetvrtak += lek.Naziv + "\n";
                            }
                            else if (recept.Trajanje.CompareTo(Nedelja.AddDays(-4)) >= 0)
                            {
                                s.Ponedeljak += lek.Naziv + "\n";
                                s.Utorak += lek.Naziv + "\n";
                                s.Sreda += lek.Naziv + "\n";
                            }
                            else if (recept.Trajanje.CompareTo(Nedelja.AddDays(-5)) >= 0)
                            {
                                s.Ponedeljak += lek.Naziv + "\n";
                                s.Utorak += lek.Naziv + "\n";
                            }
                            else if (recept.Trajanje.CompareTo(Ponedeljak) >= 0)
                            {
                                s.Ponedeljak += lek.Naziv + "\n";
                            }
                        }
                    }
                }
                else if (s.Vreme.Equals("12:00h"))
                {
                    foreach (Recept recept in anamneza.Recept)
                    {
                        Lek lek = NadjiLek(recept);
                        if (recept.VremeUzimanja == 4 || recept.VremeUzimanja == 8 || recept.VremeUzimanja == 12)
                        {
                            if (recept.Trajanje.CompareTo(Nedelja) >= 0)
                            {
                                s.Ponedeljak += lek.Naziv + "\n";
                                s.Utorak += lek.Naziv + "\n";
                                s.Sreda += lek.Naziv + "\n";
                                s.Cetvrtak += lek.Naziv + "\n";
                                s.Petak += lek.Naziv + "\n";
                                s.Subota += lek.Naziv + "\n";
                                s.Nedelja += lek.Naziv + "\n";
                            }
                            else if (recept.Trajanje.CompareTo(Nedelja.AddDays(-1)) >= 0)
                            {
                                s.Ponedeljak += lek.Naziv + "\n";
                                s.Utorak += lek.Naziv + "\n";
                                s.Sreda += lek.Naziv + "\n";
                                s.Cetvrtak += lek.Naziv + "\n";
                                s.Petak += lek.Naziv + "\n";
                                s.Subota += lek.Naziv + "\n";
                            }
                            else if (recept.Trajanje.CompareTo(Nedelja.AddDays(-2)) >= 0)
                            {
                                s.Ponedeljak += lek.Naziv + "\n";
                                s.Utorak += lek.Naziv + "\n";
                                s.Sreda += lek.Naziv + "\n";
                                s.Cetvrtak += lek.Naziv + "\n";
                                s.Petak += lek.Naziv + "\n";
                            }
                            else if (recept.Trajanje.CompareTo(Nedelja.AddDays(-3)) >= 0)
                            {
                                s.Ponedeljak += lek.Naziv + "\n";
                                s.Utorak += lek.Naziv + "\n";
                                s.Sreda += lek.Naziv + "\n";
                                s.Cetvrtak += lek.Naziv + "\n";
                            }
                            else if (recept.Trajanje.CompareTo(Nedelja.AddDays(-4)) >= 0)
                            {
                                s.Ponedeljak += lek.Naziv + "\n";
                                s.Utorak += lek.Naziv + "\n";
                                s.Sreda += lek.Naziv + "\n";
                            }
                            else if (recept.Trajanje.CompareTo(Nedelja.AddDays(-5)) >= 0)
                            {
                                s.Ponedeljak += lek.Naziv + "\n";
                                s.Utorak += lek.Naziv + "\n";
                            }
                            else if (recept.Trajanje.CompareTo(Ponedeljak) >= 0)
                            {
                                s.Ponedeljak += lek.Naziv + "\n";
                            }
                        }
                    }
                }
                else if (s.Vreme.Equals("16:00h"))
                {
                    foreach (Recept recept in anamneza.Recept)
                    {
                        Lek lek = NadjiLek(recept);
                        if (recept.VremeUzimanja == 4 || recept.VremeUzimanja == 6 || recept.VremeUzimanja == 24)
                        {
                            if (recept.Trajanje.CompareTo(Nedelja) >= 0)
                            {
                                s.Ponedeljak += lek.Naziv + "\n";
                                s.Utorak += lek.Naziv + "\n";
                                s.Sreda += lek.Naziv + "\n";
                                s.Cetvrtak += lek.Naziv + "\n";
                                s.Petak += lek.Naziv + "\n";
                                s.Subota += lek.Naziv + "\n";
                                s.Nedelja += lek.Naziv + "\n";
                            }
                            else if (recept.Trajanje.CompareTo(Nedelja.AddDays(-1)) >= 0)
                            {
                                s.Ponedeljak += lek.Naziv + "\n";
                                s.Utorak += lek.Naziv + "\n";
                                s.Sreda += lek.Naziv + "\n";
                                s.Cetvrtak += lek.Naziv + "\n";
                                s.Petak += lek.Naziv + "\n";
                                s.Subota += lek.Naziv + "\n";
                            }
                            else if (recept.Trajanje.CompareTo(Nedelja.AddDays(-2)) >= 0)
                            {
                                s.Ponedeljak += lek.Naziv + "\n";
                                s.Utorak += lek.Naziv + "\n";
                                s.Sreda += lek.Naziv + "\n";
                                s.Cetvrtak += lek.Naziv + "\n";
                                s.Petak += lek.Naziv + "\n";
                            }
                            else if (recept.Trajanje.CompareTo(Nedelja.AddDays(-3)) >= 0)
                            {
                                s.Ponedeljak += lek.Naziv + "\n";
                                s.Utorak += lek.Naziv + "\n";
                                s.Sreda += lek.Naziv + "\n";
                                s.Cetvrtak += lek.Naziv + "\n";
                            }
                            else if (recept.Trajanje.CompareTo(Nedelja.AddDays(-4)) >= 0)
                            {
                                s.Ponedeljak += lek.Naziv + "\n";
                                s.Utorak += lek.Naziv + "\n";
                                s.Sreda += lek.Naziv + "\n";
                            }
                            else if (recept.Trajanje.CompareTo(Nedelja.AddDays(-5)) >= 0)
                            {
                                s.Ponedeljak += lek.Naziv + "\n";
                                s.Utorak += lek.Naziv + "\n";
                            }
                            else if (recept.Trajanje.CompareTo(Ponedeljak) >= 0)
                            {
                                s.Ponedeljak += lek.Naziv + "\n";
                            }
                        }
                    }
                }
                else if (s.Vreme.Equals("20:00h"))
                {
                    foreach (Recept recept in anamneza.Recept)
                    {
                        Lek lek = NadjiLek(recept);
                        if (recept.VremeUzimanja == 4 || recept.VremeUzimanja == 8)
                        {
                            if (recept.Trajanje.CompareTo(Nedelja) >= 0)
                            {
                                s.Ponedeljak += lek.Naziv + "\n";
                                s.Utorak += lek.Naziv + "\n";
                                s.Sreda += lek.Naziv + "\n";
                                s.Cetvrtak += lek.Naziv + "\n";
                                s.Petak += lek.Naziv + "\n";
                                s.Subota += lek.Naziv + "\n";
                                s.Nedelja += lek.Naziv + "\n";
                            }
                            else if (recept.Trajanje.CompareTo(Nedelja.AddDays(-1)) >= 0)
                            {
                                s.Ponedeljak += lek.Naziv + "\n";
                                s.Utorak += lek.Naziv + "\n";
                                s.Sreda += lek.Naziv + "\n";
                                s.Cetvrtak += lek.Naziv + "\n";
                                s.Petak += lek.Naziv + "\n";
                                s.Subota += lek.Naziv + "\n";
                            }
                            else if (recept.Trajanje.CompareTo(Nedelja.AddDays(-2)) >= 0)
                            {
                                s.Ponedeljak += lek.Naziv + "\n";
                                s.Utorak += lek.Naziv + "\n";
                                s.Sreda += lek.Naziv + "\n";
                                s.Cetvrtak += lek.Naziv + "\n";
                                s.Petak += lek.Naziv + "\n";
                            }
                            else if (recept.Trajanje.CompareTo(Nedelja.AddDays(-3)) >= 0)
                            {
                                s.Ponedeljak += lek.Naziv + "\n";
                                s.Utorak += lek.Naziv + "\n";
                                s.Sreda += lek.Naziv + "\n";
                                s.Cetvrtak += lek.Naziv + "\n";
                            }
                            else if (recept.Trajanje.CompareTo(Nedelja.AddDays(-4)) >= 0)
                            {
                                s.Ponedeljak += lek.Naziv + "\n";
                                s.Utorak += lek.Naziv + "\n";
                                s.Sreda += lek.Naziv + "\n";
                            }
                            else if (recept.Trajanje.CompareTo(Nedelja.AddDays(-5)) >= 0)
                            {
                                s.Ponedeljak += lek.Naziv + "\n";
                                s.Utorak += lek.Naziv + "\n";
                            }
                            else if (recept.Trajanje.CompareTo(Ponedeljak) >= 0)
                            {
                                s.Ponedeljak += lek.Naziv + "\n";
                            }
                        }
                    }
                }
            }
            foreach (Recept recept in anamneza.Recept)
            {
                Lek lek = NadjiLek(recept);
                PrikazRecepta pr = new PrikazRecepta(lek, recept.DatumIzdavanja, recept.Trajanje, recept.Kolicina, recept.VremeUzimanja);
                LekoviPacijenta.Add(pr);
            }
        }

        private Lek NadjiLek(Recept recept)
        {
            foreach (Lek lek in lekovi)
            {
                if (recept.Lek.Id.Equals(lek.Id))
                {
                    return lek;
                }
            }
            return null;
        }

        private void Button_Click_Stampaj(object sender, RoutedEventArgs e)
        {
            try
            {
                this.IsEnabled = false;
                PrintDialog printDialog = new PrintDialog();
                if (printDialog.ShowDialog() == true)
                {
                    printDialog.PrintVisual(Izvestaj, "Izveštaj");
                }
            }
            finally
            {
                this.IsEnabled = true;
            }
        }
    }
}
