using Bolnica.DTO;
using Bolnica.Forms;
using Bolnica.Model.Korisnici;
using Bolnica.Model.Pregledi;
using Bolnica.Repository.Pregledi;
using Bolnica.Repository.Prostorije;
using Bolnica.ViewModel;
using Model.Korisnici;
using Model.Pacijenti;
using Model.Pregledi;
using Model.Prostorije;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Bolnica.Services
{   
    public class LekarService
    {
        private FileRepositoryPregled skladistePregleda = new FileRepositoryPregled();
        private FileRepositoryOperacija skladisteOperacija = new FileRepositoryOperacija();
        private FileRepositoryPacijent skladistePacijenata = new FileRepositoryPacijent();
        private FileRepositoryProstorija skladisteProstorija = new FileRepositoryProstorija();
        private FileRepositoryLekar skladisteLekara = new FileRepositoryLekar();
        private FileRepositoryLek skladisteLekova = new FileRepositoryLek();
        private FileRepositoryObavestenje skladisteObavestenja = new FileRepositoryObavestenje();
        private FileRepositoryKorisnik skladisteKorisnika = new FileRepositoryKorisnik();
        private FileRepositoryObavestenje oba = new FileRepositoryObavestenje();

        public List<Pregled> DobijPreglede()
        {
            return skladistePregleda.GetAll();
        }
        public List<Pacijent> DobijPacijente()
        {
            return skladistePacijenata.GetAll();
        }
        public List<Prostorija> DobijProstorije()
        {
            return skladisteProstorija.GetAll();
        }
        public List<Lekar> DobijLekare()
        {
            return skladisteLekara.GetAll();
        }
        public List<Operacija> DobijOperacije()
        {
            return skladisteOperacija.GetAll();
        }
        public List<Lek> DobijLekove()
        {
            return skladisteLekova.GetAll();
        }
        public void ZakaziPregled(LekarServiceDTO lekarServiceDTO)
        {
            TerminLekarViewModel vm = new TerminLekarViewModel(lekarServiceDTO.lekarTrenutni);
            FormNapraviTerminLekar ff = new FormNapraviTerminLekar(vm);
        }

        public void OtkaziPregled(LekarServiceDTO lekarServiceDTO)
        {
            if (lekarServiceDTO.tabela.SelectedCells.Count > 0)
            {
                var objekat = lekarServiceDTO.tabela.SelectedValue;
                if (objekat.GetType().Equals(lekarServiceDTO.prikazPregleda.GetType()))
                {
                    OtkaziPregledAkoJePregled(lekarServiceDTO);
                }
                else if (objekat.GetType().Equals(lekarServiceDTO.prikazOperacije.GetType()))
                {
                    OtkaziPregledAkoJeOperacija(lekarServiceDTO);
                }
                int index = lekarServiceDTO.tabela.SelectedIndex;
                LekarViewModel.podaciLista.Items.RemoveAt(index);
                LekarViewModel.RefreshPodaciListu();
            }

        }

        private void OtkaziPregledAkoJePregled(LekarServiceDTO lekarServiceDTO)
        {
            var objekat = lekarServiceDTO.tabela.SelectedValue;
            PrikazPregleda izabraniPrikazPregleda = objekat as PrikazPregleda;
            for (int i = 0; i < lekarServiceDTO.listaPregleda.Count; i++)
            {
                if (izabraniPrikazPregleda.Id.Equals(lekarServiceDTO.listaPregleda[i].Id))
                {
                    skladistePregleda.Delete(lekarServiceDTO.listaPregleda[i]);
                    lekarServiceDTO.listaPregleda.RemoveAt(i);
                    break;
                }
            }
        }

        private void OtkaziPregledAkoJeOperacija(LekarServiceDTO lekarServiceDTO)
        {
            var objekat = lekarServiceDTO.tabela.SelectedValue;
            PrikazPregleda izabraniPrikazOperacije = objekat as PrikazOperacije;
            for (int i = 0; i < lekarServiceDTO.listaOperacija.Count; i++)
            {
                if (izabraniPrikazOperacije.Id.Equals(lekarServiceDTO.listaOperacija[i].Id))
                {
                    skladisteOperacija.Delete(lekarServiceDTO.listaOperacija[i]);
                    lekarServiceDTO.listaOperacija.RemoveAt(i);
                    break;
                }
            }
        }

        public void IzmeniPregled(LekarServiceDTO lekarServiceDTO)
        {
            if (lekarServiceDTO.tabela.SelectedCells.Count > 0)
            {

                var objekat = lekarServiceDTO.tabela.SelectedValue;
                if (objekat.GetType().Equals(lekarServiceDTO.prikazPregleda.GetType()))
                {
                    IzmeniPregledAkoJePregled(lekarServiceDTO);
                }
                else if (objekat.GetType().Equals(lekarServiceDTO.prikazOperacije.GetType()))
                {
                    IzmeniPregledAkoJeOperacija(lekarServiceDTO);
                }


            }

        }


        private void IzmeniPregledAkoJePregled(LekarServiceDTO lekarServiceDTO)
        {
            PrikazPregleda izabraniPrikazPregleda = new PrikazPregleda();
            var objekat = lekarServiceDTO.tabela.SelectedValue;
            PrikazPregleda izabraniPrikazPregledaIzTabele = objekat as PrikazPregleda;
            for (int i = 0; i < lekarServiceDTO.listaPregleda.Count; i++)
            {
                if (izabraniPrikazPregledaIzTabele.Id.Equals(lekarServiceDTO.listaPregleda[i].Id))
                {
                    izabraniPrikazPregleda = lekarServiceDTO.tabela.SelectedItem as PrikazPregleda;
                    TerminLekarViewModel vm = new TerminLekarViewModel(izabraniPrikazPregleda, lekarServiceDTO.lekarTrenutni);
                    FormIzmeniTerminLekar ff = new FormIzmeniTerminLekar(vm);
                    break;
                }
            }
        }

        private void IzmeniPregledAkoJeOperacija(LekarServiceDTO lekarServiceDTO)
        {
            PrikazOperacije izabraniPrikazOperacije = new PrikazOperacije();
            var objekat = lekarServiceDTO.tabela.SelectedValue;
            if (!lekarServiceDTO.lekarTrenutni.Specijalizacija.OblastMedicine.Equals("Opsta"))
            {
                PrikazOperacije izabraniPrikazOperacijeIzTabele = objekat as PrikazOperacije;
                for (int i = 0; i < lekarServiceDTO.listaOperacija.Count; i++)
                {
                    if (izabraniPrikazOperacijeIzTabele.Id.Equals(lekarServiceDTO.listaOperacija[i].Id))
                    {

                        izabraniPrikazOperacije = lekarServiceDTO.tabela.SelectedItem as PrikazOperacije;
                        TerminLekarViewModel vm = new TerminLekarViewModel(izabraniPrikazOperacije, lekarServiceDTO.lekarTrenutni);
                        FormIzmeniTerminLekar ff = new FormIzmeniTerminLekar(vm);
                        break;
                    }
                }
            }
            else
            {
                MessageBox.Show("Nemate ovlastenje da menjate operacije");
            }
        }





        public void InformacijeOPacijentu(LekarServiceDTO lekarServiceDTO)
        {
            if (lekarServiceDTO.tabela.SelectedCells.Count > 0)
            {
                var objekat = lekarServiceDTO.tabela.SelectedValue;

                if (objekat.GetType().Equals(lekarServiceDTO.prikazPregleda.GetType()))
                {
                    InformacijeOPacijentuAkoJePregled(lekarServiceDTO);
                }
                else if (objekat.GetType().Equals(lekarServiceDTO.prikazOperacije.GetType()))
                {
                    InformacijeOPacijentuAkoJeOperacija(lekarServiceDTO);
                }

            }
        }
        public void HospitalizacijaPacijenta(LekarServiceDTO lekarServiceDTO)
        {
            if (lekarServiceDTO.tabela.SelectedCells.Count > 0)
            {
                var objekat = lekarServiceDTO.tabela.SelectedValue;

                if (objekat.GetType().Equals(lekarServiceDTO.prikazPregleda.GetType()))
                {
                    HospitalizacijaPacijentaAkoJePregled(lekarServiceDTO);
                }
                else if (objekat.GetType().Equals(lekarServiceDTO.prikazOperacije.GetType()))
                {
                    HospitalizacijaPacijentaAkoJeOperacija(lekarServiceDTO);
                }

            }
        }

        private void HospitalizacijaPacijentaAkoJePregled(LekarServiceDTO lekarServiceDTO)
        {
            var objekat = lekarServiceDTO.tabela.SelectedValue;
            PrikazPregleda izabraniPrikazPregledaIzTabele = objekat as PrikazPregleda;
            PrikazPregleda izabraniPrikazPregleda = new PrikazPregleda();
            for (int i = 0; i < lekarServiceDTO.listaPregleda.Count; i++)
            {
                if (izabraniPrikazPregledaIzTabele.Id.Equals(lekarServiceDTO.listaPregleda[i].Id))
                {

                    izabraniPrikazPregleda = lekarServiceDTO.tabela.SelectedItem as PrikazPregleda;
                    HospitalizacijaLekarViewModel vm = new HospitalizacijaLekarViewModel(izabraniPrikazPregleda.Pacijent);
                    FormHospitalizujLekar ff = new FormHospitalizujLekar(vm);

                    break;
                }
            }
        }

        private void HospitalizacijaPacijentaAkoJeOperacija(LekarServiceDTO lekarServiceDTO)
        {
            var objekat = lekarServiceDTO.tabela.SelectedValue;
            PrikazOperacije izabraniPrikazOperacijeIzTabele = objekat as PrikazOperacije;
            PrikazOperacije izabraniPrikazOperacije = new PrikazOperacije();
            for (int i = 0; i < lekarServiceDTO.listaOperacija.Count; i++)
            {
                if (izabraniPrikazOperacijeIzTabele.Id.Equals(lekarServiceDTO.listaOperacija[i].Id))
                {
                    izabraniPrikazOperacije = lekarServiceDTO.tabela.SelectedItem as PrikazOperacije;
                    HospitalizacijaLekarViewModel vm = new HospitalizacijaLekarViewModel(izabraniPrikazOperacije.Pacijent);
                    FormHospitalizujLekar ff = new FormHospitalizujLekar(vm);

                    break;
                }
            }
        }
        private void InformacijeOPacijentuAkoJePregled(LekarServiceDTO lekarServiceDTO)
        {
            var objekat = lekarServiceDTO.tabela.SelectedValue;
            PrikazPregleda izabraniPrikazPregledaIzTabele = objekat as PrikazPregleda;
            PrikazPregleda izabraniPrikazPregleda = new PrikazPregleda();
            for (int i = 0; i < lekarServiceDTO.listaPregleda.Count; i++)
            {
                if (izabraniPrikazPregledaIzTabele.Id.Equals(lekarServiceDTO.listaPregleda[i].Id))
                {

                    izabraniPrikazPregleda = lekarServiceDTO.tabela.SelectedItem as PrikazPregleda;
                    InformacijeOPacijentuLekarViewModel vm = new InformacijeOPacijentuLekarViewModel(izabraniPrikazPregleda.Pacijent);
                    FormPrikazInformacijaOPacijentuLekar ff = new FormPrikazInformacijaOPacijentuLekar(vm);

                    break;
                }
            }
        }

        private void InformacijeOPacijentuAkoJeOperacija(LekarServiceDTO lekarServiceDTO)
        {
            var objekat = lekarServiceDTO.tabela.SelectedValue;
            PrikazOperacije izabraniPrikazOperacijeIzTabele = objekat as PrikazOperacije;
            PrikazOperacije izabraniPrikazOperacije = new PrikazOperacije();
            for (int i = 0; i < lekarServiceDTO.listaOperacija.Count; i++)
            {
                if (izabraniPrikazOperacijeIzTabele.Id.Equals(lekarServiceDTO.listaOperacija[i].Id))
                {
                    izabraniPrikazOperacije = lekarServiceDTO.tabela.SelectedItem as PrikazOperacije;
                    InformacijeOPacijentuLekarViewModel vm = new InformacijeOPacijentuLekarViewModel(izabraniPrikazOperacije.Pacijent);
                    FormPrikazInformacijaOPacijentuLekar ff = new FormPrikazInformacijaOPacijentuLekar(vm);
                    break;
                }
            }
        }

        public void SkociNaEnter(LekarServiceDTO lekarServiceDTO)
        {

            lekarServiceDTO.dugme.Focus();

        }

        public void SkociNaLevo(LekarServiceDTO lekarServiceDTO)
        {

            lekarServiceDTO.tab.Focus();

        }

        public void SkociNaTab(LekarServiceDTO lekarServiceDTO)
        {

            var red = lekarServiceDTO.tabela.SelectedIndex;
            if (red < lekarServiceDTO.tabela.Items.Count - 1)
            {
                red = red + 1;
                lekarServiceDTO.tabela.SelectedIndex = red;

            }
            else
            {
                red = 0;
                lekarServiceDTO.tabela.SelectedIndex = red;
            }

        }

        public void SkociNaEnterIstorija(LekarServiceDTO lekarServiceDTO)
        {

            lekarServiceDTO.dugme.Focus();
        }
        public void SkociNaLevoIstorija(LekarServiceDTO lekarServiceDTO)
        {

            lekarServiceDTO.tab.Focus();
        }
        public void SkociNaTabIstorija(LekarServiceDTO lekarServiceDTO)
        {

            var red = lekarServiceDTO.tabela.SelectedIndex;
            if (red < lekarServiceDTO.tabela.Items.Count - 1)
            {
                red = red + 1;
                lekarServiceDTO.tabela.SelectedIndex = red;
            }
            else
            {
                red = 0;
                lekarServiceDTO.tabela.SelectedIndex = red;
            }
        }

        public void Anamneza(LekarServiceDTO lekarServiceDTO)
        {
            if (lekarServiceDTO.tabela.SelectedCells.Count > 0)
            { 
                var objekat = lekarServiceDTO.tabela.SelectedValue;
                if (objekat.GetType().Equals(lekarServiceDTO.prikazPregleda.GetType()))
                {
                    AnamnezaZaPregled(lekarServiceDTO);
                }
                if (objekat.GetType().Equals(lekarServiceDTO.prikazOperacije.GetType()))
                {
                    AnamnezaZaOperaciju(lekarServiceDTO);
                }

            }
        }

        private void AnamnezaZaPregled(LekarServiceDTO lekarServiceDTO)
        {
            var objekat = lekarServiceDTO.tabela.SelectedValue;
            PrikazPregleda izabraniPrikazPregleda = new PrikazPregleda();
            PrikazPregleda izabraniPrikazPregledaIzTabele = objekat as PrikazPregleda;
            if (izabraniPrikazPregledaIzTabele.Datum < DateTime.Now)
            {
                for (int i = 0; i < lekarServiceDTO.listaPregleda.Count; i++)
                {
                    if (izabraniPrikazPregledaIzTabele.Id.Equals(lekarServiceDTO.listaPregleda[i].Id))
                    {
                        izabraniPrikazPregleda = lekarServiceDTO.tabela.SelectedItem as PrikazPregleda;
                        AnamnezaLekarViewModel vm = new AnamnezaLekarViewModel(izabraniPrikazPregleda, lekarServiceDTO.lekarTrenutni);
                        FormNapraviAnamnezuLekar form = new FormNapraviAnamnezuLekar(vm);
                        break;
                    }
                }
            }
            else
            {
                MessageBox.Show("Pregled nije poceo");
                return;
            }
        }

        private void AnamnezaZaOperaciju(LekarServiceDTO lekarServiceDTO)
        {
            var objekat = lekarServiceDTO.tabela.SelectedValue;
            PrikazOperacije izabraniPrikazOperacije = new PrikazOperacije();
            PrikazOperacije izabraniPrikazOperacijeIzTabele = objekat as PrikazOperacije;
            if (izabraniPrikazOperacijeIzTabele.Datum < DateTime.Now)
            {
                for (int i = 0; i < lekarServiceDTO.listaOperacija.Count; i++)
                {
                    if (izabraniPrikazOperacijeIzTabele.Id.Equals(lekarServiceDTO.listaOperacija[i].Id))
                    {
                        izabraniPrikazOperacije = lekarServiceDTO.tabela.SelectedItem as PrikazOperacije;
                        AnamnezaLekarViewModel vm = new AnamnezaLekarViewModel(izabraniPrikazOperacije, lekarServiceDTO.lekarTrenutni);
                        FormNapraviAnamnezuLekar form = new FormNapraviAnamnezuLekar(vm);
                        break;
                    }
                }

            }
            else
            {
                MessageBox.Show("Operacija nije pocela");
                return;
            }
        }

        public void AnamnezaIstorija(LekarServiceDTO lekarServiceDTO)
        {
            if (lekarServiceDTO.tabela.SelectedCells.Count > 0)
            {
                var objekat = lekarServiceDTO.tabela.SelectedValue;
                if (objekat.GetType().Equals(lekarServiceDTO.prikazPregleda.GetType()))
                {
                    AnamnezaIstorijaZaPregled(lekarServiceDTO);
                }
                if (objekat.GetType().Equals(lekarServiceDTO.prikazOperacije.GetType()))
                {
                    AnamnezaIstorijaZaOperaciju(lekarServiceDTO);
                }
            }
        }

        private void AnamnezaIstorijaZaPregled(LekarServiceDTO lekarServiceDTO)
        {
            var objekat = lekarServiceDTO.tabela.SelectedValue;
            PrikazPregleda izabraniPrikazPregledaIzTabele = objekat as PrikazPregleda;
            PrikazPregleda izabraniPrikazPregleda = new PrikazPregleda();
            for (int i = 0; i < lekarServiceDTO.listaPregleda.Count; i++)
            {
                if (izabraniPrikazPregledaIzTabele.Id.Equals(lekarServiceDTO.listaPregleda[i].Id))
                {
                    izabraniPrikazPregleda = lekarServiceDTO.tabela.SelectedItem as PrikazPregleda;
                    AnamnezaLekarViewModel vm = new AnamnezaLekarViewModel(izabraniPrikazPregleda, lekarServiceDTO.lekarTrenutni);
                    FormNapraviAnamnezuLekar form = new FormNapraviAnamnezuLekar(vm);
                    break;
                }
            }
        }

        private void AnamnezaIstorijaZaOperaciju(LekarServiceDTO lekarServiceDTO)
        {
            var objekat = lekarServiceDTO.tabela.SelectedValue;
            PrikazOperacije izabraniPrikazOperacijeIzTabele = objekat as PrikazOperacije;
            PrikazOperacije izabraniPrikazOperacije = new PrikazOperacije();
            for (int i = 0; i < lekarServiceDTO.listaOperacija.Count; i++)
            {
                if (izabraniPrikazOperacijeIzTabele.Id.Equals(lekarServiceDTO.listaOperacija[i].Id))
                {
                    izabraniPrikazOperacije = lekarServiceDTO.tabela.SelectedItem as PrikazOperacije;
                    AnamnezaLekarViewModel vm = new AnamnezaLekarViewModel(izabraniPrikazOperacije, lekarServiceDTO.lekarTrenutni);
                    FormNapraviAnamnezuLekar form = new FormNapraviAnamnezuLekar(vm);
                    break;
                }
            }

        }
        public void IzmeniLek(LekarServiceDTO lekarServiceDTO)
        {
            PrikazLek izabraniLek = lekarServiceDTO.tabela.SelectedItem as PrikazLek;
            lekarServiceDTO.lekovi = skladisteLekova.GetAll();
            IzfiltrirajLekove(lekarServiceDTO);
            NapraviFormuZaIzmenuLeka(lekarServiceDTO,izabraniLek);
            
        }
        private void IzfiltrirajLekove(LekarServiceDTO lekarServiceDTO)
        {
            for (int i = 0; i < lekarServiceDTO.lekovi.Count; i++)
            {
                if (lekarServiceDTO.lekovi[i].Status.Equals(StatusLeka.odbijen) || lekarServiceDTO.lekovi[i].Obrisan)
                {
                    lekarServiceDTO.lekovi.RemoveAt(i);
                    i--;
                }
            }
        }
        private void NapraviFormuZaIzmenuLeka(LekarServiceDTO lekarServiceDTO,PrikazLek izabraniLek)
        {
            for (int i = 0; i < lekarServiceDTO.lekovi.Count; i++)
            {
                if (lekarServiceDTO.lekovi[i].Id.Equals(izabraniLek.Id))
                {
                    LekLekarViewModel vm = new LekLekarViewModel(lekarServiceDTO.lekovi[i]);
                    FormIzmeniLekLekar form = new FormIzmeniLekLekar(vm);
                    form.Show();
                    break;
                }
            }
        }

        public void SkociNaEnterLek(LekarServiceDTO lekarServiceDTO)
        {

            lekarServiceDTO.dugme.Focus();

        }
        public void SkociNaLevoLek(LekarServiceDTO lekarServiceDTO)
        {

            lekarServiceDTO.tab.Focus();

        }
        public void SkociNaTabLek(LekarServiceDTO lekarServiceDTO)
        {

            var red = lekarServiceDTO.tabela.SelectedIndex;
            if (red < lekarServiceDTO.tabela.Items.Count - 1)
            {
                red = red + 1;
                lekarServiceDTO.tabela.SelectedIndex = red;
            }
            else
            {
                red = 0;
                lekarServiceDTO.tabela.SelectedIndex = red;
            }

        }

        public void OdobriLek(LekarServiceDTO lekarServiceDTO)
        {
            PrikazLek izabraniLek = lekarServiceDTO.tabela.SelectedItem as PrikazLek;

            if (izabraniLek.Status.Equals(StatusLeka.cekaValidaciju))
            {
                IzfiltrirajLekove(lekarServiceDTO);
                AzurirajLek(lekarServiceDTO,izabraniLek);
                AzurirajTabeluLekova(lekarServiceDTO, izabraniLek);
                PosaljiObavestenje(izabraniLek);
            }
            else
            {
                MessageBox.Show("Lek je vec odobren");
            }
        }

        private void AzurirajLek(LekarServiceDTO lekarServiceDTO,PrikazLek izabraniLek)
        {
            for (int i = 0; i < lekarServiceDTO.lekovi.Count; i++)
            {
                if (lekarServiceDTO.lekovi[i].Id.Equals(izabraniLek.Id))
                {
                    lekarServiceDTO.lekovi[i].Status = StatusLeka.odobren;
                    skladisteLekova.Delete(lekarServiceDTO.lekovi[i]);
                    skladisteLekova.Save(lekarServiceDTO.lekovi[i]);
                    break;

                }
            }
        }
        private void PosaljiObavestenje(PrikazLek izabraniLek)
        {
            oba.Save(PopuniObavestenje(izabraniLek));
        }

        private int IzracunajId(List<Obavestenje> sva)
        {
            int max = 0;
            for (int i = 0; i < sva.Count; i++)
            {
                if (max < sva[i].Id)
                {
                    max = sva[i].Id;
                }
            }
            max = max + 1;
            return max;
        }
        private Obavestenje PopuniObavestenje(PrikazLek izabraniLek)
        {
            
            List<Korisnik> sviKorisnici = skladisteKorisnika.GetAll();
            List<Obavestenje> svaObavestenja = skladisteObavestenja.GetAll();
            Obavestenje obavestenje = new Obavestenje(IzracunajId(svaObavestenja), DateTime.Now, "Lek " + izabraniLek.Naziv + " sa dozom " + izabraniLek.KolicinaUMg + " i sastojcima: " + izabraniLek.Sastojak + " je prihvacen. ", "Lek " + izabraniLek.Naziv + " je prihvacen",false);
            for (int i = 0; i < sviKorisnici.Count; i++)
            {
                if (sviKorisnici[i].TipKorisnika.Equals(TipKorisnika.upravnik))
                {
                    obavestenje.Korisnici.Add(sviKorisnici[i]);
                }
            }
            return obavestenje;
        }
        private void AzurirajTabeluLekova(LekarServiceDTO lekarServiceDTO,PrikazLek izabraniLek)
        {
            for (int i = 0; i < LekarViewModel.lekoviPrikaz.Count; i++)
            {
                if (LekarViewModel.lekoviPrikaz[i].Id.Equals(izabraniLek.Id))
                {
                    LekarViewModel.lekoviPrikaz[i].Status = StatusLeka.odobren;
                    lekarServiceDTO.tabela.Items.Refresh();
                    break;

                }
            }
        }

        public void VratiNaIzmenu(LekarServiceDTO lekarServiceDTO)
        {
            PrikazLek p = lekarServiceDTO.tabela.SelectedItem as PrikazLek;
            if (p.Status.Equals(StatusLeka.cekaValidaciju))
            {
                KomentarLekaLekarViewModel vm = new KomentarLekaLekarViewModel(p);
                FormKomentarLekaLekar lek = new FormKomentarLekaLekar(vm);
            }
            else
            {
                MessageBox.Show("Niste odabrali lek koji ceka validaciju");
            }
        }

        
        

        
    }
}
