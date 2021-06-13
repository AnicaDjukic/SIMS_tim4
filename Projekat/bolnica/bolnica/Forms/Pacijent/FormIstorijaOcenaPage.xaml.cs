using Bolnica.Controller;
using Model.Korisnici;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace Bolnica.Forms
{
    /// <summary>
    /// Interaction logic for FormIstorijaOcenaPage.xaml
    /// </summary>
    public partial class FormIstorijaOcenaPage : Page
    {
        public static ObservableCollection<PrikazOcena> PrikazOcenaIKomentara
        {
            get;
            set;
        }

        private OcenaController ocenaController = new OcenaController();

        public FormIstorijaOcenaPage(Pacijent trenutniPacijent)
        {
            InitializeComponent();
            this.DataContext = this;
            ocenaController.DobijOcenePacijenta(trenutniPacijent);
        }
    }
}
