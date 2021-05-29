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
        private FileRepositoryPacijent storagePacijenti;
        private FileRepositoryLekar storageLekari;
        private FileRepositoryKorisnik storageKorisnici;
        private List<Pacijent> pacijenti;
        private List<Korisnik> korisnici;
        private List<Korisnik> korisniciIzStorage;
        private List<Lekar> listaLekara;
        public FormDodajObavestenje(int id, List<Korisnik> korisnici)
        {
            InitializeComponent();
            this.id = id;
            dpDatum.SelectedDate = DateTime.Now;
            dpDatum.IsEnabled = false;
            storagePacijenti = new FileRepositoryPacijent();
            pacijenti = storagePacijenti.GetAll();
            storageKorisnici = new FileRepositoryKorisnik();
            korisniciIzStorage = storageKorisnici.GetAll();
            this.korisnici = korisnici;
            listaLekara = new List<Lekar>();
            storageLekari = new FileRepositoryLekar();
            listaLekara = storageLekari.GetAll();

            FormDodajPrimaoce.DodatiPrimaoci = null;
            FormDodajPrimaoce.SviPrimaoci = null;
            FormDodajPrimaocePacijente.DodatiPrimaoci = null;
            FormDodajPrimaocePacijente.SviPrimaoci = null;

            for (int i = 0; i < FormObavestenja.Obavestenja.Count; i++)
                if (id == FormObavestenja.Obavestenja[i].Id)
                {
                    List<string> korImena = new List<string>();
                    foreach (Korisnik k in FormObavestenja.Obavestenja[i].Korisnici)
                        korImena.Add(k.KorisnickoIme);

                    bool svi = true;
                    foreach (Pacijent p in pacijenti)
                    {
                        if (!p.Guest)
                        {
                            if (!korImena.Contains(p.KorisnickoIme))
                                svi = false;
                        }
                    }

                    if (svi)
                        btnDodajPacijente.IsEnabled = false;
                }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FileRepositoryObavestenje storage = new FileRepositoryObavestenje();
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
            obavestenje.Korisnici = new List<Korisnik>();

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
                    obavestenje.Korisnici = korisnici;

                    List<string> korImena = new List<string>();
                    List<string> korImena2 = new List<string>();
                    foreach (Korisnik k in obavestenje.Korisnici)
                        korImena.Add(k.KorisnickoIme);

                    if (korImena.Contains("mico")) 
                    {
                        foreach (Lekar l in listaLekara)
                            if (!korImena.Contains(l.KorisnickoIme))
                                korImena2.Add(l.KorisnickoIme);

                        foreach (Korisnik k in korisniciIzStorage)
                            if (korImena2.Contains(k.KorisnickoIme))
                                obavestenje.Korisnici.Add(k);
                    }
                }
                else if (FormDodajPrimaoce.DodatiPrimaoci == null && FormDodajPrimaocePacijente.DodatiPrimaoci != null)
                {
                    List<string> korImena = new List<string>();
                    List<string> korImena2 = new List<string>();
                    foreach (Korisnik k in korisnici)
                        korImena.Add(k.KorisnickoIme);

                    if (korImena.Contains("upravnik"))
                        foreach(Korisnik k in korisniciIzStorage)
                            if(k.KorisnickoIme.Equals("upravnik"))
                                obavestenje.Korisnici.Add(k);

                    if (korImena.Contains("mico"))
                    {
                        foreach(Korisnik k in korisniciIzStorage)
                            if(k.KorisnickoIme.Equals("mico"))
                                obavestenje.Korisnici.Add(k);

                        foreach (Lekar l in listaLekara)
                        {
                            if (l.KorisnickoIme.Equals("mico"))
                                continue;
                            korImena2.Add(l.KorisnickoIme);
                        }

                        foreach (Korisnik k in korisniciIzStorage)
                            if (korImena2.Contains(k.KorisnickoIme))
                                obavestenje.Korisnici.Add(k);
                    }

                    foreach (Pacijent p in pacijenti)
                    {
                        if (!p.Guest)
                        {
                            if (!korImena.Contains(p.KorisnickoIme))
                                svi = false;
                        }
                    }

                    if (svi)
                        foreach (string s in korImena)
                            if (!s.Equals("upravnik") && !s.Equals("mico"))
                            {
                                bool lekar = false;
                                foreach (Lekar l in listaLekara)
                                    if (s.Equals(l.KorisnickoIme))
                                        lekar = true;
                                if (!lekar)
                                {   
                                    foreach(Korisnik k in korisniciIzStorage)
                                        if(k.KorisnickoIme.Equals(s))
                                            obavestenje.Korisnici.Add(k);
                                }
                            }
                }
                else if (FormDodajPrimaoce.DodatiPrimaoci != null && FormDodajPrimaocePacijente.DodatiPrimaoci == null)
                {
                    List<string> korImena = new List<string>();
                    foreach (Korisnik k in korisnici)
                        korImena.Add(k.KorisnickoIme);

                    foreach (Pacijent p in pacijenti)
                    {
                        if (!p.Guest)
                        {
                            if (!korImena.Contains(p.KorisnickoIme))
                                svi = false;
                        }
                    }

                    if (!svi)
                        foreach (string s in korImena)
                            if (!s.Equals("upravnik") && !s.Equals("mico"))
                            {
                                bool lekar = false;
                                foreach (Lekar l in listaLekara)
                                    if (s.Equals(l.KorisnickoIme))
                                        lekar = true;
                                if (!lekar)
                                {
                                    foreach (Korisnik k in korisniciIzStorage)
                                        if (k.KorisnickoIme.Equals(s))
                                            obavestenje.Korisnici.Add(k);
                                }
                            }
                }
            }

            bool dodat = false;
            if(FormDodajPrimaoce.DodatiPrimaoci != null && FormDodajPrimaoce.DodatiPrimaoci.Count != 0)
                foreach (Primalac p in FormDodajPrimaoce.DodatiPrimaoci) 
                {
                    List<string> korImena = new List<string>();

                    if (p.Naziv.Equals("Upravnik"))
                    {
                        foreach(Korisnik k in korisniciIzStorage)
                            if(k.KorisnickoIme.Equals("upravnik"))
                                obavestenje.Korisnici.Add(k);
                    }

                    if (p.Naziv.Equals("Svi lekari"))
                    {
                        foreach (Lekar l in listaLekara)
                            korImena.Add(l.KorisnickoIme);

                        foreach(Korisnik k in korisniciIzStorage)
                            if(korImena.Contains(k.KorisnickoIme))
                                obavestenje.Korisnici.Add(k);
                    }

                    List<string> korImena2 = new List<string>();
                    foreach (Korisnik k in obavestenje.Korisnici)
                        korImena2.Add(k.KorisnickoIme);

                    if (p.Naziv.Equals("Svi pacijenti"))
                    {
                        dodat = true;
                        foreach (Pacijent pac in pacijenti)
                            if (!pac.Guest)
                                if (!korImena2.Contains(pac.KorisnickoIme))
                                {   
                                    foreach(Korisnik k in korisniciIzStorage)
                                        if(k.KorisnickoIme.Equals(pac.KorisnickoIme))
                                            obavestenje.Korisnici.Add(k);
                                }
                    }
                }

            if (!dodat)
                if (FormDodajPrimaocePacijente.DodatiPrimaoci != null && FormDodajPrimaocePacijente.DodatiPrimaoci.Count != 0)
                    foreach (Pacijent p in FormDodajPrimaocePacijente.DodatiPrimaoci)
                    {
                        foreach(Korisnik k in korisniciIzStorage)
                            if(k.KorisnickoIme.Equals(p.KorisnickoIme))
                                obavestenje.Korisnici.Add(k);
                    }

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
                s.btnUkloni.IsEnabled = false;
            }
            else
            {
                if (FormDodajPrimaoce.SviPrimaoci.Count != 0)
                    s.btnDodaj.IsEnabled = true;
                else
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
                s.btnUkloni.IsEnabled = false;
            }
            else
            {
                if (FormDodajPrimaocePacijente.SviPrimaoci.Count != 0)
                    s.btnDodaj.IsEnabled = true;
                else
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
