using Bolnica;
using Bolnica.DTO;
using Bolnica.Forms;
using Bolnica.Forms.Upravnik;
using Bolnica.Forms.Upravnik.FactoryMethod;
using Bolnica.Localization;
using Bolnica.Model.Pregledi;
using Bolnica.Model.Prostorije;
using Bolnica.ViewModel.Upravnik;
using Model.Pregledi;
using Model.Prostorije;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
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

        private Injector inject;
        public Injector Inject
        {
            get { return inject; }
            set
            {
                inject = value;
            }
        }

        Operator<Prostorija, string> operatorProstorije = new OperatorProstorije();
        Operator<Oprema, string> operatorOpreme = new OperatorOpreme();
        Operator<Lek, int> operatorLeka = new OperatorLeka();
        public FormUpravnik()
        {
            InitializeComponent();
            this.DataContext = this;
            Inject = new Injector();
            clickedDodaj = false;
            AzurirajVreme();
            Inject.ControllerRenoviranje.PodeliISpojiProstorije();
            InicijalizujPrikaz();
        }

        private void AzurirajVreme()
        {
            Title = LocalizedStrings.Instance["Upravnik"];
            DispatcherTimer timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                Vreme.Text = DateTime.Now.ToString("HH:mm");
                Datum.Text = DateTime.Now.ToString("dd/MM/yyyy");

            }, Dispatcher);
        }

        private void InicijalizujPrikaz()
        {
            PrikaziSveProstorije();
            PrikaziSvuOpremu();
            PrikaziSveLekove();
        }

        private void PrikaziSveProstorije()
        {
            Prostorije = new ObservableCollection<Prostorija>();
            PrikaziProstorije();
            PrikaziBolnickeSobe();
        }

        private void PrikaziProstorije()
        {
            List<Prostorija> prostorije = Inject.ControllerProstorija.DobaviSveProstorije();
            foreach (Prostorija p in prostorije)
            {
                if (p.Obrisana == false)
                    Prostorije.Add(p);
            }
        }

        private void PrikaziBolnickeSobe()
        {
            List<BolnickaSoba> bolnickeSobe = Inject.ControllerBolnickaSoba.DobaviSveBolnickeSobe();
            foreach (BolnickaSoba b in bolnickeSobe)
            {
                if (b.Obrisana == false)
                    Prostorije.Add(b);
            }
        }

        private void PrikaziSvuOpremu()
        {
            Oprema = new ObservableCollection<Oprema>();

            List<Oprema> oprema = Inject.ControllerOprema.DobaviSvuOpremu();
            if (oprema != null)
            {
                foreach (Oprema o in oprema)
                    Oprema.Add(o);
            }
        }

        private void PrikaziSveLekove()
        {
            Lekovi = new ObservableCollection<Lek>();

            List<Lek> lekovi = Inject.ControllerLek.DobaviSveLekove();
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
                operatorProstorije.OperacijaDodavanja();
            else if (Tabovi.SelectedIndex == 1)
                operatorOpreme.OperacijaDodavanja();
            else
                operatorLeka.OperacijaDodavanja();
        }

        private void Button_Click_Prikazi(object sender, RoutedEventArgs e)
        {
            if (dataGridProstorije.SelectedCells.Count > 0 && Tabovi.SelectedIndex == 0)
            {
                Prostorija prostorija = (Prostorija)dataGridProstorije.SelectedItems[0];
                OtvoriFormuZaPrikazProstorije(prostorija.BrojProstorije);
            }
            else if (dataGridOprema.SelectedCells.Count > 0 && Tabovi.SelectedIndex == 1)
            {
                Oprema oprema = (Oprema)dataGridOprema.SelectedItems[0];
                OtvoriFormuZaPrikazOpreme(oprema.Sifra);
            }
            else
            {
                Lek lek = (Lek)dataGridLekovi.SelectedItems[0];
                OtvoriFormuZaPrikazLeka(lek.Id);
            }
        }

        private void OtvoriFormuZaPrikazProstorije(string brojProstorije)
        {
            var s = new ViewFormProstorije(brojProstorije);
            s.Show();
        }

        private void OtvoriFormuZaPrikazOpreme(string sifra)
        {
            var s = new ViewFormOprema(sifra);
            s.Show();
        }

        private void OtvoriFormuZaPrikazLeka(int idLeka)
        {
            var s = new ViewFormLek(idLeka);
            s.Show();
        }

        private void Button_Click_Izmeni(object sender, RoutedEventArgs e)
        {
            clickedDodaj = false;
            if (dataGridProstorije.SelectedCells.Count > 0 && Tabovi.SelectedIndex == 0)
            {
                Prostorija prostorija = (Prostorija)dataGridProstorije.SelectedItems[0];
                OtvoriFormuZaIzmenuProstorije(prostorija.BrojProstorije);

            }
            else if (dataGridOprema.SelectedCells.Count > 0 && Tabovi.SelectedIndex == 1)
            {
                Oprema oprema = (Oprema)dataGridOprema.SelectedItems[0];
                OtvoriFormuZaIzmenuOpreme(oprema.Sifra);

            }
            else if (dataGridLekovi.SelectedCells.Count > 0 && Tabovi.SelectedIndex == 2)
            {
                Lek lek = (Lek)dataGridLekovi.SelectedItems[0];
                if (lek.Status == StatusLeka.cekaValidaciju)
                    MessageBox.Show(LocalizedStrings.Instance["Nije moguće izmeniti lek koji čeka validaciju!"]);
                else
                    OtvoriFormuZaIzmenuLeka(lek.Id);
            }
        }

        private void OtvoriFormuZaIzmenuProstorije(string brojProstorije)
        {
            var s = new CreateFormProstorije(brojProstorije);
            s.Show();
        }

        private void OtvoriFormuZaIzmenuOpreme(string sifra)
        {
            var s = new CreateFormOprema(sifra);
            s.Show();
        }

        private void OtvoriFormuZaIzmenuLeka(int id)
        {
            ViewModelCreateFormLekovi vm = new ViewModelCreateFormLekovi(id);
            CreateFormLekovi s = new CreateFormLekovi(vm);
        }

        private void Button_Click_Obrisi(object sender, RoutedEventArgs e)
        {
            if (dataGridProstorije.SelectedCells.Count > 0 && Tabovi.SelectedIndex == 0)
            {
                Prostorija prostorija = (Prostorija)dataGridProstorije.SelectedItems[0];
                operatorProstorije.OperacijaBrisanja(prostorija);
            }
            else if (dataGridOprema.SelectedCells.Count > 0 && Tabovi.SelectedIndex == 1)
            {
                Oprema oprema = (Oprema)dataGridOprema.SelectedItems[0];
                operatorOpreme.OperacijaBrisanja(oprema);
            }
            else
            {
                Lek lek = (Lek)dataGridLekovi.SelectedItems[0];
                operatorLeka.OperacijaBrisanja(lek);
            }
        }

        private void Button_Click_Renoviranje(object sender, RoutedEventArgs e)
        {
            if (dataGridProstorije.SelectedCells.Count > 0)
            {
                Prostorija renoviranje = (Prostorija)dataGridProstorije.SelectedItem;
                FormRenoviranje formRenoviranje = new FormRenoviranje(renoviranje.BrojProstorije);
                formRenoviranje.Show();
            }
        }

        private void Button_Click_Obavestenja(object sender, RoutedEventArgs e)
        {
            FormObavestenja s = new FormObavestenja();
            s.Show();
        }

        private void Button_Click_Search(object sender, RoutedEventArgs e)
        {
            List<Oprema> oprema = new List<Oprema>();
            foreach (Oprema o in Inject.ControllerOprema.DobaviSvuOpremu())
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
                    string dinamicka = LocalizedStrings.Instance["dinamička"];
                    if (dinamicka.Contains(txtSearch.Text.ToLower()))
                    {
                        oprema.Remove(o);
                        oprema.Add(o);
                    }
                }

                if (o.TipOpreme == TipOpreme.staticka)
                {
                    string staticka = LocalizedStrings.Instance["statička"];
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

        private void Tabovi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Tabovi.SelectedIndex == 1)
                comboTipOpreme.Visibility = Visibility.Visible;
            else
                comboTipOpreme.Visibility = Visibility.Hidden;
        }

        private void comboTipOpreme_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboTipOpreme.Visibility == Visibility.Visible)
            {
                List<Oprema> oprema = new List<Oprema>();

                foreach (Oprema o in Inject.ControllerOprema.DobaviSvuOpremu())
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

        private void Theme_Click(object sender, RoutedEventArgs e)
        {
            var app = (App)Application.Current;

            MenuItem mi = (MenuItem)sender;
            if (mi.Header.Equals(LocalizedStrings.Instance["Tamna tema"]))
            {
                app.ChangeTheme(new Uri("Themes/Light.xaml", UriKind.Relative));
                mi.Header = LocalizedStrings.Instance["Svetla tema"];
            }
            else
            {
                app.ChangeTheme(new Uri("Themes/Dark.xaml", UriKind.Relative));
                mi.Header = LocalizedStrings.Instance["Tamna tema"];
            }
        }

        private void SrpskiMenuItem_Click(object sender, RoutedEventArgs e)
        {
            IzaberiJezikMenuItem.Header = "SRB";
            IzaberiJezikMenuItem.Icon = new Image
            {
                Source = new BitmapImage(new Uri("../Images/serbia.png", UriKind.Relative))
            };
            LocalizedStrings.Instance.SetCulture("sr-LATN-CS");
        }

        private void EngleskiMenuItem_Click(object sender, RoutedEventArgs e)
        {
            IzaberiJezikMenuItem.Header = "ENG";
            IzaberiJezikMenuItem.Icon = new Image
            {
                Source = new BitmapImage(new Uri("../Images/great-britain.png", UriKind.Relative))
            };
            LocalizedStrings.Instance.SetCulture("en-US");
        }

        private void OdjaviSeMenuItem_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }

        private void Button_Click_Izvestaj(object sender, RoutedEventArgs e)
        {
            Report r = new Report();
            r.Show();
        }

        private void Button_Click_Premesti(object sender, RoutedEventArgs e)
        {
            if (dataGridOprema.SelectedCells.Count > 0)
            {
                var s = new FormSkladiste((Oprema)dataGridOprema.SelectedItem);
                s.Show();
            }
        }

        private void Button_Click_Oceni(object sender, RoutedEventArgs e)
        {
            var s = new FormOceniAplikaciju();
            s.Show();
        }
    }
}
