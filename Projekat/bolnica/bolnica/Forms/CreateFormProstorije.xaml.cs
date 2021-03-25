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
    }
}
