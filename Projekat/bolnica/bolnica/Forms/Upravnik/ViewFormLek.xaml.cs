using bolnica.Forms;
using Bolnica.Localization;
using Bolnica.Model.Pregledi;
using Bolnica.Services.Pregledi;
using Model.Pregledi;
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
    /// Interaction logic for ViewFormLek.xaml
    /// </summary>
    public partial class ViewFormLek : Window
    {
        public static ObservableCollection<Sastojak> Sastojci
        {
            get;
            set;
        }

        public static ObservableCollection<Lek> LekoviZamene
        {
            get;
            set;
        }

        private ServiceLek serviceLek = new ServiceLek();
        public ViewFormLek(int idLeka)
        {
            InitializeComponent();
            this.DataContext = this;
            Title = LocalizedStrings.Instance["Prikaz leka"];
            
            Lek lek = PronadjiLek(idLeka);

            PrikaziZameneLeka(lek);
            
            PrikaziSastojkeLeka(lek);
            
        }

        private void PrikaziSastojkeLeka(Lek lek)
        {
            Sastojci = new ObservableCollection<Sastojak>();
            foreach (Sastojak s in serviceLek.DobaviSastojkeLeka(lek))
            {
                Sastojci.Add(s);
            }
        }

        private void PrikaziZameneLeka(Lek lek)
        {
            LekoviZamene = new ObservableCollection<Lek>();
            foreach (Lek l in serviceLek.DobaviSveZameneLeka(lek))
            {
                LekoviZamene.Add(l);
            }
        }

        private Lek PronadjiLek(int idLeka)
        {
            Lek lek = serviceLek.DobaviLek(idLeka);
            return lek;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

