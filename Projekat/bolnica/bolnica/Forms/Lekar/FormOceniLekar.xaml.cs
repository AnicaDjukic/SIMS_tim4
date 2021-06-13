using Bolnica.Model.Korisnici;
using Bolnica.Controller.Korisnici;
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
using System.Windows.Shapes;

namespace Bolnica.Forms
{
    /// <summary>
    /// Interaction logic for FormOceniLekar.xaml
    /// </summary>
    public partial class FormOceniLekar : Window
    {
        private OcenaAplikacije ocena = new OcenaAplikacije();

        private Injector inject;
        public Injector Inject
        {
            get { return inject; }
            set
            {
                inject = value;
            }
        }
        public FormOceniLekar()
        {
            InitializeComponent();
            Inject = new Injector();

        }
        private void btnPotvrdi_Click(object sender, RoutedEventArgs e)
        {
            if (rbVrloLose.IsChecked == true)
                ocena.BrojnaVrednost = 1;
            else if (rbLose.IsChecked == true)
                ocena.BrojnaVrednost = 2;
            else if (rbDobro.IsChecked == true)
                ocena.BrojnaVrednost = 3;
            else if (rbVrloDobro.IsChecked == true)
                ocena.BrojnaVrednost = 4;
            else
                ocena.BrojnaVrednost = 5;

            ocena.Komentar = txtKomentar.Text;

            Inject.ControllerOcenaAplikacije.SacuvajOcenuAplikacije(ocena);

            Close();
        }
        private void btnOdustani_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void RBVrloLosePritisnuto(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (rbVrloLose.IsChecked.Equals(true))
                {
                    rbVrloLose.IsChecked = false;
                }
                else
                {
                    rbVrloLose.IsChecked = true;
                }
            }
        }

        private void RBLosePritisnuto(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (rbLose.IsChecked.Equals(true))
                {
                    rbLose.IsChecked = false;
                }
                else
                {
                    rbLose.IsChecked = true;
                }
            }
        }

        private void RBVrloDobroPritisnuto(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (rbVrloDobro.IsChecked.Equals(true))
                {
                    rbVrloDobro.IsChecked = false;
                }
                else
                {
                    rbVrloDobro.IsChecked = true;
                }
            }
        }

        private void RBDobroPritisnuto(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (rbDobro.IsChecked.Equals(true))
                {
                    rbDobro.IsChecked = false;
                }
                else
                {
                    rbDobro.IsChecked = true;
                }
            }
        }

        private void RBOdlicnoPritisnuto(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (rbOdlicno.IsChecked.Equals(true))
                {
                    rbOdlicno.IsChecked = false;
                }
                else
                {
                    rbOdlicno.IsChecked = true;
                }
            }
        }
    }
}
