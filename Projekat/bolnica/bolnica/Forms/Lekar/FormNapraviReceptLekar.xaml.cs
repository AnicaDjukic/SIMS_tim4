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
        public DateTime datumIzdavanjaRecepta { get; set; }
        public string nazivLeka { get; set; }
        public string dozaLeka { get; set; }
        public string brojKutijaLeka { get; set; }
        public string vremeUzimanjaLeka { get; set; }
        public string proizvodjacLeka { get; set; }
        public DateTime datumPrekidaRecepta { get; set; }

        private Pacijent trenutniPacijent = new Pacijent();

        private List<Lek> sviLekovi;

        private FileStorageLek storageLekovi = new FileStorageLek();
        public FormNapraviReceptLekar(Pacijent trenutniPacijent)
        {
            FiltrirajLekove();
            this.trenutniPacijent = trenutniPacijent;
            InitializeComponent();
            this.DataContext = this;
            PopuniComboBoxoveIDatePickere();
        }

        public void Potvrdi()
        {
            Lek izabraniLek = DobijIzabraniLek();
            if (DaLiJePacijentAlergicanNaLek(izabraniLek))
            {
                return;
            }
            FormNapraviAnamnezuLekar.Recepti.Add(NapraviRecept());
            this.Close();
        }

        public Lek DobijIzabraniLek()
        {
            Lek izabraniLek = new Lek();
            for (int i = 0; i < sviLekovi.Count; i++)
            {
                if (textProizvodjac.Text.Equals(sviLekovi[i].Proizvodjac) && textLek.Text.Equals(sviLekovi[i].Naziv) && int.Parse(textDoza.Text).Equals(sviLekovi[i].KolicinaUMg))
                {
                    izabraniLek = sviLekovi[i];
                }
            }
            return izabraniLek;
        }

        public bool DaLiJePacijentAlergicanNaLek(Lek izabraniLek) {
            List<Sastojak>? alergeniPacijenta = trenutniPacijent?.Alergeni;
            if (alergeniPacijenta != null)
            {
                for (int o = 0; o < alergeniPacijenta.Count; o++)
                {
                    for (int m = 0; m < izabraniLek.Sastojak.Count; m++)
                    {
                        if (alergeniPacijenta[o].Naziv.Equals(izabraniLek.Sastojak[m].Naziv))
                        {
                            MessageBox.Show("Pacijent je alergican na izabrani lek");
                            return true;
                        }

                    }
                }
            }
            return false;

        }

        public PrikazRecepta NapraviRecept()
        {
            Recept noviRecept = new Recept();
            PrikazRecepta noviPrikazRecepta = new PrikazRecepta();
            noviRecept.DatumIzdavanja = DateTime.Parse(textDatumIzdavanja.Text);
            noviPrikazRecepta.DatumIzdavanja = DateTime.Parse(textDatumIzdavanja.Text);
            for (int i = 0; i < sviLekovi.Count; i++)
            {
                if (sviLekovi[i].Naziv.Equals(textLek.Text) && sviLekovi[i].KolicinaUMg.Equals(int.Parse(textDoza.Text)))
                {
                    noviRecept.Lek = sviLekovi[i];
                    noviPrikazRecepta.lek = sviLekovi[i];
                    break;
                }
            }
            noviRecept.Kolicina = int.Parse(textBrojKutija.Text);
            noviRecept.VremeUzimanja = TimeSpan.Parse(textVremeUzimanja.Text);
            noviRecept.Trajanje = DateTime.Parse(textDatumPrekida.Text);
            noviPrikazRecepta.Kolicina = int.Parse(textBrojKutija.Text);
            noviPrikazRecepta.VremeUzimanja = TimeSpan.Parse(textVremeUzimanja.Text);
            noviPrikazRecepta.Trajanje = DateTime.Parse(textDatumPrekida.Text);
            return noviPrikazRecepta;

        }
        public void PopuniComboBoxoveIDatePickere()
        {
            PopuniComboBoxLek();
            PopuniComboBoxDoza();
            PopuniComboBoxProizvodjac();
            PopuniComboBoxVremeUzimanja();
            PopuniComboBoxBrojKutija();
            datumIzdavanjaRecepta = DateTime.Now;
            datumPrekidaRecepta = DateTime.Now;
        }

        public int DaLiJeLekVecDodat(int i)
        {
            int lekVecDodat = 0;
            for (int p = 0; p < FormNapraviAnamnezuLekar.Recepti.Count; p++)
            {
                if (FormNapraviAnamnezuLekar.Recepti[p].lek.Id.Equals(sviLekovi[i].Id))
                {
                    lekVecDodat = 1;
                }
            }
            return lekVecDodat;
        }
        public void PopuniComboBoxLek()
        {
            for (int i = 0; i < sviLekovi.Count; i++)
            {
                if (DaLiJeLekVecDodat(i) == 0)
                {
                    if (!textLek.Items.Contains(sviLekovi[i].Naziv))
                    {
                        textLek.Items.Add(sviLekovi[i].Naziv);
                    }
                }
            }
        }
        public void PopuniComboBoxDoza()
        {
            for (int i = 0; i < sviLekovi.Count; i++)
            {
                if (DaLiJeLekVecDodat(i) == 0)
                {
                    if (!textDoza.Items.Contains(sviLekovi[i].KolicinaUMg))
                    {
                        textDoza.Items.Add(sviLekovi[i].KolicinaUMg);
                    }
                }
            }
        }

        public void PopuniComboBoxProizvodjac()
        {
            for (int i = 0; i < sviLekovi.Count; i++)
            {
                if (DaLiJeLekVecDodat(i) == 0)
                {
                    if (!textProizvodjac.Items.Contains(sviLekovi[i].Proizvodjac))
                    {
                        textProizvodjac.Items.Add(sviLekovi[i].Proizvodjac);
                    }
                }
            }
        }


        public void PopuniComboBoxBrojKutija()
        {
            for (int i = 1; i < 10; i++)
            {
                textBrojKutija.Items.Add(i);
            }
        }

        public void PopuniComboBoxVremeUzimanja()
        {
            for (int vre = 0; vre < 24; vre++)
            {
                for (int min = 0; min < 59;)
                {
                    TimeSpan ts = new TimeSpan(vre, min, 0);
                    min = min + 15;
                    textVremeUzimanja.Items.Add(ts);
                }

            }
        }

        public void FiltrirajLekove()
        {
            sviLekovi = storageLekovi.GetAll();
            for (int i = 0; i < sviLekovi.Count; i++)
            {
                if (sviLekovi[i].Status.Equals(StatusLeka.odbijen) || sviLekovi[i].Obrisan)
                {
                    sviLekovi.RemoveAt(i);
                    i--;
                }
            }
        }

        private void Potvrdi(object sender, RoutedEventArgs e)
        {
            Potvrdi();
        }

        public void Odustani()
        {
            this.Close();
        }
        private void Odustani(object sender, RoutedEventArgs e)
        {
            Odustani();
        }

        private void OtvoriIFiltirajNaEnterLek(object sender, KeyEventArgs e)
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
                    for (int i = 0; i < sviLekovi.Count; i++)
                    {
                        if (textLek.Text.Equals(sviLekovi[i].Naziv) && !textDoza.Items.Contains(sviLekovi[i].KolicinaUMg))
                        {
                            textDoza.Items.Add(sviLekovi[i].KolicinaUMg);
                        }
                    }
                }

            }
        }

        private void OtvoriNaEnterDoza(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                textDoza.IsDropDownOpen = true;
            }
        }

        private void OtvoriNaEnterBrojKutija(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                textBrojKutija.IsDropDownOpen = true;
            }
        }

        private void OtvoriNaEnterVreme(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                textVremeUzimanja.IsDropDownOpen = true;
            }
        }

        private void OtvoriIFiltirajNaEnterProizvodjac(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Tab)
            {
                if (textProizvodjac.Text.Length > 2)
                {
                    textLek.Items.Clear();
                    for (int i = 0; i < sviLekovi.Count; i++)
                    {
                        if (textProizvodjac.Text.Equals(sviLekovi[i].Proizvodjac) && !textLek.Items.Contains(sviLekovi[i].Naziv)&& DaLiJeLekVecDodat(i) == 0)
                        {
                            textLek.Items.Add(sviLekovi[i].Naziv);
                        }
                    }
                }
            }
            else if(e.Key == Key.Enter)
            {
                textProizvodjac.IsDropDownOpen = true;
            }
        }

        private void AkoJeAkceleratorPritisnut(object sender, KeyEventArgs e)
        {
            if (e.KeyboardDevice.IsKeyDown(Key.LeftCtrl) || e.KeyboardDevice.IsKeyDown(Key.RightCtrl))
            {
                switch (e.Key)
                {
                    case Key.Q:
                        Potvrdi();
                        break;
                    case Key.W:
                        Odustani();
                        break;


                }
            }
        }
    }
}
