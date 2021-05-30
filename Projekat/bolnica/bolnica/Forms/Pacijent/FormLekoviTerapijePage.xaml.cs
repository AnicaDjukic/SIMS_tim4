using Bolnica.Model.Pregledi;
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

        private FileStoragePregledi storagePregledi = new FileStoragePregledi();
        private FileStorageAnamneza storageAnamneza = new FileStorageAnamneza();
        private FileStorageLek storageLek = new FileStorageLek();

        private List<Pregled> pregledi = new List<Pregled>();
        private List<Operacija> operacije = new List<Operacija>();
        private List<Anamneza> anamneze = new List<Anamneza>();
        private List<Lek> lekovi = new List<Lek>();

        public FormLekoviTerapijePage(Pacijent trenutniPacijent)
        {
            InitializeComponent();
            this.DataContext = this;

            PopuniListe();
            NadjiPacijenta(trenutniPacijent);
        }

        private void PopuniListe()
        {
            LekoviPacijenta = new List<PrikazRecepta>();
            pregledi = storagePregledi.GetAllPregledi();
            operacije = storagePregledi.GetAllOperacije();
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

        private void Button_Click_Vidi_Detalje_Leka(object sender, RoutedEventArgs e)
        {
            var objekat = lekoviPacijenta.SelectedValue;

            if (objekat != null)
            {
                PrikazRecepta prikazRecepta = (PrikazRecepta)lekoviPacijenta.SelectedItem;
                FormPacijentWeb.Forma.Pocetna.Content = new FormLekDetaljiPage(prikazRecepta);
            }
            else
            {
                MessageBox.Show("Morate odabrati lek za koji zelite da vidite detalje!", "Upozorenje");
            }
        }

        private void Button_Click_Vidi_Detalje_Terapije(object sender, RoutedEventArgs e)
        {

        }
    }
}
