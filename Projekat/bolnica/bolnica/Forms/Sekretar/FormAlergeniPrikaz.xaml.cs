using Bolnica.Model.Pregledi;
using Bolnica.Repository.Pregledi;
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
        private FileRepositorySastojak storage;
        public FormAlergeniPrikaz(Label lblJMBG)
        {
            InitializeComponent();
            dataGridAlergeni.DataContext = this;
            Alergeni = new ObservableCollection<Sastojak>();
            storage = new FileRepositorySastojak();
            List<Sastojak> alergeni = storage.GetAll();

            for (int i = 0; i < FormSekretar.RedovniPacijenti.Count; i++)
            { 
                if (String.Equals(lblJMBG.Content, FormSekretar.RedovniPacijenti[i].Jmbg))
                {
                    if (FormSekretar.RedovniPacijenti[i].Alergeni != null)
                        foreach (Sastojak s in FormSekretar.RedovniPacijenti[i].Alergeni)
                        {
                            foreach (Sastojak sas in alergeni)
                                if (sas.Id == s.Id)
                                {
                                    Alergeni.Add(sas);
                                }
                        }
                    break;
                }
            }

            for (int i = 0; i < FormSekretar.GostiPacijenti.Count; i++)
            {
                if (String.Equals(lblJMBG.Content, FormSekretar.GostiPacijenti[i].Jmbg))
                {
                    if (FormSekretar.GostiPacijenti[i].Alergeni != null)
                        foreach (Sastojak s in FormSekretar.GostiPacijenti[i].Alergeni)
                        {
                            foreach (Sastojak sas in alergeni)
                                if (sas.Id == s.Id)
                                {
                                    Alergeni.Add(sas);
                                }
                        }
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
