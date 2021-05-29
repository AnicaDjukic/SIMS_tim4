using Bolnica.Model.Pregledi;
using Bolnica.Services.Pregledi;
using System.Windows;

namespace Bolnica.Forms.Upravnik
{
    /// <summary>
    /// Interaction logic for CreateFormSastojak.xaml
    /// </summary>
    public partial class CreateFormSastojak : Window
    {
        private ServiceSastojak serviceSastojak = new ServiceSastojak();
        public CreateFormSastojak()
        {
            InitializeComponent();
        }

        private void Button_Click_Potvrdi(object sender, RoutedEventArgs e)
        {
            if(txtNaziv.Text != "")
            {
                if(!serviceSastojak.SastojakPostoji(txtNaziv.Text))
                {
                    DodajNoviSastojak();
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

        private void DodajNoviSastojak()
        {
            Sastojak noviSastojak = new Sastojak { Id = serviceSastojak.MaxId() + 1, Naziv = txtNaziv.Text };
            serviceSastojak.SacuvajSastojak(noviSastojak);
            FormSastojci.Sastojci.Add(noviSastojak);
        }

        private void Button_Click_Odustani(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
