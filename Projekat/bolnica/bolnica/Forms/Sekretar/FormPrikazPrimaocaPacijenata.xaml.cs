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
    /// Interaction logic for FormPrikazPrimaocaPacijenata.xaml
    /// </summary>
    public partial class FormPrikazPrimaocaPacijenata : Window
    {
        public static ObservableCollection<Pacijent> Primaoci { get; set; }
        private FileStoragePacijenti storage;
        public FormPrikazPrimaocaPacijenata(int id)
        {
            InitializeComponent();
            dataGridPrimaoci.DataContext = this;
            Primaoci = new ObservableCollection<Pacijent>();
            storage = new FileStoragePacijenti();
            List<Pacijent> pacijenti = storage.GetAll();
            List<Pacijent> redovniPacijenti = new List<Pacijent>();

            foreach (Pacijent p in pacijenti)
                if (!p.Guest)
                    redovniPacijenti.Add(p);

            for (int i = 0; i < FormObavestenja.Obavestenja.Count; i++)
                if (id == FormObavestenja.Obavestenja[i].Id)
                {
                    foreach (Pacijent p in pacijenti)
                    {
                        if (!p.Guest)
                        {
                            if (FormObavestenja.Obavestenja[i].KorisnickaImena.Contains(p.KorisnickoIme))
                            {
                                Primaoci.Add(p);
                            }
                        }
                    }
                }
        }

        private void Button_Clicked_Zatvori(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
