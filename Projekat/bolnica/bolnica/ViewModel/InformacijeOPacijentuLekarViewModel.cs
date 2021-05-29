using Bolnica.Commands;
using Bolnica.Model.Pregledi;
using Model.Korisnici;
using Model.Pacijenti;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Bolnica.ViewModel
{
    public class InformacijeOPacijentuLekarViewModel : ViewModel
    {
        #region POLJA
        public bool gost { get; set; }

        public Visibility vidljiv { get; set; }
        public string ime { get; set; }
        public string prezime { get; set; }
        public string pol { get; set; }
        public string jmbg { get; set; }

        public string datumR { get; set; }
        public string adresaS { get; set; }
        public string brojTel { get; set; }
        public string emailAdresa { get; set; }
        public List<string> alergeni { get; set; }
        public string korisnickoIme { get; set; }

        public string zanimanje { get; set; }
        public BracniStatus bracniStatus { get; set; }
        public bool osiguranje { get; set; }

        private Injector inject;
        public Injector Inject
        {
            get { return inject; }
            set
            {
                inject = value;
            }
        }
        #endregion
        #region KOMANDE
        public Action ZatvoriAkcija { get; set; }

        private RelayCommand zatvoriKomanda;
        public RelayCommand ZatvoriKomanda
        {
            get { return zatvoriKomanda; }
            set
            {
                zatvoriKomanda = value;

            }
        }

        public void Executed_ZatvoriKomanda(object obj)
        {
            ZatvoriAkcija();
        }

        public bool CanExecute_ZatvoriKomanda(object obj)
        {
            return true;
        }
        #endregion
        public InformacijeOPacijentuLekarViewModel(Pacijent izabraniPacijent)
        {
            PostaviZajednickaPolja(izabraniPacijent);
            PostaviAlergene(izabraniPacijent);
            UpravljajZdravstvenimKartonom(izabraniPacijent);
            ZatvoriKomanda = new RelayCommand(Executed_ZatvoriKomanda, CanExecute_ZatvoriKomanda);

        }
        #region POMOCNE FUNKCIJE
        public void UpravljajZdravstvenimKartonom(Pacijent izabraniPacijent)
        {
            if (izabraniPacijent.Guest)
            {
                vidljiv = Visibility.Hidden;
            }
            else
            {
                vidljiv = Visibility.Visible;
                korisnickoIme = izabraniPacijent.KorisnickoIme;

                zanimanje = izabraniPacijent.ZdravstveniKarton.Zanimanje;
                bracniStatus = izabraniPacijent.ZdravstveniKarton.BracniStatus;
                osiguranje = izabraniPacijent.ZdravstveniKarton.Osiguranje;

            }
        }
        public void PostaviZajednickaPolja(Pacijent izabraniPacijent)
        {
            Inject = new Injector();
            gost = izabraniPacijent.Guest;
            ime = izabraniPacijent.Ime;
            prezime = izabraniPacijent.Prezime;

            if (izabraniPacijent.Pol.Equals(Pol.muski))
            {
                pol = "Muski";
            }
            else
            {
                pol = "Zenski";
            }

            jmbg = izabraniPacijent.Jmbg;
            datumR = izabraniPacijent.DatumRodjenja.ToString();
            adresaS = izabraniPacijent.AdresaStanovanja;
            brojTel = izabraniPacijent.BrojTelefona;
            emailAdresa = izabraniPacijent.Email;
        }
        public void PostaviAlergene(Pacijent izabraniPacijent)
        {
            List<Sastojak>? alergeniPacijenta = izabraniPacijent.Alergeni;
            List<String> alergeniZaPrikaz = new List<string>();

            if (alergeniPacijenta != null)
            {
                for (int i = 0; i < izabraniPacijent.Alergeni.Count; i++)
                {
                    foreach (Sastojak s in inject.InformacijeOPacijentuLekarController.DobijSastojke())
                    {
                        if (izabraniPacijent.Alergeni[i].Id.Equals(s.Id))
                        {
                            alergeniZaPrikaz.Add(s.Naziv);
                        }
                    }
                }
            }

            alergeni = alergeniZaPrikaz;
        }
        #endregion





    }
}
