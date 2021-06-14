using Bolnica.Controller.Sekretar;
using Bolnica.DTO.Sekretar;
using Bolnica.Model.Korisnici;
using Bolnica.Model.Pregledi;
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
        public static bool pomeriTermine = false;
        private string korisnickoIme;
        private LekarController lekarController;
        private GodisnjiController godisnjiController;
        private PregledController pregledController;
        private OperacijaController operacijaController;
        public FormGodisnji(string korisnickoIme)
        {
            InitializeComponent();
            this.korisnickoIme = korisnickoIme;
            lekarController = new LekarController();
            godisnjiController = new GodisnjiController();
            pregledController = new PregledController();
            operacijaController = new OperacijaController();
            InicijalizujGUI();
        }

        private void InicijalizujGUI() 
        {
            LekarDTO lekar = lekarController.GetLekarById(korisnickoIme);
            lblSlobodniDani.Content = lekar.BrojSlobodnihDana.ToString();

            foreach (GodisnjiDTO godisnji in godisnjiController.GetAllGodisnji())
                if (korisnickoIme == godisnji.KorisnickoImeLekara)
                    calendar.BlackoutDates.Add(new CalendarDateRange(new DateTime(godisnji.PocetakGodisnjeg.Year, godisnji.PocetakGodisnjeg.Month, godisnji.PocetakGodisnjeg.Day), new DateTime(godisnji.KrajGodisnjeg.Year, godisnji.KrajGodisnjeg.Month, godisnji.KrajGodisnjeg.Day)));
        }

        private void ZakaziGodisnjiOdlaganjeTermina(object sender, RoutedEventArgs e)
        {
            LekarDTO lekar = lekarController.GetLekarById(korisnickoIme);
            if (lekar.BrojSlobodnihDana >= calendar.SelectedDates.Count) 
            {
                GodisnjiDTO godisnji = new GodisnjiDTO();
                if (!PostavljenaPoljaZaGodisnji(godisnji, lekar))
                    return;

                godisnjiController.ZakaziGodisnji(godisnji, lekar, calendar.SelectedDates.Count, pomeriTermine);
                UpdateTabeleNaGUIOdlaganjeTermina(godisnji);

                Close();
            }
            else 
                MessageBox.Show("Lekar nema dovoljan broj slobodnih dana", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void ZakaziGodisnjiPomeranjeTermina(object sender, RoutedEventArgs e)
        {
            LekarDTO lekar = lekarController.GetLekarById(korisnickoIme);
            if (lekar.BrojSlobodnihDana >= calendar.SelectedDates.Count)
            {
                GodisnjiDTO godisnji = new GodisnjiDTO();
                if (!PostavljenaPoljaZaGodisnji(godisnji, lekar))
                    return;

                pomeriTermine = true;
                godisnjiController.ZakaziGodisnji(godisnji, lekar, calendar.SelectedDates.Count, pomeriTermine);
                pomeriTermine = false;
                UpdateTabeleNaGUIPomeranjeTermina(godisnji);

                Close();
            }
            else
                MessageBox.Show("Lekar nema dovoljan broj slobodnih dana", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void UpdateTabeleNaGUIOdlaganjeTermina(GodisnjiDTO godisnji) 
        {
            UpdateTabeleNaGUIPreglediOdlaganjeTermina(godisnji);
            UpdateTabeleNaGUIOperacijeOdlaganjeTermina(godisnji);
        }

        private void UpdateTabeleNaGUIPomeranjeTermina(GodisnjiDTO godisnji)
        {
            UpdateTabeleNaGUIPreglediPomeranjeTermina(godisnji);
            UpdateTabeleNaGUIOperacijePomeranjeTermina(godisnji);
        }

        private void UpdateTabeleNaGUIPreglediPomeranjeTermina(GodisnjiDTO godisnji)
        {
            foreach (PrikazPregleda pregledDTO in pregledController.GetAllPregledi())
                if (godisnji.PocetakGodisnjeg <= pregledDTO.Datum && godisnji.KrajGodisnjeg.AddDays(1) > pregledDTO.Datum)
                    for (int i = 0; i < FormPregledi.Pregledi.Count; i++)
                        if (FormPregledi.Pregledi[i].Id == pregledDTO.Id)
                        {
                            FormPregledi.Pregledi.RemoveAt(i);
                            FormPregledi.Pregledi.Add(pregledController.GetPregledById(pregledDTO.Id));
                            break;
                        }
        }

        private void UpdateTabeleNaGUIOperacijePomeranjeTermina(GodisnjiDTO godisnji)
        {
            foreach (PrikazOperacije operacijaDTO in operacijaController.GetAllOperacije())
                if (godisnji.PocetakGodisnjeg <= operacijaDTO.Datum && godisnji.KrajGodisnjeg.AddDays(1) > operacijaDTO.Datum)
                    for (int i = 0; i < FormPregledi.Pregledi.Count; i++)
                        if (FormPregledi.Pregledi[i].Id == operacijaDTO.Id)
                        {
                            FormPregledi.Pregledi.RemoveAt(i);
                            FormPregledi.Pregledi.Add(operacijaController.GetOperacijaById(operacijaDTO.Id));
                            break;
                        }
        }

        private void UpdateTabeleNaGUIPreglediOdlaganjeTermina(GodisnjiDTO godisnji)
        {
            foreach (PrikazPregleda pregledDTO in pregledController.GetAllPregledi())
                if (godisnji.PocetakGodisnjeg <= pregledDTO.Datum && godisnji.KrajGodisnjeg.AddDays(1) > pregledDTO.Datum)
                    for (int i = 0; i < FormPregledi.Pregledi.Count; i++)
                        if (FormPregledi.Pregledi[i].Id == pregledDTO.Id)
                        {
                            FormPregledi.Pregledi.RemoveAt(i);
                            break;
                        }
        }

        private void UpdateTabeleNaGUIOperacijeOdlaganjeTermina(GodisnjiDTO godisnji) 
        {
            foreach (PrikazOperacije operacijaDTO in operacijaController.GetAllOperacije())
                if (godisnji.PocetakGodisnjeg <= operacijaDTO.Datum && godisnji.KrajGodisnjeg.AddDays(1) > operacijaDTO.Datum)
                    for (int i = 0; i < FormPregledi.Pregledi.Count; i++)
                        if (FormPregledi.Pregledi[i].Id == operacijaDTO.Id)
                        {
                            FormPregledi.Pregledi.RemoveAt(i);
                            break;
                        }
        }

        private bool PostavljenaPoljaZaGodisnji(GodisnjiDTO godisnjiDTO, LekarDTO lekarDTO) 
        {
            try
            {
                godisnjiDTO.PocetakGodisnjeg = calendar.SelectedDates[0];
                godisnjiDTO.KrajGodisnjeg = calendar.SelectedDates[calendar.SelectedDates.Count - 1];
                godisnjiDTO.KorisnickoImeLekara = lekarDTO.KorisnickoIme;
                return true;
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Niste selektovali datume za zakazivanje godišnjeg", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        private void Zatvori(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
