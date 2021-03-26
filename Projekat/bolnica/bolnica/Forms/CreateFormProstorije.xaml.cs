using bolnica.Forms;
using Model.Prostorije;
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
    /// Interaction logic for CreateFormProstorije.xaml
    /// </summary>
    public partial class CreateFormProstorije : Window
    {
        public CreateFormProstorije()
        {
            InitializeComponent();
            lblUkBrojKreveta.Visibility = Visibility.Hidden;
            txtUkBrojKreveta.Visibility = Visibility.Hidden;
            lblBrojSlobodnihKreveta.Visibility = Visibility.Hidden;
            txtBrojSlobodnihKreveta.Visibility = Visibility.Hidden;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string brojProstorije = txtBrojProstorije.Text;
            string sprat = txtSprat.Text;
            string kvadratura = txtKvadratura.Text;
            int tipProstorije = comboTipProstorije.SelectedIndex;
            bool zauzeta = (bool)checkZauzeta.IsChecked;
            if (tipProstorije == 2)
            {
                BolnickaSoba prostorija = new BolnickaSoba();
                prostorija.BrojProstorije = Int32.Parse(brojProstorije);
                prostorija.Sprat = Int32.Parse(sprat);
                prostorija.Kvadratura = Double.Parse(kvadratura);
                prostorija.TipProstorije = TipProstorije.bolnickaSoba;
                prostorija.Zauzeta = zauzeta;
                prostorija.BrojSlobodnihKreveta = Int32.Parse(txtBrojSlobodnihKreveta.Text);
                prostorija.UkBrojKreveta = Int32.Parse(txtUkBrojKreveta.Text);
                if(prostorija.BrojSlobodnihKreveta == prostorija.UkBrojKreveta)
                {
                    prostorija.Zauzeta = true;
                }
                FileStorageProstorija storage = new FileStorageProstorija();
                storage.Save(prostorija);
                FormUpravnik.Prostorije.Add(prostorija);
            }
            else
            {
                Prostorija prostorija = new Prostorija();
                prostorija.BrojProstorije = Int32.Parse(brojProstorije);
                prostorija.Sprat = Int32.Parse(sprat);
                prostorija.Kvadratura = Double.Parse(kvadratura);

                if (tipProstorije == 0)
                {
                    prostorija.TipProstorije = TipProstorije.salaZaPreglede;
                }
                else
                {
                    prostorija.TipProstorije = TipProstorije.operacionaSala;
                }

                prostorija.Zauzeta = zauzeta;
                FileStorageProstorija storage = new FileStorageProstorija();
                storage.Save(prostorija);
                FormUpravnik.Prostorije.Add(prostorija);
            }
            this.Close();
        }

        private void comboTipProstorije_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {   
            if(comboTipProstorije.SelectedIndex == 2)
            {
                lblUkBrojKreveta.Visibility = Visibility.Visible;
                txtUkBrojKreveta.Visibility = Visibility.Visible;
                lblBrojSlobodnihKreveta.Visibility = Visibility.Visible;
                txtBrojSlobodnihKreveta.Visibility = Visibility.Visible;
            }
        }
    }
}
