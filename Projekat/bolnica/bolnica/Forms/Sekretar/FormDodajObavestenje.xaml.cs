using Bolnica.Model.Korisnici;
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

namespace Bolnica.Sekretar
{
    /// <summary>
    /// Interaction logic for FormDodajObavestenje.xaml
    /// </summary>
    public partial class FormDodajObavestenje : Window
    {
        private int id;
        public FormDodajObavestenje(int id)
        {
            InitializeComponent();
            this.id = id;
            dpDatum.SelectedDate = DateTime.Now;
            dpDatum.IsEnabled = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FileStorageObavestenja storage = new FileStorageObavestenja();
            List<Obavestenje> obavestenja = storage.GetAll();

            String naslov = txtNaslov.Text;
            String text = txtText.Text;
            DateTime datumKreiranja;
            try
            {
                datumKreiranja = dpDatum.SelectedDate.Value.Date;
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Nije unet datum kreiranja", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                dpDatum.Focusable = true;
                Keyboard.Focus(dpDatum);
                return;
            }

            if (datumKreiranja.Year > 2021 || datumKreiranja.Year < 2000)
            {
                MessageBox.Show("Neispravna godina kreiranja obaveštenja", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                dpDatum.Focusable = true;
                Keyboard.Focus(dpDatum);
                return;
            }

            int max = 0;
            foreach (Obavestenje o in obavestenja)
                if (o.Id > max)
                    max = o.Id;

            Obavestenje obavestenje = new Obavestenje();
            if (FormObavestenja.clickedDodaj)
                obavestenje.Id = max + 1;
            else
                obavestenje.Id = id;
            obavestenje.Datum = datumKreiranja;
            obavestenje.Naslov = naslov;
            obavestenje.Sadrzaj = text;
            obavestenje.Obrisan = false;

            if (obavestenje.Naslov == "")
            {
                MessageBox.Show("Nije unet naslov", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                txtNaslov.Focusable = true;
                Keyboard.Focus(txtNaslov);
                return;
            }

            if (obavestenje.Sadrzaj == "")
            {
                MessageBox.Show("Nije unet sadržaj", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                txtText.Focusable = true;
                Keyboard.Focus(txtText);
                return;
            }

            if (FormObavestenja.clickedDodaj)
            {
                storage.Save(obavestenje);
                FormObavestenja.Obavestenja.Add(obavestenje);
                FormObavestenja.clickedDodaj = false;
                this.Close();
            }
            else
            {
                foreach (Obavestenje o in obavestenja)
                {
                    if (String.Equals(o.Id, obavestenje.Id))
                    {
                        storage.Delete(o);

                        for (int i = 0; i < FormObavestenja.Obavestenja.Count; i++)
                        {
                            if (FormObavestenja.Obavestenja[i].Id == obavestenje.Id)
                            {
                                FormObavestenja.Obavestenja.Remove(FormObavestenja.Obavestenja[i]);
                                break;
                            }
                        }

                        storage.Save(obavestenje);
                        FormObavestenja.Obavestenja.Add(obavestenje);

                        this.Close();
                        break;
                    }
                }
            }
        }
    }
}
