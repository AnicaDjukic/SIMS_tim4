using Bolnica.Model.Korisnici;
using Bolnica.Repository.Korisnici;
using Bolnica.ViewModel;
using Model.Korisnici;
using System.Windows;
using System.Windows.Controls;

namespace Bolnica.Forms
{
    /// <summary>
    /// Interaction logic for FormOceniAplikacijuPage.xaml
    /// </summary>
    public partial class FormOceniAplikacijuPage : Page
    {
        private Pacijent pacijent = new Pacijent();
        public FormOceniAplikacijuPage(Pacijent trenutniPacijent)
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
                OcenaAplikacije ocena = KreirajOcenuAplikacije();
                FileRepositoryOcenaAplikacije repositoryOcenaAplikacije = new FileRepositoryOcenaAplikacije();
                repositoryOcenaAplikacije.Save(ocena);

                PacijentPageViewModel pacijentPageViewModel = new PacijentPageViewModel(pacijent);
                FormPacijentWeb.Forma.Pocetna.Content = new FormPacijentPage(pacijentPageViewModel);
            }
        }

        private OcenaAplikacije KreirajOcenuAplikacije()
        {
            return new OcenaAplikacije
            {
                BrojnaVrednost = DobijOdabranuOcenu(),
                Komentar = sadrzaj.Text
            };
        }

        private bool OcenaNijeCekirana()
        {
            return jedan.IsChecked == false && dva.IsChecked == false && tri.IsChecked == false && cetiri.IsChecked == false && pet.IsChecked == false;
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
            PacijentPageViewModel pacijentPageViewModel = new PacijentPageViewModel(pacijent);
            FormPacijentWeb.Forma.Pocetna.Content = new FormPacijentPage(pacijentPageViewModel);
        }
    }
}
