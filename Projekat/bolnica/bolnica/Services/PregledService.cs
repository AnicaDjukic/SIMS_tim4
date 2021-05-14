using Bolnica.Forms;
using Bolnica.Model.Korisnici;
using Bolnica.Model.Pregledi;
using Bolnica.Model.Prostorije;
using Model.Korisnici;
using Model.Pacijenti;
using Model.Pregledi;
using Model.Prostorije;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Bolnica.Services
{
    public class PregledService
    {
        private FileStoragePacijenti sviPacijenti = new FileStoragePacijenti();
        private FileStorageProstorija sveProstorije = new FileStorageProstorija();
        private FileStoragePregledi sviPregledi = new FileStoragePregledi();
        private FileStorageLekar sviLekari = new FileStorageLekar();
        
        private bool naRenoviranju(Prostorija p,DateTime datumB)
        {
            FileStorageRenoviranje storageRenoviranje = new FileStorageRenoviranje();
            foreach (Renoviranje r in storageRenoviranje.GetAll())
            {
                if (r.Prostorija.BrojProstorije == p.BrojProstorije)
                {
                    if (r.PocetakRenoviranja.Date <= datumB && datumB <= r.KrajRenoviranja.Date)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    
        public void PotvrdiIzmenu(string datumB,string vremeB,string trajanjeB,List<Lekar> lekariTrenutni,string lekarB,List<Pacijent> pacijentiZa, string prezimeB,List<Prostorija> prostorijaZa,bool ItemSourceDaLiJeOperacija,TipOperacije tipOper,bool ItemSourceDaLiJeHitan,string prostorijaB,Lekar ulogovaniLekar)
        {
            

            if (CheckFields())
            {

                bool ope = false;
                PrikazPregleda trenutniPregled = new PrikazPregleda();
                PrikazOperacije trenutnaOperacija = new PrikazOperacije();
                trenutnaOperacija.Zavrsen = false;
                trenutnaOperacija.Datum = DateTime.Parse(datumB + TimeSpan.Parse(vremeB));

                trenutnaOperacija.Trajanje = int.Parse(trajanjeB);
                trenutniPregled.Datum = DateTime.Parse(datumB + TimeSpan.Parse(vremeB));

                trenutniPregled.Trajanje = int.Parse(trajanjeB);
                trenutniPregled.Zavrsen = false;

                for (int le = 0; le < lekariTrenutni.Count; le++)
                {
                    string s = lekariTrenutni[le].Prezime + ' ' + lekariTrenutni[le].Ime + ' ' + lekariTrenutni[le].Jmbg;
                    if (s.Equals(lekarB))
                    {
                        trenutnaOperacija.Lekar = lekariTrenutni[le];
                        trenutniPregled.Lekar = lekariTrenutni[le];
                    }
                }


                for (int i = 0; i < pacijentiZa.Count; i++)
                {
                    string s;
                    s = pacijentiZa[i].Prezime + ' ' + pacijentiZa[i].Ime + ' ' + pacijentiZa[i].Jmbg;


                    if (s.Equals(prezimeB))
                    {
                        trenutnaOperacija.Pacijent = pacijentiZa[i];
                        trenutniPregled.Pacijent = pacijentiZa[i];
                        break;

                    }
                }
                for (int pp = 0; pp < prostorijaZa.Count; pp++)
                {
                    if (prostorijaZa[pp].BrojProstorije.ToString().Equals(prostorijaB))
                    {
                        trenutnaOperacija.Prostorija = prostorijaZa[pp];
                        trenutniPregled.Prostorija = prostorijaZa[pp];
                        break;
                    }
                }
                if (ItemSourceDaLiJeOperacija.Equals(true))
                {
                    if (tipOper.Equals(TipOperacije.prvaKat))
                    {
                        ope = true;
                        trenutnaOperacija.TipOperacije = TipOperacije.prvaKat;
                    }
                    else if (tipOper.Equals(TipOperacije.drugaKat))
                    {
                        ope = true;
                        trenutnaOperacija.TipOperacije = TipOperacije.drugaKat;
                    }
                    else if (tipOper.Equals(TipOperacije.trecaKat))
                    {
                        ope = true;
                        trenutnaOperacija.TipOperacije = TipOperacije.trecaKat;
                    }

                }
                if (ope)
                {
                    List<Operacija> zaId = new List<Operacija>();
                    zaId = sviPregledi.GetAllOperacije();
                    int max = 0;
                    for (int i = 0; i < zaId.Count; i++)
                    {
                        if (zaId[i].Id > max)
                            max = zaId[i].Id;
                    }
                    trenutnaOperacija.Id = max + 1;
                    Operacija o = new Operacija();
                    trenutnaOperacija.Anamneza.Id = -1;
                    trenutnaOperacija.Hitan = ItemSourceDaLiJeHitan;
                    o.Id = trenutnaOperacija.Id;
                    o.Hitan = ItemSourceDaLiJeHitan;
                    o.Lekar = trenutnaOperacija.Lekar;
                    o.Pacijent = trenutnaOperacija.Pacijent;
                    o.TipOperacije = trenutnaOperacija.TipOperacije;
                    o.Trajanje = trenutnaOperacija.Trajanje;
                    o.Zavrsen = trenutnaOperacija.Zavrsen;
                    o.Anamneza.Id = -1;
                    o.Prostorija = trenutnaOperacija.Prostorija;
                    o.Datum = trenutnaOperacija.Datum;
                    if (ulogovaniLekar.Mbr.Equals(trenutnaOperacija.Lekar.Mbr))
                    {

                        FormLekar.listaOperacija.Add(o);
                        FormLekar.dataList.Items.Add(trenutnaOperacija);
                        FormLekar.data();
                    }
                    sviPregledi.Save(o);

                }
                else
                {
                    List<Pregled> zaId = new List<Pregled>();
                    zaId = sviPregledi.GetAllPregledi();
                    int max = 0;
                    for (int i = 0; i < zaId.Count; i++)
                    {
                        if (zaId[i].Id > max)
                            max = zaId[i].Id;
                    }
                    trenutniPregled.Id = max + 1;
                    Pregled o = new Pregled();
                    trenutniPregled.Anamneza.Id = -1;
                    trenutniPregled.Hitan = false;
                    o.Id = trenutniPregled.Id;
                    o.Hitan = false;
                    o.Lekar = trenutniPregled.Lekar;
                    o.Pacijent = trenutniPregled.Pacijent;
                    o.Trajanje = trenutniPregled.Trajanje;
                    o.Zavrsen = trenutniPregled.Zavrsen;
                    o.Anamneza.Id = -1;
                    o.Prostorija = trenutniPregled.Prostorija;
                    o.Datum = trenutniPregled.Datum;
                    if (ulogovaniLekar.Mbr.Equals(trenutniPregled.Lekar.Mbr))
                    {

                        FormLekar.listaPregleda.Add(o);
                        FormLekar.dataList.Items.Add(trenutniPregled);
                        FormLekar.data();
                    }
                    sviPregledi.Save(o);

                }
               

            }
        }
    
       
        public bool PostojiLekar(string specijalizacija, string lekarB)
        {

            FileStorageLekar ProveraL = new FileStorageLekar();
            List<Lekar> lekari = new List<Lekar>();
            lekari = ProveraL.GetAll();
            for (int i = 0; i < lekari.Count; i++)
            {
                string ss = lekari[i].Prezime + ' ' + lekari[i].Ime + ' ' + lekari[i].Jmbg;
                if (lekari[i].Specijalizacija.OblastMedicine.Equals(specijalizacija) && ss.Equals(lekarB))
                {
                    return true;
                }

            }
            return false;

        }

        public bool LekarSlobodanUToVreme(string lekarB, DateTime datumB, string trajanjeB, string vremeB)
        {
            FileStorageLekar ProveraL = new FileStorageLekar();
            List<Lekar> lekari = new List<Lekar>();
            lekari = ProveraL.GetAll();
            List<TimeSpan> zauzetiTermini = new List<TimeSpan>();
            List<Pregled> preglediLekara1 = sviPregledi.GetAllPregledi();
            List<Operacija> operacijeLekara1 = sviPregledi.GetAllOperacije();
            for (int lek = 0; lek < lekari.Count; lek++)
            {
                string ss = lekari[lek].Prezime + ' ' + lekari[lek].Ime + ' ' + lekari[lek].Jmbg;
                if (ss.Equals(lekarB) && lekari[lek].Specijalizacija.OblastMedicine != null)
                {


                    List<Lekar> lekar = new List<Lekar>();
                    lekar = sviLekari.GetAll();
                    string jmbgLekar = "";
                    for (int l = 0; l < lekar.Count; l++)
                    {
                        string pp = lekar[l].Prezime + ' ' + lekar[l].Ime + ' ' + lekar[l].Jmbg;
                        if (pp.Equals(lekarB))
                        {
                            jmbgLekar = lekar[l].Jmbg;
                            break;
                        }
                    }
                    for (int da = 0; da < preglediLekara1.Count; da++)
                    {
                        if (!preglediLekara1[da].Lekar.Jmbg.Equals(jmbgLekar))
                        {
                            preglediLekara1.RemoveAt(da);
                            da = da - 1;
                        }
                    }
                    for (int ad = 0; ad < operacijeLekara1.Count; ad++)
                    {
                        if (!operacijeLekara1[ad].Lekar.Jmbg.Equals(jmbgLekar))
                        {
                            operacijeLekara1.RemoveAt(ad);
                            ad = ad - 1;
                        }
                    }
                    for (int pre = 0; pre < preglediLekara1.Count; pre++)
                    {
                        if (preglediLekara1[pre].Datum.Date.Equals(datumB.Date))
                        {
                            string[] div = preglediLekara1[pre].Datum.ToString().Split(" ");
                            string v = div[1];
                            TimeSpan pocetni = TimeSpan.Parse(v);
                            for (int jos = 0; jos <= preglediLekara1[pre].Trajanje; jos++)
                            {
                                TimeSpan dodatni = new TimeSpan(0, jos, 0);
                                zauzetiTermini.Add(pocetni + dodatni);
                            }
                        }
                    }
                    for (int ope = 0; ope < operacijeLekara1.Count; ope++)
                    {
                        if (operacijeLekara1[ope].Datum.Date.Equals(datumB.Date))
                        {
                            string[] div = operacijeLekara1[ope].Datum.ToString().Split(" ");
                            string v = div[1];
                            TimeSpan pocetni = TimeSpan.Parse(v);
                            for (int jos = 0; jos <= operacijeLekara1[ope].Trajanje; jos++)
                            {
                                TimeSpan dodatni = new TimeSpan(0, jos, 0);
                                zauzetiTermini.Add(pocetni + dodatni);
                            }
                        }



                    }

                }
            }
           
            for (int mek = 0; mek < int.Parse(trajanjeB); mek++)
            {
                TimeSpan jes = new TimeSpan(0, mek, 0);
                if (zauzetiTermini.Contains(TimeSpan.Parse(vremeB) + jes))
                {
                    return false;
                }

            }
            return true;

        }

        public bool PacijentSlobodanUToVreme(List<Pacijent> pacijentiZa, string prezimeB, DateTime datumB, string trajanjeB, string vremeB)
        {
            FileStoragePacijenti ProveraP = new FileStoragePacijenti();
            List<Pacijent> pacijenti = sviPacijenti.GetAll();
            List<TimeSpan> zauzetiTermini = new List<TimeSpan>();
            List<Pregled> preglediPacijenta1 = sviPregledi.GetAllPregledi();
            List<Operacija> operacijePacijenta1 = sviPregledi.GetAllOperacije();
            for (int lek = 0; lek < pacijenti.Count; lek++)
            {
                string s;
                s = pacijentiZa[lek].Prezime + ' ' + pacijentiZa[lek].Ime + ' ' + pacijentiZa[lek].Jmbg;


                if (s.Equals(prezimeB))
                {


                    List<Pacijent> pacijent = new List<Pacijent>();
                    pacijent = sviPacijenti.GetAll();
                    string jmbgPacijent = "";
                    for (int l = 0; l < pacijent.Count; l++)
                    {
                        string d;
                        d = pacijent[l].Prezime + ' ' + pacijent[l].Ime + ' ' + pacijent[l].Jmbg;

                        if (d.Equals(prezimeB))
                        {
                            jmbgPacijent = pacijent[l].Jmbg;
                            break;
                        }
                    }
                    for (int da = 0; da < preglediPacijenta1.Count; da++)
                    {
                        if (!preglediPacijenta1[da].Pacijent.Jmbg.Equals(jmbgPacijent))
                        {
                            preglediPacijenta1.RemoveAt(da);
                            da = da - 1;
                        }
                    }
                    for (int ad = 0; ad < operacijePacijenta1.Count; ad++)
                    {
                        if (!operacijePacijenta1[ad].Pacijent.Jmbg.Equals(jmbgPacijent))
                        {
                            operacijePacijenta1.RemoveAt(ad);
                            ad = ad - 1;
                        }
                    }
                    for (int pre = 0; pre < preglediPacijenta1.Count; pre++)
                    {
                        if (preglediPacijenta1[pre].Datum.Date.Equals(datumB.Date))
                        {
                            string[] div = preglediPacijenta1[pre].Datum.ToString().Split(" ");
                            string v = div[1];
                            TimeSpan pocetni = TimeSpan.Parse(v);
                            for (int jos = 0; jos <= preglediPacijenta1[pre].Trajanje; jos++)
                            {
                                TimeSpan dodatni = new TimeSpan(0, jos, 0);
                                zauzetiTermini.Add(pocetni + dodatni);
                            }
                        }
                    }
                    for (int ope = 0; ope < operacijePacijenta1.Count; ope++)
                    {
                        if (operacijePacijenta1[ope].Datum.Date.Equals(datumB.Date))
                        {
                            string[] div = operacijePacijenta1[ope].Datum.ToString().Split(" ");
                            string v = div[1];
                            TimeSpan pocetni = TimeSpan.Parse(v);
                            for (int jos = 0; jos <= operacijePacijenta1[ope].Trajanje; jos++)
                            {
                                TimeSpan dodatni = new TimeSpan(0, jos, 0);
                                zauzetiTermini.Add(pocetni + dodatni);
                            }
                        }



                    }

                }

            }
            
            for (int mek = 0; mek < int.Parse(trajanjeB); mek++)
            {
                TimeSpan jes = new TimeSpan(0, mek, 0);
                if (zauzetiTermini.Contains(TimeSpan.Parse(vremeB) + jes))
                {
                    return false;
                }
            }
            return true;


        }

        public bool PostojiProstorija(List<Prostorija> prostorijaZa, string prostorijaB)
        {

            for (int i = 0; i < prostorijaZa.Count; i++)
            {

                if (prostorijaZa[i].BrojProstorije.ToString().Equals(prostorijaB) && !prostorijaZa[i].Obrisana)
                {
                    return true;
                }

            }
            return false;
        }

        public bool ProstorijaSlobodna(List<Prostorija> prostorijaZa, string prostorijaB, DateTime datumB)
        {
            for (int i = 0; i < prostorijaZa.Count; i++)
            {

                if (prostorijaZa[i].BrojProstorije.ToString().Equals(prostorijaB) && !prostorijaZa[i].Obrisana)
                {
                    if (naRenoviranju(prostorijaZa[i],datumB))
                    {
                        return false;
                    }
                    if (prostorijaZa[i].Zauzeta)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }

            }
            return false;

        }

        public bool CheckFields()
        {
            return true;
        }

       
        public List<TimeSpan> filterLekar(string zaFilLek, List<TimeSpan> vremeB, List<Lekar> lekariTrenutni, string lekarB, DateTime datumB)
        {
            if (zaFilLek != lekarB)
            {
                vremeB = new List<TimeSpan>();
            for (int vre = 0; vre < 24; vre++)
            {
                for (int min = 0; min < 59;)
                {
                    TimeSpan ts = new TimeSpan(vre, min, 0);
                    min = min + 15;
                    vremeB.Add(ts);
                }

            }


                for (int lek = 0; lek < lekariTrenutni.Count; lek++)
                {
                    string h = lekariTrenutni[lek].Prezime + ' ' + lekariTrenutni[lek].Ime + ' ' + lekariTrenutni[lek].Jmbg;
                    if (h.Equals(lekarB) && lekariTrenutni[lek].Specijalizacija.OblastMedicine != null)
                    {

                        List<TimeSpan> zauzetiTermini = new List<TimeSpan>();
                        List<Pregled> preglediLekara = sviPregledi.GetAllPregledi();
                        List<Operacija> operacijeLekara = sviPregledi.GetAllOperacije();
                        List<Lekar> lekar = new List<Lekar>();
                        lekar = lekariTrenutni;
                        string jmbgLekar = "";
                        for (int l = 0; l < lekar.Count; l++)
                        {
                            string hh = lekar[l].Prezime + ' ' + lekar[l].Ime + ' ' + lekar[l].Jmbg;
                            if (hh.Equals(lekarB))
                            {
                                jmbgLekar = lekar[l].Jmbg;
                                break;
                            }
                        }
                        for (int da = 0; da < preglediLekara.Count; da++)
                        {
                            if (!preglediLekara[da].Lekar.Jmbg.Equals(jmbgLekar))
                            {
                                preglediLekara.RemoveAt(da);
                                da = da - 1;
                            }
                        }
                        for (int ad = 0; ad < operacijeLekara.Count; ad++)
                        {
                            if (!operacijeLekara[ad].Lekar.Jmbg.Equals(jmbgLekar))
                            {
                                operacijeLekara.RemoveAt(ad);
                                ad = ad - 1;
                            }
                        }
                        for (int pre = 0; pre < preglediLekara.Count; pre++)
                        {
                            if (preglediLekara[pre].Datum.Date.Equals(datumB.Date))
                            {
                                string[] div = preglediLekara[pre].Datum.ToString().Split(" ");
                                string v = div[1];
                                TimeSpan pocetni = TimeSpan.Parse(v);
                                for (int jos = 0; jos <= preglediLekara[pre].Trajanje; jos++)
                                {
                                    TimeSpan dodatni = new TimeSpan(0, jos, 0);
                                    zauzetiTermini.Add(pocetni + dodatni);
                                }
                            }
                        }
                        for (int ope = 0; ope < operacijeLekara.Count; ope++)
                        {
                            if (operacijeLekara[ope].Datum.Date.Equals(datumB.Date))
                            {
                                string[] div = operacijeLekara[ope].Datum.ToString().Split(" ");
                                string v = div[1];
                                TimeSpan pocetni = TimeSpan.Parse(v);
                                for (int jos = 0; jos <= operacijeLekara[ope].Trajanje; jos++)
                                {
                                    TimeSpan dodatni = new TimeSpan(0, jos, 0);
                                    zauzetiTermini.Add(pocetni + dodatni);
                                }
                            }



                        }

                        for (int tm = 0; tm < zauzetiTermini.Count; tm++)
                        {
                            vremeB.Remove(zauzetiTermini[tm]);
                        }



                        break;
                    }
                   
                }
                return vremeB;

            }
            return null;
        }
        

       

       

        public List<string> DatumDateKey(DateTime zaFilLekDat,DateTime datumB,List<string> ItemSourceProstorijaB,List<Prostorija> prostorijaZa)
        {
                if (zaFilLekDat != datumB)
                {
                    zaFilLekDat = datumB;
                    ItemSourceProstorijaB = new List<string>();
                    for (int pr = 0; pr < prostorijaZa.Count; pr++)
                    {
                        if (prostorijaZa[pr].Obrisana == false && prostorijaZa[pr].Zauzeta == false && prostorijaZa[pr].TipProstorije.Equals(TipProstorije.salaZaPreglede) && !naRenoviranju(prostorijaZa[pr],datumB))
                        {
                            ItemSourceProstorijaB.Add(prostorijaZa[pr].BrojProstorije);
                        }
                    }
                return ItemSourceProstorijaB;
                }
            return null;
            
        }
        
        public string LekarComboOpenTab(string zaFilLek, string lekarB, List<Lekar> lekariTrenutni, string specijalizacija)
        {
            
                if (zaFilLek != lekarB)
                {
                    zaFilLek = lekarB;
                    for (int le = 0; le < lekariTrenutni.Count; le++)
                    {
                        string h = lekariTrenutni[le].Prezime + ' ' + lekariTrenutni[le].Ime + ' ' + lekariTrenutni[le].Jmbg;
                        if (h.Equals(lekarB))
                        {
                            specijalizacija = lekariTrenutni[le].Specijalizacija.OblastMedicine;
                        }

                    }
                return specijalizacija;

                }
            return null;
            

            

            
        }

        public List<string> SpecijalizacijaComboOpenTab(List<string> specijalizacije, String specijalizacija, List<Lekar> lekariTrenutni)
        {

            if (specijalizacije.Contains(specijalizacija))
            {
                List<string> noviLekari = new List<string>();
                for (int le = 0; le < lekariTrenutni.Count; le++)
                {
                    if (lekariTrenutni[le].Specijalizacija.OblastMedicine.Equals(specijalizacija))
                    {
                        string ss = lekariTrenutni[le].Prezime + ' ' + lekariTrenutni[le].Ime + ' ' + lekariTrenutni[le].Jmbg;
                        noviLekari.Add(ss);
                    }
                }
                return noviLekari;
            }
            return null;

        }
        /*

        
        
        private void isAkcelerator(object sender, KeyEventArgs e)
        {
            if (e.KeyboardDevice.IsKeyDown(Key.LeftCtrl) || e.KeyboardDevice.IsKeyDown(Key.RightCtrl))
            {
                switch (e.Key)
                {
                    case Key.Q:
                        PotvrdiIzmenu();
                        break;
                    case Key.W:
                        OtkaziIzmenu();
                        break;


                }
            }
        }*/
        
    }
}
