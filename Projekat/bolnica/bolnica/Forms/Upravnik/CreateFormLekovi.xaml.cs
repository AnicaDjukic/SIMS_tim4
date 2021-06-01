using bolnica.Forms;
using Bolnica.Controller.Pregledi;
using Bolnica.Model.Korisnici;
using Bolnica.Model.Pregledi;
using Bolnica.Repository.Korisnici;
using Bolnica.Services.Korisnici;
using Bolnica.Services.Pregledi;
using Bolnica.ViewModel.Upravnik;
using Model.Korisnici;
using Model.Pregledi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
            DataContext = viewModel;
            if (viewModel.ZatvoriAkcija == null)
                viewModel.ZatvoriAkcija = new Action(this.Close);
            Show();
        }
    }
}
