using Model.Prostorije;
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

namespace bolnica.Forms
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class FormUpravnik : Window
    {   
        public ObservableCollection<Prostorija> Prostorije 
        {
            get;
            set;
        }

        public FormUpravnik()
        {
            InitializeComponent();
            this.DataContext = this;
            Prostorije = new ObservableCollection<Prostorija>();
            Prostorije.Add(new Prostorija { BrojProstorije = 101, Sprat = 1, Kvadratura = 50.5, TipProstorije = TipProstorije.salaZaPreglede, Zauzeta = true});
            Prostorije.Add(new Prostorija { BrojProstorije = 102, Sprat = 1, Kvadratura = 65, TipProstorije = TipProstorije.operacionaSala, Zauzeta = true });
            Prostorije.Add(new BolnickaSobe { BrojProstorije = 103, Sprat = 1, TipProstorije = TipProstorije.bolnickaSoba, Zauzeta = false, BrojSlobodnihKreveta = 10, Kvadratura = 50.1, UkBrojKreveta = 12 });
        }
    }
}
