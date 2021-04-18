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
    /// Interaction logic for FormObavestenja.xaml
    /// </summary>
    public partial class FormObavestenjaPacijent : Window
    {
        public static ObservableCollection<string> ObavestenjaZaPacijente
        {
            get;
            set;
        }

        public FormObavestenjaPacijent()
        {
            InitializeComponent();

            this.DataContext = this;
        }
    }
}
