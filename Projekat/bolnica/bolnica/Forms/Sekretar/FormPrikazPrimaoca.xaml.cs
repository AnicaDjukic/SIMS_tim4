using Bolnica.Sekretar;
using Model.Korisnici;
using Model.Pacijenti;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for FormPrikazPrimaoca.xaml
    /// </summary>
    public partial class FormPrikazPrimaoca : Window
    {
        public static ObservableCollection<Primalac> Primaoci { get; set; }
        private FileStoragePacijenti storagePacijenti;
        private List<Pacijent> pacijenti;
        public FormPrikazPrimaoca(int id)
        {
            InitializeComponent();
            dataGridPrimaoci.DataContext = this;
            Primaoci = new ObservableCollection<Primalac>();
            storagePacijenti = new FileStoragePacijenti();
            pacijenti = storagePacijenti.GetAll();
            Primalac p1 = new Primalac("Upravnik");
            Primalac p2 = new Primalac("Svi lekari");
            Primalac p3 = new Primalac("Svi pacijenti");

            for (int i = 0; i < FormObavestenja.Obavestenja.Count; i++)
                if (id == FormObavestenja.Obavestenja[i].Id)
                {
                    List<string> korImena = new List<string>();
                    foreach (Korisnik k in FormObavestenja.Obavestenja[i].Korisnici)
                        korImena.Add(k.KorisnickoIme);

                    if (korImena.Contains("upravnik"))
                    {
                        Primaoci.Add(p1);
                    }

                    if (korImena.Contains("mico"))
                    {
                        Primaoci.Add(p2);
                    }

                    bool svi = true;
                    foreach (Pacijent p in pacijenti)
                    {
                        if (!p.Guest)
                        {
                            if (!korImena.Contains(p.KorisnickoIme))
                                svi = false;
                        }
                    }

                    if (svi)
                    {
                        Primaoci.Add(p3);
                    }

                    break;
                }
        }

        private void Button_Clicked_Zatvori(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
