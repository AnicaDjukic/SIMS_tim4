using Bolnica.Forms.Sekretar;
using Bolnica.Model.Korisnici;
using Model.Korisnici;
using Model.Pacijenti;
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
        private FileStoragePacijenti storagePacijenti;
        private FileStorageLekar storageLekari;
        private List<Pacijent> pacijenti;
        private List<string> korisnickaImena;
        private List<Lekar> listaLekara;
        public FormDodajObavestenje(int id, List<string> korisnickaImena)
        {
            InitializeComponent();
            this.id = id;
            dpDatum.SelectedDate = DateTime.Now;
            dpDatum.IsEnabled = false;
            storagePacijenti = new FileStoragePacijenti();
            pacijenti = storagePacijenti.GetAll();
            this.korisnickaImena = korisnickaImena;
            listaLekara = new List<Lekar>();
            storageLekari = new FileStorageLekar();
            listaLekara = storageLekari.GetAll();

            FormDodajPrimaoce.DodatiPrimaoci = null;
            FormDodajPrimaoce.SviPrimaoci = null;
            FormDodajPrimaocePacijente.DodatiPrimaoci = null;
            FormDodajPrimaocePacijente.SviPrimaoci = null;

            for (int i = 0; i < FormObavestenja.Obavestenja.Count; i++)
                if (id == FormObavestenja.Obavestenja[i].Id)
                {
                    bool svi = true;
                    foreach (Pacijent p in pacijenti)
                    {
                        if (!p.Guest)
                        {
                            if (!FormObavestenja.Obavestenja[i].KorisnickaImena.Contains(p.KorisnickoIme))
                                svi = false;
                        }
                    }

                    if (svi)
                        btnDodajPacijente.IsEnabled = false;
                }
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
            obavestenje.KorisnickaImena = new List<string>();

            if(FormObavestenja.clickedDodaj)
                if ((FormDodajPrimaoce.DodatiPrimaoci == null || FormDodajPrimaoce.DodatiPrimaoci.Count == 0) && (FormDodajPrimaocePacijente.DodatiPrimaoci == null || FormDodajPrimaocePacijente.DodatiPrimaoci.Count == 0))
                {
                    MessageBox.Show("Nisu uneti primaoci obaveštenja", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

            if (!FormObavestenja.clickedDodaj)
            {
                bool svi = true;
                if (FormDodajPrimaoce.DodatiPrimaoci == null && FormDodajPrimaocePacijente.DodatiPrimaoci == null)
                {
                    obavestenje.KorisnickaImena = korisnickaImena;
                    if (obavestenje.KorisnickaImena.Contains("lekar")) 
                    {
                        foreach (Lekar l in listaLekara)
                            if (!obavestenje.KorisnickaImena.Contains(l.KorisnickoIme))
                                obavestenje.KorisnickaImena.Add(l.KorisnickoIme);
                    }
                }
                else if (FormDodajPrimaoce.DodatiPrimaoci == null && FormDodajPrimaocePacijente.DodatiPrimaoci != null)
                {
                    if (korisnickaImena.Contains("upravnik"))
                        obavestenje.KorisnickaImena.Add("upravnik");

                    if (korisnickaImena.Contains("lekar"))
                    {
                        obavestenje.KorisnickaImena.Add("lekar");
                        foreach (Lekar l in listaLekara)
                            obavestenje.KorisnickaImena.Add(l.KorisnickoIme);
                    }

                    foreach (Pacijent p in pacijenti)
                    {
                        if (!p.Guest)
                        {
                            if (!korisnickaImena.Contains(p.KorisnickoIme))
                                svi = false;
                        }
                    }

                    if (svi)
                        foreach (string s in korisnickaImena)
                            if (!s.Equals("upravnik") && !s.Equals("lekar"))
                            {
                                bool lekar = false;
                                foreach (Lekar l in listaLekara)
                                    if (s.Equals(l.KorisnickoIme))
                                        lekar = true;
                                if (!lekar)
                                    obavestenje.KorisnickaImena.Add(s);
                            }
                }
                else if (FormDodajPrimaoce.DodatiPrimaoci != null && FormDodajPrimaocePacijente.DodatiPrimaoci == null)
                {
                    foreach (Pacijent p in pacijenti)
                    {
                        if (!p.Guest)
                        {
                            if (!korisnickaImena.Contains(p.KorisnickoIme))
                                svi = false;
                        }
                    }

                    if (!svi)
                        foreach (string s in korisnickaImena)
                            if (!s.Equals("upravnik") && !s.Equals("lekar"))
                            {
                                bool lekar = false;
                                foreach (Lekar l in listaLekara)
                                    if (s.Equals(l.KorisnickoIme))
                                        lekar = true;
                                if (!lekar)
                                    obavestenje.KorisnickaImena.Add(s);
                            }
                }
            }

            bool dodat = false;
            if(FormDodajPrimaoce.DodatiPrimaoci != null && FormDodajPrimaoce.DodatiPrimaoci.Count != 0)
                foreach (Primalac p in FormDodajPrimaoce.DodatiPrimaoci) 
                {
                    if (p.Naziv.Equals("Upravnik"))
                    {
                        obavestenje.KorisnickaImena.Add("upravnik");
                    }

                    if (p.Naziv.Equals("Svi lekari"))
                    {
                        obavestenje.KorisnickaImena.Add("lekar");
                        foreach (Lekar l in listaLekara)
                            obavestenje.KorisnickaImena.Add(l.KorisnickoIme);
                    }

                    if (p.Naziv.Equals("Svi pacijenti"))
                    {
                        dodat = true;
                        foreach (Pacijent pac in pacijenti)
                            if (!pac.Guest)
                                if(!obavestenje.KorisnickaImena.Contains(pac.KorisnickoIme))
                                    obavestenje.KorisnickaImena.Add(pac.KorisnickoIme);
                    }
                }

            if (!dodat)
                if (FormDodajPrimaocePacijente.DodatiPrimaoci != null && FormDodajPrimaocePacijente.DodatiPrimaoci.Count != 0)
                    foreach (Pacijent p in FormDodajPrimaocePacijente.DodatiPrimaoci)
                        obavestenje.KorisnickaImena.Add(p.KorisnickoIme);

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

        private void Button_Click_Dodaj_Primaoce(object sender, RoutedEventArgs e)
        {
            var s = new FormDodajPrimaoce(btnDodajPacijente, id);
            if (FormObavestenja.clickedDodaj)
            {
                s.ti1.IsSelected = true;
                s.btnUkloni.IsEnabled = false;
            }
            else
            {
                s.ti2.IsSelected = true;
                s.btnDodaj.IsEnabled = false;
                if (FormDodajPrimaoce.DodatiPrimaoci.Count != 0)
                    s.btnUkloni.IsEnabled = true;
                else
                    s.btnUkloni.IsEnabled = false;
            }
            s.ShowDialog();
        }

        private void Button_Click_Dodaj_Pacijente(object sender, RoutedEventArgs e)
        {
            var s = new FormDodajPrimaocePacijente(id);
            if (FormObavestenja.clickedDodaj)
            {
                s.ti1.IsSelected = true;
                s.btnUkloni.IsEnabled = false;
            }
            else
            {
                s.ti2.IsSelected = true;
                s.btnDodaj.IsEnabled = false;
                if (FormDodajPrimaocePacijente.DodatiPrimaoci.Count != 0)
                    s.btnUkloni.IsEnabled = true;
                else
                    s.btnUkloni.IsEnabled = false;
            }
            s.ShowDialog();
        }
    }
}
