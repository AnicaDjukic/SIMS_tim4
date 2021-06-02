using Bolnica.ViewModel;
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
    /// Interaction logic for FormIzvestajLekar.xaml
    /// </summary>
    public partial class FormIzvestajLekar : Window
    {
        public FormIzvestajLekar(IzvestajLekarViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
            if (viewModel.ZatvoriAction == null)
                viewModel.ZatvoriAction = new Action(this.Close);
            viewModel.Postavi(dataGridLekovi);

            this.Show();
        }
    }
}
