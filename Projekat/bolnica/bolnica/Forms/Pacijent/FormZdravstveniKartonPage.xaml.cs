using Bolnica.ViewModel;
using Model.Korisnici;
using System.Windows;
using System.Windows.Controls;

namespace Bolnica.Forms
{
    /// <summary>
    /// Interaction logic for FormZdravstveniKartonPage.xaml
    /// </summary>
    public partial class FormZdravstveniKartonPage : Page
    {
        public Pacijent Pacijent { get; set; }
        public FormZdravstveniKartonPage(Pacijent trenutniPacijent)
        {
            InitializeComponent();
            this.DataContext = this;
            Pacijent = trenutniPacijent;
        }

        private void Button_Click_Istorija_Pregleda(object sender, RoutedEventArgs e)
        {
            IstorijaPregledaPacijentViewModel istorijaPregledaPacijentViewModel = new IstorijaPregledaPacijentViewModel(Pacijent);
            FormPacijentWeb.Forma.Pocetna.Content = new FormIstorijaPregledaPage(istorijaPregledaPacijentViewModel);
        }

        private void Button_Click_Moji_Lekovi(object sender, RoutedEventArgs e)
        {
            FormPacijentWeb.Forma.Pocetna.Content = new FormLekoviTerapijePage(Pacijent);
        }
    }
}
