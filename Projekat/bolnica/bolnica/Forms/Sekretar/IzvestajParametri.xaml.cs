using Bolnica.Repository.Pregledi;
using ceTe.DynamicPDF;
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
    /// Interaction logic for IzvestajParametri.xaml
    /// </summary>
    public partial class IzvestajParametri : Window
    {
        private string jmbgLekara;
        public IzvestajParametri(string jmbg)
        {
            InitializeComponent();
            jmbgLekara = jmbg;
        }

        private void Zatvori(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void GenerisiIzvestaj(object sender, RoutedEventArgs e)
        {
            DateTime pocetak;
            try
            {
                pocetak = dpPocetak.SelectedDate.Value.Date;
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Datum početka nije ispravno unet", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            DateTime kraj;
            try
            {
                kraj = dpKraj.SelectedDate.Value.Date;
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Datum kraja nije ispravno unet", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (DateTime.Compare(pocetak, kraj) >= 0)
            {
                MessageBox.Show("Datum početka mora biti prije datuma kraja", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int brojPregleda = 0;
            int brojOperacija = 0;
            FileRepositoryPregled storagePregledi = new FileRepositoryPregled();
            FileRepositoryOperacija storageOperacije = new FileRepositoryOperacija();
            List<Pregled> sviPregledi = storagePregledi.GetAll();
            List<Pregled> preglediUPeriodu = new List<Pregled>();
            List<Operacija> sveOperacije = storageOperacije.GetAll();
            List<Operacija> operacijeUPeriodu = new List<Operacija>();

            foreach (Pregled p in sviPregledi)
                if (DateTime.Compare(pocetak, p.Datum) <= 0 && DateTime.Compare(kraj, p.Datum) >= 0) 
                {
                    brojPregleda++;
                    preglediUPeriodu.Add(p);
                }

            foreach (Operacija o in sveOperacije)
                if (DateTime.Compare(pocetak, o.Datum) <= 0 && DateTime.Compare(kraj, o.Datum) >= 0)
                {
                    brojOperacija++;
                    operacijeUPeriodu.Add(o);
                }

            Document document = new Document();

            Page page = new Page(PageSize.Letter, PageOrientation.Portrait, 54.0f);
            document.Pages.Add(page);

            string labelText = "Hello World...\nFrom DynamicPDF Generator for .NET\nDynamicPDF.com";
            Label label = new Label(labelText, 0, 0, 504, 100, Font.Helvetica, 18, TextAlign.Center);
            page.Elements.Add(label);

            document.Draw("Output.pdf");
        }
    }
}
