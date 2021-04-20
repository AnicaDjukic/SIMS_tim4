﻿using Bolnica.Forms;
using Bolnica.Forms.Upravnik;
using Bolnica.Model.Prostorije;
using Model.Prostorije;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Threading;

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

        private FileStorageProstorija storageProstorije;
        private FileStorageOprema storageOprema;

        public static bool clickedDodaj;

        public static ObservableCollection<Oprema> Oprema
        {
            get;
            set;
        }

        public FormUpravnik()
        {
            InitializeComponent();

            DispatcherTimer timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                Vreme.Text = DateTime.Now.ToString("HH:mm");
                Datum.Text = DateTime.Now.ToString("dd/MM/yyyy");

            }, Dispatcher);

            clickedDodaj = false;
            this.DataContext = this;
            Prostorije = new ObservableCollection<Prostorija>();
            storageProstorije = new FileStorageProstorija();

            List<Prostorija> prostorije = storageProstorije.GetAllProstorije();
            foreach (Prostorija p in prostorije)
            {
                if (p.Obrisana == false)
                    Prostorije.Add(p);
            }
            List<BolnickaSoba> bolnickeSobe = storageProstorije.GetAllBolnickeSobe();
            foreach (BolnickaSoba b in bolnickeSobe)
            {
                if (b.Obrisana == false)
                    Prostorije.Add(b);
            }

            Oprema = new ObservableCollection<Oprema>();
            storageOprema = new FileStorageOprema();

            /*Oprema o1 = new Oprema();

            o1.Sifra = "a123";
            o1.Naziv = "Stolica";
            o1.Kolicina = 100;
            o1.TipOpreme = TipOpreme.staticka;
            o1.OpremaPoSobama.Add(111,50);
            o1.OpremaPoSobama.Add(103, 50);

            Oprema.Add(o1);
            storageOprema.Save(o1);*/

            List<Oprema> oprema = storageOprema.GetAll();

            foreach (Oprema o in oprema)
            {
                Oprema.Add(o);
            }
        }

        private void Button_Click_Dodaj(object sender, RoutedEventArgs e)
        {
            clickedDodaj = true;
            if (Tabovi.SelectedIndex == 0)
            {
                var s = new CreateFormProstorije();
                s.Show();
            }
            else if (Tabovi.SelectedIndex == 1)
            {
                var o = new CreateFormOprema();
                o.Show();
            }
        }

        private void Button_Click_Prikazi(object sender, RoutedEventArgs e)
        {
            if (dataGridProstorije.SelectedCells.Count > 0 && Tabovi.SelectedIndex == 0)
            {
                Prostorija row = (Prostorija)dataGridProstorije.SelectedItems[0];
                int brojProstorije = row.BrojProstorije;
                List<Prostorija> prostorije = storageProstorije.GetAllProstorije();
                List<BolnickaSoba> bolnickeSobe = storageProstorije.GetAllBolnickeSobe();
                var s = new ViewFormProstorije(brojProstorije);
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
            else if (dataGridOprema.SelectedCells.Count > 0 && Tabovi.SelectedIndex == 1)
            {
                Oprema row = (Oprema)dataGridOprema.SelectedItems[0];
                string sifraOpreme = row.Sifra;
                List<Oprema> oprema = storageOprema.GetAll();
                var s = new ViewFormOprema(sifraOpreme);
                foreach (Oprema o in oprema)
                {
                    if (o.Sifra == row.Sifra)
                    {
                        s.lblSifra.Content = o.Sifra;
                        s.lblNaziv.Content = o.Naziv;
                        if (o.TipOpreme == TipOpreme.dinamicka)
                            s.lblTipOpreme.Content = "Dinamička";
                        else
                            s.lblTipOpreme.Content = "Statička";
                        s.lblKolicina.Content = o.Kolicina.ToString();
                        s.Show();
                        break;
                    }
                }
            }
        }

        private void Button_Click_Izmeni(object sender, RoutedEventArgs e)
        {
            if (dataGridProstorije.SelectedCells.Count > 0 && Tabovi.SelectedIndex == 0)
            {
                clickedDodaj = false;
                Prostorija row = (Prostorija)dataGridProstorije.SelectedItems[0];
                List<Prostorija> prostorije = storageProstorije.GetAllProstorije();
                List<BolnickaSoba> bolnickeSobe = storageProstorije.GetAllBolnickeSobe();
                var s = new CreateFormProstorije();
                bool found = false;
                foreach (Prostorija p in prostorije)
                {
                    if (p.BrojProstorije == row.BrojProstorije)
                    {
                        s.BrojProstorije = p.BrojProstorije;
                        s.Sprat = p.Sprat;
                        s.Kvadratura = p.Kvadratura;
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
                            s.BrojProstorije = b.BrojProstorije;
                            s.Sprat = b.Sprat;
                            s.Kvadratura = b.Kvadratura;
                            s.comboTipProstorije.SelectedIndex = 2;
                            s.checkZauzeta.IsChecked = b.Zauzeta;
                            s.lblUkBrojKreveta.Visibility = Visibility.Visible;
                            s.txtUkBrojKreveta.Visibility = Visibility.Visible;
                            s.UkBrojKreveta = b.UkBrojKreveta;
                            s.lblBrojSlobodnihKreveta.Visibility = Visibility.Visible;
                            s.txtBrojSlobodnihKreveta.Visibility = Visibility.Visible;
                            s.BrojSlobodnihKreveta = b.BrojSlobodnihKreveta;
                            s.Show();
                            break;
                        }
                    }
                }
            }
            else if (dataGridOprema.SelectedCells.Count > 0 && Tabovi.SelectedIndex == 1)
            {
                clickedDodaj = false;
                Oprema row = (Oprema)dataGridOprema.SelectedItems[0];
                List<Oprema> Svaoprema = storageOprema.GetAll();
                var s = new CreateFormOprema();
                foreach (Oprema o in Oprema)
                {
                    if (o.Sifra == row.Sifra)
                    {
                        s.Sifra = o.Sifra;
                        s.Naziv = o.Naziv;
                        s.Kolicina = o.Kolicina;
                        s.Oprema = o;
                        if (o.TipOpreme == TipOpreme.staticka)
                            s.ComboTipOpreme.SelectedIndex = 0;
                        else
                            s.ComboTipOpreme.SelectedIndex = 1;
                        s.Show();
                        break;
                    }
                }
            }
        }

        private void Button_Click_Obrisi(object sender, RoutedEventArgs e)
        {
            if (dataGridProstorije.SelectedCells.Count > 0 && Tabovi.SelectedIndex == 0)
            {
                Prostorija row = (Prostorija)dataGridProstorije.SelectedItems[0];
                if (row.Zauzeta)
                {
                    MessageBox.Show("Prostorija je trenutno zauzeta, ne možete je obrisati.");
                }
                else
                {
                    List<Prostorija> prostorije = storageProstorije.GetAllProstorije();
                    List<BolnickaSoba> bolnickeSobe = storageProstorije.GetAllBolnickeSobe();

                    var s = new CreateFormProstorije();
                    bool found = false;
                    foreach (Prostorija p in prostorije)
                    {
                        if (p.BrojProstorije == row.BrojProstorije)
                        {
                            storageProstorije.Delete(p);
                            p.Obrisana = true;
                            storageProstorije.Save(p);

                            for (int i = 0; i < FormUpravnik.Prostorije.Count; i++)
                            {
                                if (FormUpravnik.Prostorije[i].BrojProstorije == row.BrojProstorije)
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
                                storageProstorije.Delete(b);
                                b.Obrisana = true;
                                storageProstorije.Save(b);

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
            else if (dataGridOprema.SelectedCells.Count > 0 && Tabovi.SelectedIndex == 1)
            {
                Oprema row = (Oprema)dataGridOprema.SelectedItems[0];
                List<Oprema> oprema = storageOprema.GetAll();

                string sMessageBoxText = "Da li ste sigurni da želite da obrišete opremu sa nazivom \"" + row.Naziv + "\" i šifrom \"" + row.Sifra + "\"?";
                string sCaption = "Brisanje opreme";

                MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
                MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

                MessageBoxResult rsltMessageBox = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);

                if(rsltMessageBox == MessageBoxResult.Yes)
                {
                    foreach (Oprema o in oprema)
                    {
                        if (o.Sifra == row.Sifra)
                        {
                            storageOprema.Delete(o);

                            for (int i = 0; i < FormUpravnik.Oprema.Count; i++)
                            {
                                if (FormUpravnik.Oprema[i].Sifra == row.Sifra)
                                {
                                    FormUpravnik.Oprema.Remove(FormUpravnik.Oprema[i]);
                                    break;
                                }
                            }
                            break;
                        }
                    }
                }
            }
        }
    }
}