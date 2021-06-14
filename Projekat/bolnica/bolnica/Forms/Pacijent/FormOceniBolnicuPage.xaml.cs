using Bolnica.Controller;
using Bolnica.ViewModel;
using Model.Korisnici;
using Model.Pregledi;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Bolnica.Forms
{
    /// <summary>
    /// Interaction logic for FormOceniBolnicuPage.xaml
    /// </summary>
    public partial class FormOceniBolnicuPage : Page
    {
        private Pacijent pacijent = new Pacijent();
        private OcenaController controllerOcena = new OcenaController();

        public FormOceniBolnicuPage(Pacijent trenutniPacijent)
        {
            InitializeComponent();
            pacijent = trenutniPacijent;
        }

        private void Button_Click_Potvrdi(object sender, RoutedEventArgs e)
        {
            if (OcenaNijeCekirana())
            {
                MessageBox.Show("Morate dati ocenu bolnici da biste potvrdili!", "Upozorenje");
            }
            else
            {
                Ocena ocena = KreirajOcenu();
                controllerOcena.SacuvajOcenu(ocena);

                IstorijaPregledaPacijentViewModel istorijaPregledaPacijentViewModel = new IstorijaPregledaPacijentViewModel(pacijent);
                FormPacijentWeb.Forma.Pocetna.Content = new FormIstorijaPregledaPage(istorijaPregledaPacijentViewModel);
            }
        }

        private bool OcenaNijeCekirana()
        {
            return jedan.IsChecked == false && dva.IsChecked == false && tri.IsChecked == false && cetiri.IsChecked == false && pet.IsChecked == false;
        }

        private Ocena KreirajOcenu()
        {
            Ocena ocena = new Ocena
            {
                IdOcene = controllerOcena.IzracunajIdOcene(),
                BrojOcene = DobijOdabranuOcenu(),
                Datum = DateTime.Now,
                Sadrzaj = sadrzaj.Text,
                Pregled = new Pregled(),
                Pacijent = pacijent,
                Lekar = new Lekar()
            };
            ocena.Lekar.Jmbg = null;
            ocena.Lekar.KorisnickoIme = null;
            ocena.Pregled.Id = -1;
            ocena.Pregled.Pacijent = pacijent;
            return ocena;
        }

        private int DobijOdabranuOcenu()
        {
            int brojOcene = 0;
            if (jedan.IsChecked == true)
            {
                brojOcene = 1;
            }
            else if (dva.IsChecked == true)
            {
                brojOcene = 2;
            }
            else if (tri.IsChecked == true)
            {
                brojOcene = 3;
            }
            else if (cetiri.IsChecked == true)
            {
                brojOcene = 4;
            }
            else if (pet.IsChecked == true)
            {
                brojOcene = 5;
            }

            return brojOcene;
        }

        private void Button_Click_Otkazi(object sender, RoutedEventArgs e)
        {
            IstorijaPregledaPacijentViewModel istorijaPregledaPacijentViewModel = new IstorijaPregledaPacijentViewModel(pacijent);
            FormPacijentWeb.Forma.Pocetna.Content = new FormIstorijaPregledaPage(istorijaPregledaPacijentViewModel);
        }
    }
}
