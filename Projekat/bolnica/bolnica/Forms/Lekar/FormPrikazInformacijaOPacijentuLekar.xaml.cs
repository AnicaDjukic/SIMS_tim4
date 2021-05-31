using Bolnica.Model.Pregledi;
using Bolnica.ViewModel;
using Model.Korisnici;
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

namespace Bolnica.Forms
{
    
    public partial class FormPrikazInformacijaOPacijentuLekar : Window
    {

        public FormPrikazInformacijaOPacijentuLekar(InformacijeOPacijentuLekarViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
            if (viewModel.ZatvoriAkcija == null)
                viewModel.ZatvoriAkcija = new Action(this.Close);
            this.Show();
        }
    }
}
