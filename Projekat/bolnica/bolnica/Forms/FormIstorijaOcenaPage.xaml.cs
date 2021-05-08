using Bolnica.Model;
using Bolnica.Model.Pregledi;
using Model.Korisnici;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
