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
            storage = new FileStorageProstorija();
            /*Prostorija prva = new Prostorija { BrojProstorije = 101, Sprat = 1, Kvadratura = 50.5, TipProstorije = TipProstorije.salaZaPreglede, Zauzeta = true, Obrisana = false };
            Prostorija druga = new Prostorija { BrojProstorije = 102, Sprat = 1, Kvadratura = 65, TipProstorije = TipProstorije.operacionaSala, Zauzeta = true, Obrisana = false};
            BolnickaSoba treca = new BolnickaSoba { BrojProstorije = 111, Sprat = 1, Kvadratura = 50.1, TipProstorije = TipProstorije.bolnickaSoba, Zauzeta = false, Obrisana = false, BrojSlobodnihKreveta = 10, UkBrojKreveta = 12 };
            storage.Save(prva);
            storage.Save(druga);
            storage.Save(treca);*/
            List<Prostorija> prostorije = storage.GetAllProstorije();
            foreach (Prostorija p in prostorije)
            {
                if(p.Obrisana == false)
                    Prostorije.Add(p);
            }
            List<BolnickaSoba> bolnickeSobe = storage.GetAllBolnickeSobe();
            foreach (BolnickaSoba b in bolnickeSobe)
            {
                if(b.Obrisana == false)
                    Prostorije.Add(b);
            }
        }

        private void Button_Click_Dodaj(object sender, RoutedEventArgs e)
        {
            var s = new CreateFormProstorije();
            clickedDodaj = true;
            s.Show();
        }

        private void Button_Click_Prikazi(object sender, RoutedEventArgs e)
        {
            if (dataGridProstorije.SelectedCells.Count > 0)
            {
                Prostorija row = (Prostorija)dataGridProstorije.SelectedItems[0];
                List<Prostorija> prostorije = storage.GetAllProstorije();
                List<BolnickaSoba> bolnickeSobe = storage.GetAllBolnickeSobe();
                var s = new ViewFormProstorije();
                bool found = false;
                foreach (Prostorija p in prostorije)
                {
                    if (p.BrojProstorije == row.BrojProstorije)
                    {
                        s.lblUkBrojKreveta.Visibility = Visibility.Hidden;
                        s.lblBrojSlobodnihKreveta.Visibility = Visibility.Hidden;
                        s.lblBrojProstorije.Content = p.BrojProstorije.ToString();
                        s.lblSprat.Content = p.Sprat.ToString();
                        s.lblKvadratura.Content = p.Kvadratura.ToString();
                        s.checkZauzeta.IsEnabled = false;
                        if (p.TipProstorije == TipProstorije.salaZaPreglede)
                            s.lblTipProstorije.Content = "Sala za preglede";
                        else
                            s.lblTipProstorije.Content = "Operaciona sala";
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
                            s.lblBrojProstorije.Content = b.BrojProstorije.ToString();
                            s.lblSprat.Content = b.Sprat.ToString();
                            s.lblKvadratura.Content = b.Kvadratura.ToString();
                            s.checkZauzeta.IsEnabled = false;
                            s.lblTipProstorije.Content = "Bolnička soba";
                            s.checkZauzeta.IsChecked = b.Zauzeta;
                            s.lblUkBrojKreveta.Visibility = Visibility.Visible;
                            s.lblUkBrKreveta.Content = b.UkBrojKreveta.ToString();
                            s.lblBrojSlobodnihKreveta.Visibility = Visibility.Visible;
                            s.lblBrSlobodnihKreveta.Content = b.BrojSlobodnihKreveta.ToString();
                            s.Show();
                            break;
                        }
                    }
                }
            }
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

        private void Button_Click_Obrisi(object sender, RoutedEventArgs e)
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
                    storage.Delete(p);
                    p.Obrisana = true;
                    storage.Save(p);
                    
                    for(int i = 0; i < FormUpravnik.Prostorije.Count; i++)
                    {
                        if(FormUpravnik.Prostorije[i].BrojProstorije == row.BrojProstorije)
                        {
                            FormUpravnik.Prostorije.Remove(FormUpravnik.Prostorije[i]);
                        }
                    }
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
                        storage.Delete(b);
                        b.Obrisana = true;
                        storage.Save(b);

                        for (int i = 0; i < FormUpravnik.Prostorije.Count; i++)
                        {
                            if (FormUpravnik.Prostorije[i].BrojProstorije == row.BrojProstorije)
                            {
                                FormUpravnik.Prostorije.Remove(FormUpravnik.Prostorije[i]);
                            }
                        }

                        break;
                    }
                }
            }
        }
    }
}
