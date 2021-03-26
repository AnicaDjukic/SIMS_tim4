using Bolnica.Forms;
using Model.Prostorije;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

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

        private FileStorageProstorija storage;

        public static bool clickedDodaj;

        public FormUpravnik()
        {
            InitializeComponent();
            clickedDodaj = false;
            this.DataContext = this;
            Prostorije = new ObservableCollection<Prostorija>();
            /*Prostorija prva = new Prostorija { BrojProstorije = 101, Sprat = 1, Kvadratura = 50.5, TipProstorije = TipProstorije.salaZaPreglede, Zauzeta = true };
            Prostorija druga = new Prostorija { BrojProstorije = 102, Sprat = 1, Kvadratura = 65, TipProstorije = TipProstorije.operacionaSala, Zauzeta = true };
            BolnickaSoba treca = new BolnickaSoba { BrojProstorije = 111, Sprat = 1, Kvadratura = 50.1, TipProstorije = TipProstorije.bolnickaSoba, Zauzeta = false, BrojSlobodnihKreveta = 10, UkBrojKreveta = 12 };
            storage = new FileStorageProstorija();
            storage.Save(prva);
            storage.Save(druga);
            storage.Save(treca);*/
            List<Prostorija> prostorije = storage.GetAllProstorije();
            foreach (Prostorija p in prostorije)
            {
                Prostorije.Add(p);
            }
            List<BolnickaSoba> bolnickeSobe = storage.GetAllBolnickeSobe();
            foreach (BolnickaSoba b in bolnickeSobe)
            {
                Prostorije.Add(b);
            }
        }

        private void Button_Click_Dodaj(object sender, RoutedEventArgs e)
        {
            var s = new CreateFormProstorije();
            clickedDodaj = true;
            s.Show();
        }

        private void Button_Click_Vidi(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_Izmeni(object sender, RoutedEventArgs e)
        {
            if (dataGridProstorije.SelectedCells.Count > 0)
            {
                Prostorija row = (Prostorija)dataGridProstorije.SelectedItems[0];
                List<Prostorija> prostorije = storage.GetAllProstorije();
                List<BolnickaSoba> bolnickeSobe = storage.GetAllBolnickeSobe();
                var s = new CreateFormProstorije();
                bool found = false;
                foreach (Prostorija p in prostorije)
                {
                    if (p.BrojProstorije == row.BrojProstorije)
                    {
                        s.txtBrojProstorije.Text = p.BrojProstorije.ToString();
                        s.txtSprat.Text = p.Sprat.ToString();
                        s.txtKvadratura.Text = p.Kvadratura.ToString();
                        if (p.TipProstorije == TipProstorije.salaZaPreglede)
                            s.comboTipProstorije.SelectedIndex = 0;
                        else
                            s.comboTipProstorije.SelectedIndex = 1;
                        s.checkZauzeta.IsChecked = p.Zauzeta;
                        s.Show();
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    foreach (BolnickaSoba b in bolnickeSobe)
                    {
                        if (b.BrojProstorije == row.BrojProstorije)
                        {
                            s.txtBrojProstorije.Text = b.BrojProstorije.ToString();
                            s.txtSprat.Text = b.Sprat.ToString();
                            s.txtKvadratura.Text = b.Kvadratura.ToString();
                            s.comboTipProstorije.SelectedIndex = 2;
                            s.checkZauzeta.IsChecked = b.Zauzeta;
                            s.lblUkBrojKreveta.Visibility = Visibility.Visible;
                            s.txtUkBrojKreveta.Visibility = Visibility.Visible;
                            s.txtUkBrojKreveta.Text = b.UkBrojKreveta.ToString();
                            s.lblBrojSlobodnihKreveta.Visibility = Visibility.Visible;
                            s.txtBrojSlobodnihKreveta.Visibility = Visibility.Visible;
                            s.txtBrojSlobodnihKreveta.Text = b.BrojSlobodnihKreveta.ToString();
                            s.Show();
                            break;
                        }
                    }
                }
            }
        }
    }
}
