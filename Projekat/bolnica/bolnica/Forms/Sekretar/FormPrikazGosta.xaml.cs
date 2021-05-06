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

namespace Bolnica.Forms.Sekretar
{
    /// <summary>
    /// Interaction logic for FormPrikazGosta.xaml
    /// </summary>
    public partial class FormPrikazGosta : Window
    {
        public FormPrikazGosta()
        {
            InitializeComponent();
        }

        private void PrikaziAlergene(object sender, RoutedEventArgs e)
        {
            FormAlergeniPrikaz s = new FormAlergeniPrikaz(lblJMBG);
            s.ShowDialog();
        }

        private void Zatvori(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
