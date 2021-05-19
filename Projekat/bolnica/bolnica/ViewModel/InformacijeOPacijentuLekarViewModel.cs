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
        public bool guest { get; set; }

        public Visibility visibility { get; set; }
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
        public Action CloseAction { get; set; }

        private RelayCommand zatvoriCommand;
        public RelayCommand ZatvoriCommand
        {
            get { return zatvoriCommand; }
            set
            {
                zatvoriCommand = value;

            }
        }

        public void Executed_ZatvoriCommand(object obj)
        {
            CloseAction();
        }

        public bool CanExecute_ZatvoriCommand(object obj)
        {
            return true;
        }
        public InformacijeOPacijentuLekarViewModel(Pacijent p1)
        {
            
            
            
            guest = p1.Guest;
            ime = p1.Ime;
            prezime = p1.Prezime;

            if (p1.Pol.Equals(Pol.muski))
            {
                pol = "Muski";
            }
            else
            {
                pol = "Zenski";
            }
     
            jmbg = p1.Jmbg;
            datumR = p1.DatumRodjenja.ToString();
            adresaS = p1.AdresaStanovanja;
            brojTel = p1.BrojTelefona;
            emailAdresa = p1.Email;
            List<Sastojak>? l = p1.Alergeni;
            List<String> aler = new List<string>();
            FileStorageSastojak storageSas = new FileStorageSastojak();
            if (l != null)
            {
                for (int i = 0; i < p1.Alergeni.Count; i++)
                {
                    foreach (Sastojak s in storageSas.GetAll())
                    {
                        if (p1.Alergeni[i].Id.Equals(s.Id))
                        {
                           aler.Add(s.Naziv);
                        }
                    }
                }
            }
           
            alergeni = aler;
            

            if (p1.Guest)
            {
                visibility = Visibility.Hidden;
            }
            else
            {
                visibility = Visibility.Visible;
                korisnickoIme = p1.KorisnickoIme;

                zanimanje = p1.ZdravstveniKarton.Zanimanje;
               bracniStatus = p1.ZdravstveniKarton.BracniStatus;
                osiguranje = p1.ZdravstveniKarton.Osiguranje;

            }
            ZatvoriCommand = new RelayCommand(Executed_ZatvoriCommand, CanExecute_ZatvoriCommand);
        }
        







    }
}
