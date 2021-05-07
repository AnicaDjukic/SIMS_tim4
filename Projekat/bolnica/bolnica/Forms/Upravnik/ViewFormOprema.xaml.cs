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
            FileStorageBuducaZaliha storageBuducaZaliha = new FileStorageBuducaZaliha();
            List<BuducaZaliha> buduceZalihe = new List<BuducaZaliha>();
            if (storageBuducaZaliha.GetAll() != null)
            {
                foreach (BuducaZaliha bz in storageBuducaZaliha.GetAll())
                {
                    if (bz.Oprema.Sifra == sifraOpreme && bz.Datum <= DateTime.Now.Date)
                    {
                        buduceZalihe.Add(bz);
                        storageBuducaZaliha.Delete(bz);
                    }
                }
            }

            FileStorageZaliha storageZaliha = new FileStorageZaliha();
            if (buduceZalihe.Count > 0)
            {
                foreach (Zaliha z in storageZaliha.GetAll())
                {
                    if (z.Oprema.Sifra == sifraOpreme)
                        storageZaliha.Delete(z);
                }
                foreach (BuducaZaliha bz in buduceZalihe)
                {
                    Zaliha z = new Zaliha { Kolicina = bz.Kolicina };
                    z.Prostorija = bz.Prostorija;
                    z.Oprema = bz.Oprema;
                    storageZaliha.Save(z);
                    Zalihe.Add(z);
                }
            }
            else
            {
                foreach (Zaliha z in storageZaliha.GetAll())
                {
                    if (sifraOpreme == z.Oprema.Sifra)
                        Zalihe.Add(z);
                }
            }
            /*FileStorageZaliha storageZalihe = new FileStorageZaliha();
            List<Zaliha> zalihe = storageZalihe.GetAll();
            foreach(Zaliha z in zalihe)
            {
                if(z.SifraOpreme == sifraOpreme)
                {
                    Zalihe.Add(z);
                }
            }*/

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
