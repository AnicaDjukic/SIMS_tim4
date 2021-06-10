using Bolnica.Localization;
using Bolnica.Model.Prostorije;
using Bolnica.Services.Prostorije;
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

namespace Bolnica.Forms.Upravnik
{
    /// <summary>
    /// Interaction logic for ViewFormOprema.xaml
    /// </summary>
    public partial class ViewFormOprema : Window
    {
        public static ObservableCollection<Zaliha> Zalihe
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
        public ViewFormOprema(string sifraOpreme)
        {
            InitializeComponent();
            this.DataContext = this;
            Inject = new Injector();
            Title = LocalizedStrings.Instance["Prikaz opreme"];
            List<Zaliha> zalihe = DobaviZaliheOpreme(sifraOpreme);
            PrikaziZalihe(zalihe);
        }

        private List<Zaliha> DobaviZaliheOpreme(string sifraOpreme)
        {
            List<BuducaZaliha> buduceZalihe = Inject.ControllerBuducaZaliha.DobaviBuduceZaliheOpreme(sifraOpreme);

            List<Zaliha> zalihe;
            if (buduceZalihe.Count > 0)
            {
                ObrisiStareZalihe(sifraOpreme);
                zalihe = NapraviNoveZalihe(buduceZalihe);
                inject.ControllerZaliha.SacuvajZalihe(zalihe);
            }
            else
            {
                zalihe = Inject.ControllerZaliha.DobaviZaliheOpreme(sifraOpreme);
            }

            return zalihe;
        }

        private void ObrisiStareZalihe(string sifraOpreme)
        {
            Inject.ControllerZaliha.ObrisiZaliheOpreme(sifraOpreme);
        }

        private List<Zaliha> NapraviNoveZalihe(List<BuducaZaliha> buduceZalihe)
        {
            return Inject.ControllerZaliha.NapraviZaliheOdBuducihZaliha(buduceZalihe);
        }

        private void PrikaziZalihe(List<Zaliha> zalihe)
        {
            Zalihe = new ObservableCollection<Zaliha>();
            foreach (Zaliha z in zalihe)
                Zalihe.Add(z);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
