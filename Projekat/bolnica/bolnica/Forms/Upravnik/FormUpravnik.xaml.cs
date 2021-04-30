using Bolnica.Forms;
using Bolnica.Forms.Upravnik;
using Bolnica.Model.Pregledi;
using Bolnica.Model.Prostorije;
using Model.Pregledi;
using Model.Prostorije;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace bolnica.Forms
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class FormUpravnik : Window
    {
        public static ObservableCollection<Prostorija> Prostorije
        {
            get;
            set;
        }

        private FileStorageProstorija storageProstorije;
        private FileStorageOprema storageOprema;
        private FileStorageLek storageLekovi;
        private FileStorageRenoviranje storageRenoviranje = new FileStorageRenoviranje();

        public static bool clickedDodaj;

        public static ObservableCollection<Oprema> Oprema
        {
            get;
            set;
        }

        public static ObservableCollection<Lek> Lekovi
        {
            get;
            set;
        }

        public FormUpravnik()
        {
            InitializeComponent();

            DispatcherTimer timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                Vreme.Text = DateTime.Now.ToString("HH:mm");
                Datum.Text = DateTime.Now.ToString("dd/MM/yyyy");

            }, Dispatcher);

            clickedDodaj = false;
            this.DataContext = this;
            Prostorije = new ObservableCollection<Prostorija>();
            storageProstorije = new FileStorageProstorija();

            List<Prostorija> prostorije = storageProstorije.GetAllProstorije();
            foreach (Prostorija p in prostorije)
            {
                if (p.Obrisana == false)
                    Prostorije.Add(p);
            }
            List<BolnickaSoba> bolnickeSobe = storageProstorije.GetAllBolnickeSobe();
            foreach (BolnickaSoba b in bolnickeSobe)
            {
                if (b.Obrisana == false)
                    Prostorije.Add(b);
            }

            Oprema = new ObservableCollection<Oprema>();
            storageOprema = new FileStorageOprema();

            List<Oprema> oprema = storageOprema.GetAll();
            if (oprema != null)
            {
                foreach (Oprema o in oprema)
                {
                    Oprema.Add(o);
                }
            }

            Lekovi = new ObservableCollection<Lek>();
            storageLekovi = new FileStorageLek();

            List<Lek> lekovi = storageLekovi.GetAll();
            if (lekovi != null)
            {
                foreach (Lek l in lekovi)
                {
                    if (!l.Obrisan)
                        Lekovi.Add(l);
                }
            }
        }

        private void Button_Click_Dodaj(object sender, RoutedEventArgs e)
        {
            clickedDodaj = true;
            if (Tabovi.SelectedIndex == 0)
            {
                var p = new CreateFormProstorije();
                p.Show();
            }
            else if (Tabovi.SelectedIndex == 1)
            {
                var o = new CreateFormOprema();
                o.Show();
            }
            else if (Tabovi.SelectedIndex == 2)
            {
                Lek noviLek = new Lek();
                var l = new CreateFormLekovi(noviLek);
                l.Show();
            }
        }

        private void Button_Click_Prikazi(object sender, RoutedEventArgs e)
        {
            if (dataGridProstorije.SelectedCells.Count > 0 && Tabovi.SelectedIndex == 0)
            {
                Prostorija row = (Prostorija)dataGridProstorije.SelectedItems[0];
                string brojProstorije = row.BrojProstorije;
                List<Prostorija> prostorije = storageProstorije.GetAllProstorije();
                List<BolnickaSoba> bolnickeSobe = storageProstorije.GetAllBolnickeSobe();
                var s = new ViewFormProstorije(brojProstorije);
                bool found = false;
                foreach (Prostorija p in prostorije)
                {
                    if (p.BrojProstorije == row.BrojProstorije)
                    {
                        s.lblUkBrojKreveta.Visibility = Visibility.Hidden;
                        s.lblBrojSlobodnihKreveta.Visibility = Visibility.Hidden;
                        s.lblBrojProstorije.Content = p.BrojProstorije.ToString();
                        s.lblSprat.Content = p.Sprat.ToString();
                        s.lblKvadratura.Content = p.Kvadratura.ToString();
                        s.checkZauzeta.IsEnabled = false;
                        if (p.TipProstorije == TipProstorije.salaZaPreglede)
                            s.lblTipProstorije.Content = "Sala za preglede";
                        else
                            s.lblTipProstorije.Content = "Operaciona sala";
                        s.checkZauzeta.IsChecked = p.Zauzeta;
                        s.Show();
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    foreach (BolnickaSoba b in bolnickeSobe)
                    {
                        if (b.BrojProstorije == row.BrojProstorije)
                        {
                            s.lblBrojProstorije.Content = b.BrojProstorije.ToString();
                            s.lblSprat.Content = b.Sprat.ToString();
                            s.lblKvadratura.Content = b.Kvadratura.ToString();
                            s.checkZauzeta.IsEnabled = false;
                            s.lblTipProstorije.Content = "Bolnička soba";
                            s.checkZauzeta.IsChecked = b.Zauzeta;
                            s.lblUkBrojKreveta.Visibility = Visibility.Visible;
                            s.lblUkBrKreveta.Content = b.UkBrojKreveta.ToString();
                            s.lblBrojSlobodnihKreveta.Visibility = Visibility.Visible;
                            s.lblBrSlobodnihKreveta.Content = b.BrojSlobodnihKreveta.ToString();
                            s.Show();
                            break;
                        }
                    }
                }
            }
            else if (dataGridOprema.SelectedCells.Count > 0 && Tabovi.SelectedIndex == 1)
            {
                Oprema row = (Oprema)dataGridOprema.SelectedItems[0];
                string sifra = row.Sifra;
                var s = new ViewFormOprema(sifra);
                List<Oprema> oprema = storageOprema.GetAll();
                foreach (Oprema o in oprema)
                {
                    if (o.Sifra == row.Sifra)
                    {
                        s.lblSifra.Content = o.Sifra;
                        s.lblNaziv.Content = o.Naziv;
                        if (o.TipOpreme == TipOpreme.dinamicka)
                            s.lblTipOpreme.Content = "Dinamička";
                        else
                            s.lblTipOpreme.Content = "Statička";
                        s.lblKolicina.Content = o.Kolicina.ToString();
                        s.Show();
                        break;
                    }
                }
            }
            else if (dataGridLekovi.SelectedCells.Count > 0 && Tabovi.SelectedIndex == 2)
            {
                Lek row = (Lek)dataGridLekovi.SelectedItems[0];
                int id = row.Id;
                var s = new ViewFormLek(id);
                List<Lek> lekovi = storageLekovi.GetAll();
                foreach (Lek l in lekovi)
                {
                    if (l.Id == row.Id)
                    {
                        s.lblId.Content = l.Id;
                        s.lblNaziv.Content = l.Naziv;
                        s.lblKolicinaUMg.Content = l.KolicinaUMg;
                        s.lblProizvodjac.Content = l.Proizvodjac;
                        if (l.Status == StatusLeka.odobren)
                            s.lblStatus.Content = "Odobren";
                        else if (l.Status == StatusLeka.odbijen)
                            s.lblStatus.Content = "Odbijen";
                        else
                            s.lblStatus.Content = "Čeka validaciju";

                        s.lblZalihe.Content = l.Zalihe;
                        s.Show();
                        break;
                    }
                }
            }
        }

        private void Button_Click_Izmeni(object sender, RoutedEventArgs e)
        {
            if (dataGridProstorije.SelectedCells.Count > 0 && Tabovi.SelectedIndex == 0)
            {
                clickedDodaj = false;
                Prostorija row = (Prostorija)dataGridProstorije.SelectedItems[0];
                List<Prostorija> prostorije = storageProstorije.GetAllProstorije();
                List<BolnickaSoba> bolnickeSobe = storageProstorije.GetAllBolnickeSobe();
                var s = new CreateFormProstorije();
                bool found = false;
                foreach (Prostorija p in prostorije)
                {
                    if (p.BrojProstorije == row.BrojProstorije)
                    {
                        s.BrojProstorije = p.BrojProstorije;
                        s.Sprat = p.Sprat;
                        s.Kvadratura = p.Kvadratura;
                        if (p.TipProstorije == TipProstorije.salaZaPreglede)
                            s.comboTipProstorije.SelectedIndex = 0;
                        else
                            s.comboTipProstorije.SelectedIndex = 1;
                        s.checkZauzeta.IsChecked = p.Zauzeta;
                        s.Show();
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    foreach (BolnickaSoba b in bolnickeSobe)
                    {
                        if (b.BrojProstorije == row.BrojProstorije)
                        {
                            s.BrojProstorije = b.BrojProstorije;
                            s.Sprat = b.Sprat;
                            s.Kvadratura = b.Kvadratura;
                            s.comboTipProstorije.SelectedIndex = 2;
                            s.checkZauzeta.IsChecked = b.Zauzeta;
                            s.lblUkBrojKreveta.Visibility = Visibility.Visible;
                            s.txtUkBrojKreveta.Visibility = Visibility.Visible;
                            s.UkBrojKreveta = b.UkBrojKreveta;
                            s.lblBrojSlobodnihKreveta.Visibility = Visibility.Visible;
                            s.txtBrojSlobodnihKreveta.Visibility = Visibility.Visible;
                            s.BrojSlobodnihKreveta = b.BrojSlobodnihKreveta;
                            s.Show();
                            break;
                        }
                    }
                }
            }
            else if (dataGridOprema.SelectedCells.Count > 0 && Tabovi.SelectedIndex == 1)
            {
                clickedDodaj = false;
                Oprema row = (Oprema)dataGridOprema.SelectedItems[0];
                List<Oprema> Svaoprema = storageOprema.GetAll();
                var s = new CreateFormOprema();
                foreach (Oprema o in Oprema)
                {
                    if (o.Sifra == row.Sifra)
                    {
                        s.Sifra = o.Sifra;
                        s.Naziv = o.Naziv;
                        s.Kolicina = o.Kolicina;
                        s.Oprema = o;
                        if (o.TipOpreme == TipOpreme.staticka)
                            s.ComboTipOpreme.SelectedIndex = 0;
                        else
                            s.ComboTipOpreme.SelectedIndex = 1;
                        s.Show();
                        break;
                    }
                }
            }
            else if (dataGridLekovi.SelectedCells.Count > 0 && Tabovi.SelectedIndex == 2)
            {
                clickedDodaj = false;
                Lek row = (Lek)dataGridLekovi.SelectedItems[0];
                List<Lek> sviLekovi = storageLekovi.GetAll();
                foreach (Lek l in sviLekovi)
                {
                    if (l.Id == row.Id)
                    {
                        var s = new CreateFormLekovi(l);
                        s.Id = l.Id;
                        s.Naziv = l.Naziv;
                        s.KolicinaUMg = l.KolicinaUMg;
                        s.Proizvodjac = l.Proizvodjac;
                        s.Zalihe = l.Zalihe;
                        foreach (Sastojak sastojak in l.Sastojak)
                        {
                            CreateFormLekovi.Sastojci.Add(sastojak);
                        }
                        if (l.Status == StatusLeka.odobren)
                        {
                            s.txtId.IsEnabled = false;
                            s.txtNaziv.IsEnabled = false;
                            s.txtKolicinaUMg.IsEnabled = false;
                            s.txtProizvodjac.IsEnabled = false;
                            s.btnValidacija.Content = "Potvrdi";
                            s.btnDodaj.IsEnabled = false;
                            s.btnUkloni.IsEnabled = false;
                            s.Show();
                        }
                        else if (l.Status == StatusLeka.odbijen)
                        {
                            s.lblZalihe.Visibility = Visibility.Hidden;
                            s.txtZalihe.Visibility = Visibility.Hidden;
                            s.txtZalihe.IsEnabled = false;
                            s.Show();
                        }
                        else
                        {
                            MessageBox.Show("Nije moguće izmeniti lek koji čeka validaciju!");
                        }

                        break;
                    }
                }
            }
        }

        private void Button_Click_Obrisi(object sender, RoutedEventArgs e)
        {
            if (dataGridProstorije.SelectedCells.Count > 0 && Tabovi.SelectedIndex == 0)
            {
                Prostorija row = (Prostorija)dataGridProstorije.SelectedItems[0];
                if (row.Zauzeta)
                {
                    MessageBox.Show("Prostorija je trenutno zauzeta, ne možete je obrisati.");
                }
                else
                {
                    List<Prostorija> prostorije = storageProstorije.GetAllProstorije();
                    List<BolnickaSoba> bolnickeSobe = storageProstorije.GetAllBolnickeSobe();

                    var s = new CreateFormProstorije();
                    bool found = false;
                    foreach (Prostorija p in prostorije)
                    {
                        if (p.BrojProstorije == row.BrojProstorije)
                        {
                            storageProstorije.Delete(p);
                            p.Obrisana = true;
                            storageProstorije.Save(p);

                            for (int i = 0; i < Prostorije.Count; i++)
                            {
                                if (Prostorije[i].BrojProstorije == row.BrojProstorije)
                                {
                                    Prostorije.Remove(Prostorije[i]);
                                }
                            }
                            found = true;
                            break;
                        }
                    }

                    if (!found)
                    {
                        foreach (BolnickaSoba b in bolnickeSobe)
                        {
                            if (b.BrojProstorije == row.BrojProstorije)
                            {
                                storageProstorije.Delete(b);
                                b.Obrisana = true;
                                storageProstorije.Save(b);

                                for (int i = 0; i < Prostorije.Count; i++)
                                {
                                    if (Prostorije[i].BrojProstorije == row.BrojProstorije)
                                    {
                                        Prostorije.Remove(Prostorije[i]);
                                    }
                                }

                                break;
                            }
                        }
                    }
                }
            }
            else if (dataGridOprema.SelectedCells.Count > 0 && Tabovi.SelectedIndex == 1)
            {
                Oprema row = (Oprema)dataGridOprema.SelectedItems[0];
                List<Oprema> oprema = storageOprema.GetAll();

                string sMessageBoxText = "Da li ste sigurni da želite da obrišete opremu sa nazivom \"" + row.Naziv + "\" i šifrom \"" + row.Sifra + "\"?";
                string sCaption = "Brisanje opreme";

                MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
                MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

                MessageBoxResult rsltMessageBox = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);

                if (rsltMessageBox == MessageBoxResult.Yes)
                {
                    foreach (Oprema o in oprema)
                    {
                        if (o.Sifra == row.Sifra)
                        {
                            storageOprema.Delete(o);

                            for (int i = 0; i < Oprema.Count; i++)
                            {
                                if (Oprema[i].Sifra == row.Sifra)
                                {
                                    Oprema.Remove(Oprema[i]);
                                    break;
                                }
                            }
                            break;
                        }
                    }
                }
            }
            else if (dataGridLekovi.SelectedCells.Count > 0 && Tabovi.SelectedIndex == 2)
            {
                Lek row = (Lek)dataGridLekovi.SelectedItems[0];
                List<Lek> lekovi = storageLekovi.GetAll();

                if (row.Status != StatusLeka.cekaValidaciju)
                {
                    string sMessageBoxText = "Da li ste sigurni da želite da obrišete lek sa nazivom \"" + row.Naziv + "\" i id-jem \"" + row.Id + "\"?";
                    string sCaption = "Brisanje opreme";

                    MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
                    MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

                    MessageBoxResult rsltMessageBox = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);


                    if (rsltMessageBox == MessageBoxResult.Yes)
                    {
                        foreach (Lek l in lekovi)
                        {
                            if (l.Id == row.Id)
                            {

                                storageLekovi.Delete(l);
                                if (l.Status == StatusLeka.odobren)
                                {
                                    l.Obrisan = true;
                                    storageLekovi.Save(l);
                                }

                                for (int i = 0; i < Lekovi.Count; i++)
                                {
                                    if (Lekovi[i].Id == l.Id)
                                    {
                                        Lekovi.Remove(Lekovi[i]);
                                        break;
                                    }
                                }
                                break;
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Nije moguće obrisati lek koji čeka validaciju!");
                }
            }
        }

        private void Button_Click_Obavestenja(object sender, RoutedEventArgs e)
        {
            FormObavestenja s = new FormObavestenja();
            s.Show();
        }

        private void Button_Click_Search(object sender, RoutedEventArgs e)
        {
            List<Oprema> svaOprema = storageOprema.GetAll();
            List<Oprema> oprema = new List<Oprema>();
            foreach (Oprema o in svaOprema)
            {
                if (o.Sifra.ToLower().Contains(txtSearch.Text.ToLower()))
                {
                    oprema.Remove(o);
                    oprema.Add(o);
                }

                if (o.Naziv.ToLower().Contains(txtSearch.Text.ToLower()))
                {
                    oprema.Remove(o);
                    oprema.Add(o);
                }

                if (o.TipOpreme == TipOpreme.dinamicka)
                {
                    string dinamicka = "dinamička";
                    if (dinamicka.Contains(txtSearch.Text.ToLower()))
                    {
                        oprema.Remove(o);
                        oprema.Add(o);
                    }
                }

                if (o.TipOpreme == TipOpreme.staticka)
                {
                    string staticka = "statička";
                    if (staticka.Contains(txtSearch.Text.ToLower()))
                    {
                        oprema.Remove(o);
                        oprema.Add(o);
                    }
                }
            }
            Oprema.Clear();
            foreach (Oprema o in oprema)
            {
                if (comboTipOpreme.SelectedIndex == 1 && o.TipOpreme == TipOpreme.staticka)
                    Oprema.Add(o);
                else if (comboTipOpreme.SelectedIndex == 2 && o.TipOpreme == TipOpreme.dinamicka)
                    Oprema.Add(o);
                else if (comboTipOpreme.SelectedIndex == 0)
                    Oprema.Add(o);
            }
        }

        private void Tabovi_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (Tabovi.SelectedIndex == 1)
                comboTipOpreme.Visibility = Visibility.Visible;
            else
                comboTipOpreme.Visibility = Visibility.Hidden;
        }

        private void comboTipOpreme_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (comboTipOpreme.Visibility == Visibility.Visible)
            {
                storageOprema = new FileStorageOprema();
                List<Oprema> svaOprema = storageOprema.GetAll();
                List<Oprema> oprema = new List<Oprema>();

                foreach (Oprema o in svaOprema)
                {
                    if (comboTipOpreme.SelectedIndex == 1 && o.TipOpreme == TipOpreme.staticka)
                    {
                        oprema.Remove(o);
                        oprema.Add(o);
                    }
                    else if (comboTipOpreme.SelectedIndex == 2 && o.TipOpreme == TipOpreme.dinamicka)
                    {
                        oprema.Remove(o);
                        oprema.Add(o);
                    }
                    else if (comboTipOpreme.SelectedIndex == 0)
                    {
                        oprema.Remove(o);
                        oprema.Add(o);
                    }
                }
                Oprema.Clear();
                foreach (Oprema o in oprema)
                {
                    Oprema.Add(o);
                }
            }
        }

        private void Button_Click_Renoviranje(object sender, RoutedEventArgs e)
        {
            if(dataGridProstorije.SelectedCells.Count > 0)
            {
                List<Renoviranje> renoviranja = storageRenoviranje.GetAll();
                if (renoviranja == null)
                    return;
                Prostorija row = (Prostorija)dataGridProstorije.SelectedItem;
                FormRenoviranje formRenoviranje = new FormRenoviranje(row.BrojProstorije);
                List<Renoviranje> renoviranjaProstorije = new List<Renoviranje>();
                foreach (Renoviranje r in renoviranja)
                {
                    if (r.BrojProstorije == row.BrojProstorije)
                        renoviranjaProstorije.Add(r);
                }
                foreach(Renoviranje r in renoviranjaProstorije)
                {
                    formRenoviranje.Calendar.BlackoutDates.Add(new CalendarDateRange(r.PocetakRenoviranja, r.KrajRenoviranja));
                }
                FileStoragePregledi storagePregledi = new FileStoragePregledi();
                List<Pregled> pregledi = storagePregledi.GetAllPregledi();
                foreach(Pregled p in pregledi)
                {
                    if(p.brojProstorije == row.BrojProstorije)
                    {
                        formRenoviranje.Calendar.BlackoutDates.Add(new CalendarDateRange(p.Datum));
                    } 
                }
                if(row.TipProstorije == TipProstorije.operacionaSala)
                {
                    List<Operacija> operacije = storagePregledi.GetAllOperacije();
                    foreach(Operacija o in operacije)
                    {
                        if(o.brojProstorije == row.BrojProstorije)
                        {
                            formRenoviranje.Calendar.BlackoutDates.Add(new CalendarDateRange(o.Datum));
                        }
                    }
                }
                
                formRenoviranje.datePickerPocetak.DisplayDateStart = DateTime.Now;
                formRenoviranje.datePickerKraj.DisplayDateStart = DateTime.Now;
                formRenoviranje.Show();
            }
            
        }
    }
}
