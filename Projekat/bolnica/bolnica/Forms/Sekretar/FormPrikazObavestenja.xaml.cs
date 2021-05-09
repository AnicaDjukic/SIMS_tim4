using Bolnica.Forms.Sekretar;
using Model.Korisnici;
using Model.Pacijenti;
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

namespace Bolnica.Sekretar
{
    /// <summary>
    /// Interaction logic for FormPrikazObavestenja.xaml
    /// </summary>
    public partial class FormPrikazObavestenja : Window
    {
        private int id;
        private FileStoragePacijenti storagePacijenti;
        private List<Pacijent> pacijenti;
        public FormPrikazObavestenja(int id)
        {
            InitializeComponent();
            this.id = id;
            storagePacijenti = new FileStoragePacijenti();
            pacijenti = storagePacijenti.GetAll();

            for (int i = 0; i < FormObavestenja.Obavestenja.Count; i++)
                if (id == FormObavestenja.Obavestenja[i].Id)
                {
                    List<string> korImena = new List<string>();
                    foreach (Korisnik k in FormObavestenja.Obavestenja[i].Korisnici)
                        korImena.Add(k.KorisnickoIme);

                    bool svi = true;
                    foreach (Pacijent p in pacijenti)
                    {
                        if (!p.Guest)
                        {
                            if (!korImena.Contains(p.KorisnickoIme))
                                svi = false;
                        }
                    }

                    bool barJedanPacijent = false;
                    foreach (Pacijent p in pacijenti)
                    {
                        if (!p.Guest)
                        {
                            if (korImena.Contains(p.KorisnickoIme))
                                barJedanPacijent = true;
                        }
                    }

                    btnDodajPacijente.IsEnabled = false;
                    if (!svi && barJedanPacijent)
                        btnDodajPacijente.IsEnabled = true;

                    bool enabledPrimaoci = false;
                    btnDodajPrimaoce.IsEnabled = false;
                    foreach (string s in korImena)
                        if (s.Equals("upravnik") || s.Equals("mico") || svi)
                            enabledPrimaoci = true;

                    if (enabledPrimaoci)
                        btnDodajPrimaoce.IsEnabled = true;
                }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_Prikazi_Primaoce(object sender, RoutedEventArgs e)
        {
            FormPrikazPrimaoca s = new FormPrikazPrimaoca(id);
            s.ShowDialog();
        }

        private void Button_Click_Prikazi_Pacijente(object sender, RoutedEventArgs e)
        {
            FormPrikazPrimaocaPacijenata s = new FormPrikazPrimaocaPacijenata(id);
            s.ShowDialog();
        }
    }
}
