using Bolnica.Controller;
using Bolnica.Model.Pregledi;
using Model.Pregledi;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Bolnica.Forms
{
    /// <summary>
    /// Interaction logic for FormAnamnezaPage.xaml
    /// </summary>
    public partial class FormAnamnezaPage : Page
    {
        private PrikazPregleda prikaz = new PrikazPregleda();
        public Anamneza Anamneza { get; set; }
        public static List<PrikazRecepta> LekoviPacijenta { get; set; }
        public Beleska Beleska { get; set; }
        public static List<TimeSpan> Vremena { get; set; }
        public static List<DateTime> Datumi { get; set; }

        private RepositoryController repositoryController = new RepositoryController();
        private AnamnezaPacijentController anamnezaPacijentController = new AnamnezaPacijentController();
        
        public FormAnamnezaPage(PrikazPregleda prikazPregleda)
        {
            InitializeComponent();
            this.DataContext = this;
            prikaz = prikazPregleda;

            anamnezaPacijentController.PostaviVremeComboBox();
            anamnezaPacijentController.PostaviDatumComboBox();
            Anamneza = anamnezaPacijentController.DobijAnamnezu(prikazPregleda);
            Beleska = anamnezaPacijentController.DobijBelesku(Anamneza);
            anamnezaPacijentController.PostaviSveLekovePacijentu(Anamneza);
        }

        private void Button_Click_Vidi_Detalje(object sender, RoutedEventArgs e)
        {
            var objekat = lekoviPacijentaGrid.SelectedValue;

            if (objekat != null)
            {
                PrikazRecepta prikazRecepta = (PrikazRecepta)lekoviPacijentaGrid.SelectedItem;
                FormPacijentWeb.Forma.Pocetna.Content = new FormLekDetaljiPage(prikazRecepta);
            }
            else
            {
                MessageBox.Show("Morate odabrati lek za koji zelite da vidite detalje!", "Upozorenje");
            }
        }

        private void Button_Click_Sacuvaj_Belesku(object sender, RoutedEventArgs e)
        {
            if (beleskaTekst.Text.Equals(""))
            {
                MessageBox.Show("Morate uneti belesku da bi je sacuvali!", "Upozorenje");
            }
            else
            {
                Beleska novaBeleska = DobijNovuBelesku();
                anamnezaPacijentController.SacuvajNovuBelesku(novaBeleska, prikaz);
            }
        }

        private Beleska DobijNovuBelesku()
        {
            return new Beleska
            {
                Id = DobijId(),
                Zabeleska = beleskaTekst.Text,
                Podsetnik = PodsetnikUkljucen(),
                Vreme = DobijVreme(),
                DatumPrekida = DobijDatum(),
                Prikazana = false
            };
        }

        private int DobijId()
        {
            return anamnezaPacijentController.DobijAnamnezu(prikaz).Beleska.Id;
        }

        private bool PodsetnikUkljucen()
        {
            if (comboVreme.SelectedItem is null && comboDatumPrekida.SelectedItem is null)
            {
                return false;
            }
            return true;
        }

        private TimeSpan DobijVreme()
        {
            if (comboVreme.SelectedItem is null)
            {
                return TimeSpan.Zero;
            }
            return (TimeSpan)comboVreme.SelectedItem;
        }

        private DateTime DobijDatum()
        {
            if (comboDatumPrekida.SelectedItem is null)
            {
                return DateTime.Today.AddDays(1);
            }
            return (DateTime)comboDatumPrekida.SelectedItem;
        }
    }
}
