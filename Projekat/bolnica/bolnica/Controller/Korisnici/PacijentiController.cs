using Bolnica.DTO;
using Bolnica.Forms;
using Bolnica.Sekretar;
using Bolnica.Services;
using Model.Korisnici;
using Model.Pacijenti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Bolnica.Controller
{
    public class PacijentiController
    {
        private PacijentiService service = new PacijentiService();

        public List<Pacijent> DobaviPacijente()
        {
            return service.DobaviPacijente();
        }

        public List<ZdravstveniKarton> DobaviZdravstveneKartone()
        {
            return service.DobaviZdravstveneKartone();
        }

        public void ObrisiRedovnogPacijenta(PacijentDTO pacijentDTO) 
        {
            service.ObrisiRedovnogPacijenta(pacijentDTO);
        }

        public void ObrisiGostPacijenta(PacijentDTO pacijentDTO) 
        {
            service.ObrisiGostPacijenta(pacijentDTO);
        }

        public void OdblokirajPacijenta(PacijentDTO pacijentDTO) 
        {
            service.OdblokirajPacijenta(pacijentDTO);
        }

        public void DodajIliIzmeniRedovnogPacijenta(PacijentDTO pacijentDTO) 
        {
            TipKorisnika tipKorisnika = TipKorisnika.pacijent;
            bool guestNalog = false;
            bool obrisan = false;
            Pol pol = new Pol();
            if ((bool)pacijentDTO.radioButton.IsChecked)
                pol = Pol.muski;
            else
                pol = Pol.zenski;
            String jmbg = pacijentDTO.textBoxes[2].Text;
            String ime = pacijentDTO.textBoxes[0].Text;
            String prezime = pacijentDTO.textBoxes[1].Text;
            String brojTelefona = pacijentDTO.textBoxes[4].Text;
            String adresaStanovanja = pacijentDTO.textBoxes[3].Text;
            String email = pacijentDTO.textBoxes[5].Text;
            DateTime datumRodjenja;
            try
            {
                datumRodjenja = pacijentDTO.datePicker.SelectedDate.Value.Date;
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Postoje greške pri popunjavanju forme ili neki podatak nije unet", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            String korisnickoIme = pacijentDTO.textBoxes[6].Text;
            String lozinka = pacijentDTO.textBoxes[7].Text;
            String zanimanje = pacijentDTO.textBoxes[9].Text;
            int brojKartona;
            try
            {
                brojKartona = Int32.Parse(pacijentDTO.textBoxes[8].Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Postoje greške pri popunjavanju forme ili neki podatak nije unet", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            catch (OverflowException)
            {
                MessageBox.Show("Postoje greške pri popunjavanju forme ili neki podatak nije unet", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            bool osiguranje = (bool)pacijentDTO.checkBox.IsChecked;
            int selectedBracniStatus = pacijentDTO.comboBox.SelectedIndex;
            BracniStatus bracniStatus = new BracniStatus();
            if (selectedBracniStatus == 0)
                bracniStatus = BracniStatus.neozenjen_neudata;
            else if (selectedBracniStatus == 1)
                bracniStatus = BracniStatus.ozenjen_udata;
            else if (selectedBracniStatus == 2)
                bracniStatus = BracniStatus.udovac_udovica;
            else if (selectedBracniStatus == 3)
                bracniStatus = BracniStatus.razveden_razvedena;

            PacijentDTO pacijent = new PacijentDTO();
            pacijent.Pacijent = new Pacijent();
            pacijent.Pacijent.KorisnickoIme = korisnickoIme;
            pacijent.Pacijent.Lozinka = lozinka;
            pacijent.Pacijent.TipKorisnika = tipKorisnika;
            pacijent.Pacijent.Jmbg = jmbg;
            pacijent.Pacijent.Ime = ime;
            pacijent.Pacijent.Prezime = prezime;
            pacijent.Pacijent.Pol = pol;
            pacijent.Pacijent.DatumRodjenja = datumRodjenja;
            pacijent.Pacijent.BrojTelefona = brojTelefona;
            pacijent.Pacijent.AdresaStanovanja = adresaStanovanja;
            pacijent.Pacijent.Email = email;
            pacijent.Pacijent.Obrisan = obrisan;
            pacijent.Pacijent.Guest = guestNalog;
            ZdravstveniKarton zk = new ZdravstveniKarton()
            {
                BrojKartona = brojKartona,
                Zanimanje = zanimanje,
                BracniStatus = bracniStatus,
                Osiguranje = osiguranje
            };
            pacijent.Pacijent.ZdravstveniKarton = zk;

            if (!FormSekretar.clickedDodaj && FormAlergeniDodaj.DodatiAlergeni == null)
                pacijent.Pacijent.Alergeni = pacijentDTO.Alergeni;
            else if (FormAlergeniDodaj.DodatiAlergeni != null && FormAlergeniDodaj.DodatiAlergeni.Count != 0)
                pacijent.Pacijent.Alergeni = FormAlergeniDodaj.DodatiAlergeni.ToList();
            else
                pacijent.Pacijent.Alergeni = null;

            service.DodajIliIzmeniRedovnogPacijenta(pacijent);
        }
    }
}
