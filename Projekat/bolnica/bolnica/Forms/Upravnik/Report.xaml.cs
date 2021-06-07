using Bolnica.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Bolnica.Controller.Pregledi;
using Bolnica.Localization;

namespace Bolnica.Forms.Upravnik
{
    /// <summary>
    /// Interaction logic for Report.xaml
    /// </summary>
    public partial class Report : Window
    {
        public ObservableCollection<LekDTO> Lekovi
        {
            get;
            set;
        }

        private ControllerLek contollerLek = new ControllerLek();
        public Report()
        {
            InitializeComponent();
            Title = LocalizedStrings.Instance["Izveštaj o količini lekova - trenutno stanje"];
            DataContext = this;
            UcitajSveLekove();
            UcitajVreme();
        }

        private void UcitajSveLekove()
        {
            Lekovi = new ObservableCollection<LekDTO>();
            List<LekDTO> sviLekovi = contollerLek.DobaviSveLekove();
            foreach(LekDTO l in sviLekovi)
            {
                Lekovi.Add(l);
            }
        }

        private void UcitajVreme()
        {
            lblDatumIVreme.Text = DateTime.Now.ToString("dd/MM/yyyy") + ", " + DateTime.Now.ToString("HH:mm");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                IsEnabled = false;

                PrintDialog printDialog = new PrintDialog();
                if(printDialog.ShowDialog() == true)
                {
                    printDialog.PrintVisual(print, "izvestaj");
                }
            } 
            finally
            {
                IsEnabled = true;
            }
        }
    }
}
