using Bolnica.Commands;
using Bolnica.DTO;
using Bolnica.Forms;
using Bolnica.Model.Korisnici;
using Bolnica.Model.Pregledi;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using Model.Korisnici;
using Model.Pregledi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Bolnica.ViewModel
{
    public class AnamnezaLekarViewModel : ViewModel
    {
        #region POLJA

        private string simptomi;

        private string dijagnoza;
        public string Simptomi { get { return simptomi; }
            set { simptomi = value; OnPropertyChanged(); } }
        public string Dijagnoza {
            get { return dijagnoza; }
            set { dijagnoza = value; OnPropertyChanged(); }
        }

        public List<PrikazRecepta> recepti { get; set; }
        private bool DaLiPostojiAnamneza = false;
        private List<Lek> sviLekovi = new List<Lek>();
        private List<Pregled> sviPregledi = new List<Pregled>();
        private List<Operacija> sveOperacije = new List<Operacija>();
        private List<Lekar> sviLekari = new List<Lekar>();
        private Lekar ulogovaniLekar = new Lekar();
        private Pacijent trenutniPacijent = new Pacijent();
        private PrikazPregleda trenutniPregled = new PrikazPregleda();
        private PrikazOperacije trenutnaOperacija = new PrikazOperacije();
        private PrikazPregleda stariPregled = new PrikazPregleda();
        private PrikazOperacije staraOperacija = new PrikazOperacije();
        private List<Anamneza> sveAnamneze = new List<Anamneza>();
        private int idAnamneze;
        private bool DaLiJePregled = false;

        public static ObservableCollection<PrikazRecepta> Recepti
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
        public Action ZatvoriAction { get; set; }

        private bool datumProsao;
        public bool DatumProsao
        {
            get { return datumProsao; }
            set
            {
                datumProsao = value;
                OnPropertyChanged();
            }
        }


        private PrikazRecepta selektovaniItem;
        public PrikazRecepta SelektovaniItem
        {
            get { return selektovaniItem; }
            set
            {
                selektovaniItem = value;
                OnPropertyChanged();
            }
        }

        private bool fokusirajZatvoriDugme;
        public bool FokusirajZatvoriDugme
        {
            get { return fokusirajZatvoriDugme; }
            set
            {
                fokusirajZatvoriDugme = value;
                OnPropertyChanged();
            }
        }

        private int selektovaniIndeks;
        public int SelektovaniIndeks
        {
            get { return selektovaniIndeks; }
            set
            {
                selektovaniIndeks = value;
                OnPropertyChanged();
            }
        }
        #endregion
        #region KOMANDE
        private RelayCommand demoOtkaziKomanda;
        public RelayCommand DemoOtkaziKomanda
        {
            get { return demoOtkaziKomanda; }
            set
            {
                demoOtkaziKomanda = value;

            }
        }

        public void Executed_DemoOtkaziKomanda(object obj)
        {
            LekarViewModel.prekidaj = true;
        }

        public bool CanExecute_DemoOtkaziKomanda(object obj)
        {
            return true;
        }

        private RelayCommand fokusirajDole;
        public RelayCommand FokusirajDole
        {
            get { return fokusirajDole; }
            set
            {
                fokusirajDole = value;

            }
        }

        public void Executed_FokusirajDole(object obj)
        {
            FokusirajZatvoriDugme = true;
            
        }

        public bool CanExecute_FokusirajDole(object obj)
        {
            return true;
        }


        private RelayCommand obrisiReceptKomanda;
        public RelayCommand ObrisiReceptKomanda
        {
            get { return obrisiReceptKomanda; }
            set
            {
                obrisiReceptKomanda = value;

            }
        }   

        public void Executed_ObrisiReceptKomanda(object obj)
        {
            inject.AnamnezaLekarController.ObrisiRecept(new AnamnezaLekarDTO(SelektovaniIndeks));
        }

        public bool CanExecute_ObrisiReceptKomanda(object obj)
        {
            return true;
        }

        private RelayCommand izvestajKomanda;
        public RelayCommand IzvestajKomanda
        {
            get { return izvestajKomanda; }
            set
            {
                izvestajKomanda = value;

            }
        }

        public void Executed_IzvestajKomanda(object obj)
        {
            /* IzvestajLekarViewModel vm = new IzvestajLekarViewModel(Simptomi, Dijagnoza);
             FormIzvestajLekar form = new FormIzvestajLekar(vm); */
            PdfPTable pdfTable = new PdfPTable(4) {  WidthPercentage = 100 }; 
            float[] widths = new float[] { 150f, 150f, 150f, 150f};
            pdfTable.SetWidths(widths);
            pdfTable.DefaultCell.Padding = 3;
            pdfTable.WidthPercentage = 80;
            pdfTable.HorizontalAlignment = Element.ALIGN_CENTER;
            pdfTable.DefaultCell.BorderWidth = 1;
 
            PdfPCell cell = new PdfPCell(new Phrase("Naziv", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 24)));
            cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240);
            pdfTable.AddCell(cell);
            cell = new PdfPCell(new Phrase("Datum prepisivanja", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 24)));
            cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240);
            pdfTable.AddCell(cell);
            cell = new PdfPCell(new Phrase("Datum prekida", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 24)));
            cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240);
            pdfTable.AddCell(cell);
            cell = new PdfPCell(new Phrase("Vreme", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 24)));
            cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240);
            pdfTable.AddCell(cell);
            Paragraph anamnezaText = new Paragraph("Anamneza", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA,  30));
            Paragraph separator = new Paragraph("---------------------------------------------------------------------------------------------------------------------------------------------------------------", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 22));
            Paragraph text = new Paragraph("Simptomi:   " + Simptomi, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 24));
            Paragraph lekoviText = new Paragraph("Lekovi/terapije", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA,  30));
            Paragraph text1 = new Paragraph("Dijagnoza:   " + Dijagnoza, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 24));
            Paragraph p = new Paragraph("\n", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 24));
            Paragraph potpis = new Paragraph("Potpis doktora:_____________________________  ", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 24));
            


            anamnezaText.Alignment = Element.ALIGN_CENTER;
            separator.Alignment = Element.ALIGN_CENTER;
            //text.Alignment = Element.ALIGN_CENTER;
            lekoviText.Alignment = Element.ALIGN_CENTER;
            //text1.Alignment = Element.ALIGN_CENTER;
            foreach (PrikazRecepta row in Recepti)
            {
                pdfTable.AddCell(new PdfPCell(new Phrase(row.lek.Naziv, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 22))));
                pdfTable.AddCell(new PdfPCell(new Phrase(row.DatumIzdavanja.ToShortDateString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 22))));
                pdfTable.AddCell(new PdfPCell(new Phrase(row.Trajanje.ToShortDateString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 22))));
                pdfTable.AddCell(new PdfPCell(new Phrase(row.VremeUzimanja.ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 22))));
            }

            //Exporting to PDF
            string folderPath = "C:\\Users\\Minja\\Documents\\GitHub\\SIMS_tim4\\Projekat\\bolnica\\bolnica\\Izvestaji\\";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
           
            using (FileStream stream = new FileStream(folderPath + trenutniPacijent.KorisnickoIme+DateTime.Now.Hour+DateTime.Now.Minute+DateTime.Now.Second+".pdf", FileMode.Create))
            {
                Document pdfDoc = new Document(PageSize.A2, 10f, 10f, 10f, 0f);
                PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();
                pdfDoc.Add(anamnezaText);
                pdfDoc.Add(p);
                pdfDoc.Add(p);
                pdfDoc.Add(text);
                pdfDoc.Add(text1);
                pdfDoc.Add(p);
                pdfDoc.Add(separator);
                pdfDoc.Add(p);
                pdfDoc.Add(lekoviText);
                pdfDoc.Add(p);
                pdfDoc.Add(p);
                pdfDoc.Add(p);

                pdfDoc.Add(pdfTable);

                pdfDoc.Add(p);
                pdfDoc.Add(p);
                pdfDoc.Add(p);
                
                pdfDoc.Add(potpis);

                pdfDoc.Close();
                stream.Close();
                MessageBox.Show("Izvestaj je uspesno istampan u pdf");
            }
            

        }

        public bool CanExecute_IzvestajKomanda(object obj)
        {
            return true;
        }


        private RelayCommand zakaziPregledKomanda;
        public RelayCommand ZakaziPregledKomanda
        {
            get { return zakaziPregledKomanda; }
            set
            {
                zakaziPregledKomanda = value;

            }
        }

        public void Executed_ZakaziPregledKomanda(object obj)
        {
            inject.AnamnezaLekarController.ZakaziPregled(new AnamnezaLekarDTO(ulogovaniLekar, trenutniPacijent));

        }

        public bool CanExecute_ZakaziPregledKomanda(object obj)
        {
            return true;
        }

        private RelayCommand vidiReceptKomanda;
        public RelayCommand VidiReceptKomanda
        {
            get { return vidiReceptKomanda; }
            set
            {
                vidiReceptKomanda = value;

            }
        }

        public void Executed_VidiReceptKomanda(object obj)
        {
            inject.AnamnezaLekarController.VidiDetaljeOReceptu(new AnamnezaLekarDTO(SelektovaniIndeks, trenutniPacijent, SelektovaniItem));
        }

        public bool CanExecute_VidiReceptKomanda(object obj)
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

        private RelayCommand dodajReceptKomanda;
        public RelayCommand DodajReceptKomanda
        {
            get { return dodajReceptKomanda; }
            set
            {
                dodajReceptKomanda = value;

            }
        }

        public void Executed_DodajReceptKomanda(object obj)
        {
            inject.AnamnezaLekarController.DodajLek(new AnamnezaLekarDTO(trenutniPacijent));
        }

        public bool CanExecute_DodajReceptKomanda(object obj)
        {
            return true;
        }

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
            inject.AnamnezaLekarController.Potvrdi(new AnamnezaLekarDTO(DaLiPostojiAnamneza, DaLiJePregled, idAnamneze, Simptomi, Dijagnoza, stariPregled, trenutniPregled, staraOperacija, trenutnaOperacija, sveAnamneze));
            ZatvoriAction();
        }

        public bool CanExecute_PotvrdiKomanda(object obj)
        {
            return true;
        }

     
