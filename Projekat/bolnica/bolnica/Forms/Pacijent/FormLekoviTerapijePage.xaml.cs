using Bolnica.Controller;
using Bolnica.Model.Pregledi;
using Model.Korisnici;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Bolnica.Forms
{
    /// <summary>
    /// Interaction logic for FormLekoviTerapijePage.xaml
    /// </summary>
    public partial class FormLekoviTerapijePage : Page
    {
        private TerapijaPacijentController terapijaPacijentController = new TerapijaPacijentController();

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

            terapijaPacijentController.DobijTrenutnuSedmicu();
            terapijaPacijentController.InicijalizujSedmicnuTerapiju();
            terapijaPacijentController.PopuniTerapijuPacijenta(trenutniPacijent);
        }

        private void Button_Click_Stampaj(object sender, RoutedEventArgs e)
        {
            try
            {
                IsEnabled = false;
                PrintDialog printDialog = new PrintDialog();
                if (printDialog.ShowDialog() == true)
                {
                    printDialog.PrintVisual(Izvestaj, "Izveštaj");
                }
            }
            finally
            {
                IsEnabled = true;
            }
        }
    }
}
