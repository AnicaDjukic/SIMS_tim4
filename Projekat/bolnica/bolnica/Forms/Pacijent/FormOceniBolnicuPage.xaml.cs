using Bolnica.Model;
using Bolnica.Model.Korisnici;
using Bolnica.Repository.Pregledi;
using Model.Korisnici;
using System;
using System.Collections.Generic;
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
        private FileRepositoryOcena storageOcene = new FileRepositoryOcena();

        public FormOceniBolnicuPage(Pacijent trenutniPacijent)
        {
            InitializeComponent();

            pacijent = trenutniPacijent;
        }

        private void Button_Click_Potvrdi(object sender, RoutedEventArgs e)
        {
            if (jedan.IsChecked == false && dva.IsChecked == false && tri.IsChecked == false && cetiri.IsChecked == false && pet.IsChecked == false)
            {
                MessageBox.Show("Morate dati ocenu bolnici da biste potvrdili!", "Upozorenje");
            }
            else
            {
                List<Ocena> ocene = storageOcene.GetAll();
                if (ocene is null)
                {
                    ocene = new List<Ocena>();
                }

                int max = 0;
                foreach (Ocena o in ocene)
                {
                    if (o.IdOcene > max)
                    {
                        max = o.IdOcene;
                    }
                }
                
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

                Ocena ocena = new Ocena
                {
                    IdOcene = max + 1,
                    BrojOcene = brojOcene,
                    Datum = DateTime.Now,
                    Sadrzaj = sadrzaj.Text,
                    Pacijent = pacijent,
                    Lekar = new Lekar()
                };
                ocena.Lekar.Jmbg = null;
                ocena.Lekar.KorisnickoIme = null;

                storageOcene.Save(ocena);

                FormPacijentWeb.Forma.Pocetna.Content = new FormIstorijaPregledaPage(pacijent);
            }
        }

        private void Button_Click_Otkazi(object sender, RoutedEventArgs e)
        {
            FormPacijentWeb.Forma.Pocetna.Content = new FormIstorijaPregledaPage(pacijent);
        }
    }
}
