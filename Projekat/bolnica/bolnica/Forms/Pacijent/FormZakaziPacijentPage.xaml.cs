using Bolnica.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Bolnica.Forms
{
    /// <summary>
    /// Interaction logic for FormZakaziPacijentPage.xaml
    /// </summary>
    public partial class FormZakaziPacijentPage : Page
    {
        public FormZakaziPacijentPage(ZakaziPregledPacijentViewModel zakaziPregledPacijentViewModel)
        {
            InitializeComponent();

            this.DataContext = zakaziPregledPacijentViewModel;
            FormPacijentWeb.Forma.Pocetna.Content = this;
        }

        private void RadioButton_Checked_Datum(object sender, RoutedEventArgs e)
        {
            datumPicker.IsEnabled = true;
            datumPicker.Focus();
            datumPicker.Background = Brushes.Aqua;
            comboSat.IsEnabled = false;
            comboMinut.IsEnabled = false;
            comboLekar.IsEnabled = false;
            potvrdi.IsEnabled = false;
            nasiPredlozi.IsEnabled = true;
            datum.IsEnabled = false;
            lekar.IsEnabled = false;
        }

        private void RadioButton_Checked_Lekar(object sender, RoutedEventArgs e)
        {
            comboLekar.IsEnabled = true;
            comboLekar.Focus();
            comboLekar.Background = Brushes.Aqua;
            datumPicker.IsEnabled = false;
            comboSat.IsEnabled = false;
            comboMinut.IsEnabled = false;
            potvrdi.IsEnabled = false;
            nasiPredlozi.IsEnabled = true;
            datum.IsEnabled = false;
            lekar.IsEnabled = false;
        }

        private void SelectedDateChanged_Datum(object sender, SelectionChangedEventArgs e)
        {
            comboSat.IsEnabled = true;
            comboMinut.IsEnabled = false;
            datumPicker.IsEnabled = false;
            datumPicker.FontWeight = FontWeights.UltraBold;
            datumPicker.Background = Brushes.Green;
            datumPicker.Foreground = Brushes.Green;
            comboSat.Background = Brushes.Aqua;
        }

        private void SelectionChanged_Sat(object sender, SelectionChangedEventArgs e)
        {
            comboSat.IsEnabled = false;
            comboMinut.IsEnabled = false;
            datumPicker.IsEnabled = false;
            comboSat.Foreground = Brushes.Green;
            comboMinut.IsEnabled = true;
            comboMinut.Background = Brushes.Aqua;
        }

        private void SelectionChanged_Minut(object sender, SelectionChangedEventArgs e)
        {
            comboSat.IsEnabled = false;
            comboMinut.IsEnabled = false;
            datumPicker.IsEnabled = false;
            comboMinut.Foreground = Brushes.Green;
            if (datum.IsChecked == false)
            {
                potvrdi.IsEnabled = true;
            }
            else
            {
                comboLekar.IsEnabled = true;
                comboLekar.Background = Brushes.Aqua;
            }
        }

        private void SelectionChanged_Lekar(object sender, SelectionChangedEventArgs e)
        {
            if (!(comboLekar.SelectedItem is null))
            {
                comboLekar.IsEnabled = false;
                comboLekar.Background = Brushes.Green;
                comboLekar.Foreground = Brushes.Green;
                if (lekar.IsChecked == false)
                {
                    potvrdi.IsEnabled = true;
                }
                else
                {
                    datumPicker.IsEnabled = true;
                    datumPicker.Background = Brushes.Aqua;
                }
            }
        }
    }
}
