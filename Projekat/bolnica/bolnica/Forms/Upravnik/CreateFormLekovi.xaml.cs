using bolnica.Forms;
using Bolnica.Localization;
using Bolnica.ViewModel.Upravnik;
using System;
using System.Windows;

namespace Bolnica.Forms.Upravnik
{
    /// <summary>
    /// Interaction logic for CreateFormLekovi.xaml
    /// </summary>
    public partial class CreateFormLekovi : Window
    {
        public CreateFormLekovi(ViewModelCreateFormLekovi viewModel)
        {
            InitializeComponent();
            if (!FormUpravnik.clickedDodaj)
                Title = LocalizedStrings.Instance["Izmena leka"];
            else
                Title = LocalizedStrings.Instance["Dodavanje leka"];
            DataContext = viewModel;
            if (viewModel.ZatvoriAkcija == null)
                viewModel.ZatvoriAkcija = new Action(this.Close);
            Show();
        }
    }
}
