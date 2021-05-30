using Bolnica.DTO;
using Bolnica.Forms;
using Bolnica.Model.Korisnici;
using Bolnica.Model.Pacijenti;
using Bolnica.Repository.Pregledi;
using Bolnica.Sekretar;
using Model.Korisnici;
using Model.Pacijenti;
using Model.Pregledi;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;

namespace Bolnica.Services
{
    public class PacijentiService
    {
        private FileRepositoryPacijent skladistePacijenata = new FileRepositoryPacijent();
        private FileRepositoryZdravstveniKarton skladisteZdravstvenihKartona = new FileRepositoryZdravstveniKarton();
        private FileRepositoryKorisnik skladisteKorisnika = new FileRepositoryKorisnik();
        private FileRepositoryPregled skladistePregleda = new FileRepositoryPregled();
        private FileRepositoryOperacija skladisteOperacija = new FileRepositoryOperacija();

        public List<Pacijent> DobaviPacijente() 
        {
            return skladistePacijenata.GetAll();
        }

        public List<ZdravstveniKarton> DobaviZdravstveneKartone()
        {
            return skladisteZdravstvenihKartona.GetAll();
        }

        private List<Pregled> DobaviPreglede()
        {
            return skladistePregleda.GetAll();
        }

        private List<Operacija> DobaviOperacije()
        {
            return skladisteOperacija.GetAll();
        }

        public void ObrisiRedovnogPacijenta(PacijentDTO pacijentDTO)
        {
            Pacijent pacijent = pacijentDTO.Pacijent;
            Korisnik korisnik = new Korisnik() { KorisnickoIme = pacijent.KorisnickoIme, Lozinka = pacijent.Lozinka, TipKorisnika = TipKorisnika.pacijent };
            skladisteKorisnika.Delete(korisnik);
            List<Pregled> pregledi = DobaviPreglede();
            List<Operacija> operacije = DobaviOperacije();
            foreach (Pregled p in pregledi)
                if (p.Pacijent.Jmbg == pacijent.Jmbg)
                    skladistePregleda.Delete(p);
            foreach (Operacija o in operacije)
                if (o.Pacijent.Jmbg == pacijent.Jmbg)
                    skladisteOperacija.Delete(o);
            if (FormPregledi.Pregledi != null)
                for (int i = FormPregledi.Pregledi.Count - 1; i >= 0; i--)
                    if (FormPregledi.Pregledi[i].Pacijent.Jmbg == pacijent.Jmbg)
                        FormPregledi.Pregledi.RemoveAt(i);
            FormSekretar.RedovniPacijenti.Remove(pacijentDTO);
            FormSekretar.ObrisaniPacijenti.Add(pacijentDTO);
            skladistePacijenata.Delete(pacijent);
            pacijent.Obrisan = true;
            skladistePacijenata.Save(pacijent);
        }

        public void ObrisiGostPacijenta(PacijentDTO pacijentDTO)
        {
            Pacijent pacijent = pacijentDTO.Pacijent;
            List<Pregled> pregledi = DobaviPreglede();
            List<Operacija> operacije = DobaviOperacije();
            foreach (Pregled p in pregledi)
                if (p.Pacijent.Jmbg == pacijent.Jmbg)
                    skladistePregleda.Delete(p);
            foreach (Operacija o in operacije)
                if (o.Pacijent.Jmbg == pacijent.Jmbg)
                    skladisteOperacija.Delete(o);
            if (FormPregledi.Pregledi != null)
                for (int i = FormPregledi.Pregledi.Count - 1; i >= 0; i--)
                    if (FormPregledi.Pregledi[i].Pacijent.Jmbg == pacijent.Jmbg)
                        FormPregledi.Pregledi.RemoveAt(i);
            FormSekretar.GostiPacijenti.Remove(pacijentDTO);
            FormSekretar.ObrisaniPacijenti.Add(pacijentDTO);
            skladistePacijenata.Delete(pacijent);
            pacijent.Obrisan = true;
            skladistePacijenata.Save(pacijent);
        }

        public void OdblokirajPacijenta(PacijentDTO pacijentDTO) 
        {
            Pacijent pacijent = pacijentDTO.Pacijent;
            if (!pacijent.Guest)
            {
                Korisnik korisnik = new Korisnik() { KorisnickoIme = pacijent.KorisnickoIme, Lozinka = pacijent.Lozinka, TipKorisnika = TipKorisnika.pacijent };
                skladisteKorisnika.Save(korisnik);
            }

            FormSekretar.ObrisaniPacijenti.Remove(pacijentDTO);
            if (!pacijent.Guest)
                FormSekretar.RedovniPacijenti.Add(pacijentDTO);
            else
                FormSekretar.GostiPacijenti.Add(pacijentDTO);
            skladistePacijenata.Delete(pacijent);
            pacijent.Obrisan = false;
            skladistePacijenata.Save(pacijent);
        }

