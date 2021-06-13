using Bolnica.ViewModel;
using System.Windows.Controls;

namespace Bolnica.Forms
{
    /// <summary>
    /// Interaction logic for FormIstorijaPregledaPage.xaml
    /// </summary>
    public partial class FormIstorijaPregledaPage : Page
    {
        public FormIstorijaPregledaPage(IstorijaPregledaPacijentViewModel istorijaPregledaPacijentViewModel)
        {
            InitializeComponent();

            this.DataContext = istorijaPregledaPacijentViewModel;
            FormPacijentWeb.Forma.Pocetna.Content = this;
        }
    }
}
