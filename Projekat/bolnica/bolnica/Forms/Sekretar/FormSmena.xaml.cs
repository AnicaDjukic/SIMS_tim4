using Bolnica.Model.Korisnici;
using Bolnica.Repository.Korisnici;
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
    /// Interaction logic for FormSmena.xaml
    /// </summary>
    public partial class FormSmena : Window
    {
        public const int BROJ_SATI_U_DANU = 24;
        public const int BROJ_MINUTA_U_SATU = 60;
        public const int POMAK_IZMEDJU_TERMINA = 15;
        private FileRepositoryLekar skladisteLekara;
        private FileRepositorySmena skladisteSmena;
        private FileRepositoryPregled skladistePregleda;
        private FileRepositoryOperacija skladisteOperacija;
        private string korisnickoIme;
        public FormSmena(string korisnickoIme)
        {
            InitializeComponent();
            dpDanasnjiDatum.SelectedDate = DateTime.Now;
            dpDanasnjiDatum.IsEnabled = false;
            this.korisnickoIme = korisnickoIme;
            skladisteLekara = new FileRepositoryLekar();
            skladisteSmena = new FileRepositorySmena();
            skladistePregleda = new FileRepositoryPregled();
            skladisteOperacija = new FileRepositoryOperacija();

            for (int sat = 0; sat < BROJ_SATI_U_DANU; sat++)
                for (int min = 0; min < BROJ_MINUTA_U_SATU;)
                {
                    TimeSpan ts = new TimeSpan(sat, min, 0);
                    min = min + POMAK_IZMEDJU_TERMINA;
                    comboPocetakSmene.Items.Add(string.Format("{0:00}:{1:00}", ts.Hours, ts.Minutes));
                    comboKrajSmene.Items.Add(string.Format("{0:00}:{1:00}", ts.Hours, ts.Minutes));
                }
        }

        private void Zatvori(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Potvrdi(object sender, RoutedEventArgs e)
        {
            DateTime datum = (DateTime)dpDanasnjiDatum.SelectedDate;
            string satiPocetak, minutiPocetak, satiKraj, minutiKraj;
            try
            {
                satiPocetak = comboPocetakSmene.Text.Split(":")[0];
                minutiPocetak = comboPocetakSmene.Text.Split(":")[1];
                satiKraj = comboKrajSmene.Text.Split(":")[0];
                minutiKraj = comboKrajSmene.Text.Split(":")[1];
            }
            catch (IndexOutOfRangeException) 
            {
                MessageBox.Show("Nisu uneti svi podaci", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            DateTime pocetak = new DateTime(datum.Year, datum.Month, datum.Day, Int32.Parse(satiPocetak), Int32.Parse(minutiPocetak), 0);
            DateTime kraj = new DateTime(datum.Year, datum.Month, datum.Day, Int32.Parse(satiKraj), Int32.Parse(minutiKraj), 0);

            if (DateTime.Compare(pocetak, kraj) >= 0)
            {
                MessageBox.Show("Datum početka mora biti prije datuma kraja", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Lekar lekar = skladisteLekara.GetById(korisnickoIme);
            Smena smena = skladisteSmena.GetById(lekar.Smena.Id);
            for (int i = 0; i < FormLekari.Lekari.Count; i++)
                if (FormLekari.Lekari[i].KorisnickoIme == lekar.KorisnickoIme)
                {
                    FormLekari.Lekari.RemoveAt(i);
                    break;
                }
            smena.PocetakSmene = pocetak;
            smena.KrajSmene = kraj;
            skladisteSmena.Update(smena);
            lekar.Smena = smena;
            FormLekari.Lekari.Add(lekar);

            foreach (Pregled p in skladistePregleda.GetAll())
                if (smena.Id == skladisteLekara.GetById(p.Lekar.KorisnickoIme).Smena.Id && smena.PocetakSmene.Date == p.Datum.Date && smena.PocetakSmene <= p.Datum && smena.KrajSmene > p.Datum)
                {
                    skladistePregleda.Delete(p);
                    for (int i = 0; i < FormPregledi.Pregledi.Count; i++)
                        if (FormPregledi.Pregledi[i].Id == p.Id)
                        {
                            FormPregledi.Pregledi.RemoveAt(i);
                            break;
                        }
                }

            foreach (Operacija o in skladisteOperacija.GetAll())
                if (smena.Id == skladisteLekara.GetById(o.Lekar.KorisnickoIme).Smena.Id && smena.PocetakSmene.Date == o.Datum.Date && smena.PocetakSmene <= o.Datum && smena.KrajSmene > o.Datum)
                {
                    skladisteOperacija.Delete(o);
                    for (int i = 0; i < FormPregledi.Pregledi.Count; i++)
                        if (FormPregledi.Pregledi[i].Id == o.Id)
                        {
                            FormPregledi.Pregledi.RemoveAt(i);
                            break;
                        }
                }

            Close();
        }
    }
}
