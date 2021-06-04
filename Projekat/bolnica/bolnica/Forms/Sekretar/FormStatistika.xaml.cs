using Bolnica.Sekretar;
using Model.Korisnici;
using Model.Pacijenti;
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
using LiveCharts;
using LiveCharts.Wpf;
using Bolnica.Model.Korisnici;
using Bolnica.Validation;
using Bolnica.Repository.Pregledi;
using Model.Pregledi;
using System.Linq;

namespace Bolnica.Forms.Sekretar
{
    /// <summary>
    /// Interaction logic for FormStatistika.xaml
    /// </summary>
    public partial class FormStatistika : Window
    {
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<int, string> Formatter { get; set; }
        private Func<ChartPoint, string> labelPoint = chartPoint => string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);
        private FileRepositoryLekar skladisteLekara;
        private FileRepositoryPregled skladistePregleda;
        private FileRepositoryOperacija skladisteOperacija;
        public FormStatistika()
        {
            InitializeComponent();
            DataContext = this;

            skladisteLekara = new FileRepositoryLekar();
            skladistePregleda = new FileRepositoryPregled();
            skladisteOperacija = new FileRepositoryOperacija();
            List<Lekar> lekari = skladisteLekara.GetAll();
            List<Pregled> pregledi = skladistePregleda.GetAll();
            List<Operacija> operacije = skladisteOperacija.GetAll();

            List<string> specijalizacije = new List<string>();
            foreach (Lekar l in lekari)
                specijalizacije.Add(l.Specijalizacija.OblastMedicine);
            specijalizacije = specijalizacije.Distinct().ToList();
            
            int[] termini = new int[specijalizacije.Count];
            foreach (Pregled p in pregledi)
                if(p.Datum.Month == 5)
                    foreach (Lekar l in lekari)
                        if (l.Jmbg == p.Lekar.Jmbg && l.Specijalizacija.OblastMedicine == "Opsta")
                            termini[0] += 1;
                        else if (l.Jmbg == p.Lekar.Jmbg && l.Specijalizacija.OblastMedicine == "Neurologija")
                            termini[1] += 1;
                        else if (l.Jmbg == p.Lekar.Jmbg && l.Specijalizacija.OblastMedicine == "Kardiologija")
                            termini[2] += 1;
            foreach(Operacija o in operacije)
                if (o.Datum.Month == 5)
                    foreach (Lekar l in lekari)
                        if (l.Jmbg == o.Lekar.Jmbg && l.Specijalizacija.OblastMedicine == "Opsta")
                            termini[0] += 1;
                        else if (l.Jmbg == o.Lekar.Jmbg && l.Specijalizacija.OblastMedicine == "Neurologija")
                            termini[1] += 1;
                        else if (l.Jmbg == o.Lekar.Jmbg && l.Specijalizacija.OblastMedicine == "Kardiologija")
                            termini[2] += 1;

            SeriesCollection = new SeriesCollection();
            ColumnSeries cs = new ColumnSeries
            {
                Title = "Maj",
                Values = new ChartValues<int> { termini[0], termini[1], termini[2] }
            };

            cs.Fill = Brushes.Gray;
            SeriesCollection.Add(cs);


            Labels = specijalizacije.ToArray();
            Formatter = value => value.ToString("N");


            int[] brojLekaraPoSpecijalizaciji = new int[specijalizacije.Count];
            foreach (Lekar l in lekari)
                if (l.Specijalizacija.OblastMedicine == "Opsta")
                    brojLekaraPoSpecijalizaciji[0] += 1;
                else if (l.Specijalizacija.OblastMedicine == "Neurologija")
                    brojLekaraPoSpecijalizaciji[1] += 1;
                else if (l.Specijalizacija.OblastMedicine == "Kardiologija")
                    brojLekaraPoSpecijalizaciji[2] += 1;

            SeriesCollection psc = new SeriesCollection();

            PieSeries ps1 = new PieSeries
            {
                Values = new ChartValues<decimal> { brojLekaraPoSpecijalizaciji[0] },
                Title = "Opsta",
                DataLabels = true,
                LabelPoint = labelPoint
            };

            PieSeries ps2 = new PieSeries
            {
                Values = new ChartValues<decimal> { brojLekaraPoSpecijalizaciji[1] },
                Title = "Neurologija",
                DataLabels = true,
                LabelPoint = labelPoint
            };

            PieSeries ps3 = new PieSeries
            {
                Values = new ChartValues<decimal> { brojLekaraPoSpecijalizaciji[2] },
                Title = "Kardiologija",
                DataLabels = true,
                LabelPoint = labelPoint
            };

            ps1.Fill = Brushes.LightGray;
            ps2.Fill = Brushes.Gray;
            ps3.Fill = Brushes.DarkGray;
            psc.Add(ps1);
            psc.Add(ps2);
            psc.Add(ps3);

            foreach (LiveCharts.Wpf.PieSeries ps in psc)
            {
                myPieChart.Series.Add(ps);
            }

        }

        private void Button_Click_Pacijenti(object sender, RoutedEventArgs e)
        {
            var s = new FormSekretar();
            s.btnPacijenti.Background = new SolidColorBrush(Color.FromArgb(255, 169, 169, 169));
            s.Show();
            this.Close();
        }

        private void Button_Click_Pregledi(object sender, RoutedEventArgs e)
        {
            var s = new FormPregledi();
            s.btnPregledi.Background = new SolidColorBrush(Color.FromArgb(255, 169, 169, 169));
            s.Show();
            this.Close();
        }

        private void Button_Click_Obavestenja(object sender, RoutedEventArgs e)
        {
            var s = new FormObavestenja();
            s.btnObavestenja.Background = new SolidColorBrush(Color.FromArgb(255, 169, 169, 169));
            s.Show();
            this.Close();
        }

        private void Button_Click_Lekari(object sender, RoutedEventArgs e)
        {
            var s = new FormLekari();
            s.btnLekari.Background = new SolidColorBrush(Color.FromArgb(255, 169, 169, 169));
            s.Show();
            this.Close();
        }
    }
}
