using bolnica;
using Bolnica.Controller;
using Bolnica.Model.Korisnici;
using Bolnica.ViewModel;
using Model.Korisnici;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Threading;

namespace Bolnica.Forms
{
    /// <summary>
    /// Interaction logic for FormPacijentWeb.xaml
    /// </summary>
    public partial class FormPacijentWeb : Window
    {
        public static FormPacijentWeb Forma;
        public static Pacijent Pacijent;

        private PodsetnikController podsetnikController = new PodsetnikController();
        private AktivnostController controllerAktivnost = new AktivnostController();

        public static string ImeIPre
        {
            get;
            set;
        }

        public FormPacijentWeb(Pacijent pacijent)
        {
            FormObavestenjaPacijentPage.ObavestenjaPacijent = new ObservableCollection<Obavestenje>();
            InitializeComponent();
            this.DataContext = this;
            Forma = this;
            Pacijent = pacijent;

            PacijentPageViewModel pacijentPageViewModel = new PacijentPageViewModel(pacijent);
            Pocetna.Content = new FormPacijentPage(pacijentPageViewModel);
            ImeIPre = pacijent.Ime + " " + pacijent.Prezime;
        }

        private void Button_Click_Pocetna_Stranica(object sender, RoutedEventArgs e)
        {
            PacijentPageViewModel pacijentPageViewModel = new PacijentPageViewModel(Pacijent);
            Pocetna.Content = new FormPacijentPage(pacijentPageViewModel);
        }

        private void Button_Click_Zakazivanje_Pregleda(object sender, RoutedEventArgs e)
        {
            int brojac = controllerAktivnost.DobijBrojAktivnosti(Pacijent);
            
            if (brojac > 5)
            {
                podsetnikController.BlokirajPacijenta(Pacijent);
                Close();
            }
            else
            {
                if (brojac > 4)
                {
                    MessageBox.Show("Posljednje upozorenje pred gasenje Vaseg naloga. Ukoliko nastavite da zloupotrebljavate " +
                        "nasu aplikaciju pristup samoj aplikaciji ce Vam biti onemogucen!", "Upozorenje");
                }
                ZakaziPregledPacijentViewModel zakaziPregledPacijentViewModel = new ZakaziPregledPacijentViewModel(Pacijent);
                Pocetna.Content = new FormZakaziPacijentPage(zakaziPregledPacijentViewModel);
            }
            
        }

        public void DanasnjaObavestenja()
        {
            new PacijentPageViewModel(Pacijent).PrikaziObavestenja();
        }

        private void Button_Click_Istorija_Pregleda(object sender, RoutedEventArgs e)
        {
            IstorijaPregledaPacijentViewModel istorijaPregledaPacijentViewModel = new IstorijaPregledaPacijentViewModel(Pacijent);
            Pocetna.Content = new FormIstorijaPregledaPage(istorijaPregledaPacijentViewModel);
        }

        private void Button_Click_Obavestenja(object sender, RoutedEventArgs e)
        {
            Pocetna.Content = new FormObavestenjaPacijentPage(Pacijent);
        }

        private void Button_Click_Odjava(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }

        private void Button_Click_Lekovi_Terapije(object sender, RoutedEventArgs e)
        {
            Pocetna.Content = new FormLekoviTerapijePage(Pacijent);
        }

        private void Button_Click_Zdravstveni_Karton(object sender, RoutedEventArgs e)
        {
            Pocetna.Content = new FormZdravstveniKartonPage(Pacijent);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DispatcherTimer timer = new DispatcherTimer(TimeSpan.FromSeconds(1), DispatcherPriority.Normal, (object s, EventArgs ev) =>
            {
                vremeLabel.Content = DateTime.Now;
                podsetnikController.ProveriObavestenja(Pacijent);
            }, this.Dispatcher);
            timer.Start();
        }

        private void Button_Click_Oceni_Aplikaciju(object sender, RoutedEventArgs e)
        {
            Pocetna.Content = new FormOceniAplikacijuPage(Pacijent);
        }
    }
}
