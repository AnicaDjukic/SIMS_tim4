using Bolnica.Model.Prostorije;
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
    /// Interaction logic for FormDatumPremestanja.xaml
    /// </summary>
    public partial class FormDatumPremestanja : Window
    {
        public FormDatumPremestanja()
        {
            InitializeComponent();
        }

        private void Button_Click_Potvrdi(object sender, RoutedEventArgs e)
        {
            FileStorageBuducaZaliha storage = new FileStorageBuducaZaliha();
            List<BuducaZaliha> buduceZalihe = new List<BuducaZaliha>();
            string sifraOpreme = "";
            foreach (Zaliha z in FormSkladiste.Zalihe)
            {
                BuducaZaliha bz = new BuducaZaliha { Kolicina = z.Kolicina, BrojProstorije = z.BrojProstorije, SifraOpreme = z.SifraOpreme, Datum = datePickerDatum.SelectedDate.Value };
                sifraOpreme = bz.SifraOpreme;
                buduceZalihe.Add(bz);
            }
            if (storage.GetAll() != null)
            {
                foreach (BuducaZaliha bz in storage.GetAll())
                {
                    if (bz.SifraOpreme == sifraOpreme && bz.Datum == datePickerDatum.SelectedDate.Value)
                        storage.Delete(bz);
                }
            }
            foreach (BuducaZaliha bz in buduceZalihe)
                storage.Save(bz);

            Close();
        }

        private void Button_Click_Odustani(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
