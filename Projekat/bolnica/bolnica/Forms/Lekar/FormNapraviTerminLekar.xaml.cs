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
using Model.Pregledi;
using Model.Korisnici;
using Model.Prostorije;
using Model.Pacijenti;
using Bolnica.Validation;
using Bolnica.Model.Pregledi;
using Bolnica.Model.Korisnici;
using Bolnica.Model.Prostorije;
using Bolnica.ViewModel;

namespace Bolnica.Forms
{

    public partial class FormNapraviTerminLekar : Window
    {
        

        public FormNapraviTerminLekar(TerminLekarViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
            if (viewModel.CloseAction == null)
                viewModel.CloseAction = new Action(this.Close);
            this.Show();

        }



       
    }
}
