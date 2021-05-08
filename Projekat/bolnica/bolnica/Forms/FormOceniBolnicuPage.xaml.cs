using Bolnica.Model;
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
    /// Interaction logic for FormOceniBolnicuPage.xaml
    /// </summary>
    public partial class FormOceniBolnicuPage : Page
    {
        private Pacijent pacijent = new Pacijent();

        private FormPacijentWeb form;

        public FormOceniBolnicuPage(Pacijent trenutniPacijent, FormPacijentWeb formPacijentWeb)
        {
            InitializeComponent();

            form = formPacijentWeb;
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
                FileStorageOcene storageOcene = new FileStorageOcene();
                List<Ocena> ocene = storageOcene.GetAll();
                if (ocene is null)
                {
                    ocene = new List<Ocena>();
                }

                Ocena ocena = new Ocena();

                int max = 0;
                foreach (Ocena o in ocene)
                {
                    if (o.IdOcene > max)
                    {
                        max = o.IdOcene;
                    }
                }
                ocena.IdOcene = max + 1;

                ocena.Datum = DateTime.Now;

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
                ocena.BrojOcene = brojOcene;

                ocena.Sadrzaj = sadrzaj.Text;

                ocena.PosiljalacJMBG = pacijent.Jmbg;

                ocena.Primalac = "Bolnica";

                storageOcene.Save(ocena);

                form.Pocetna.Content = new FormIstorijaPregledaPage(pacijent, form);
            }
        }

        private void Button_Click_Otkazi(object sender, RoutedEventArgs e)
        {
            form.Pocetna.Content = new FormIstorijaPregledaPage(pacijent, form);
        }
    }
}
