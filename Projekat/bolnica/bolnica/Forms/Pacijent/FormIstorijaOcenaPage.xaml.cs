using Bolnica.Controller;
using Model.Korisnici;
using System.Collections.Generic;
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

        private IstorijaOcenaController istorijaOcenaController = new IstorijaOcenaController();

        public FormIstorijaOcenaPage(Pacijent trenutniPacijent)
        {
            InitializeComponent();
            this.DataContext = this;
            istorijaOcenaController.PopuniTabeluOcena(trenutniPacijent);
        }
    }
}
