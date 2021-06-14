using Bolnica.Localization;
using Bolnica.Model.Korisnici;
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

namespace Bolnica.Forms.Upravnik
{
    /// <summary>
    /// Interaction logic for FormOceniAplikaciju.xaml
    /// </summary>
    public partial class FormOceniAplikaciju : Window
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
        public FormOceniAplikaciju()
        {
            InitializeComponent();
            Inject = new Injector();
            Title = LocalizedStrings.Instance["Ocenjivanje aplikacije"];
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
    }
}
