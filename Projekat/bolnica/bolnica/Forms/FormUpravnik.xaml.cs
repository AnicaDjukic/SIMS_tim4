using Bolnica.Forms;
using Model.Prostorije;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace bolnica.Forms
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class FormUpravnik : Window
    {
        public static ObservableCollection<Prostorija> Prostorije
        {
            get;
            set;
        }

        private FileStorageProstorija storage;

        public FormUpravnik()
        {
            InitializeComponent();
            this.DataContext = this;
            Prostorije = new ObservableCollection<Prostorija>();
            Prostorija prva = new Prostorija { BrojProstorije = 101, Sprat = 1, Kvadratura = 50.5, TipProstorije = TipProstorije.salaZaPreglede, Zauzeta = true };
            Prostorija druga = new Prostorija { BrojProstorije = 102, Sprat = 1, Kvadratura = 65, TipProstorije = TipProstorije.operacionaSala, Zauzeta = true };
            Prostorija treca = new BolnickaSoba { BrojProstorije = 111, Sprat = 1, Kvadratura = 50.1, TipProstorije = TipProstorije.bolnickaSoba, Zauzeta = false, BrojSlobodnihKreveta = 10, UkBrojKreveta = 12 };
            storage = new FileStorageProstorija();
            /*storage.Save(prva);
            storage.Save(druga);*/
            storage.Save(treca);
            List<Prostorija> prostorije = storage.GetAll();
            foreach(Prostorija p in prostorije)
            {
                Prostorije.Add(p);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var s = new CreateFormProstorije();
            s.Show();
        }
    }
}
