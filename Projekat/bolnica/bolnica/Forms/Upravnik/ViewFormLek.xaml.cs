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
        public ViewFormLek(int idLeka)
        {
            InitializeComponent();
            this.DataContext = this;
            Sastojci = new ObservableCollection<Sastojak>();

            FileStorageLek storageLek = new FileStorageLek();
            List<Lek> lekovi = storageLek.GetAll();
            Lek lek = new Lek();
            foreach (Lek l in lekovi)
            {
                if (l.Id == idLeka)
                {
                    lek = l;
                    break;
                }
            }

            foreach(Sastojak s in lek.Sastojak)
            {
                Sastojci.Add(s);
            }
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

