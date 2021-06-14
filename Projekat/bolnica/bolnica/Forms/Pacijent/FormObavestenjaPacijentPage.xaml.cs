using Bolnica.Controller;
using Bolnica.Model.Korisnici;
using Model.Korisnici;
using Model.Pregledi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace Bolnica.Forms
{
    /// <summary>
    /// Interaction logic for FormObavestenjaPacijentPage.xaml
    /// </summary>
    public partial class FormObavestenjaPacijentPage : Page
    {
        public static ObservableCollection<Obavestenje> ObavestenjaPacijent
        {
            get;
            set;
        }
        public static string DanasnjiDatum
        {
            get;
            set;
        }
        public static List<Obavestenje> Obavestenja { get; set; }

        private ObavestenjaPacijentController obavestenjaPacijentController = new ObavestenjaPacijentController();


        public FormObavestenjaPacijentPage(Pacijent trenutniPacijent)
        {
            InitializeComponent();
            this.DataContext = this;
            PostaviDanasnjiDatum();

            obavestenjaPacijentController.DobijObavestenjaBolnice(trenutniPacijent);
            obavestenjaPacijentController.DobijObavestenjaOLekovima(trenutniPacijent);
        }

        private static void PostaviDanasnjiDatum()
        {
            DanasnjiDatum = "Obaveštenja o lekovima za dan " + DateTime.Today.ToShortDateString();
        }
    }
}
