using bolnica.Forms;
using Model.Prostorije;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Bolnica.Forms
{
    /// <summary>
    /// Interaction logic for CreateFormProstorije.xaml
    /// </summary>
    public partial class CreateFormProstorije : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private string brojProstorije;
        private int sprat;
        private double kvadratura;
        private int brojSlobodnihKreveta;
        private int ukBrojKreveta;
        public string BrojProstorije
        {
            get
            {
                return brojProstorije;
            }
            set
            {
                if (value != brojProstorije)
                {
                    brojProstorije = value;
                    OnPropertyChanged("BrojProstorije");
                }
            }
        }

        public int Sprat
        {
            get
            {
                return sprat;
            }
            set
            {
                if (value != sprat)
                {
                    sprat = value;
                    OnPropertyChanged("Sprat");
                }
            }
        }

        public double Kvadratura
        {
            get
            {
                return kvadratura;
            }
            set
            {
                if (value != kvadratura)
                {
                    kvadratura = value;
                    OnPropertyChanged("Kvadratura");
                }
            }
        }

        public int BrojSlobodnihKreveta
        {
            get
            {
                return brojSlobodnihKreveta;
            }
            set
            {
                if (value != brojSlobodnihKreveta)
                {
                    brojSlobodnihKreveta = value;
                    OnPropertyChanged("BrojSlobodnihKreveta");
                }
            }
        }

        public int UkBrojKreveta
        {
            get
            {
                return ukBrojKreveta;
            }
            set
            {
                if (value != ukBrojKreveta)
                {
                    ukBrojKreveta = value;
                    OnPropertyChanged("UkBrojKreveta");
                }
            }
        }

        public CreateFormProstorije()
        {
            InitializeComponent();
            this.DataContext = this;
            if (!FormUpravnik.clickedDodaj)
            {
                Title = "Izmeni prostoriju";
            }
            lblUkBrojKreveta.Visibility = Visibility.Hidden;
            txtUkBrojKreveta.Visibility = Visibility.Hidden;
            lblBrojSlobodnihKreveta.Visibility = Visibility.Hidden;
            txtBrojSlobodnihKreveta.Visibility = Visibility.Hidden;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int tipProstorije = comboTipProstorije.SelectedIndex;
            bool zauzeta = (bool)checkZauzeta.IsChecked;
            if (tipProstorije == 2)
            {
                BolnickaSoba prostorija = new BolnickaSoba { BrojProstorije = BrojProstorije, Sprat = Sprat, Kvadratura = Kvadratura, TipProstorije = TipProstorije.bolnickaSoba, Zauzeta = zauzeta, Obrisana = false, UkBrojKreveta = UkBrojKreveta, BrojSlobodnihKreveta = BrojSlobodnihKreveta};

                if (prostorija.BrojSlobodnihKreveta == 0)
                {
                    prostorija.Zauzeta = true;
                }
                update(prostorija);
            }
            else
            {
                Prostorija prostorija = new Prostorija();
                prostorija.BrojProstorije = BrojProstorije;
                prostorija.Sprat = Sprat;
                prostorija.Kvadratura = Kvadratura;

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
                        MessageBox.Show("Prostorija već postoji");
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
                        MessageBox.Show("Prostorija već postoji");
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
                        MessageBox.Show("Prostorija već postoji");
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
                        MessageBox.Show("Prostorija već postoji");
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

        private void checkZauzeta_Checked(object sender, RoutedEventArgs e)
        {
            if (comboTipProstorije.SelectedIndex == 2)
                txtBrojSlobodnihKreveta.Text = "0";
        }
    }
}
