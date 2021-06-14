using Bolnica.Model.Korisnici;
using Bolnica.Repository.Pregledi;
using ceTe.DynamicPDF;
using ceTe.DynamicPDF.PageElements;
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
                if (DateTime.Compare(pocetak, p.Datum) <= 0 && DateTime.Compare(kraj, p.Datum) >= 0 && p.Lekar.Jmbg == jmbgLekara) 
                {
                    brojPregleda++;
                    preglediUPeriodu.Add(p);
                }

            foreach (Operacija o in sveOperacije)
                if (DateTime.Compare(pocetak, o.Datum) <= 0 && DateTime.Compare(kraj, o.Datum) >= 0 && o.Lekar.Jmbg == jmbgLekara)
                {
                    brojOperacija++;
                    operacijeUPeriodu.Add(o);
                }

            FileRepositoryLekar storageLekari = new FileRepositoryLekar();
            string ime = "";
            string prezime = "";
            foreach (Lekar l in storageLekari.GetAll())
                if (l.Jmbg == jmbgLekara) {
                    ime = l.Ime;
                    prezime = l.Prezime;
                }

            Document document = new Document();

            ceTe.DynamicPDF.Page page = new ceTe.DynamicPDF.Page(PageSize.Letter, PageOrientation.Portrait, 45.0f);
            document.Pages.Add(page);
             
            string naslov = "Izveštaj o zauzetosti lekara " + ime + " " + prezime + " za period " + pocetak.ToString("dd.MM.yyyy.") + " - " + kraj.ToString("dd.MM.yyyy.");
            ceTe.DynamicPDF.PageElements.Label label = new ceTe.DynamicPDF.PageElements.Label(naslov, 0, 0, 504, 100, Font.TimesBold, 20, TextAlign.Center);
            page.Elements.Add(label);
            ceTe.DynamicPDF.PageElements.Label label1 = new ceTe.DynamicPDF.PageElements.Label("Broj pregleda: " + brojPregleda, 0, 100, 504, 100, Font.TimesRoman, 18, TextAlign.Center);
            page.Elements.Add(label1);
            ceTe.DynamicPDF.PageElements.Label label2 = new ceTe.DynamicPDF.PageElements.Label("Broj operacija: " + brojOperacija, 0, 120, 504, 100, Font.TimesRoman, 18, TextAlign.Center);
            page.Elements.Add(label2);

            Table2 table = new Table2(0, 180, 600, 1000);

            table.Columns.Add(125);
            table.Columns.Add(125);
            table.Columns.Add(125);
            table.Columns.Add(125);

            Row2 row1 = table.Rows.Add(30, Font.TimesRoman, 16, Grayscale.Black, Grayscale.Gray);
            row1.Cells.Add("Termin");
            row1.Cells.Add("Datum");
            row1.Cells.Add("Vreme");
            row1.Cells.Add("Trajanje (min)");

            foreach (Pregled p in preglediUPeriodu) {
                Row2 row2 = table.Rows.Add(30, Font.TimesRoman, 16, Grayscale.Black, Grayscale.White);
                row2.Cells.Add("Pregled");
                row2.Cells.Add(p.Datum.ToString("dd.MM.yyyy"));
                row2.Cells.Add(p.Datum.ToString("hh:mm"));
                row2.Cells.Add(p.Trajanje.ToString());
            }

            foreach (Operacija o in operacijeUPeriodu)
            {
                Row2 row2 = table.Rows.Add(30, Font.TimesRoman, 16, Grayscale.Black, Grayscale.White);
                row2.Cells.Add("Operacija");
                row2.Cells.Add(o.Datum.ToString("dd.MM.yyyy"));
                row2.Cells.Add(o.Datum.ToString("hh:mm"));
                row2.Cells.Add(o.Trajanje.ToString());
            }

            table.CellDefault.Padding.Value = 5.0f;
            table.CellSpacing = 3.0f;
            table.CellDefault.VAlign = VAlign.Center;
            table.CellDefault.Align = TextAlign.Center;
            table.Border.LineStyle = LineStyle.Solid;

            page.Elements.Add(table);

            document.Draw("D:/izvestaj.pdf");

            MessageBox.Show("Izveštaj je uspešno generisan", "Generisanje izveštaja", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
