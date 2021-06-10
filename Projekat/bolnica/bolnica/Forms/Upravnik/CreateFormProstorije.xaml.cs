using bolnica.Forms;
using Bolnica.Localization;
using Bolnica.Services.Prostorije;
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

        private Injector inject;
        public Injector Inject
        {
            get { return inject; }
            set
            {
                inject = value;
            }
        }
        public CreateFormProstorije()
        {
            InitializeComponent();
            Inject = new Injector();
            this.DataContext = this;
            if (!FormUpravnik.clickedDodaj)
                Title = LocalizedStrings.Instance["Izmena prostorije"];
            else
                Title = LocalizedStrings.Instance["Dodavanje prostorije"];
            SakrijPoljaZaBolnickuSobu();
        }

        private void SakrijPoljaZaBolnickuSobu()
        {
            lblUkBrojKreveta.Visibility = Visibility.Hidden;
            txtUkBrojKreveta.Visibility = Visibility.Hidden;
            lblBrojSlobodnihKreveta.Visibility = Visibility.Hidden;
            txtBrojSlobodnihKreveta.Visibility = Visibility.Hidden;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (BrojProstorije != null && Kvadratura != 0.0)
            {
                int tipProstorije = comboTipProstorije.SelectedIndex;
                if (!Inject.ControllerProstorija.ProstorijaPostoji(brojProstorije) || !FormUpravnik.clickedDodaj)
                {
                    if (tipProstorije == 2)
                    {
                        if (BrojSlobodnihKreveta <= UkBrojKreveta)
                        {
                            BolnickaSoba bolnickaSoba = NapraviBolnickuSobu();
                            SacuvajBolnickuSobu(bolnickaSoba);
                        } else
                        {
                            MessageBox.Show(LocalizedStrings.Instance["Broj slobodnih kreveta ne sme biti veći od ukupnog broja kreveta!"]);
                            return;
                        }
                    }
                    else
                    {
                        Prostorija prostorija = NapraviProstoriju(tipProstorije);
                        SacuvajProstoriju(prostorija);
                    }
                }
                else
                {
                    MessageBox.Show(LocalizedStrings.Instance["Prostorija već postoji!"]);
                    return;
                }
                this.Close();
            }
        }

        private BolnickaSoba NapraviBolnickuSobu()
        {
            bool zauzeta = (bool)checkZauzeta.IsChecked;
            BolnickaSoba bolnickaSoba = new BolnickaSoba { BrojProstorije = BrojProstorije, Sprat = Sprat, Kvadratura = Kvadratura, TipProstorije = TipProstorije.bolnickaSoba, Zauzeta = zauzeta, Obrisana = false, UkBrojKreveta = UkBrojKreveta, BrojSlobodnihKreveta = BrojSlobodnihKreveta };

            if (bolnickaSoba.BrojSlobodnihKreveta == 0)
            {
                bolnickaSoba.Zauzeta = true;
            }
            return bolnickaSoba;
        }

        private void SacuvajBolnickuSobu(BolnickaSoba bolnickaSoba)
        {
            if (!FormUpravnik.clickedDodaj)
            {
                Inject.ControllerBolnickaSoba.ObrisiBolnickuSobu(brojProstorije);
                UkloniIzPrikaza(brojProstorije);
            }

            Inject.ControllerBolnickaSoba.SacuvajBolnickuSobu(bolnickaSoba);
            FormUpravnik.Prostorije.Add(bolnickaSoba);
        }

        private void UkloniIzPrikaza(string brojProstorije)
        {
            for (int i = 0; i < FormUpravnik.Prostorije.Count; i++)
            {
                if (FormUpravnik.Prostorije[i].BrojProstorije == brojProstorije)
                {
                    FormUpravnik.Prostorije.Remove(FormUpravnik.Prostorije[i]);
                    break;
                }
            }
        }

        private Prostorija NapraviProstoriju(int tipProstorije)
        {
            bool zauzeta = (bool)checkZauzeta.IsChecked;
            Prostorija prostorija = new Prostorija { BrojProstorije = brojProstorije, Sprat = sprat, Kvadratura = kvadratura, Zauzeta = zauzeta };
            if (tipProstorije == 0)
                prostorija.TipProstorije = TipProstorije.salaZaPreglede;
            else
                prostorija.TipProstorije = TipProstorije.operacionaSala;
            return prostorija;
        }

        private void SacuvajProstoriju(Prostorija prostorija)
        {
            if (!FormUpravnik.clickedDodaj)
            {
                Inject.ControllerProstorija.ObrisiProstoriju(brojProstorije);
                UkloniIzPrikaza(brojProstorije);
            }

            Inject.ControllerProstorija.SacuvajProstoriju(prostorija);
            FormUpravnik.Prostorije.Add(prostorija);
        }

        private void comboTipProstorije_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboTipProstorije.SelectedIndex == 2)
                PrikaziPojaZaBolnickuSobu();
            else
                SakrijPoljaZaBolnickuSobu();
        }

        private void PrikaziPojaZaBolnickuSobu()
        {
            lblUkBrojKreveta.Visibility = Visibility.Visible;
            txtUkBrojKreveta.Visibility = Visibility.Visible;
            lblBrojSlobodnihKreveta.Visibility = Visibility.Visible;
            txtBrojSlobodnihKreveta.Visibility = Visibility.Visible;
        }

        private void checkZauzeta_Checked(object sender, RoutedEventArgs e)
        {
            if (comboTipProstorije.SelectedIndex == 2)
                txtBrojSlobodnihKreveta.Text = "0";
        }

        private void Button_Click_Odustani(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
