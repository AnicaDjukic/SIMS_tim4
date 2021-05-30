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

        private ServiceZaliha serviceZaliha = new ServiceZaliha();
        private ServiceBuducaZaliha serviceBuduceZalihe = new ServiceBuducaZaliha();
        public ViewFormOprema(string sifraOpreme)
        {
            InitializeComponent();
            this.DataContext = this;
            
            List<BuducaZaliha> buduceZalihe = serviceBuduceZalihe.DobaviBuduceZaliheOpreme(sifraOpreme);

            List<Zaliha> zalihe;
            if (buduceZalihe.Count > 0)
            {
                ObrisiStareZalihe(sifraOpreme);
                zalihe = NapraviNoveZalihe(buduceZalihe);
                serviceZaliha.SacuvajZalihe(zalihe);
            }
            else
            {
                zalihe = serviceZaliha.DobaviZaliheOpreme(sifraOpreme);
            }

            PrikaziZalihe(zalihe);
        }

        private void PrikaziZalihe(List<Zaliha> zalihe)
        {
            Zalihe = new ObservableCollection<Zaliha>();
            foreach (Zaliha z in zalihe)
                Zalihe.Add(z);
        }

        private List<Zaliha> NapraviNoveZalihe(List<BuducaZaliha> buduceZalihe)
        {
            return serviceZaliha.NapraviZaliheOdBuducihZaliha(buduceZalihe); ;
        }

        private void ObrisiStareZalihe(string sifraOpreme)
        {
            serviceZaliha.ObrisiZalihe(sifraOpreme);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
