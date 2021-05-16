using Bolnica.Forms;
using Bolnica.Model.Korisnici;
using Bolnica.Model.Pregledi;
using Bolnica.ViewModel;
using Model.Korisnici;
using Model.Pacijenti;
using Model.Pregledi;
using Model.Prostorije;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Bolnica.Services
{
    public class LekarService
    {
        private FileStoragePregledi sviPregledi = new FileStoragePregledi();
        private FileStoragePacijenti sviPacijenti = new FileStoragePacijenti();
        private FileStorageProstorija sveProstorije = new FileStorageProstorija();
        private FileStorageLekar sviLekari = new FileStorageLekar();
        private FileStorageLek sviLekovi = new FileStorageLek();
        public void ZakaziPregled(Lekar lekarTrenutni)
        {
            IzmeniINapraviTerminLekarViewModel vm = new IzmeniINapraviTerminLekarViewModel(lekarTrenutni);
            FormNapraviTerminLekar ff = new FormNapraviTerminLekar(vm);
        }
        public void OtkaziPregled(DataGrid lekarGrid,PrikazPregleda prikazPregleda,List<Pregled> listaPregleda,PrikazOperacije prikazOperacije,List<Operacija> listaOperacija)
        {
            if (lekarGrid.SelectedCells.Count > 0)
            {
                var objekat = lekarGrid.SelectedValue;
                if (objekat.GetType().Equals(prikazPregleda.GetType()))
                {
                    PrikazPregleda pri = objekat as PrikazPregleda;
                    for (int i = 0; i < listaPregleda.Count; i++)
                    {
                        if (pri.Id.Equals(listaPregleda[i].Id))
                        {

                            sviPregledi.Delete(listaPregleda[i]);
                            listaPregleda.RemoveAt(i);
                            break;
                        }
                    }
                }
                else if (objekat.GetType().Equals(prikazOperacije.GetType()))
                {
                    PrikazPregleda pri = objekat as PrikazOperacije;
                    for (int i = 0; i < listaOperacija.Count; i++)
                    {
                        if (pri.Id.Equals(listaOperacija[i].Id))
                        {
                            sviPregledi.Delete(listaOperacija[i]);
                            listaOperacija.RemoveAt(i);
                            break;
                        }
                    }
                }
                int index = lekarGrid.SelectedIndex;
                LekarViewModel.dataList.Items.RemoveAt(index);
                LekarViewModel.data();
            }

        }



        public void IzmeniPregled(DataGrid lekarGrid,PrikazPregleda prikazPregleda, List<Pregled> listaPregleda, PrikazOperacije prikazOperacije, List<Operacija> listaOperacija, Lekar lekarTrenutni)
        {
            if (lekarGrid.SelectedCells.Count > 0)
            {

                var objekat = lekarGrid.SelectedValue;
                PrikazPregleda p1 = new PrikazPregleda();
                PrikazOperacije op = new PrikazOperacije();
                p1.Pacijent = new Pacijent();
                op.Pacijent = new Pacijent();

                if (objekat.GetType().Equals(prikazPregleda.GetType()))
                {
                    PrikazPregleda pri = objekat as PrikazPregleda;
                    for (int i = 0; i < listaPregleda.Count; i++)
                    {
                        if (pri.Id.Equals(listaPregleda[i].Id))
                        {

                            p1 = lekarGrid.SelectedItem as PrikazPregleda;
                            IzmeniINapraviTerminLekarViewModel vm = new IzmeniINapraviTerminLekarViewModel(p1, lekarTrenutni);
                            FormIzmeniTerminLekar ff = new FormIzmeniTerminLekar(vm);
                            break;
                        }
                    }
                }
                else if (objekat.GetType().Equals(prikazOperacije.GetType()))
                {
                    if (!lekarTrenutni.Specijalizacija.OblastMedicine.Equals("Opsta"))
                    {
                        PrikazOperacije pri = objekat as PrikazOperacije;
                        for (int i = 0; i < listaOperacija.Count; i++)
                        {
                            if (pri.Id.Equals(listaOperacija[i].Id))
                            {

                                op = lekarGrid.SelectedItem as PrikazOperacije;
                                IzmeniINapraviTerminLekarViewModel vm = new IzmeniINapraviTerminLekarViewModel(op, lekarTrenutni);
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


            }

        }






        public void InformacijeOPacijentu(DataGrid lekarGrid, PrikazPregleda prikazPregleda, List<Pregled> listaPregleda, PrikazOperacije prikazOperacije, List<Operacija> listaOperacija)
        {
            if (lekarGrid.SelectedCells.Count > 0)
            {

                var objekat = lekarGrid.SelectedValue;
                PrikazPregleda p1 = new PrikazPregleda();
                PrikazOperacije op = new PrikazOperacije();

                if (objekat.GetType().Equals(prikazPregleda.GetType()))
                {
                    PrikazPregleda pri = objekat as PrikazPregleda;
                    for (int i = 0; i < listaPregleda.Count; i++)
                    {
                        if (pri.Id.Equals(listaPregleda[i].Id))
                        {

                            p1 = lekarGrid.SelectedItem as PrikazPregleda;
                            InformacijeOPacijentuLekarViewModel vm = new InformacijeOPacijentuLekarViewModel(p1.Pacijent);
                            FormPrikazInformacijaOPacijentuLekar ff = new FormPrikazInformacijaOPacijentuLekar(vm);

                            break;
                        }
                    }
                }
                else if (objekat.GetType().Equals(prikazOperacije.GetType()))
                {
                    PrikazOperacije pri = objekat as PrikazOperacije;
                    for (int i = 0; i < listaOperacija.Count; i++)
                    {
                        if (pri.Id.Equals(listaOperacija[i].Id))
                        {
                            op = lekarGrid.SelectedItem as PrikazOperacije;
                            InformacijeOPacijentuLekarViewModel vm = new InformacijeOPacijentuLekarViewModel(op.Pacijent);
                            FormPrikazInformacijaOPacijentuLekar ff = new FormPrikazInformacijaOPacijentuLekar(vm);
                            break;
                        }
                    }
                }

            }
        }

        private void JumpOnButtonEnter(Button Zakazi)
        {

            Zakazi.Focus();

        }

        private void JumpOnButtonLeft(TabItem PreglediTab)
        {

            PreglediTab.Focus();

        }

        private void JumpOnButtonTab(DataGrid lekarGrid)
        {

            var row = lekarGrid.SelectedIndex;
            if (row < lekarGrid.Items.Count - 1)
            {
                row = row + 1;
                lekarGrid.SelectedIndex = row;


            }
            else
            {
                row = 0;
                lekarGrid.SelectedIndex = row;
            }

        }

        private void JumpOnButtonIstorijaEnter(Button AnamnezaIstorijaDugme)
        {

            AnamnezaIstorijaDugme.Focus();
        }
        private void JumpOnButtonIstorijaLeft(TabItem IstorijaTab)
        {

            IstorijaTab.Focus();
        }
        private void JumpOnButtonIstorijaTab(DataGrid lekarGridIstorija)
        {

            var row = lekarGridIstorija.SelectedIndex;
            if (row < lekarGridIstorija.Items.Count - 1)
            {
                row = row + 1;
                lekarGridIstorija.SelectedIndex = row;


            }
            else
            {
                row = 0;
                lekarGridIstorija.SelectedIndex = row;
            }
        }

        public void CollorLekarGrid(DataGrid lekarGrid)
        {
            DateTime trenutni = new DateTime();
            int dozvola = 0;
            PrikazPregleda preg = new PrikazPregleda();
            PrikazOperacije oper = new PrikazOperacije();

            for (int i = 0; i < lekarGrid.Items.Count; i++)
            {

                var row = (DataGridRow)lekarGrid.ItemContainerGenerator.ContainerFromIndex(i);
                if (row != null)
                {


                    var Objekat = row.Item;
                    if (Objekat.GetType().Equals(preg.GetType()))
                    {
                        preg = Objekat as PrikazPregleda;
                        if (trenutni.Date != preg.Datum.Date)
                        {
                            trenutni = preg.Datum;
                            dozvola++;
                            if (dozvola == 2)
                            {
                                dozvola = 0;
                            }
                        }
                    }
                    else if (Objekat.GetType().Equals(oper.GetType()))
                    {
                        oper = Objekat as PrikazOperacije;
                        if (trenutni.Date != oper.Datum.Date)
                        {
                            trenutni = oper.Datum;
                            dozvola++;
                            if (dozvola == 2)
                            {
                                dozvola = 0;
                            }
                        }
                    }

                    if (dozvola == 0)
                    {
                        row.Foreground = Brushes.Black;
                    }
                    else if (dozvola == 1)
                    {
                        row.Foreground = Brushes.DarkViolet;
                    }



                }
            }

        }

        public void CollorLekarGridIstorija(DataGrid lekarGridIstorija)
        {

            DateTime trenutni = new DateTime();
            int dozvola = 0;
            PrikazPregleda preg = new PrikazPregleda();
            PrikazOperacije oper = new PrikazOperacije();

            for (int i = 0; i < lekarGridIstorija.Items.Count + 1; i++)
            {

                var row = (DataGridRow)lekarGridIstorija.ItemContainerGenerator.ContainerFromIndex(i);
                if (row != null)
                {


                    var Objekat = row.Item;
                    if (Objekat.GetType().Equals(preg.GetType()))
                    {
                        preg = Objekat as PrikazPregleda;
                        if (trenutni.Date != preg.Datum.Date)
                        {
                            trenutni = preg.Datum;
                            dozvola++;
                            if (dozvola == 2)
                            {
                                dozvola = 0;
                            }
                        }
                    }
                    else if (Objekat.GetType().Equals(oper.GetType()))
                    {
                        oper = Objekat as PrikazOperacije;
                        if (trenutni.Date != oper.Datum.Date)
                        {
                            trenutni = oper.Datum;
                            dozvola++;
                            if (dozvola == 2)
                            {
                                dozvola = 0;
                            }
                        }
                    }

                    if (dozvola == 0)
                    {
                        row.Foreground = Brushes.Black;
                    }
                    else if (dozvola == 1)
                    {
                        row.Foreground = Brushes.DarkViolet;
                    }



                }
            }
        }




        private void focusTab(TabItem PreglediTab)
        {
            PreglediTab.Focus();
        }


        public void Anamneza(DataGrid lekarGrid, PrikazPregleda prikazPregleda, List<Pregled> listaPregleda, PrikazOperacije prikazOperacije, List<Operacija> listaOperacija,Lekar lekarTrenutni)
        {
            if (lekarGrid.SelectedCells.Count > 0)
            {

                var objekat = lekarGrid.SelectedValue;
                PrikazPregleda p1 = new PrikazPregleda();
                PrikazOperacije op = new PrikazOperacije();

                if (objekat.GetType().Equals(prikazPregleda.GetType()))
                {
                    PrikazPregleda pri = objekat as PrikazPregleda;
                    if (pri.Datum < DateTime.Now)
                    {
                        for (int i = 0; i < listaPregleda.Count; i++)
                        {
                            if (pri.Id.Equals(listaPregleda[i].Id))
                            {

                                p1 = lekarGrid.SelectedItem as PrikazPregleda;
                                NapraviAnamnezuLekarViewModel vm = new NapraviAnamnezuLekarViewModel(p1, lekarTrenutni);
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
                if (objekat.GetType().Equals(prikazOperacije.GetType()))
                {
                    PrikazOperacije pri = objekat as PrikazOperacije;
                    if (pri.Datum < DateTime.Now)
                    {
                        for (int i = 0; i < listaOperacija.Count; i++)
                        {
                            if (pri.Id.Equals(listaOperacija[i].Id))
                            {

                                op = lekarGrid.SelectedItem as PrikazOperacije;

                                NapraviAnamnezuLekarViewModel vm = new NapraviAnamnezuLekarViewModel(op, lekarTrenutni);
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

            }
        }

        public void AnamnezaIstorija(DataGrid lekarGridIstorija, PrikazPregleda prikazPregleda, List<Pregled> listaPregleda, PrikazOperacije prikazOperacije, List<Operacija> listaOperacija, Lekar lekarTrenutni)
        {
            if (lekarGridIstorija.SelectedCells.Count > 0)
            {

                var objekat = lekarGridIstorija.SelectedValue;
                PrikazPregleda p1 = new PrikazPregleda();
                PrikazOperacije op = new PrikazOperacije();

                if (objekat.GetType().Equals(prikazPregleda.GetType()))
                {
                    PrikazPregleda pri = objekat as PrikazPregleda;

                    for (int i = 0; i < listaPregleda.Count; i++)
                    {
                        if (pri.Id.Equals(listaPregleda[i].Id))
                        {

                            p1 = lekarGridIstorija.SelectedItem as PrikazPregleda;
                            NapraviAnamnezuLekarViewModel vm = new NapraviAnamnezuLekarViewModel(p1, lekarTrenutni);
                            FormNapraviAnamnezuLekar form = new FormNapraviAnamnezuLekar(vm);
                            break;
                        }
                    }

                }
                if (objekat.GetType().Equals(prikazOperacije.GetType()))
                {
                    PrikazOperacije pri = objekat as PrikazOperacije;

                    for (int i = 0; i < listaOperacija.Count; i++)
                    {
                        if (pri.Id.Equals(listaOperacija[i].Id))
                        {

                            op = lekarGridIstorija.SelectedItem as PrikazOperacije;
                            NapraviAnamnezuLekarViewModel vm = new NapraviAnamnezuLekarViewModel(op, lekarTrenutni);
                            FormNapraviAnamnezuLekar form = new FormNapraviAnamnezuLekar(vm);
                            break;
                        }
                    }

                }


            }
        }

        public void IzmeniLek(DataGrid dataGridLekovi,List<Lek> lekovi)
        {
            PrikazLek p = dataGridLekovi.SelectedItem as PrikazLek;
            lekovi = sviLekovi.GetAll();

            for (int i = 0; i < lekovi.Count; i++)
            {
                if (lekovi[i].Status.Equals(StatusLeka.odbijen) || lekovi[i].Obrisan)
                {
                    lekovi.RemoveAt(i);
                    i--;
                }
            }
            for (int i = 0; i < lekovi.Count; i++)
            {
                if (lekovi[i].Id.Equals(p.Id))
                {
                    IzmeniLekLekarViewModel vm = new IzmeniLekLekarViewModel(lekovi[i]);
                    FormIzmeniLekLekar form = new FormIzmeniLekLekar(vm);
                    form.Show();
                    break;

                }
            }
        }

        private void JumpOnButtonLekEnter(Button Odobri)
        {

            Odobri.Focus();

        }
        private void JumpOnButtonLekLeft(TabItem lekTab)
        {

            lekTab.Focus();

        }
        private void JumpOnButtonLekTab(DataGrid dataGridLekovi)
        {

            var row = dataGridLekovi.SelectedIndex;
            if (row < dataGridLekovi.Items.Count - 1)
            {
                row = row + 1;
                dataGridLekovi.SelectedIndex = row;


            }
            else
            {
                row = 0;
                dataGridLekovi.SelectedIndex = row;
            }

        }
   
        public void OdobriLek(DataGrid dataGridLekovi, List<Lek> lekovi,List<PrikazLek> lekoviPrikaz)
        {
            PrikazLek p = dataGridLekovi.SelectedItem as PrikazLek;

            if (p.Status.Equals(StatusLeka.cekaValidaciju))
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
                for (int i = 0; i < lekovi.Count; i++)
                {
                    if (lekovi[i].Id.Equals(p.Id))
                    {
                        lekovi[i].Status = StatusLeka.odobren;
                        sviLekovi.Delete(lekovi[i]);
                        sviLekovi.Save(lekovi[i]);
                        break;

                    }
                }
                for (int i = 0; i < lekoviPrikaz.Count; i++)
                {
                    if (lekoviPrikaz[i].Id.Equals(p.Id))
                    {
                        lekoviPrikaz[i].Status = StatusLeka.odobren;
                        dataGridLekovi.Items.Refresh();
                        break;

                    }
                }
                Obavestenje obavestenje = new Obavestenje();
                FileStorageObavestenja svaObavestenja = new FileStorageObavestenja();
                FileStorageKorisnici sviKorisnici = new FileStorageKorisnici();
                List<Korisnik> svi = sviKorisnici.GetAll();
                List<Obavestenje> sva = svaObavestenja.GetAll();
                int max = 0;
                for (int i = 0; i < sva.Count; i++)
                {
                    if (max < sva[i].Id)
                    {
                        max = sva[i].Id;
                    }
                }
                max = max + 1;
                obavestenje.Id = max;

                for (int i = 0; i < svi.Count; i++)
                {
                    if (svi[i].TipKorisnika.Equals(TipKorisnika.upravnik))
                    {
                        obavestenje.Korisnici.Add(svi[i]);
                    }
                }
                obavestenje.Naslov = "Lek " + p.Naziv + " je prihvacen";
                obavestenje.Obrisan = false;
                obavestenje.Sadrzaj = "Lek " + p.Naziv + " sa dozom " + p.KolicinaUMg + " i sastojcima: " + p.Sastojak + " je prihvacen. ";
                obavestenje.Datum = DateTime.Now;
                FileStorageObavestenja oba = new FileStorageObavestenja();
                oba.Save(obavestenje);


            }
            else
            {
                MessageBox.Show("Lek je vec odobren");
            }
        }

        public void VratiNaIzmenu(DataGrid dataGridLekovi)
        {
            PrikazLek p = dataGridLekovi.SelectedItem as PrikazLek;
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
