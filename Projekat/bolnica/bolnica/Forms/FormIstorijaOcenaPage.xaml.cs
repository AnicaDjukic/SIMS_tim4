using Bolnica.Model;
using Bolnica.Model.Korisnici;
using Model.Korisnici;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Bolnica.Forms
{
    /// <summary>
    /// Interaction logic for FormIstorijaOcenaPage.xaml
    /// </summary>
    public partial class FormIstorijaOcenaPage : Page
    {
        public FormIstorijaOcenaPage(Pacijent trenutniPacijent, FormPacijentWeb formPacijentWeb)
        {
            InitializeComponent();

            FileStorageOcene storageOcene = new FileStorageOcene();
            List<Ocena> ocene = storageOcene.GetAll();
            foreach (Ocena o in ocene)
            {
                if (trenutniPacijent.Jmbg.Equals(o.PosiljalacJMBG))
                {
                    oceneGrid.Items.Add(o);
                }
            }
        }
    }
}
