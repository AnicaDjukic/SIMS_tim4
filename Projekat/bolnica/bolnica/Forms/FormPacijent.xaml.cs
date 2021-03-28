using Model.Korisnici;
using Model.Pregledi;
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
using Model.Pacijenti;

namespace Bolnica.Forms
{
    /// <summary>
    /// Interaction logic for FormPacijent.xaml
    /// </summary>
    public partial class FormPacijent : Window
    {
        private Pacijent trenutniPacijent = new Pacijent();

        public static ObservableCollection<Pregled> Pregledi
        {
            get;
            set;
        }

        private FileStoragePregledi storage;

        public FormPacijent(Pacijent pacijent)
        {
            InitializeComponent();

            this.DataContext = this;

            trenutniPacijent = pacijent;

            Pregledi = new ObservableCollection<Pregled>();
            storage = new FileStoragePregledi();

            List<Pregled> pregledi = storage.GetAllPregledi();
            foreach (Pregled p in pregledi)
            {
                if (p.Pacijent.Guest == false)
                {
                    if (p.Zavrsen == false && p.Pacijent.KorisnickoIme.Equals(pacijent.KorisnickoIme))
                        Pregledi.Add(p);
                }
            }
            List<Operacija> operacije = storage.GetAllOperacije();
            foreach (Operacija o in operacije)
            {
                if (o.Pacijent.Guest == false)
                {
                    if (o.Zavrsen == false && o.Pacijent.KorisnickoIme.Equals(pacijent.KorisnickoIme))
                        Pregledi.Add(o);
                }
            }
        }

        private void ZakaziPregled(object sender, RoutedEventArgs e)
        {
            var s = new FormNapraviTerminPacijent(trenutniPacijent);
            s.Show();
        }

        private void OtkaziPregled(object sender, RoutedEventArgs e)
        {
            var objekat = pacijentGrid.SelectedValue;
            
            if (objekat != null)
            {
                Operacija o = new Operacija();

                if (objekat.GetType().Equals(o.GetType()))
                {
                    for (int i = 0; i < Pregledi.Count; i++)
                    {
                        if (objekat.Equals(Pregledi[i]))
                        {
                            Pregledi.RemoveAt(i);
                            o = (Operacija)objekat;
                            FileStoragePregledi storage = new FileStoragePregledi();
                            storage.Delete(o);
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < Pregledi.Count; i++)
                    {
                        if (objekat.Equals(Pregledi[i]))
                        {
                            Pregledi.RemoveAt(i);
                            Pregled p = (Pregled)objekat;
                            FileStoragePregledi storage = new FileStoragePregledi();
                            storage.Delete(p);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Morate odabrati pregled ili operaciju koju zelite da otkazete!", "Upozorenje");
            }
        }

        private void IzmeniPregled(object sender, RoutedEventArgs e)
        {
            var objekat = pacijentGrid.SelectedValue;

            if (objekat != null)
            {
                Operacija o = new Operacija();

                if (objekat.GetType().Equals(o.GetType()))
                {
                    MessageBox.Show("Ne mozete da izmenite operaciju!");
                }
                else
                {
                    Pregled p = (Pregled)pacijentGrid.SelectedItem;
                    var s = new FormIzmeniTerminPacijent(p);
                    s.Show();
                }
            }
            else
            {
                MessageBox.Show("Morate odabrati pregled koju zelite da izmenite!", "Upozorenje");
            }
        }

        private void IstorijaPregleda(object sender, RoutedEventArgs e)
        {
            var s = new FormIstorijaPregledaPacijent(trenutniPacijent);
            s.Show();
        }
    }
}
