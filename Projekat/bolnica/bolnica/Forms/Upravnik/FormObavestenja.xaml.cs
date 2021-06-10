using Bolnica.Localization;
using Bolnica.Model.Korisnici;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace Bolnica.Forms.Upravnik
{
    /// <summary>
    /// Interaction logic for FormObavestenja.xaml
    /// </summary>
    public partial class FormObavestenja : Window
    {
        private Injector inject;
        public Injector Inject
        {
            get { return inject; }
            set
            {
                inject = value;
            }
        }
        public FormObavestenja()
        {
            InitializeComponent();
            DataContext = this;
            Inject = new Injector();
            Title = LocalizedStrings.Instance["Obaveštenja"];
            List<Obavestenje> obavestenjaZaPrikaz = NadjiObavestenjaZaPrikaz();

            SortirajObavestenjaPoDatumu(obavestenjaZaPrikaz);

            lvDataBinding.ItemsSource = obavestenjaZaPrikaz;
        }

        private void SortirajObavestenjaPoDatumu(List<Obavestenje> obavestenjaZaPrikaz)
        {
            Inject.ControllerObavestenje.SortirajObavestenja(obavestenjaZaPrikaz);
        }

        private List<Obavestenje> NadjiObavestenjaZaPrikaz()
        {
            return Inject.ControllerObavestenje.NadjiObavestenjaKorisnika("upravnik");
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ViewFormObavestenje viewForm = new ViewFormObavestenje(((Obavestenje)lvDataBinding.SelectedItem).Id);
            viewForm.Owner = this;
            viewForm.Show();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
