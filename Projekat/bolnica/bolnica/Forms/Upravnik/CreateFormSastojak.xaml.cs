using Bolnica.Model.Pregledi;
using Bolnica.Services.Pregledi;
using Bolnica.ViewModel.Upravnik;
using System;
using System.Windows;

namespace Bolnica.Forms.Upravnik
{
    /// <summary>
    /// Interaction logic for CreateFormSastojak.xaml
    /// </summary>
    public partial class CreateFormSastojak : Window
    {
        public CreateFormSastojak(ViewModelCreateFormSastojak viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            if (viewModel.ZatvoriAkcija == null)
                viewModel.ZatvoriAkcija = new Action(this.Close);
            Show();
        }
    }
}
