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

namespace Bolnica.Forms
{
    /// <summary>
    /// Interaction logic for FormNapraviReceptLekar.xaml
    /// </summary>
    public partial class FormNapraviReceptLekar : Window
    {

        public DateTime datumIzdavanja { get; set; }
        public string lek { get; set; }
        public string doza { get; set; }
        public string brojKutija { get; set; }
        public string vremeUzimanja { get; set; }
        public DateTime datumPrekida { get; set; }

        private List<Lek> lekovi;
        public FormNapraviReceptLekar(List<Lek> lek,Pacijent trenutniPacijent)
        {
            lekovi = lek;

            InitializeComponent();

            this.DataContext = this;

            datumIzdavanja = DateTime.Now;

            for (int i = 0; i< lekovi.Count; i++)
            {
                int dozvolica = 0;
                for(int p = 0; p < FormNapraviAnamnezuLekar.Recepti.Count; p++)
                {
                    if (FormNapraviAnamnezuLekar.Recepti[p].lek.Id.Equals(lekovi[i].Id))
                    {
                        dozvolica = 1;
                    }
                   /* for (int o = 0; o < trenutniPacijent.Alergeni.Count; o++)
                    {
                        for (int m = 0; m < lekovi[i].Sastojak.Count; m++)
                        {
                            if (trenutniPacijent.Alergeni[o].Equals(lekovi[i].Sastojak[m]))
                            {
                                dozvolica = 1;
                            }

                        }
                    } dio za ako je alergican*/

                }
                if (dozvolica == 0)
                {
                    textLek.Items.Add(lekovi[i].Naziv);
                    textDoza.Items.Add(lekovi[i].KolicinaUMg);
                }
            }
            
               
            
            for (int i = 1; i < 10; i++)
            {
                textBrojKutija.Items.Add(i);
            }
            for (int vre = 0; vre < 24; vre++)
            {
                for (int min = 0; min < 59;)
                {
                    TimeSpan ts = new TimeSpan(vre, min, 0);
                    min = min + 15;
                    textVremeUzimanja.Items.Add(ts);
                }

            }

            datumPrekida = DateTime.Now;


        }

        private void Potvrdi(object sender, RoutedEventArgs e)
        {
            Recept r = new Recept();
            PrikazRecepta rr = new PrikazRecepta();
            r.DatumIzdavanja = DateTime.Parse(textDatumIzdavanja.Text);
            rr.DatumIzdavanja = DateTime.Parse(textDatumIzdavanja.Text);
            for (int i = 0; i < lekovi.Count; i++)
            {
                if (lekovi[i].Naziv.Equals(textLek.Text) && lekovi[i].KolicinaUMg.Equals(int.Parse(textDoza.Text)))
                {
                    r.Lek_id = lekovi[i].Id;
                    rr.lek = lekovi[i];
                    break;
                }
            }
            r.Kolicina = int.Parse(textBrojKutija.Text);
            r.VremeUzimanja = TimeSpan.Parse(textVremeUzimanja.Text);
            r.Trajanje = DateTime.Parse(textDatumPrekida.Text);
            rr.Kolicina = int.Parse(textBrojKutija.Text);
            rr.VremeUzimanja = TimeSpan.Parse(textVremeUzimanja.Text);
            rr.Trajanje = DateTime.Parse(textDatumPrekida.Text);

            FormNapraviAnamnezuLekar.Recepti.Add(rr);
            this.Close();
            
        }

        private void Odustani(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void isTab(object sender, KeyEventArgs e)
        {   
            if(e.Key == Key.Enter)
            {
                textLek.IsDropDownOpen = true;
            }
            if (e.Key == Key.Tab)

            {
                if (textLek.Text.Length > 2)
                {
                    textDoza.Items.Clear();
                    for (int i = 0; i < lekovi.Count; i++)
                    {
                        if (textLek.Text.Equals(lekovi[i].Naziv))
                        {
                            textDoza.Items.Add(lekovi[i].KolicinaUMg);
                        }
                    }
                }

            }
        }

        private void isEnterDoza(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                textDoza.IsDropDownOpen = true;
            }
        }

        private void isEnterBrojKutija(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                textBrojKutija.IsDropDownOpen = true;
            }
        }

        private void isEnterVreme(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                textVremeUzimanja.IsDropDownOpen = true;
            }
        }

       
    }
}
