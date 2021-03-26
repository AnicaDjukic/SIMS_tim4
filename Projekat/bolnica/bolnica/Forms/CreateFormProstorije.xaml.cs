using bolnica.Forms;
using Model.Prostorije;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

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
            int brojProstorije = Int32.Parse(txtBrojProstorije.Text);
            int sprat = Int32.Parse(txtSprat.Text);
            double kvadratura = Double.Parse(txtKvadratura.Text);
            int tipProstorije = comboTipProstorije.SelectedIndex;
            bool zauzeta = (bool)checkZauzeta.IsChecked;
            if (tipProstorije == 2)
            {
                int brojSlobodnihKreveta = Int32.Parse(txtBrojSlobodnihKreveta.Text);
                int ukBrojKreveta = Int32.Parse(txtUkBrojKreveta.Text);
                BolnickaSoba prostorija = new BolnickaSoba { BrojProstorije = brojProstorije, Sprat = sprat, Kvadratura = kvadratura, TipProstorije = TipProstorije.bolnickaSoba, Zauzeta = zauzeta, UkBrojKreveta = ukBrojKreveta, BrojSlobodnihKreveta = brojSlobodnihKreveta};

                if (prostorija.BrojSlobodnihKreveta == prostorija.UkBrojKreveta)
                {
                    prostorija.Zauzeta = true;
                }
                update(prostorija);
            }
            else
            {
                Prostorija prostorija = new Prostorija();
                prostorija.BrojProstorije = brojProstorije;
                prostorija.Sprat = sprat;
                prostorija.Kvadratura = kvadratura;

                if (tipProstorije == 0)
                {
                    prostorija.TipProstorije = TipProstorije.salaZaPreglede;
                }
                else
                {
                    prostorija.TipProstorije = TipProstorije.operacionaSala;
                }
                update(prostorija);
            }
            this.Close();
        }

        private void update(Prostorija prostorija)
        {
            bool postoji = false;
            FileStorageProstorija storage = new FileStorageProstorija();
            List<BolnickaSoba> bolnickeSobe = storage.GetAllBolnickeSobe();
            List<Prostorija> prostorije = storage.GetAllProstorije();
            foreach (Prostorija p in prostorije)
            {
                if (p.BrojProstorije == prostorija.BrojProstorije)
                {
                    if (FormUpravnik.clickedDodaj)
                    {
                        MessageBox.Show("Prostorija vec postoji");
                        postoji = true;
                        FormUpravnik.clickedDodaj = false;
                    }
                    else
                    {
                        storage.Delete(p);
                        for (int i = 0; i < FormUpravnik.Prostorije.Count; i++)
                        {
                            if (FormUpravnik.Prostorije[i].BrojProstorije == prostorija.BrojProstorije)
                            {
                                FormUpravnik.Prostorije.Remove(FormUpravnik.Prostorije[i]);
                                break;
                            }

                        }
                    }
                }
            }

            foreach (BolnickaSoba p in bolnickeSobe)
            {
                if (p.BrojProstorije == prostorija.BrojProstorije)
                {
                    if (FormUpravnik.clickedDodaj)
                    {
                        MessageBox.Show("Prostorija vec postoji");
                        postoji = true;
                        FormUpravnik.clickedDodaj = false;
                    }
                    else
                    {
                        storage.Delete(p);
                        for (int i = 0; i < FormUpravnik.Prostorije.Count; i++)
                        {
                            if (FormUpravnik.Prostorije[i].BrojProstorije == prostorija.BrojProstorije)
                            {
                                FormUpravnik.Prostorije.Remove(FormUpravnik.Prostorije[i]);
                                break;
                            }

                        }
                    }
                }
            }
            FormUpravnik.clickedDodaj = false;
            if (!postoji)
            {
                storage.Save(prostorija);
                FormUpravnik.Prostorije.Add(prostorija);
            }
        }

        private void update(BolnickaSoba prostorija)
        {
            bool postoji = false;
            FileStorageProstorija storage = new FileStorageProstorija();
            List<BolnickaSoba> bolnickeSobe = storage.GetAllBolnickeSobe();
            List<Prostorija> prostorije = storage.GetAllProstorije();
            foreach (Prostorija p in prostorije)
            {
                if (p.BrojProstorije == prostorija.BrojProstorije)
                {
                    if (FormUpravnik.clickedDodaj)
                    {
                        MessageBox.Show("Prostorija vec postoji");
                        postoji = true;
                        FormUpravnik.clickedDodaj = false;
                    }
                    else
                    {
                        storage.Delete(p);
                        for (int i = 0; i < FormUpravnik.Prostorije.Count; i++)
                        {
                            if (FormUpravnik.Prostorije[i].BrojProstorije == prostorija.BrojProstorije)
                            {
                                FormUpravnik.Prostorije.Remove(FormUpravnik.Prostorije[i]);
                                break;
                            }

                        }
                    }
                }
            }

            foreach (BolnickaSoba p in bolnickeSobe)
            {
                if (p.BrojProstorije == prostorija.BrojProstorije)
                {
                    if (FormUpravnik.clickedDodaj)
                    {
                        MessageBox.Show("Prostorija vec postoji");
                        postoji = true;
                        FormUpravnik.clickedDodaj = false;
                    }
                    else
                    {
                        storage.Delete(p);
                        for (int i = 0; i < FormUpravnik.Prostorije.Count; i++)
                        {
                            if (FormUpravnik.Prostorije[i].BrojProstorije == prostorija.BrojProstorije)
                            {
                                FormUpravnik.Prostorije.Remove(FormUpravnik.Prostorije[i]);
                                break;
                            }

                        }
                    }
                }
            }
            FormUpravnik.clickedDodaj = false;
            if (!postoji)
            {
                storage.Save(prostorija);
                FormUpravnik.Prostorije.Add(prostorija);
            }
        }

        private void comboTipProstorije_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboTipProstorije.SelectedIndex == 2)
            {
                lblUkBrojKreveta.Visibility = Visibility.Visible;
                txtUkBrojKreveta.Visibility = Visibility.Visible;
                lblBrojSlobodnihKreveta.Visibility = Visibility.Visible;
                txtBrojSlobodnihKreveta.Visibility = Visibility.Visible;
            } else
            {
                lblUkBrojKreveta.Visibility = Visibility.Hidden;
                txtUkBrojKreveta.Visibility = Visibility.Hidden;
                lblBrojSlobodnihKreveta.Visibility = Visibility.Hidden;
                txtBrojSlobodnihKreveta.Visibility = Visibility.Hidden;
            }
        }
    }
}
