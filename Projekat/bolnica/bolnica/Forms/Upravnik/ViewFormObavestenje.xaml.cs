using bolnica;
using Bolnica.Localization;
using Bolnica.Model.Korisnici;
using Bolnica.Services.Korisnici;
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
        private ServiceObavestenje serviceObavestenje = new ServiceObavestenje();
        public ViewFormObavestenje(int id)
        {
            InitializeComponent();
            Title = LocalizedStrings.Instance["Obaveštenje"];
            Owner = App.Current.MainWindow;
            PrikaziObavestenje(id);
        }

        private void PrikaziObavestenje(int id)
        {
            Obavestenje obavestenje = serviceObavestenje.DobaviObavestenje(id);
            txtNaslov.Text = obavestenje.Naslov;
            txtSadrzaj.Text = obavestenje.Sadrzaj;
        }

        private void Button_Click_Zatvori(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
