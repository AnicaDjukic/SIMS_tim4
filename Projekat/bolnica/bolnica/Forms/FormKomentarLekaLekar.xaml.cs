using Bolnica.Model.Korisnici;
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
    /// Interaction logic for FormKomentarLekaLekar.xaml
    /// </summary>
    public partial class FormKomentarLekaLekar : Window
    {
        public String komentar { get; set; }

        private PrikazLek prik = new PrikazLek();

        private List<Lek> leko = new List<Lek>();
        public FormKomentarLekaLekar(PrikazLek p,List<Lek> lekovi)
        {
            leko = lekovi;
            for (int i = 0; i < leko.Count; i++)
            {
                if (leko[i].Status.Equals(StatusLeka.Odbijen))
                {
                    leko.RemoveAt(i);
                    i--;
                }
            }
            prik = p;
            InitializeComponent();
            this.DataContext = this;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            Owner = Application.Current.MainWindow;
        }

        private void Potvrdi(object sender, RoutedEventArgs e)
        {
            komentar = textbox.Text;
            
            Obavestenje obavestenje = new Obavestenje();
            obavestenje.KorisnickaImena = new List<string>();
            FileStorageObavestenja svaObavestenja = new FileStorageObavestenja();
            FileStorageKorisnici sviKorisnici = new FileStorageKorisnici();
            List<Korisnik> svi = sviKorisnici.GetAll();
            List<Obavestenje> sva = svaObavestenja.GetAll();
            int max = 0;
            for (int i = 0; i < sva.Count; i++)
            {
                if (max < sva[i].Id)
                {
                    max = sva[i].Id;
                }
            }
            max = max + 1;
            obavestenje.Id = max;

            for (int i = 0; i < svi.Count; i++)
            {
                if (svi[i].TipKorisnika.Equals(TipKorisnika.upravnik))
                {
                    obavestenje.KorisnickaImena.Add(svi[i].KorisnickoIme);
                }
            }
            obavestenje.Naslov = "Lek " + prik.Naziv + " je odbijen";
            obavestenje.Obrisan = false;
            obavestenje.Sadrzaj = "Lek " + prik.Naziv + " sa dozom " + prik.KolicinaUMg + " i sastojcima: " + prik.Sastojak + " je odbijen. "+"Komentar: "+komentar;
            obavestenje.Datum = DateTime.Now;
            FileStorageObavestenja oba = new FileStorageObavestenja();
            oba.Save(obavestenje);

            FormLekar.lekoviPrikaz.Remove(prik);
            for(int i = 0; i < leko.Count; i++)
            {
                if (leko[i].Id.Equals(prik.Id))
                {
                    leko[i].Status = StatusLeka.Odbijen;
                }
            }

            this.Close();
        }

        private void Otkazi(object sender, RoutedEventArgs e)
        {
            
            this.Close();
            
        }
    }
}