        public void DodajIliIzmeniRedovnogPacijenta(PacijentDTO pacijentDTO) 
        {
            Regex rgxBrojTelefona = new Regex(@"^\([0-9]{3}\)\s[0-9]{3}-[0-9]{3,4}$");
            bool isEmail = Regex.IsMatch(pacijentDTO.Pacijent.Email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            Regex rgxIDKartona = new Regex(@"^[1-9][0-9]*$");
            Regex rgxJmbg = new Regex(@"^[0-9]{13}$");
            List<Pacijent> pacijenti = skladistePacijenata.GetAll();

            if (DateTime.Compare(pacijentDTO.Pacijent.DatumRodjenja, DateTime.Now) > 0 || pacijentDTO.Pacijent.DatumRodjenja.Year < 1900)
            {
                MessageBox.Show("Postoje greške pri popunjavanju forme ili neki podatak nije unet", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (pacijentDTO.Pacijent.Ime == "")
            {
                MessageBox.Show("Postoje greške pri popunjavanju forme ili neki podatak nije unet", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (pacijentDTO.Pacijent.Prezime == "")
            {
                MessageBox.Show("Postoje greške pri popunjavanju forme ili neki podatak nije unet", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (FormSekretar.clickedDodaj)
            {
                if (!rgxJmbg.IsMatch(pacijentDTO.Pacijent.Jmbg))
                {
                    MessageBox.Show("Postoje greške pri popunjavanju forme ili neki podatak nije unet", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            if (FormSekretar.clickedDodaj)
            {
                foreach (Pacijent p in pacijenti)
                {
                    if (String.Equals(p.Jmbg, pacijentDTO.Pacijent.Jmbg))
                    {
                        MessageBox.Show("Postoje greške pri popunjavanju forme ili neki podatak nije unet", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
            }

            if ((Int32.Parse(pacijentDTO.Pacijent.Jmbg.Substring(0, 2)) != pacijentDTO.Pacijent.DatumRodjenja.Day) || (Int32.Parse(pacijentDTO.Pacijent.Jmbg.Substring(2, 2)) != pacijentDTO.Pacijent.DatumRodjenja.Month) || (Int32.Parse(pacijentDTO.Pacijent.Jmbg.Substring(4, 3)) != (pacijentDTO.Pacijent.DatumRodjenja.Year % 1000)))
            {
                MessageBox.Show("JMBG i datum rođenja se ne poklapaju", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            if (pacijentDTO.Pacijent.AdresaStanovanja == "")
            {
                MessageBox.Show("Postoje greške pri popunjavanju forme ili neki podatak nije unet", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!rgxBrojTelefona.IsMatch(pacijentDTO.Pacijent.BrojTelefona) || pacijentDTO.Pacijent.BrojTelefona == "")
            {
                MessageBox.Show("Postoje greške pri popunjavanju forme ili neki podatak nije unet", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!isEmail || pacijentDTO.Pacijent.Email == "")
            {
                MessageBox.Show("Postoje greške pri popunjavanju forme ili neki podatak nije unet", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (pacijentDTO.Pacijent.KorisnickoIme == "")
            {
                MessageBox.Show("Postoje greške pri popunjavanju forme ili neki podatak nije unet", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (FormSekretar.clickedDodaj)
            {
                foreach (Pacijent p in pacijenti)
                {
                    if (String.Equals(pacijentDTO.Pacijent.KorisnickoIme, p.KorisnickoIme))
                    {
                        MessageBox.Show("Postoje greške pri popunjavanju forme ili neki podatak nije unet", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
            }

            if (pacijentDTO.Pacijent.Lozinka == "")
            {
                MessageBox.Show("Postoje greške pri popunjavanju forme ili neki podatak nije unet", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (FormSekretar.clickedDodaj)
            {
                if (!rgxIDKartona.IsMatch(pacijentDTO.Pacijent.ZdravstveniKarton.BrojKartona.ToString()))
                {
                    MessageBox.Show("Postoje greške pri popunjavanju forme ili neki podatak nije unet", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            if (FormSekretar.clickedDodaj)
            {
                foreach (Pacijent p in pacijenti)
                {
                    if (!p.Guest && p.ZdravstveniKarton.BrojKartona == pacijentDTO.Pacijent.ZdravstveniKarton.BrojKartona)
                    {
                        MessageBox.Show("Postoje greške pri popunjavanju forme ili neki podatak nije unet", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
            }

            if (pacijentDTO.Pacijent.ZdravstveniKarton.Zanimanje == "")
            {
                MessageBox.Show("Postoje greške pri popunjavanju forme ili neki podatak nije unet", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Pacijent pacijent = pacijentDTO.Pacijent;
            if (FormSekretar.clickedDodaj)
            {
                skladistePacijenata.Save(pacijent);
                skladisteZdravstvenihKartona.Save(pacijent.ZdravstveniKarton);
                FormSekretar.RedovniPacijenti.Add(pacijentDTO);

                Korisnik korisnik = new Korisnik() { KorisnickoIme = pacijent.KorisnickoIme, Lozinka = pacijent.Lozinka, TipKorisnika = TipKorisnika.pacijent };
                skladisteKorisnika.Save(korisnik);

                FormSekretar.clickedDodaj = false;
                FormDodajPacijenta.pacijentiUpdate = true;
            }
            else
            {
                foreach (Pacijent p in pacijenti)
                {
                    if (String.Equals(p.Jmbg, pacijent.Jmbg))
                    {
                        skladistePacijenata.Update(pacijent);
                        skladisteZdravstvenihKartona.Update(pacijent.ZdravstveniKarton);
                        skladisteKorisnika.Update(pacijent);

                        for (int i = 0; i < FormSekretar.RedovniPacijenti.Count; i++)
                        {
                            if (FormSekretar.RedovniPacijenti[i].Pacijent.Jmbg == pacijent.Jmbg)
                            {
                                FormSekretar.RedovniPacijenti.Remove(FormSekretar.RedovniPacijenti[i]);
                                break;
                            }
                        }

                        FormSekretar.RedovniPacijenti.Add(pacijentDTO);
                        FormDodajPacijenta.pacijentiUpdate = true;

                        break;
                    }
                }
            }
        }
    }
}
