using Bolnica.Model.Pregledi;
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

        public string proizvodjac { get; set; }
        public DateTime datumPrekida { get; set; }

        private Pacijent trePac = new Pacijent();

        private List<Lek> lekovi;

        private FileStorageLek sviLekovi = new FileStorageLek();
        public FormNapraviReceptLekar(Pacijent trenutniPacijent)
        {
            lekovi = sviLekovi.GetAll();
            for (int i = 0; i < lekovi.Count; i++)
            {
                if (lekovi[i].Status.Equals(StatusLeka.Odbijen))
                {
                    lekovi.RemoveAt(i);
                    i--;
                }
            }
            trePac = trenutniPacijent;
            InitializeComponent();

            this.DataContext = this;

            datumIzdavanja = DateTime.Now;

            for (int i = 0; i < lekovi.Count; i++)
            {
                int dozvolica = 0;
                for (int p = 0; p < FormNapraviAnamnezuLekar.Recepti.Count; p++)
                {
                    if (FormNapraviAnamnezuLekar.Recepti[p].lek.Id.Equals(lekovi[i].Id))
                    {
                        dozvolica = 1;
                    }


                }
                if (dozvolica == 0)
                {
                    if (!textLek.Items.Contains(lekovi[i].Naziv))
                    {
                        textLek.Items.Add(lekovi[i].Naziv);
                    }
                    if (!textDoza.Items.Contains(lekovi[i].KolicinaUMg))
                    {
                        textDoza.Items.Add(lekovi[i].KolicinaUMg);
                    }
                    if (!textProizvodjac.Items.Contains(lekovi[i].Proizvodjac))
                    {
                        textProizvodjac.Items.Add(lekovi[i].Proizvodjac);
                    }
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
            Lek izabraniLek = new Lek();
            for (int i = 0; i < lekovi.Count; i++)
            {
                if (textProizvodjac.Text.Equals(lekovi[i].Proizvodjac)&&textLek.Text.Equals(lekovi[i].Naziv) && int.Parse(textDoza.Text).Equals(lekovi[i].KolicinaUMg))
                {
                    izabraniLek = lekovi[i];
                }
            }
            List<Sastojak>? alergeniPacijenta = trePac?.Alergeni;
            if (alergeniPacijenta != null)
            {
                for (int o = 0; o < alergeniPacijenta.Count; o++)
                {
                    for (int m = 0; m < izabraniLek.Sastojak.Count; m++)
                    {
                        if (alergeniPacijenta[o].Naziv.Equals(izabraniLek.Sastojak[m].Naziv))
                        {
                            MessageBox.Show("Pacijent je alergican na izabrani lek");
                            return;
                        }

                    }
                }
            }
        
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
                        if (textLek.Text.Equals(lekovi[i].Naziv) && !textDoza.Items.Contains(lekovi[i].KolicinaUMg))
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

        private void IsPro(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Tab)
            {
                if (textProizvodjac.Text.Length > 2)
                {
                    textLek.Items.Clear();
                    for (int i = 0; i < lekovi.Count; i++)
                    {
                        if (textProizvodjac.Text.Equals(lekovi[i].Proizvodjac) && !textLek.Items.Contains(lekovi[i].Naziv))
                        {
                            textLek.Items.Add(lekovi[i].Naziv);
                        }
                    }
                }
            }
            else if(e.Key == Key.Enter)
            {
                textProizvodjac.IsDropDownOpen = true;
            }
        }
    }
}
