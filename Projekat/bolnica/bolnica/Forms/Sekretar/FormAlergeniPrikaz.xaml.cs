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

namespace Bolnica.Forms.Sekretar
{
    /// <summary>
    /// Interaction logic for FormAlergeniPrikaz.xaml
    /// </summary>
    public partial class FormAlergeniPrikaz : Window
    {
        public static ObservableCollection<Sastojak> Alergeni { get; set; }
        private FileStorageSastojak storage;
        public FormAlergeniPrikaz(Label lblJMBG)
        {
            InitializeComponent();
            dataGridAlergeni.DataContext = this;
            Alergeni = new ObservableCollection<Sastojak>();
            storage = new FileStorageSastojak();
            List<Sastojak> alergeni = storage.GetAll();

            for (int i = 0; i < FormSekretar.Pacijenti.Count; i++)
            { 
                if (String.Equals(lblJMBG.Content, FormSekretar.Pacijenti[i].Jmbg))
                {
                    if (FormSekretar.Pacijenti[i].Alergeni != null)
                        foreach (Sastojak s in FormSekretar.Pacijenti[i].Alergeni)
                            Alergeni.Add(s);
                    break;
                }
            }
        }

        private void Button_Clicked_Zatvori(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