#endregion
        public AnamnezaLekarViewModel(PrikazPregleda izabraniPregled, Lekar ulogovaniLekar)
        {
            FokusirajZatvoriDugme = false;
            Inject = new Injector();
            DaLiJePregled = true;
            FiltirajLekove();
            InicirajPodatkeZaPregled(izabraniPregled, ulogovaniLekar);
            PopuniIliKreirajAnamnezuPregleda(izabraniPregled);
            NapraviKomande();
           
        }

        public AnamnezaLekarViewModel(PrikazOperacije izabranaOperacija, Lekar ulogovaniLekar)
        {
            FokusirajZatvoriDugme = false;
            Inject = new Injector();
            InicirajPodatkeZaOperaciju(izabranaOperacija, ulogovaniLekar);
            FiltirajLekove();
            PopuniIliKreirajAnamnezuOperacije(izabranaOperacija);
            NapraviKomande();
     
        }
        public AnamnezaLekarViewModel(PrikazPregleda izabraniPregled)
        {
            FokusirajZatvoriDugme = false;
            Inject = new Injector();
            DaLiJePregled = true;
            FiltirajLekove();
            InicirajPodatkeZaPregled(izabraniPregled, ulogovaniLekar);
            PopuniIliKreirajAnamnezuPregleda(izabraniPregled);
            NapraviKomande();
            DatumProsao = false;

        }

        public AnamnezaLekarViewModel(PrikazOperacije izabranaOperacija)
        {
            FokusirajZatvoriDugme = false;
            Inject = new Injector();
            InicirajPodatkeZaOperaciju(izabranaOperacija, ulogovaniLekar);
            FiltirajLekove();
            PopuniIliKreirajAnamnezuOperacije(izabranaOperacija);
            NapraviKomande();
            DatumProsao = false ;

        }
        #region POMOCNE FUNKCIJE
        public void NapraviKomande()
        {
            ZakaziPregledKomanda = new RelayCommand(Executed_ZakaziPregledKomanda, CanExecute_ZakaziPregledKomanda);
            ObrisiReceptKomanda = new RelayCommand(Executed_ObrisiReceptKomanda, CanExecute_ObrisiReceptKomanda);
            VidiReceptKomanda = new RelayCommand(Executed_VidiReceptKomanda, CanExecute_VidiReceptKomanda);
            DodajReceptKomanda = new RelayCommand(Executed_DodajReceptKomanda, CanExecute_DodajReceptKomanda);
            ZatvoriKomanda = new RelayCommand(Executed_ZatvoriKomanda, CanExecute_ZatvoriKomanda);
            PotvrdiKomanda = new RelayCommand(Executed_PotvrdiKomanda, CanExecute_PotvrdiKomanda);
            DemoOtkaziKomanda = new RelayCommand(Executed_DemoOtkaziKomanda, CanExecute_DemoOtkaziKomanda);
            IzvestajKomanda = new RelayCommand(Executed_IzvestajKomanda, CanExecute_IzvestajKomanda);
            FokusirajDole = new RelayCommand(Executed_FokusirajDole, CanExecute_FokusirajDole);
        }
       
        
        private void PopuniAnamnezu(PrikazPregleda izabraniPregled, int i)
        {
            idAnamneze = izabraniPregled.Anamneza.Id;
            Simptomi = sveAnamneze[i].Simptomi;
            Dijagnoza = sveAnamneze[i].Dijagnoza;
            popuniRecepte(i);
        }
        private void PopuniAnamnezu(PrikazOperacije izabranaOperacija, int i)
        {
            idAnamneze = izabranaOperacija.Anamneza.Id;
            Simptomi = sveAnamneze[i].Simptomi;
            Dijagnoza = sveAnamneze[i].Dijagnoza;
            popuniRecepte(i);
        }

        private void popuniRecepte(int i)
        {
            PrikazRecepta noviPrikazRecepta = new PrikazRecepta();
            for (int r = 0; r < sveAnamneze[i].Recept.Count; r++)
            {
                noviPrikazRecepta = new PrikazRecepta(sveAnamneze[i].Recept[r].DatumIzdavanja, sveAnamneze[i].Recept[r].Kolicina, sveAnamneze[i].Recept[r].VremeUzimanja, sveAnamneze[i].Recept[r].Trajanje);
                noviPrikazRecepta.Id = sveAnamneze[i].Recept[r].Id;
                for (int le = 0; le < sviLekovi.Count; le++)
                {
                    if (sveAnamneze[i].Recept[r].Lek.Id.Equals(sviLekovi[le].Id))
                    {
                        noviPrikazRecepta.lek = sviLekovi[le];
                        break;
                    }
                }
                Recepti.Add(noviPrikazRecepta);

            }
        }

        
        private void FiltirajLekove()
        {
            sviLekovi = inject.AnamnezaLekarController.DobijLekove();

            for (int i = 0; i < sviLekovi.Count; i++)
            {
                if (sviLekovi[i].Status.Equals(StatusLeka.odbijen) || sviLekovi[i].Obrisan)
                {
                    sviLekovi.RemoveAt(i);
                    i--;
                }
            }
        }

        private void InicirajPodatkeZaPregled(PrikazPregleda izabraniPregled, Lekar ulogovaniLekar)
        {
            DatumProsao = true;
            trenutniPacijent = izabraniPregled.Pacijent;
            sveAnamneze = inject.AnamnezaLekarController.DobijAnamneze();
            Recepti = new ObservableCollection<PrikazRecepta>();
            Simptomi = "";
            Dijagnoza = "";
            trenutniPregled = izabraniPregled;
            sviLekari = inject.AnamnezaLekarController.DobijLekare();
            stariPregled = izabraniPregled;
            this.ulogovaniLekar = ulogovaniLekar;
        }
        private void InicirajPodatkeZaOperaciju(PrikazOperacije izabranaOperacija, Lekar ulogovaniLekar)
        {
            DatumProsao = true;
            trenutniPacijent = izabranaOperacija.Pacijent;
            trenutnaOperacija = izabranaOperacija;
            staraOperacija = izabranaOperacija;
            sveAnamneze = inject.AnamnezaLekarController.DobijAnamneze();
            Simptomi = "";
            Dijagnoza = "";
            sviLekovi = inject.AnamnezaLekarController.DobijLekove();
            sviLekari = inject.AnamnezaLekarController.DobijLekare();
            this.ulogovaniLekar = ulogovaniLekar;
            Recepti = new ObservableCollection<PrikazRecepta>();
        }
        private void PopuniIliKreirajAnamnezuPregleda(PrikazPregleda izabraniPregled)
        {
            for (int i = 0; i < sveAnamneze.Count; i++)
            {
                if (izabraniPregled.Anamneza.Id == sveAnamneze[i].Id)
                {
                    ProveriValidnostDatumaIzdavanja(izabraniPregled);
                    PopuniAnamnezu(izabraniPregled, i);
                    DaLiPostojiAnamneza = true;
                    break;
                }
            }

            if (!DaLiPostojiAnamneza)
            {
             
                Recepti = new ObservableCollection<PrikazRecepta>();
            }
        }

        private void PopuniIliKreirajAnamnezuOperacije(PrikazOperacije izabranaOperacija)
        {
            for (int i = 0; i < sveAnamneze.Count; i++)
            {
                if (izabranaOperacija.Anamneza.Id == sveAnamneze[i].Id)
                {
                    ProveriValidnostDatumaIzdavanja(izabranaOperacija);
                    PopuniAnamnezu(izabranaOperacija, i);
                    DaLiPostojiAnamneza = true;
                    break;
                }
            }
            if (!DaLiPostojiAnamneza )
            {
               
                Recepti = new ObservableCollection<PrikazRecepta>();
            }
        }

        private void ProveriValidnostDatumaIzdavanja(PrikazPregleda izabraniPregled)
        {
            if ((izabraniPregled.Datum > DateTime.Now) || (izabraniPregled.Datum.AddDays(7) < DateTime.Now))
            {
                DatumProsao = false;
            }
        }

        private void ProveriValidnostDatumaIzdavanja(PrikazOperacije izabranaOperacija)
        {
            if ((izabranaOperacija.Datum > DateTime.Now) || (izabranaOperacija.Datum.AddDays(7) < DateTime.Now))
            {
                DatumProsao = false;
            }

        }

        #endregion
    }
}
