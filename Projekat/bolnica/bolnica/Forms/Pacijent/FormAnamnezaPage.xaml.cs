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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Bolnica.Forms
{
    /// <summary>
    /// Interaction logic for FormAnamnezaPage.xaml
    /// </summary>
    public partial class FormAnamnezaPage : Page
    {
        private FileStorageLek storageLek = new FileStorageLek();
        private FileStorageAnamneza storageAnamneza = new FileStorageAnamneza();

        private List<Anamneza> anamneze = new List<Anamneza>();
        private List<Lek> lekovi = new List<Lek>();

        public Anamneza Anamneza { get; set; }
        public static List<PrikazRecepta> LekoviPacijenta { get; set; }

        public FormAnamnezaPage(PrikazPregleda prikazPregleda)
        {
            InitializeComponent();
            this.DataContext = this;

            Anamneza = DobijAnamnezu(prikazPregleda);
            lekovi = DobijLekove();
            PostaviSveLekovePacijentu();
        }

        private Anamneza DobijAnamnezu(PrikazPregleda prikazPregleda)
        {
            anamneze = storageAnamneza.GetAll();
            foreach (Anamneza a in anamneze)
            {
                if (a.Id.Equals(prikazPregleda.Anamneza.Id))
                {
                    return a;
                }
            }
            return null;
        }

        private List<Lek> DobijLekove()
        {
            return storageLek.GetAll();
        }

        private void PostaviSveLekovePacijentu()
        {
            LekoviPacijenta = new List<PrikazRecepta>();
            foreach (Recept r in Anamneza.Recept)
            {
                DodajLekPacijentu(r);
            }
        }
        
        private void DodajLekPacijentu(Recept recept)
        {
            foreach (Lek lek in lekovi)
            {
                if (recept.Lek.Id.Equals(lek.Id))
                {
                    PrikazRecepta pr = new PrikazRecepta(lek, recept.DatumIzdavanja, recept.Trajanje, recept.Kolicina, recept.VremeUzimanja);
                    LekoviPacijenta.Add(pr);
                }
            }
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
    }
}
