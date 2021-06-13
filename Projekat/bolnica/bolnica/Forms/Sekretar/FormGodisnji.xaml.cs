using Bolnica.Controller.Sekretar;
using Bolnica.DTO.Sekretar;
using Bolnica.Model.Korisnici;
using Bolnica.Repository.Pregledi;
using Bolnica.Sekretar;
using Model.Korisnici;
using Model.Pregledi;
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

namespace Bolnica.Forms.Sekretar
{
    /// <summary>
    /// Interaction logic for FormGodisnji.xaml
    /// </summary>
    public partial class FormGodisnji : Window
    {
        private string korisnickoIme;
        private LekarController lekarController;
        private GodisnjiController godisnjiController;
        public FormGodisnji(string korisnickoIme)
        {
            InitializeComponent();
            this.korisnickoIme = korisnickoIme;
            lekarController = new LekarController();
            godisnjiController = new GodisnjiController();
            InicijalizujGUI();
        }

        private void InicijalizujGUI() 
        {
            /*LekarDTO lekar = lekarController.GetLekarById(korisnickoIme);
            lblSlobodniDani.Content = lekar.BrojSlobodnihDana.ToString();

            foreach (Godisnji g in storageGodisnji.GetAll())
                if (korisnickoIme == g.Lekar.KorisnickoIme)
                    calendar.BlackoutDates.Add(new CalendarDateRange(new DateTime(g.PocetakGodisnjeg.Year, g.PocetakGodisnjeg.Month, g.PocetakGodisnjeg.Day), new DateTime(g.KrajGodisnjeg.Year, g.KrajGodisnjeg.Month, g.KrajGodisnjeg.Day)));
        */}

        private void ZakaziGodisnji(object sender, RoutedEventArgs e)
        {/*
            Lekar lekar = storageLekari.GetById(korisnickoIme);
            if (lekar.BrojSlobodnihDana >= calendar.SelectedDates.Count) 
            {
                Godisnji godisnji = new Godisnji();
                try
                {
                    godisnji.PocetakGodisnjeg = calendar.SelectedDates[0];
                    godisnji.KrajGodisnjeg = calendar.SelectedDates[calendar.SelectedDates.Count - 1];
                }
                catch (ArgumentOutOfRangeException)
                {
                    MessageBox.Show("Niste selektovali datume za zakazivanje godišnjeg", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                       
                godisnji.Lekar = lekar;
                lekar.BrojSlobodnihDana -= calendar.SelectedDates.Count;
                storageLekari.Update(lekar);
                storageGodisnji.Save(godisnji);

                foreach (Pregled p in storagePregledi.GetAll())
                    if (godisnji.PocetakGodisnjeg <= p.Datum && godisnji.KrajGodisnjeg.AddDays(1) > p.Datum)
                    {
                        storagePregledi.Delete(p);
                        for (int i = 0; i < FormPregledi.Pregledi.Count; i++)
                            if (FormPregledi.Pregledi[i].Id == p.Id)
                            {
                                FormPregledi.Pregledi.RemoveAt(i);
                                break;
                            }
                    }

                foreach (Operacija o in storageOperacije.GetAll())
                    if (godisnji.PocetakGodisnjeg <= o.Datum && godisnji.KrajGodisnjeg.AddDays(1) > o.Datum)
                    {
                        storageOperacije.Delete(o);
                        for (int i = 0; i < FormPregledi.Pregledi.Count; i++)
                            if (FormPregledi.Pregledi[i].Id == o.Id)
                            {
                                FormPregledi.Pregledi.RemoveAt(i);
                                break;
                            }
                    }

                Close();
            }
            else 
            {
                MessageBox.Show("Lekar nema dovoljan broj slobodnih dana", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }*/
        }

        private void Zatvori(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
