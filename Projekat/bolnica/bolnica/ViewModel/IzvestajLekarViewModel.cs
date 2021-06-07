using Bolnica.Commands;
using Model.Pregledi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Controls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using System.Collections;
using System.Windows.Controls.Primitives;
using System.IO;

namespace Bolnica.ViewModel
{
    public class IzvestajLekarViewModel : ViewModel
    {
        #region POLJA
        public string Simptomi { get; set; }

        public string Dijagnoza { get; set; }

        private DataGrid grid = new DataGrid();

        private Injector inject;
        public Injector Inject
        {
            get { return inject; }
            set
            {
                inject = value;
            }
        }
        public Action ZatvoriAction { get; set; }

        #endregion
        #region KOMANDE
        private RelayCommand potvrdiKomanda;
        public RelayCommand PotvrdiKomanda
        {
            get { return potvrdiKomanda; }
            set
            {
                potvrdiKomanda = value;

            }
        }

        public void Executed_PotvrdiKomanda(object obj)
        {

            PdfPTable pdfTable = new PdfPTable(grid.Columns.Count);
            pdfTable.DefaultCell.Padding = 3;
            pdfTable.WidthPercentage = 30;
            pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfTable.DefaultCell.BorderWidth = 1;        
            PdfPCell cell = new PdfPCell(new Phrase("Naziv"));
            cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240);
            pdfTable.AddCell(cell);
            cell = new PdfPCell(new Phrase("Datum prepisivanja"));
            cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240);
            pdfTable.AddCell(cell);
            cell = new PdfPCell(new Phrase("Datum prekida"));
            cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240);
            pdfTable.AddCell(cell);
            cell = new PdfPCell(new Phrase("Vreme"));
            cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240);
            pdfTable.AddCell(cell);

            Paragraph text = new Paragraph("Simptomi:   " + Simptomi, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6));
            Paragraph text1 = new Paragraph("Dijagnoza:   " + Dijagnoza, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6));
            //Adding DataRow
            foreach (PrikazRecepta row in Recepti)
            {
                pdfTable.AddCell(row.lek.Naziv);
                pdfTable.AddCell(row.DatumIzdavanja.ToShortDateString());
                pdfTable.AddCell(row.Trajanje.ToShortDateString());
                pdfTable.AddCell(row.VremeUzimanja.ToString());
            }

            //Exporting to PDF
            string folderPath = "C:\\Users\\Minja\\Desktop\\Ejada\\";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            using (FileStream stream = new FileStream(folderPath + "DataGridViewExport.pdf", FileMode.Create))
            {
                Document pdfDoc = new Document(PageSize.A2, 10f, 10f, 10f, 0f);
                PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();
                pdfDoc.Add(text);
                pdfDoc.Add(text1);
                pdfDoc.Add(pdfTable);
               
                pdfDoc.Close();
                stream.Close();
            }
            ZatvoriAction();
        }
      
        

        public bool CanExecute_PotvrdiKomanda(object obj)
        {
            return true;
        }
        private RelayCommand zatvoriKomanda;
        public RelayCommand ZatvoriKomanda
        {
            get { return zatvoriKomanda; }
            set
            {
                zatvoriKomanda = value;

            }
        }

        public void Executed_ZatvoriKomanda(object obj)
        {
            ZatvoriAction();
        }

        public bool CanExecute_ZatvoriKomanda(object obj)
        {
            return true;
        }
        public ObservableCollection<PrikazRecepta> Recepti
        {
            get;
            set;
        }
        #endregion 
        public IzvestajLekarViewModel(string simptom, string dijagnoz) {

            Inject = new Injector();
            Recepti = AnamnezaLekarViewModel.Recepti;
            Simptomi = simptom;
            Dijagnoza = dijagnoz;
            PotvrdiKomanda = new RelayCommand(Executed_PotvrdiKomanda, CanExecute_PotvrdiKomanda);
            ZatvoriKomanda = new RelayCommand(Executed_ZatvoriKomanda, CanExecute_ZatvoriKomanda);
        
        }
        #region POMOCNE FUNKCIJE
        public void Postavi(DataGrid grid)
    {
            this.grid = grid;
    }
        #endregion

    }
}
