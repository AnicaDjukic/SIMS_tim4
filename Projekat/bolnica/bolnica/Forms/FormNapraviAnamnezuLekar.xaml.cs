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
    /// Interaction logic for FormNapraviAnamnezuLekar.xaml
    /// </summary>
    public partial class FormNapraviAnamnezuLekar : Window
    {
        private string glavneTegobe { get; set; }
        private string sadasnjaBolest { get; set; }
        private string anamnezaSistema { get; set; }
        private string licnaAnamneza { get; set; }

        private string porodicnaAnamneza { get; set; }
        private string socijalnoEpidem { get; set; }


        public FormNapraviAnamnezuLekar()
        {
            InitializeComponent();
        }

        private void OtkaziDugme(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
