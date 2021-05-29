using Bolnica.Model.Korisnici;
using Bolnica.Model.Pregledi;
using Bolnica.ViewModel;
using Model.Korisnici;
using Model.Pregledi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Bolnica.Forms
{
    /// <summary>
    /// Interaction logic for FormNapraviAnamnezuLekar.xaml
    /// </summary>
    public partial class FormNapraviAnamnezuLekar : Window
    {
        public FormNapraviAnamnezuLekar(AnamnezaLekarViewModel viewModel)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            Owner = Application.Current.MainWindow;
            this.DataContext = viewModel;
            if (viewModel.ZatvoriAction == null)
                viewModel.ZatvoriAction = new Action(this.Close);
            viewModel.ScrollBar = ScroolBar;
            viewModel.izbrisiButton = IzbrisiButton;
            viewModel.dataGridLekovi = dataGridLekovi;
            this.Show();
        }
    }
}
