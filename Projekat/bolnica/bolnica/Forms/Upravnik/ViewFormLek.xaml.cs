using bolnica.Forms;
using Bolnica.Model.Pregledi;
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
        public ViewFormLek(int idLeka)
        {
            InitializeComponent();
            this.DataContext = this;
            Sastojci = new ObservableCollection<Sastojak>();
            LekoviZamene = new ObservableCollection<Lek>();
            FileStorageLek storageLek = new FileStorageLek();
            List<Lek> lekovi = storageLek.GetAll();
            Lek lek = new Lek();
            foreach (Lek l in lekovi)
            {
                if (l.Id == idLeka && !l.Obrisan)
                {
                    lek = l;
                    break;
                }
            }

            foreach (Lek l in lekovi)
            {
                foreach (int id in lek.IdZamena)
                {
                    if(l.Id == id)
                    {
                        if(!l.Obrisan)
                            LekoviZamene.Add(l);
                    }
                }
            }
            FileStorageSastojak storageSastojak = new FileStorageSastojak();
            foreach(Sastojak s in lek.Sastojak)
            {
                foreach(Sastojak sas in storageSastojak.GetAll())
                    if(s.Id == sas.Id)
                        Sastojci.Add(sas);
            }
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

