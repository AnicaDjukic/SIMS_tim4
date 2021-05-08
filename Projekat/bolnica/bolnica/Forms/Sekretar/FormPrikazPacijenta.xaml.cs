using Bolnica.Forms.Sekretar;
using Bolnica.Model.Pregledi;
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
using System.Windows.Shapes;

namespace Bolnica.Forms
{
    /// <summary>
    /// Interaction logic for FormPrikazPacijenta.xaml
    /// </summary>
    public partial class FormPrikazPacijenta : Window
    {
        public FormPrikazPacijenta()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void PrikaziAlergene(object sender, RoutedEventArgs e)
        {
            FormAlergeniPrikaz s = new FormAlergeniPrikaz(lblJMBG);
            s.ShowDialog();
        }
    }
}

