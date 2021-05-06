using bolnica;
using Bolnica.Model.Korisnici;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for ViewFormObavestenje.xaml
    /// </summary>
    public partial class ViewFormObavestenje : Window
    {
        public ViewFormObavestenje(int id)
        {
            InitializeComponent();
            this.Owner = App.Current.MainWindow;
            FileStorageObavestenja storage = new FileStorageObavestenja();
            
            foreach(Obavestenje o in storage.GetAll())
            {
                if(o.Id == id)
                {
                    txtNaslov.Text = o.Naslov;
                    txtSadrzaj.Text = o.Sadrzaj;
                }
            }
        }

        private void Button_Click_Zatvori(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
