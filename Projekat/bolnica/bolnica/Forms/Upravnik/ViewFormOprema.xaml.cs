using Bolnica.Model.Prostorije;
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
        public ViewFormOprema(string sifraOpreme)
        {
            InitializeComponent();
            this.DataContext = this;
            Zalihe = new ObservableCollection<Zaliha>();

            FileStorageZaliha storage = new FileStorageZaliha();
            List<Zaliha> zalihe = storage.GetAll();
            
            foreach(Zaliha z in zalihe)
            {
                if(z.Oprema.Sifra == sifraOpreme)
                {
                    Zalihe.Add(z);
                }
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
