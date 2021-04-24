using Bolnica.Forms.Upravnik;
using Bolnica.Model.Prostorije;
using Model.Prostorije;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace Bolnica.Forms
{
    /// <summary>
    /// Interaction logic for ViewFormProstorije.xaml
    /// </summary>
    public partial class ViewFormProstorije : Window
    {
        public static ObservableCollection<Zaliha> OpremaSobe
        {
            get;
            set;
        }
        public ViewFormProstorije(string brojProstorije)
        {
            InitializeComponent();
            this.DataContext = this;
            OpremaSobe = new ObservableCollection<Zaliha>();
            FileStorageZaliha storageZaliha = new FileStorageZaliha();
            List<Zaliha> zalihe = storageZaliha.GetAll();
            FileStorageOprema storageOprema = new FileStorageOprema();
            List<Oprema> oprema = storageOprema.GetAll();
            if (zalihe != null)
            {
                foreach (Zaliha z in zalihe)
                {
                    if (z.BrojProstorije == brojProstorije)
                    {
                        foreach(Oprema o in oprema)
                        {
                            if(o.Sifra == z.SifraOpreme)
                            {
                                z.Oprema = o;
                                OpremaSobe.Add(z);
                            }
                        }
                    }
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
