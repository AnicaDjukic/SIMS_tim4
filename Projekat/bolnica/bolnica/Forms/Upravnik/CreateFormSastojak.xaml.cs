using Bolnica.Model.Pregledi;
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

namespace Bolnica.Forms.Upravnik
{
    /// <summary>
    /// Interaction logic for CreateFormSastojak.xaml
    /// </summary>
    public partial class CreateFormSastojak : Window
    {
        public CreateFormSastojak()
        {
            InitializeComponent();
        }

        private void Button_Click_Potvrdi(object sender, RoutedEventArgs e)
        {
            if(txtNaziv.Text != "")
            {
                FileStorageSastojak storage = new FileStorageSastojak();
                List<Sastojak> sastojci = storage.GetAll();
                int maxId = 0;
                bool postoji = false;
                foreach (Sastojak s in sastojci)
                {
                    if (s.Id > maxId)
                        maxId = s.Id;

                    if (s.Naziv == txtNaziv.Text)
                        postoji = true;
                }
                if(!postoji)
                {
                    Sastojak noviSastojak = new Sastojak { Id = maxId + 1, Naziv = txtNaziv.Text };
                    storage.Save(noviSastojak);
                    FormSastojci.Sastojci.Add(noviSastojak);
                    Close();
                } else
                {
                    MessageBox.Show("Sastojak već postoji!");
                }
            }
            else
            {
                MessageBox.Show("Morate uneti naziv sastojka da biste ga dodali.");
            }
        }

        private void Button_Click_Odustani(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
