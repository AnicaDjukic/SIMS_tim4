using Bolnica.Localization;
using Bolnica.Model.Pregledi;
using Bolnica.Repository.Pregledi;
using Bolnica.Services.Pregledi;
using Bolnica.ViewModel.Upravnik;
using Model.Pregledi;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace Bolnica.Forms.Upravnik
{
    /// <summary>
    /// Interaction logic for FormSastojci.xaml
    /// </summary>
    public partial class FormSastojci : Window
    {
        public static ObservableCollection<Sastojak> Sastojci
        {
            get;
            set;
        }

        public FormSastojci(ViewModelFormSastojci viewModel)
        {
            InitializeComponent();
            Title = LocalizedStrings.Instance["Sastojci"];
            DataContext = viewModel;
            if (viewModel.ZatvoriAkcija == null)
                viewModel.ZatvoriAkcija = new Action(this.Close);
            Show();
        }
    }
}
