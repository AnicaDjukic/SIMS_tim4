using Bolnica.Model.Korisnici;
using Bolnica.Model.Pregledi;
using Model.Korisnici;
using Model.Pregledi;
using Model.Prostorije;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for FormIstorijaPregledaPage.xaml
    /// </summary>
    public partial class FormIstorijaPregledaPage : Page
    {
        private Pacijent trenutniPacijent = new Pacijent();
        private FileStoragePregledi storage;
        public static ObservableCollection<PrikazPregleda> PrikazZavrsenihPregleda
        {
            get;
            set;
        }

        private FileStorageLekar storageLekari = new FileStorageLekar();
        private List<Lekar> lekari = new List<Lekar>();
        private List<Prostorija> prostorije = new List<Prostorija>();

        private FormPacijentWeb form;

        public FormIstorijaPregledaPage(Pacijent pacijent, FormPacijentWeb formPacijentWeb)
        {
            InitializeComponent();

            form = formPacijentWeb;
            trenutniPacijent = pacijent;

            this.DataContext = this;

            PrikazZavrsenihPregleda = new ObservableCollection<PrikazPregleda>();

            lekari = storageLekari.GetAll();

            FileStorageProstorija storageProstorije = new FileStorageProstorija();
            prostorije = storageProstorije.GetAllProstorije();

            storage = new FileStoragePregledi();

            List<Pregled> pregledi = storage.GetAllPregledi();
            foreach (Pregled p in pregledi)
            {
                if (p.Pacijent.Jmbg.Equals(pacijent.Jmbg))
                {
                    if (!pacijent.Obrisan && !pacijent.Guest && p.Zavrsen)
                    {
                        PrikazPregleda prikaz = new PrikazPregleda();
                        prikaz.Datum = p.Datum;
                        prikaz.Trajanje = p.Trajanje;
                        prikaz.Zavrsen = p.Zavrsen;
                        prikaz.Hitan = p.Hitan;
                        prikaz.Id = p.Id;
                        prikaz.Anamneza = p.Anamneza;

                        prikaz.Pacijent = pacijent;

                        foreach (Lekar l in lekari)
                        {
                            if (p.Lekar.Jmbg.Equals(l.Jmbg))
                            {
                                prikaz.Lekar = l;
                            }
                        }
                        
                        foreach (Prostorija pro in prostorije)
                        {
                            if (p.Prostorija.BrojProstorije.Equals(pro.BrojProstorije))
                            {
                                prikaz.Prostorija = pro;
                            }
                        }

                        PrikazZavrsenihPregleda.Add(prikaz);
                    }
                }
            }
            List<Operacija> operacije = storage.GetAllOperacije();
            foreach (Operacija o in operacije)
            {
                if (o.Pacijent.Jmbg.Equals(pacijent.Jmbg))
                {
                    if (!pacijent.Obrisan && !pacijent.Guest && o.Zavrsen)
                    {
                        PrikazOperacije prikaz = new PrikazOperacije();
                        prikaz.Datum = o.Datum;
                        prikaz.Trajanje = o.Trajanje;
                        prikaz.Zavrsen = o.Zavrsen;
                        prikaz.Hitan = o.Hitan;
                        prikaz.Id = o.Id;
                        prikaz.Anamneza = o.Anamneza;
                        prikaz.TipOperacije = o.TipOperacije;

                        prikaz.Pacijent = pacijent;

                        foreach (Lekar l in lekari)
                        {
                            if (o.Lekar.Jmbg.Equals(l.Jmbg))
                            {
                                prikaz.Lekar = l;
                            }
                        }

                        foreach (Prostorija pro in prostorije)
                        {
                            if (o.Prostorija.BrojProstorije.Equals(pro.BrojProstorije))
                            {
                                prikaz.Prostorija = pro;
                            }
                        }

                        PrikazZavrsenihPregleda.Add(prikaz);
                    }
                }
            }
        }

        private void Button_Click_Oceni_Lekara(object sender, RoutedEventArgs e)
        {
            var objekat = pacijentIstorijaGrid.SelectedValue;

            if (objekat != null)
            {
                PrikazPregleda p = (PrikazPregleda)pacijentIstorijaGrid.SelectedItem;
                form.Pocetna.Content = new FormOceniLekaraPage(p, form);
            }
            else
            {
                MessageBox.Show("Morate odabrati pregled kod odredjenog lekara koga zelite da ocenite!", "Upozorenje");
            }
        }

        private void Button_Click_Oceni_Bolnicu(object sender, RoutedEventArgs e)
        {
            form.Pocetna.Content = new FormOceniBolnicuPage(trenutniPacijent, form);
        }

        private void Button_Click_Istorija_Ocena_I_Komentara(object sender, RoutedEventArgs e)
        {
            form.Pocetna.Content = new FormIstorijaOcenaPage(trenutniPacijent, form);
        }
    }
}
