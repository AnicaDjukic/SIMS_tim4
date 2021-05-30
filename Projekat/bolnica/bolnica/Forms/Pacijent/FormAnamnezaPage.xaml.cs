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
        public Anamneza Anamneza { get; set; }
        public static List<PrikazRecepta> LekoviPacijenta { get; set; }
        public Beleska Beleska { get; set; }
        public List<TimeSpan> Vremena { get; set; }
        public List<DateTime> Datumi { get; set; }

        private FileStorageBeleska storageBeleska = new FileStorageBeleska();
        private FileStorageAnamneza storageAnamneza = new FileStorageAnamneza();
        private FileStorageLek storageLek = new FileStorageLek();

        private List<Beleska> beleske = new List<Beleska>();
        private List<Anamneza> anamneze = new List<Anamneza>();
        private List<Lek> lekovi = new List<Lek>();

        private PrikazPregleda prikaz = new PrikazPregleda();
        
        public FormAnamnezaPage(PrikazPregleda prikazPregleda)
        {
            InitializeComponent();
            this.DataContext = this;

            prikaz = prikazPregleda;
            PostaviVremeComboBox();
            PostaviDatumComboBox();
            Anamneza = DobijAnamnezu(prikazPregleda);
            Beleska = DobijBelesku();
            lekovi = DobijLekove();
            PostaviSveLekovePacijentu();
        }

       
        private void PostaviVremeComboBox()
        {
            Vremena = new List<TimeSpan>();
            for (int i = 0; i < 24; i++)
            {
                Vremena.Add(new TimeSpan(i,0,0));
                Vremena.Add(new TimeSpan(i, 30, 0));
            }
        }

        private void PostaviDatumComboBox()
        {
            Datumi = new List<DateTime>();
            DateTime danas = DateTime.Today;
            for (int i = 0; i < 10; i++)
            {
                Datumi.Add(danas.AddDays(i+1));
            }
        }

        private Beleska DobijBelesku()
        {
            beleske = storageBeleska.GetAll();
            foreach (Beleska beleska in beleske)
            {
                if (Anamneza.Beleska.Id.Equals(beleska.Id))
                {
                    return beleska;
                }
            }
            return null;
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

        private void Button_Click_Sacuvaj_Belesku(object sender, RoutedEventArgs e)
        {
            if (beleskaTekst.Text.Equals(""))
            {
                MessageBox.Show("Morate uneti belesku da bi je sacuvali!", "Upozorenje");
            }
            else
            {
                Beleska novaBeleska = DobijNovuBelesku();
                SacuvajNovuBelesku(novaBeleska);
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
            return DobijAnamnezu(prikaz).Beleska.Id;
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

        private void SacuvajNovuBelesku(Beleska novaBeleska)
        {
            bool izmenjen = false;
            foreach (Beleska beleska in beleske)
            {
                if (novaBeleska.Id.Equals(beleska.Id))
                {
                    storageBeleska.Izmeni(novaBeleska);
                    izmenjen = true;
                    MessageBox.Show("Beleska uspesno izmenjena.");
                    break;
                }
            }
            if (!izmenjen)
            {
                novaBeleska.Id = IzracunajIdBeleske();
                storageBeleska.Save(novaBeleska);
                Anamneza novaAnamneza = DobijAnamnezu(prikaz);
                novaAnamneza.Beleska.Id = novaBeleska.Id;
                storageAnamneza.Izmeni(novaAnamneza);
                MessageBox.Show("Beleska uspesno napravljena.");
            }
        }

        private int IzracunajIdBeleske()
        {
            beleske = storageBeleska.GetAll();
            int max = 0;
            foreach (Beleska beleska in beleske)
            {
                if (beleska.Id > max)
                {
                    max = beleska.Id;
                }
            }
            return max + 1;
        }

    }
}
