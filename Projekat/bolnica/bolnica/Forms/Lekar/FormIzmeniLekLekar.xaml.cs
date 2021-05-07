using Bolnica.Model.Pregledi;
using Model.Pregledi;
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
    /// Interaction logic for FormIzmeniLekLekar.xaml
    /// </summary>
    
    public partial class FormIzmeniLekLekar : Window
    {
        private Lek l = new Lek();

        public string proizvodjac { get; set; }
        public string lek { get; set; }

        public string doza { get; set; }

        public string stariLek = "";

        public string stariProizvodjac = "";
        public List<String> sastojci { get; set; }

        public List<Lek> zamene { get; set; }

        private FileStorageSastojak sviSastojci = new FileStorageSastojak();
        private FileStorageLek sviLekovi = new FileStorageLek();

        private List<Lek> lekovi = new List<Lek>();
        private List<Sastojak> sas = new List<Sastojak>();

        public FormIzmeniLekLekar(Lek p)
        {
            lekovi = sviLekovi.GetAll();
            for (int i = 0; i < lekovi.Count; i++)
            {
                if (lekovi[i].Status.Equals(StatusLeka.odbijen) || lekovi[i].Obrisan)
                {
                    lekovi.RemoveAt(i);
                    i--;
                }
            }
            l = p;
            sastojci = new List<String>();
            InitializeComponent();
            this.DataContext = this;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            Owner = Application.Current.MainWindow;

            for (int i = 0; i < lekovi.Count; i++)
            {
                if (!textLek.Items.Contains(lekovi[i].Naziv))
                {
                    textLek.Items.Add(lekovi[i].Naziv);
                }
                if (!textProizvodjac.Items.Contains(lekovi[i].Proizvodjac))
                {
                    textProizvodjac.Items.Add(lekovi[i].Proizvodjac);
                }
            }
            for (int i = 0; i < lekovi.Count; i++)
            {
                if (lekovi[i].Naziv.Equals(l.Naziv)&& !textDoza.Items.Contains(lekovi[i].KolicinaUMg))
                {
                    textDoza.Items.Add(lekovi[i].KolicinaUMg);
                }
            }
            proizvodjac = l.Proizvodjac;
            lek = l.Naziv;
            doza = l.KolicinaUMg.ToString();
            stariLek = lek;
            stariProizvodjac = proizvodjac;
            
            sas = sviSastojci.GetAll();

            for(int i = 0; i < sas.Count; i++)
            {
                int dozvola = 0;
                for(int m=0; m < l.Sastojak.Count; m++)
                {
                    if (l.Sastojak[m].Naziv.Equals(sas[i].Naziv))
                    {
                        dozvola = 1;
                    }
                }
                textSastojci.Items.Add(sas[i].Naziv);
                if (dozvola==1)
                {
                    textSastojci.SelectedItems.Add(sas[i].Naziv);
                }
                
                    
                
            }
            for (int i = 0; i < lekovi.Count; i++)
            {
                int dozvola = 0;
                for (int m = 0; m < l.IdZamena.Count; m++)
                {
                    Lek novi = new Lek();
                    for(int mo = 0; mo < lekovi.Count; mo++)
                    {
                        if (l.IdZamena[m].Equals(lekovi[mo].Id))
                        {
                            novi = lekovi[mo];
                        }
                    }
                    if (novi.Naziv.Equals(lekovi[i].Naziv)&&novi.Proizvodjac.Equals(lekovi[i].Proizvodjac)&&novi.KolicinaUMg.Equals(lekovi[i].KolicinaUMg))
                    {
                        dozvola = 1;
                    }
                }
                string k = lekovi[i].Proizvodjac + ", " + lekovi[i].Naziv + ", " + lekovi[i].KolicinaUMg;


                textZamene.Items.Add(k);
                if (dozvola == 1)
                {
                    textZamene.SelectedItems.Add(k);
                }



            }






        }



        private void isEnterDoza(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                textDoza.IsDropDownOpen = true;
            }
         }

        private void isTab(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                textLek.IsDropDownOpen = true;
            }
            if (e.Key == Key.Tab)

            {
                if (textLek.Text.Length > 2)
                {
                    if (!stariLek.Equals(textLek.Text)) {
                        stariLek = lek;
                    textDoza.Items.Clear();
                    for (int i = 0; i < lekovi.Count; i++)
                    {
                        if (textLek.Text.Equals(lekovi[i].Naziv)&& !textDoza.Items.Contains(lekovi[i].KolicinaUMg))
                        {
                            textDoza.Items.Add(lekovi[i].KolicinaUMg);
                        }
                    }
                }
                }

            }
        }

        private void Select(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                for(int i = 0; i<textSastojci.Items.Count; i++)
                {
                    ListBoxItem item = textSastojci.ItemContainerGenerator.ContainerFromIndex(i) as ListBoxItem;
                    if (item.IsFocused.Equals(true))
                    {
                        if (item.IsSelected.Equals(true))
                        {
                            item.IsSelected = false;
                        }
                        else
                        {
                            item.IsSelected = true;
                        }
                       
                    }
                }
                

                
            }
        }

        private void SelectZamena(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                for (int i = 0; i < textZamene.Items.Count; i++)
                {
                    ListBoxItem item = textZamene.ItemContainerGenerator.ContainerFromIndex(i) as ListBoxItem;
                    if (item.IsFocused.Equals(true))
                    {
                        if (item.IsSelected.Equals(true))
                        {
                            item.IsSelected = false;
                        }
                        else
                        {
                            item.IsSelected = true;
                        }

                    }
                }



            }
        }

        private void Potvrdi(object sender, RoutedEventArgs e)
        {
            Potvrdi();

        }

        private void Odustani(object sender, RoutedEventArgs e)
        {
            Odustani();
        }

        private void IsPro(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                if (textProizvodjac.Text.Length > 2)
                {
                    if (!stariProizvodjac.Equals(textProizvodjac.Text))
                    {
                        stariProizvodjac = proizvodjac;
                        textLek.Items.Clear();
                        for (int i = 0; i < lekovi.Count; i++)
                        {
                            if (textProizvodjac.Text.Equals(lekovi[i].Proizvodjac) && !textLek.Items.Contains(lekovi[i].Naziv))
                            {
                                textLek.Items.Add(lekovi[i].Naziv);
                            }
                        }
                    }
                }
            }
            else if (e.Key == Key.Enter)
            {
                textProizvodjac.IsDropDownOpen = true;
            }
        }

        public void Potvrdi()
        {
            Lek lekk = new Lek();
            lekk.Id = l.Id;
            lekk.KolicinaUMg = int.Parse(doza);
            lekk.Naziv = lek;
            lekk.Proizvodjac = proizvodjac;
            lekk.Zalihe = l.Zalihe;
            lekk.Sastojak = new List<Sastojak>();
            lekk.IdZamena = new List<int>();
            lekk.Status = l.Status;
            PrikazLek lekpri = new PrikazLek();
            lekpri.Sastojak = "";
            int doz = 0;
            int dozz = 0;
            for (int i = 0; i < textSastojci.Items.Count; i++)
            {
                ListBoxItem item = textSastojci.ItemContainerGenerator.ContainerFromIndex(i) as ListBoxItem;
                if (item.IsSelected.Equals(true))
                {
                    for (int h = 0; h < sas.Count; h++)
                    {
                        if (sas[h].Naziv.Equals(item.Content as string))
                        {
                            lekk.Sastojak.Add(sas[h]);
                            if (doz == 0)
                            {
                                lekpri.Sastojak = lekpri.Sastojak + " " + sas[h].Naziv;
                                doz = 1;
                            }
                            else
                            {
                                lekpri.Sastojak = lekpri.Sastojak + "," + sas[h].Naziv;
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < textZamene.Items.Count; i++)
            {
                ListBoxItem item = textZamene.ItemContainerGenerator.ContainerFromIndex(i) as ListBoxItem;
                if (item.IsSelected.Equals(true))
                {
                    for (int h = 0; h < lekovi.Count; h++)
                    {
                        string k = lekovi[h].Proizvodjac + ", " + lekovi[h].Naziv + ", " + lekovi[h].KolicinaUMg;
                        if (k.Equals(item.Content as string))
                        {
                            lekk.IdZamena.Add(lekovi[h].Id);
                            if (dozz == 0)
                            {
                                lekpri.Zamena = lekpri.Zamena + " " + lekovi[h].Naziv;
                                dozz = 1;
                            }
                            else
                            {
                                lekpri.Zamena = lekpri.Zamena + "," + lekovi[h].Naziv;
                            }
                        }
                    }
                }
            }


            lekpri.Id = l.Id;
            lekpri.KolicinaUMg = int.Parse(doza);
            lekpri.Naziv = lek;
            lekpri.Zalihe = l.Zalihe;
            lekpri.Proizvodjac = proizvodjac;
            lekpri.Status = l.Status;

            for (int j = 0; j < FormLekar.lekoviPrikaz.Count; j++)
            {
                if (FormLekar.lekoviPrikaz[j].Id.Equals(lekpri.Id))
                {
                    FormLekar.lekoviPrikaz[j] = lekpri;
                }
            }
            sviLekovi.Delete(lekk);
            sviLekovi.Save(lekk);

            this.Close();
        }

        public void Odustani()
        {
            this.Close();
        }

        private void isAkcelerator(object sender, KeyEventArgs e)
        {
            if (e.KeyboardDevice.IsKeyDown(Key.LeftCtrl) || e.KeyboardDevice.IsKeyDown(Key.RightCtrl))
            {
                switch (e.Key)
                {
                    case Key.Q:
                        Potvrdi();
                        break;
                    case Key.W:
                        Odustani();
                        break;
                    

                }
            }
        }
    }
}
