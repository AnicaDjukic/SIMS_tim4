using Bolnica.ViewModel;
using System.Windows.Controls;

namespace Bolnica.Forms
{
    /// <summary>
    /// Interaction logic for FormPacijentPage.xaml
    /// </summary>
    public partial class FormPacijentPage : Page
    {
        public FormPacijentPage(PacijentPageViewModel pacijentPageViewModel)
        {
            InitializeComponent();

            this.DataContext = pacijentPageViewModel;
            FormPacijentWeb.Forma.Pocetna.Content = this;
        }

    }
}
