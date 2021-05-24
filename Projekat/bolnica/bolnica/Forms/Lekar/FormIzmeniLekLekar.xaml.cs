using Bolnica.Model.Pregledi;
using Bolnica.ViewModel;
using Model.Pregledi;
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
    /// <summary>
    /// Interaction logic for FormIzmeniLekLekar.xaml
    /// </summary>

    public partial class FormIzmeniLekLekar : Window
    {
       public FormIzmeniLekLekar(IzmeniLekLekarViewModel viewModel)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            Owner = Application.Current.MainWindow;
            this.DataContext = viewModel;
            if (viewModel.ZatvoriAkcija == null)
                viewModel.ZatvoriAkcija = new Action(this.Close);
            viewModel.PopuniSastojkeIZamene(textSastojci, textZamene);
            this.Show();
        }
    }
}
