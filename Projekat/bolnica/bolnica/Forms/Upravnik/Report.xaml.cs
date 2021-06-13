using Bolnica.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
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

        private Injector inject;
        public Injector Inject
        {
            get { return inject; }
            set
            {
                inject = value;
            }
        }
        public Report()
        {
            InitializeComponent();
            DataContext = this;
            Inject = new Injector();
            Title = LocalizedStrings.Instance["Izveštaj o količini lekova - trenutno stanje"];
            UcitajSveLekove();
            UcitajVreme();
        }

        private void UcitajSveLekove()
        {
            Lekovi = new ObservableCollection<LekDTO>();
            List<LekDTO> sviLekovi = Inject.ControllerLek.DobaviSveLekoveDTO();
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
