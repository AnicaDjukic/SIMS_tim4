using Bolnica.Model.Korisnici;
using Bolnica.Model.Pregledi;
using Bolnica.Sekretar;
using Model.Korisnici;
using Model.Pacijenti;
using Model.Pregledi;
using Model.Prostorije;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Bolnica.Forms.Sekretar
{
    /// <summary>
    /// Interaction logic for FormPomeranjeTermina.xaml
    /// </summary>
    public partial class FormPomeranjeTermina : Window
    {
        public static List<Pregled> listaPregleda = new List<Pregled>();
        public static List<Operacija> listaOperacija = new List<Operacija>();
        public static ObservableCollection<PrikazPregleda> Pregledi { get; set; }
        public static List<Lekar> listaLekara = new List<Lekar>();
        private FileStoragePregledi sviPregledi = new FileStoragePregledi();
        private FileStoragePregledi sveOperacije = new FileStoragePregledi();
        private FileStoragePacijenti sviPacijenti = new FileStoragePacijenti();
        private FileStorageProstorija sveProstorije = new FileStorageProstorija();
        private FileStorageLekar sviLekari = new FileStorageLekar();
        private List<Pacijent> listaPacijenata = new List<Pacijent>();
        private List<Prostorija> listaProstorija = new List<Prostorija>();
        private PrikazPregleda prikazPregleda = new PrikazPregleda();
        private PrikazOperacije prikazOperacije = new PrikazOperacije();
        private List<Pregled> listaPregledaUTerminuHitnog = new List<Pregled>();
        private List<Operacija> listaOperacijaUTerminuHitnog = new List<Operacija>();
        List<PrikazPregleda> pomereniPregledi = new List<PrikazPregleda>();
        List<PrikazOperacije> pomereneOperacije = new List<PrikazOperacije>();
        private int trajanjeHitnogTermina;
        private Pacijent pacijentHitnogTermina = new Pacijent();
        private string specijalizacijaHitnogTermina;
        TipOperacije tipOperacijeHitnogTermina = new TipOperacije();
        private bool hitnaOperacija;
        private Window zakaziHitanTermin = new Window();
        private DateTime datumHitnogTermina = new DateTime();
        private DataGrid dataGrid;

        public FormPomeranjeTermina(DataGrid dg, Window w, DateTime dt, bool op, int t, Pacijent pac, string s, TipOperacije to)
        {
            InitializeComponent();
            dataGridPregledi.DataContext = this;
            Pregledi = new ObservableCollection<PrikazPregleda>();
            dataGrid = dg;

            zakaziHitanTermin = w;
            hitnaOperacija = op;
            trajanjeHitnogTermina = t;
            datumHitnogTermina = dt;
            pacijentHitnogTermina = pac;
            specijalizacijaHitnogTermina = s;
            tipOperacijeHitnogTermina = to;
            listaPregleda = sviPregledi.GetAllPregledi();
            listaOperacija = sveOperacije.GetAllOperacije();
            listaPacijenata = sviPacijenti.GetAll();
            listaProstorija = sveProstorije.GetAllProstorije();
            listaLekara = sviLekari.GetAll();
            List<TimeSpan> ts = new List<TimeSpan>();
            List<double> razlikaDatuma = new List<double>();

            for (int i = 0; i < listaPregleda.Count; i++)
                if (listaPregleda[i].Zavrsen.Equals(false) && ((DateTime.Compare(listaPregleda[i].Datum, datumHitnogTermina) <= 0 && DateTime.Compare(listaPregleda[i].Datum.AddMinutes(listaPregleda[i].Trajanje), datumHitnogTermina) > 0) || (DateTime.Compare(listaPregleda[i].Datum, datumHitnogTermina) >= 0 && DateTime.Compare(listaPregleda[i].Datum, datumHitnogTermina.AddMinutes(trajanjeHitnogTermina)) < 0)))
                    listaPregledaUTerminuHitnog.Add(listaPregleda[i]);

            for (int i = 0; i < listaOperacija.Count; i++)
                if (listaOperacija[i].Zavrsen.Equals(false) && ((DateTime.Compare(listaOperacija[i].Datum, datumHitnogTermina) <= 0 && DateTime.Compare(listaOperacija[i].Datum.AddMinutes(listaOperacija[i].Trajanje), datumHitnogTermina) > 0) || (DateTime.Compare(listaOperacija[i].Datum, datumHitnogTermina) >= 0 && DateTime.Compare(listaOperacija[i].Datum, datumHitnogTermina.AddMinutes(trajanjeHitnogTermina)) < 0)))
                    listaOperacijaUTerminuHitnog.Add(listaOperacija[i]);

            bool zauzet;

            for (int i = 0; i < listaPregledaUTerminuHitnog.Count; i++)
            {
                DateTime datum = datumHitnogTermina;
                if (trajanjeHitnogTermina % 15 == 0)
                    datum = datum.AddMinutes(trajanjeHitnogTermina);
                else
                    datum = datum.AddMinutes((trajanjeHitnogTermina / 15 + 1) * 15);
                do
                {
                    zauzet = false;
                    foreach (Pregled p in listaPregleda)
                    {
                        if (p.Id == listaPregledaUTerminuHitnog[i].Id)
                            continue;

                        if (DateTime.Compare(p.Datum.AddMinutes(p.Trajanje), datum) <= 0 || DateTime.Compare(p.Datum, datum.AddMinutes(listaPregledaUTerminuHitnog[i].Trajanje)) >= 0)
                            continue;
                        else if (listaPregledaUTerminuHitnog[i].brojProstorije == p.brojProstorije || listaPregledaUTerminuHitnog[i].lekarJmbg.Equals(p.lekarJmbg) || listaPregledaUTerminuHitnog[i].pacijentJmbg.Equals(p.pacijentJmbg))
                            zauzet = true;
                    }

                    foreach (Operacija o in listaOperacija)
                        if (DateTime.Compare(o.Datum.AddMinutes(o.Trajanje), datum) <= 0 || DateTime.Compare(o.Datum, datum.AddMinutes(listaOperacijaUTerminuHitnog[i].Trajanje)) >= 0)
                            continue;
                        else if (listaOperacijaUTerminuHitnog[i].brojProstorije == o.brojProstorije || listaOperacijaUTerminuHitnog[i].lekarJmbg.Equals(o.lekarJmbg) || listaOperacijaUTerminuHitnog[i].pacijentJmbg.Equals(o.pacijentJmbg))
                            zauzet = true;

                    if (zauzet)
                        datum = datum.AddMinutes(15);
                    else
                        ts.Add(datum - listaPregledaUTerminuHitnog[i].Datum);

                } while (zauzet);
            }

            for (int i = 0; i < listaOperacijaUTerminuHitnog.Count; i++)
            {
                DateTime datum = datumHitnogTermina;
                if (trajanjeHitnogTermina % 15 == 0)
                    datum = datum.AddMinutes(trajanjeHitnogTermina);
                else
                    datum = datum.AddMinutes((trajanjeHitnogTermina / 15 + 1) * 15);
                do
                {
                    zauzet = false;
                    foreach (Pregled p in listaPregleda)
                        if (DateTime.Compare(p.Datum.AddMinutes(p.Trajanje), datum) <= 0 || DateTime.Compare(p.Datum, datum.AddMinutes(listaOperacijaUTerminuHitnog[i].Trajanje)) >= 0)
                            continue;
                        else if (listaOperacijaUTerminuHitnog[i].brojProstorije == p.brojProstorije || listaOperacijaUTerminuHitnog[i].lekarJmbg.Equals(p.lekarJmbg) || listaOperacijaUTerminuHitnog[i].pacijentJmbg.Equals(p.pacijentJmbg))
                            zauzet = true;

                    foreach (Operacija o in listaOperacija)
                    {
                        if (o.Id == listaOperacijaUTerminuHitnog[i].Id)
                            continue;

                        if (DateTime.Compare(o.Datum.AddMinutes(o.Trajanje), datum) <= 0 || DateTime.Compare(o.Datum, datum.AddMinutes(listaOperacijaUTerminuHitnog[i].Trajanje)) >= 0)
                            continue;
                        else if (listaOperacijaUTerminuHitnog[i].brojProstorije == o.brojProstorije || listaOperacijaUTerminuHitnog[i].lekarJmbg.Equals(o.lekarJmbg) || listaOperacijaUTerminuHitnog[i].pacijentJmbg.Equals(o.pacijentJmbg))
                            zauzet = true;
                    }
                    
                    if (zauzet)
                        datum = datum.AddMinutes(15);
                    else
                        ts.Add(datum - listaOperacijaUTerminuHitnog[i].Datum);
     
                } while (zauzet);
            }

            foreach (TimeSpan timespan in ts)
                razlikaDatuma.Add(timespan.TotalMinutes);

            List<Pregled> lista1 = new List<Pregled>();
            List<Operacija> lista2 = new List<Operacija>();

            foreach (Pregled p in listaPregledaUTerminuHitnog)
                lista1.Add(p);

            foreach (Operacija o in listaOperacijaUTerminuHitnog)
                lista2.Add(o);

            for (int i = 0; i < listaPregledaUTerminuHitnog.Count + listaOperacijaUTerminuHitnog.Count; i++) 
            {
                int najveciIndeks = razlikaDatuma.IndexOf(razlikaDatuma.Min());

                if (najveciIndeks >= 0 && najveciIndeks < lista1.Count)
                {
                    prikazPregleda = new PrikazPregleda();
                    prikazPregleda.Id = lista1[najveciIndeks].Id;
                    prikazPregleda.Trajanje = lista1[najveciIndeks].Trajanje;
                    prikazPregleda.Zavrsen = lista1[najveciIndeks].Zavrsen;
                    prikazPregleda.Datum = lista1[najveciIndeks].Datum;
                    prikazPregleda.AnamnezaId = lista1[najveciIndeks].AnamnezaId;
                    prikazPregleda.Hitan = lista1[najveciIndeks].Hitan;
                    for (int p = 0; p < listaPacijenata.Count; p++)
                    {
                        if (lista1[najveciIndeks].pacijentJmbg.Equals(listaPacijenata[p].Jmbg) && listaPacijenata[p].Obrisan == false)
                        {
                            prikazPregleda.Pacijent = listaPacijenata[p];
                            break;
                        }
                    }
                    for (int p = 0; p < listaProstorija.Count; p++)
                    {
                        if (lista1[najveciIndeks].brojProstorije.Equals(listaProstorija[p].BrojProstorije) && listaProstorija[p].Obrisana == false)
                        {
                            prikazPregleda.Prostorija = listaProstorija[p];
                            break;
                        }
                    }
                    for (int p = 0; p < listaLekara.Count; p++)
                    {
                        if (lista1[najveciIndeks].lekarJmbg.Equals(listaLekara[p].Jmbg))
                        {
                            prikazPregleda.Lekar = listaLekara[p];
                        }
                    }
                    lista1.RemoveAt(najveciIndeks);
                    razlikaDatuma.RemoveAt(najveciIndeks);
                    Pregledi.Add(prikazPregleda);
                }
                else
                {
                    int indeks = najveciIndeks - lista1.Count();

                    prikazOperacije = new PrikazOperacije();
                    prikazOperacije.Id = lista2[indeks].Id;
                    prikazOperacije.Trajanje = lista2[indeks].Trajanje;
                    prikazOperacije.Zavrsen = lista2[indeks].Zavrsen;
                    prikazOperacije.Datum = lista2[indeks].Datum;
                    prikazOperacije.AnamnezaId = lista2[indeks].AnamnezaId;
                    prikazOperacije.Hitan = lista2[indeks].Hitan;
                    prikazOperacije.TipOperacije = lista2[indeks].TipOperacije;
                    for (int p = 0; p < listaPacijenata.Count; p++)
                    {
                        if (lista2[indeks].pacijentJmbg.Equals(listaPacijenata[p].Jmbg) && listaPacijenata[p].Obrisan == false)
                        {
                            prikazOperacije.Pacijent = listaPacijenata[p];
                            break;
                        }
                    }
                    for (int p = 0; p < listaProstorija.Count; p++)
                    {
                        if (lista2[indeks].brojProstorije.Equals(listaProstorija[p].BrojProstorije) && listaProstorija[p].Obrisana == false)
                        {
                            prikazOperacije.Prostorija = listaProstorija[p];
                            break;
                        }
                    }
                    for (int p = 0; p < listaLekara.Count; p++)
                    {
                        if (lista2[indeks].lekarJmbg.Equals(listaLekara[p].Jmbg))
                        {
                            prikazOperacije.Lekar = listaLekara[p];
                        }
                    }
                    lista2.RemoveAt(indeks);
                    razlikaDatuma.RemoveAt(najveciIndeks);
                    Pregledi.Add(prikazOperacije);
                }
            }
        }

        private void Zatvori(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ZakaziHitanTermin(object sender, RoutedEventArgs e)
        {
            if (dataGridPregledi.SelectedItems.Count == 0)
            {
                MessageBoxResult result = MessageBox.Show("Niste odabrali termine za pomeranje.",
                                          "Zakazivanje hitnog termina",
                                          MessageBoxButton.OK,
                                          MessageBoxImage.Information);
            }
            else
            {
                if (pomereniPregledi.Count != 0)
                    pomereniPregledi.Clear();

                if (pomereneOperacije.Count != 0)
                    pomereneOperacije.Clear();

                for (int i = 0; i < dataGridPregledi.SelectedItems.Count; i++)
                {
                    var objekat = dataGridPregledi.SelectedItems[i];
   
                    if (objekat.GetType().Equals(prikazPregleda.GetType()))
                    {
                        PrikazPregleda pregled = objekat as PrikazPregleda;
                        bool zauzet;

                        DateTime datum = datumHitnogTermina;
                        if (trajanjeHitnogTermina % 15 == 0)
                            datum = datum.AddMinutes(trajanjeHitnogTermina);
                        else
                            datum = datum.AddMinutes((trajanjeHitnogTermina / 15 + 1) * 15);
                        do
                        {
                            zauzet = false;
                            foreach (Pregled p in listaPregleda)
                            {
                                if (p.Id == pregled.Id)
                                    continue;

                                bool preskoci = false;
                                foreach (PrikazPregleda pp in pomereniPregledi)
                                    if (p.Id == pp.Id)
                                        preskoci = true;

                                if (preskoci)
                                    continue;

                                if (DateTime.Compare(p.Datum.AddMinutes(p.Trajanje), datum) <= 0 || DateTime.Compare(p.Datum, datum.AddMinutes(pregled.Trajanje)) >= 0)
                                    continue;
                                else if (pregled.Prostorija.BrojProstorije == p.brojProstorije || pregled.Lekar.Jmbg.Equals(p.lekarJmbg) || pregled.Pacijent.Jmbg.Equals(p.pacijentJmbg))
                                    zauzet = true;
                            }

                            foreach (PrikazPregleda p in pomereniPregledi)
                                if (DateTime.Compare(p.Datum.AddMinutes(p.Trajanje), datum) <= 0 || DateTime.Compare(p.Datum, datum.AddMinutes(pregled.Trajanje)) >= 0)
                                    continue;
                                else if (pregled.Prostorija.BrojProstorije == p.Prostorija.BrojProstorije || pregled.Lekar.Jmbg.Equals(p.Lekar.Jmbg) || pregled.Pacijent.Jmbg.Equals(p.Pacijent.Jmbg))
                                    zauzet = true;

                            foreach (Operacija o in listaOperacija)
                            {
                                bool preskoci = false;
                                foreach (PrikazOperacije po in pomereneOperacije)
                                    if (o.Id == po.Id)
                                        preskoci = true;

                                if (preskoci)
                                    continue;

                                if (DateTime.Compare(o.Datum.AddMinutes(o.Trajanje), datum) <= 0 || DateTime.Compare(o.Datum, datum.AddMinutes(pregled.Trajanje)) >= 0)
                                    continue;
                                else if (pregled.Prostorija.BrojProstorije == o.brojProstorije || pregled.Lekar.Jmbg.Equals(o.lekarJmbg) || pregled.Pacijent.Jmbg.Equals(o.pacijentJmbg))
                                    zauzet = true;
                            }

                            foreach (PrikazOperacije o in pomereneOperacije)
                                if (DateTime.Compare(o.Datum.AddMinutes(o.Trajanje), datum) <= 0 || DateTime.Compare(o.Datum, datum.AddMinutes(pregled.Trajanje)) >= 0)
                                    continue;
                                else if (pregled.Prostorija.BrojProstorije == o.Prostorija.BrojProstorije || pregled.Lekar.Jmbg.Equals(o.Lekar.Jmbg) || pregled.Pacijent.Jmbg.Equals(o.Pacijent.Jmbg))
                                    zauzet = true;

                            if (zauzet)
                                datum = datum.AddMinutes(15);
                            else
                            {
                                pregled.Datum = datum;
                                pomereniPregledi.Add(pregled);
                            }

                        } while (zauzet);
                    }
                    else if (objekat.GetType().Equals(prikazOperacije.GetType())) 
                    {
                        PrikazOperacije operacija = objekat as PrikazOperacije;
                        bool zauzet;

                        DateTime datum = datumHitnogTermina;
                        if (trajanjeHitnogTermina % 15 == 0)
                            datum = datum.AddMinutes(trajanjeHitnogTermina);
                        else
                            datum = datum.AddMinutes((trajanjeHitnogTermina / 15 + 1) * 15);
                        do
                        {
                            zauzet = false;
                            foreach (Pregled p in listaPregleda)
                            {
                                bool preskoci = false;
                                foreach (PrikazPregleda pp in pomereniPregledi)
                                    if (p.Id == pp.Id)
                                        preskoci = true;

                                if (preskoci)
                                    continue;

                                if (DateTime.Compare(p.Datum.AddMinutes(p.Trajanje), datum) <= 0 || DateTime.Compare(p.Datum, datum.AddMinutes(operacija.Trajanje)) >= 0)
                                    continue;
                                else if (operacija.Prostorija.BrojProstorije == p.brojProstorije || operacija.Lekar.Jmbg.Equals(p.lekarJmbg) || operacija.Pacijent.Jmbg.Equals(p.pacijentJmbg))
                                    zauzet = true;
                            }

                            foreach (PrikazPregleda p in pomereniPregledi)
                                if (DateTime.Compare(p.Datum.AddMinutes(p.Trajanje), datum) <= 0 || DateTime.Compare(p.Datum, datum.AddMinutes(operacija.Trajanje)) >= 0)
                                    continue;
                                else if (operacija.Prostorija.BrojProstorije == p.Prostorija.BrojProstorije || operacija.Lekar.Jmbg.Equals(p.Lekar.Jmbg) || operacija.Pacijent.Jmbg.Equals(p.Pacijent.Jmbg))
                                    zauzet = true;

                            foreach (Operacija o in listaOperacija)
                            {
                                if (o.Id == operacija.Id)
                                    continue;

                                bool preskoci = false;
                                foreach (PrikazOperacije po in pomereneOperacije)
                                    if (o.Id == po.Id)
                                        preskoci = true;

                                if (preskoci)
                                    continue;

                                if (DateTime.Compare(o.Datum.AddMinutes(o.Trajanje), datum) <= 0 || DateTime.Compare(o.Datum, datum.AddMinutes(operacija.Trajanje)) >= 0)
                                    continue;
                                else if (operacija.Prostorija.BrojProstorije == o.brojProstorije || operacija.Lekar.Jmbg.Equals(o.lekarJmbg) || operacija.Pacijent.Jmbg.Equals(o.pacijentJmbg))
                                    zauzet = true;
                            }

                            foreach (PrikazOperacije o in pomereneOperacije)
                                if (DateTime.Compare(o.Datum.AddMinutes(o.Trajanje), datum) <= 0 || DateTime.Compare(o.Datum, datum.AddMinutes(operacija.Trajanje)) >= 0)
                                    continue;
                                else if (operacija.Prostorija.BrojProstorije == o.Prostorija.BrojProstorije || operacija.Lekar.Jmbg.Equals(o.Lekar.Jmbg) || operacija.Pacijent.Jmbg.Equals(o.Pacijent.Jmbg))
                                    zauzet = true;

                            if (zauzet)
                                datum = datum.AddMinutes(15);
                            else
                            {
                                operacija.Datum = datum;
                                pomereneOperacije.Add(operacija);
                            }

                        } while (zauzet);
                    }
                }

                if (!hitnaOperacija)
                {
                    PrikazPregleda trenutniPregled = new PrikazPregleda();

                    trenutniPregled.Trajanje = trajanjeHitnogTermina;
                    trenutniPregled.Zavrsen = false;
                    trenutniPregled.Hitan = true;
                    trenutniPregled.Pacijent = pacijentHitnogTermina;
                    trenutniPregled.Datum = datumHitnogTermina;

                    if (PacijentZauzet(trenutniPregled.Pacijent, trenutniPregled.Datum, trenutniPregled.Trajanje))
                    {
                        MessageBox.Show("Nije moguće zakazati hitan termin nakon pomeranja odabranih termina. Pogledajte tabelu zakazanih termina, te odaberite druge termine za pomeranje", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    bool zauzeteSveProstorije = true;
                    foreach (Prostorija pr in listaProstorija)
                        if (!pr.Obrisana && !ProstorijaZauzeta(pr, trenutniPregled.Datum, trenutniPregled.Trajanje))
                        {
                            zauzeteSveProstorije = false;
                            trenutniPregled.Prostorija = pr;
                            break;
                        }

                    if (zauzeteSveProstorije)
                    {
                        MessageBox.Show("Nije moguće zakazati hitan termin nakon pomeranja odabranih termina. Pogledajte tabelu zakazanih termina, te odaberite druge termine za pomeranje", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    bool zauzetiSviLekari = true;
                    foreach (Lekar l in listaLekara)
                        if (specijalizacijaHitnogTermina.Equals(l.Specijalizacija.OblastMedicine) && !LekarZauzet(l, trenutniPregled.Datum, trenutniPregled.Trajanje))
                        {
                            zauzetiSviLekari = false;
                            trenutniPregled.Lekar = l;
                            break;
                        }

                    if (zauzetiSviLekari)
                    {
                        MessageBox.Show("Nije moguće zakazati hitan termin nakon pomeranja odabranih termina. Pogledajte tabelu zakazanih termina, te odaberite druge termine za pomeranje", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    int max = 0;
                    for (int i = 0; i < listaPregleda.Count; i++)
                    {
                        if (listaPregleda[i].Id > max)
                            max = listaPregleda[i].Id;
                    }
                    trenutniPregled.Id = max + 1;
                    Pregled p = new Pregled();
                    p.Id = trenutniPregled.Id;
                    p.lekarJmbg = trenutniPregled.Lekar.Jmbg;
                    p.pacijentJmbg = trenutniPregled.Pacijent.Jmbg;
                    p.Trajanje = trenutniPregled.Trajanje;
                    p.Zavrsen = trenutniPregled.Zavrsen;
                    p.AnamnezaId = -1;
                    p.brojProstorije = trenutniPregled.Prostorija.BrojProstorije;
                    p.Datum = trenutniPregled.Datum;
                    p.Hitan = trenutniPregled.Hitan;

                    bool guest = true;
                    foreach (Pacijent pac in listaPacijenata)
                        if (pac.Jmbg.Equals(p.pacijentJmbg))
                            guest = false;

                    if (guest)
                        sviPacijenti.Save(trenutniPregled.Pacijent);

                    sviPregledi.Save(p);
                    FormPregledi.listaPregleda.Add(p);
                    FormPregledi.Pregledi.Add(trenutniPregled);

                    foreach(PrikazPregleda pp in pomereniPregledi) 
                    {
                        for (int i = 0; i < listaPregleda.Count; i++) 
                        {
                            if (pp.Id == listaPregleda[i].Id) 
                            {
                                sviPregledi.Delete(listaPregleda[i]);
                                listaPregleda[i].Datum = pp.Datum;
                                sviPregledi.Save(listaPregleda[i]);
                            }
                        }

                        for (int i = 0; i < FormPregledi.listaPregleda.Count; i++)
                            if (pp.Id == FormPregledi.listaPregleda[i].Id)
                                FormPregledi.listaPregleda[i].Datum = pp.Datum;

                        for (int i = 0; i < FormPregledi.Pregledi.Count; i++)
                            if (pp.Id == FormPregledi.Pregledi[i].Id)
                                FormPregledi.Pregledi[i].Datum = pp.Datum;

                        for (int i = 0; i < listaPregledaUTerminuHitnog.Count; i++)
                            if (pp.Id == listaPregledaUTerminuHitnog[i].Id)
                                listaPregledaUTerminuHitnog[i].Datum = pp.Datum;
                    }

                    foreach (PrikazOperacije po in pomereneOperacije)
                    {
                        for (int i = 0; i < listaOperacija.Count; i++)
                        {
                            if (po.Id == listaOperacija[i].Id)
                            {
                                sviPregledi.Delete(listaOperacija[i]);
                                listaOperacija[i].Datum = po.Datum;
                                sviPregledi.Save(listaOperacija[i]);
                            }
                        }

                        for (int i = 0; i < FormPregledi.listaOperacija.Count; i++)
                            if (po.Id == FormPregledi.listaOperacija[i].Id)
                                FormPregledi.listaOperacija[i].Datum = po.Datum;

                        for (int i = 0; i < FormPregledi.Pregledi.Count; i++)
                            if (po.Id == FormPregledi.Pregledi[i].Id)
                                FormPregledi.Pregledi[i].Datum = po.Datum;

                        for (int i = 0; i < listaOperacijaUTerminuHitnog.Count; i++)
                            if (po.Id == listaOperacijaUTerminuHitnog[i].Id)
                                listaOperacijaUTerminuHitnog[i].Datum = po.Datum;
                    }
                }
                else
                {
                    PrikazOperacije trenutnaOperacija = new PrikazOperacije();

                    trenutnaOperacija.Trajanje = trajanjeHitnogTermina;
                    trenutnaOperacija.Zavrsen = false;
                    trenutnaOperacija.Hitan = true;
                    trenutnaOperacija.Pacijent = pacijentHitnogTermina;
                    trenutnaOperacija.TipOperacije = tipOperacijeHitnogTermina;
                    trenutnaOperacija.Datum = datumHitnogTermina;

                    if (PacijentZauzet(trenutnaOperacija.Pacijent, trenutnaOperacija.Datum, trenutnaOperacija.Trajanje))
                    {
                        MessageBox.Show("Nije moguće zakazati hitan termin nakon pomeranja odabranih termina. Pogledajte tabelu zakazanih termina, te odaberite druge termine za pomeranje", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    bool zauzeteSveProstorije = true;
                    foreach (Prostorija p in listaProstorija)
                        if (!p.Obrisana && !ProstorijaZauzeta(p, trenutnaOperacija.Datum, trenutnaOperacija.Trajanje))
                        {
                            zauzeteSveProstorije = false;
                            trenutnaOperacija.Prostorija = p;
                            break;
                        }

                    if (zauzeteSveProstorije)
                    {
                        MessageBox.Show("Nije moguće zakazati hitan termin nakon pomeranja odabranih termina. Pogledajte tabelu zakazanih termina, te odaberite druge termine za pomeranje", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    bool zauzetiSviLekari = true;
                    foreach (Lekar l in listaLekara)
                        if (specijalizacijaHitnogTermina.Equals(l.Specijalizacija.OblastMedicine) && !LekarZauzet(l, trenutnaOperacija.Datum, trenutnaOperacija.Trajanje))
                        {
                            zauzetiSviLekari = false;
                            trenutnaOperacija.Lekar = l;
                            break;
                        }

                    if (zauzetiSviLekari)
                    {
                        MessageBox.Show("Nije moguće zakazati hitan termin nakon pomeranja odabranih termina. Pogledajte tabelu zakazanih termina, te odaberite druge termine za pomeranje", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    int max = 0;
                    for (int i = 0; i < listaOperacija.Count; i++)
                    {
                        if (listaOperacija[i].Id > max)
                            max = listaOperacija[i].Id;
                    }
                    trenutnaOperacija.Id = max + 1;
                    Operacija o = new Operacija();
                    o.Id = trenutnaOperacija.Id;
                    o.lekarJmbg = trenutnaOperacija.Lekar.Jmbg;
                    o.pacijentJmbg = trenutnaOperacija.Pacijent.Jmbg;
                    o.Trajanje = trenutnaOperacija.Trajanje;
                    o.Zavrsen = trenutnaOperacija.Zavrsen;
                    o.AnamnezaId = -1;
                    o.brojProstorije = trenutnaOperacija.Prostorija.BrojProstorije;
                    o.Datum = trenutnaOperacija.Datum;
                    o.Hitan = trenutnaOperacija.Hitan;
                    o.TipOperacije = trenutnaOperacija.TipOperacije;

                    bool guest = true;
                    foreach (Pacijent pac in listaPacijenata)
                        if (pac.Jmbg.Equals(o.pacijentJmbg))
                            guest = false;

                    if (guest)
                        sviPacijenti.Save(trenutnaOperacija.Pacijent);

                    sviPregledi.Save(o);
                    FormPregledi.listaOperacija.Add(o);
                    FormPregledi.Pregledi.Add(trenutnaOperacija);

                    foreach (PrikazPregleda pp in pomereniPregledi)
                    {
                        for (int i = 0; i < listaPregleda.Count; i++)
                        {
                            if (pp.Id == listaPregleda[i].Id)
                            {
                                sviPregledi.Delete(listaPregleda[i]);
                                listaPregleda[i].Datum = pp.Datum;
                                sviPregledi.Save(listaPregleda[i]);
                            }
                        }

                        for (int i = 0; i < FormPregledi.listaPregleda.Count; i++)
                            if (pp.Id == FormPregledi.listaPregleda[i].Id)
                                FormPregledi.listaPregleda[i].Datum = pp.Datum;

                        for (int i = 0; i < FormPregledi.Pregledi.Count; i++)
                            if (pp.Id == FormPregledi.Pregledi[i].Id)
                                FormPregledi.Pregledi[i].Datum = pp.Datum;

                        for (int i = 0; i < listaPregledaUTerminuHitnog.Count; i++)
                            if (pp.Id == listaPregledaUTerminuHitnog[i].Id)
                                listaPregledaUTerminuHitnog[i].Datum = pp.Datum;
                    }

                    foreach (PrikazOperacije po in pomereneOperacije)
                    {
                        for (int i = 0; i < listaOperacija.Count; i++)
                        {
                            if (po.Id == listaOperacija[i].Id)
                            {
                                sviPregledi.Delete(listaOperacija[i]);
                                listaOperacija[i].Datum = po.Datum;
                                sviPregledi.Save(listaOperacija[i]);
                            }
                        }

                        for (int i = 0; i < FormPregledi.listaOperacija.Count; i++)
                            if (po.Id == FormPregledi.listaOperacija[i].Id)
                                FormPregledi.listaOperacija[i].Datum = po.Datum;

                        for (int i = 0; i < FormPregledi.Pregledi.Count; i++)
                            if (po.Id == FormPregledi.Pregledi[i].Id)
                                FormPregledi.Pregledi[i].Datum = po.Datum;

                        for (int i = 0; i < listaOperacijaUTerminuHitnog.Count; i++)
                            if (po.Id == listaOperacijaUTerminuHitnog[i].Id)
                                listaOperacijaUTerminuHitnog[i].Datum = po.Datum;
                    }
                }
                dataGrid.Items.Refresh();
                Close();
                zakaziHitanTermin.Close();
            }
        }

        private bool ProstorijaZauzeta(Prostorija prostorija, DateTime datum, int trajanje)
        {
            bool zauzet = false;
            List<Pregled> pregledi = new List<Pregled>();
            List<Pregled> preglediProstorije = new List<Pregled>();
            List<Operacija> operacije = new List<Operacija>();
            List<Operacija> operacijeProstorije = new List<Operacija>();
            pregledi = sviPregledi.GetAllPregledi();
            operacije = sviPregledi.GetAllOperacije();

            foreach (Pregled p in pregledi)
                if (p.brojProstorije.Equals(prostorija.BrojProstorije))
                    preglediProstorije.Add(p);

            foreach (Pregled p in preglediProstorije)
            {
                bool preskoci = false;
                foreach (PrikazPregleda pp in pomereniPregledi)
                    if (pp.Id == p.Id)
                        preskoci = true;

                if (preskoci)
                    continue;

                if (DateTime.Compare(datum, p.Datum) >= 0 && DateTime.Compare(datum, p.Datum.AddMinutes(p.Trajanje)) < 0)
                {
                    zauzet = true;
                    break;
                }

                if (DateTime.Compare(datum, p.Datum) <= 0 && DateTime.Compare(datum.AddMinutes(trajanje), p.Datum) > 0)
                {
                    zauzet = true;
                    break;
                }
            }

            foreach(PrikazPregleda pp in pomereniPregledi) 
                if(pp.Prostorija.BrojProstorije == prostorija.BrojProstorije)
                {
                    if (DateTime.Compare(datum, pp.Datum) >= 0 && DateTime.Compare(datum, pp.Datum.AddMinutes(pp.Trajanje)) < 0)
                    {
                        zauzet = true;
                        break;
                    }

                    if (DateTime.Compare(datum, pp.Datum) <= 0 && DateTime.Compare(datum.AddMinutes(trajanje), pp.Datum) > 0)
                    {
                        zauzet = true;
                        break;
                    }
                }

            foreach (Operacija o in operacije)
                if (o.brojProstorije.Equals(prostorija.BrojProstorije))
                    operacijeProstorije.Add(o);

            foreach (Operacija o in operacijeProstorije)
            {
                bool preskoci = false;
                foreach (PrikazOperacije po in pomereneOperacije)
                    if (po.Id == o.Id)
                        preskoci = true;

                if (preskoci)
                    continue;

                if (DateTime.Compare(datum, o.Datum) >= 0 && DateTime.Compare(datum, o.Datum.AddMinutes(o.Trajanje)) < 0)
                {
                    zauzet = true;
                    break;
                }

                if (DateTime.Compare(datum, o.Datum) <= 0 && DateTime.Compare(datum.AddMinutes(trajanje), o.Datum) > 0)
                {
                    zauzet = true;
                    break;
                }
            }

            foreach (PrikazOperacije po in pomereneOperacije)
                if (po.Prostorija.BrojProstorije == prostorija.BrojProstorije)
                {
                    if (DateTime.Compare(datum, po.Datum) >= 0 && DateTime.Compare(datum, po.Datum.AddMinutes(po.Trajanje)) < 0)
                    {
                        zauzet = true;
                        break;
                    }

                    if (DateTime.Compare(datum, po.Datum) <= 0 && DateTime.Compare(datum.AddMinutes(trajanje), po.Datum) > 0)
                    {
                        zauzet = true;
                        break;
                    }
                }

            return zauzet;
        }

        private bool LekarZauzet(Lekar lekar, DateTime datum, int trajanje)
        {
            bool zauzet = false;
            List<Pregled> pregledi = new List<Pregled>();
            List<Pregled> preglediLekara = new List<Pregled>();
            List<Operacija> operacije = new List<Operacija>();
            List<Operacija> operacijeLekara = new List<Operacija>();
            pregledi = sviPregledi.GetAllPregledi();
            operacije = sviPregledi.GetAllOperacije();

            foreach (Pregled p in pregledi)
                if (p.lekarJmbg.Equals(lekar.Jmbg))
                    preglediLekara.Add(p);

            foreach (Pregled p in preglediLekara)
            {
                bool preskoci = false;
                foreach (PrikazPregleda pp in pomereniPregledi)
                    if (pp.Id == p.Id)
                        preskoci = true;

                if (preskoci)
                    continue;

                if (DateTime.Compare(datum, p.Datum) >= 0 && DateTime.Compare(datum, p.Datum.AddMinutes(p.Trajanje)) < 0)
                {
                    zauzet = true;
                    break;
                }

                if (DateTime.Compare(datum, p.Datum) <= 0 && DateTime.Compare(datum.AddMinutes(trajanje), p.Datum) > 0)
                {
                    zauzet = true;
                    break;
                }
            }

            foreach (PrikazPregleda pp in pomereniPregledi)
                if (pp.Lekar.Jmbg.Equals(lekar.Jmbg))
                {
                    if (DateTime.Compare(datum, pp.Datum) >= 0 && DateTime.Compare(datum, pp.Datum.AddMinutes(pp.Trajanje)) < 0)
                    {
                        zauzet = true;
                        break;
                    }

                    if (DateTime.Compare(datum, pp.Datum) <= 0 && DateTime.Compare(datum.AddMinutes(trajanje), pp.Datum) > 0)
                    {
                        zauzet = true;
                        break;
                    }
                }

            foreach (Operacija o in operacije)
                if (o.lekarJmbg.Equals(lekar.Jmbg))
                    operacijeLekara.Add(o);

            foreach (Operacija o in operacijeLekara)
            {
                bool preskoci = false;
                foreach (PrikazOperacije po in pomereneOperacije)
                    if (po.Id == o.Id)
                        preskoci = true;

                if (preskoci)
                    continue;

                if (DateTime.Compare(datum, o.Datum) >= 0 && DateTime.Compare(datum, o.Datum.AddMinutes(o.Trajanje)) < 0)
                {
                    zauzet = true;
                    break;
                }

                if (DateTime.Compare(datum, o.Datum) <= 0 && DateTime.Compare(datum.AddMinutes(trajanje), o.Datum) > 0)
                {
                    zauzet = true;
                    break;
                }
            }

            foreach (PrikazOperacije po in pomereneOperacije)
                if (po.Lekar.Jmbg.Equals(lekar.Jmbg))
                {
                    if (DateTime.Compare(datum, po.Datum) >= 0 && DateTime.Compare(datum, po.Datum.AddMinutes(po.Trajanje)) < 0)
                    {
                        zauzet = true;
                        break;
                    }

                    if (DateTime.Compare(datum, po.Datum) <= 0 && DateTime.Compare(datum.AddMinutes(trajanje), po.Datum) > 0)
                    {
                        zauzet = true;
                        break;
                    }
                }

            return zauzet;
        }

        private bool PacijentZauzet(Pacijent pacijent, DateTime datum, int trajanje)
        {
            bool zauzet = false;
            List<Pregled> pregledi = new List<Pregled>();
            List<Pregled> preglediPacijenta = new List<Pregled>();
            List<Operacija> operacije = new List<Operacija>();
            List<Operacija> operacijePacijenta = new List<Operacija>();
            pregledi = sviPregledi.GetAllPregledi();
            operacije = sviPregledi.GetAllOperacije();

            foreach (Pregled p in pregledi)
                if (p.pacijentJmbg.Equals(pacijent.Jmbg))
                    preglediPacijenta.Add(p);

            foreach (Pregled p in preglediPacijenta)
            {
                bool preskoci = false;
                foreach (PrikazPregleda pp in pomereniPregledi)
                    if (pp.Id == p.Id)
                        preskoci = true;

                if (preskoci)
                    continue;

                if (DateTime.Compare(datum, p.Datum) >= 0 && DateTime.Compare(datum, p.Datum.AddMinutes(p.Trajanje)) < 0)
                {
                    zauzet = true;
                    break;
                }

                if (DateTime.Compare(datum, p.Datum) <= 0 && DateTime.Compare(datum.AddMinutes(trajanje), p.Datum) > 0)
                {
                    zauzet = true;
                    break;
                }
            }

            foreach (PrikazPregleda pp in pomereniPregledi)
                if (pp.Pacijent.Jmbg.Equals(pacijent.Jmbg))
                {
                    if (DateTime.Compare(datum, pp.Datum) >= 0 && DateTime.Compare(datum, pp.Datum.AddMinutes(pp.Trajanje)) < 0)
                    {
                        zauzet = true;
                        break;
                    }

                    if (DateTime.Compare(datum, pp.Datum) <= 0 && DateTime.Compare(datum.AddMinutes(trajanje), pp.Datum) > 0)
                    {
                        zauzet = true;
                        break;
                    }
                }

            foreach (Operacija o in operacije)
                if (o.pacijentJmbg.Equals(pacijent.Jmbg))
                    operacijePacijenta.Add(o);

            foreach (Operacija o in operacijePacijenta)
            {
                bool preskoci = false;
                foreach (PrikazOperacije po in pomereneOperacije)
                    if (po.Id == o.Id)
                        preskoci = true;

                if (preskoci)
                    continue;

                if (DateTime.Compare(datum, o.Datum) >= 0 && DateTime.Compare(datum, o.Datum.AddMinutes(o.Trajanje)) < 0)
                {
                    zauzet = true;
                    break;
                }

                if (DateTime.Compare(datum, o.Datum) <= 0 && DateTime.Compare(datum.AddMinutes(trajanje), o.Datum) > 0)
                {
                    zauzet = true;
                    break;
                }
            }

            foreach (PrikazOperacije po in pomereneOperacije)
                if (po.Pacijent.Jmbg.Equals(pacijent.Jmbg))
                {
                    if (DateTime.Compare(datum, po.Datum) >= 0 && DateTime.Compare(datum, po.Datum.AddMinutes(po.Trajanje)) < 0)
                    {
                        zauzet = true;
                        break;
                    }

                    if (DateTime.Compare(datum, po.Datum) <= 0 && DateTime.Compare(datum.AddMinutes(trajanje), po.Datum) > 0)
                    {
                        zauzet = true;
                        break;
                    }
                }

            return zauzet;
        }
    }
}
