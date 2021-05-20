using Bolnica.Forms;
using Bolnica.Model.Korisnici;
using Bolnica.Model.Pregledi;
using Bolnica.Model.Prostorije;
using Bolnica.ViewModel;
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
    public class IzmeniINapraviTerminLekarService
    {
        private FileStoragePacijenti skladistePacijenti = new FileStoragePacijenti();
        private FileStorageProstorija skladisteProstorije = new FileStorageProstorija();
        private FileStoragePregledi skladistePregledi = new FileStoragePregledi();
        private FileStorageLekar skladisteLekari = new FileStorageLekar();
        private FileStorageRenoviranje skladisteRenoviranja = new FileStorageRenoviranje();

        private bool naRenoviranju(Prostorija p, DateTime datumB)
        {
            foreach (Renoviranje r in skladisteRenoviranja.GetAll())
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


         public void PotvrdiIzmenuIzmenu(string datumB, string vremeB, string trajanjeB, List<Lekar> lekariTrenutni, string lekarB, List<Pacijent> pacijentiZa, string prezimeB, List<Prostorija> prostorijaZa, bool ItemSourceDaLiJeOperacija, TipOperacije tipOper, bool ItemSourceDaLiJeHitan, string prostorijaB, Lekar ulogovaniLekar,PrikazOperacije staraOperacija, PrikazPregleda stariPregled) {
            bool ope = false;
            if (CheckFields())
            {

                ope = false;
                PrikazPregleda trenutniPregled = new PrikazPregleda();
                PrikazOperacije trenutnaOperacija = new PrikazOperacije();
                trenutnaOperacija.Zavrsen = false;
                trenutnaOperacija.Datum = DateTime.Parse(datumB + TimeSpan.Parse(vremeB));
                trenutnaOperacija.Id = staraOperacija.Id;
                trenutnaOperacija.Anamneza = staraOperacija.Anamneza;
                trenutnaOperacija.Trajanje = int.Parse(trajanjeB);
                trenutnaOperacija.Hitan = ItemSourceDaLiJeHitan;
                trenutniPregled.Datum = DateTime.Parse(datumB + TimeSpan.Parse(vremeB));
                trenutniPregled.Id = stariPregled.Id;
                trenutniPregled.Anamneza = stariPregled.Anamneza;
                trenutniPregled.Trajanje = int.Parse(trajanjeB);
                trenutniPregled.Zavrsen = false;
                trenutniPregled.Hitan = ItemSourceDaLiJeHitan;

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



                    for (int i = 0; i < LekarViewModel.listaOperacija.Count; i++)
                    {
                        if (LekarViewModel.listaOperacija[i].Id.Equals(staraOperacija.Id))
                        {
                            if (ulogovaniLekar.Mbr.Equals(trenutnaOperacija.Lekar.Mbr))
                            {
                                LekarViewModel.listaOperacija[i].Id = trenutnaOperacija.Id;
                                LekarViewModel.listaOperacija[i].Hitan = trenutnaOperacija.Hitan;
                                LekarViewModel.listaOperacija[i].Lekar = trenutnaOperacija.Lekar;
                                LekarViewModel.listaOperacija[i].Pacijent = trenutnaOperacija.Pacijent;
                                LekarViewModel.listaOperacija[i].TipOperacije = trenutnaOperacija.TipOperacije;
                                LekarViewModel.listaOperacija[i].Trajanje = trenutnaOperacija.Trajanje;
                                LekarViewModel.listaOperacija[i].Zavrsen = trenutnaOperacija.Zavrsen;
                                LekarViewModel.listaOperacija[i].Anamneza = trenutnaOperacija.Anamneza;
                                LekarViewModel.listaOperacija[i].Prostorija = trenutnaOperacija.Prostorija;
                                LekarViewModel.listaOperacija[i].Datum = trenutnaOperacija.Datum;

                            }
                            else
                            {
                                LekarViewModel.listaOperacija.RemoveAt(i);
                            }

                        }
                    }
                    for (int p = 0; p < LekarViewModel.podaciLista.Items.Count; p++)
                    {
                        if (LekarViewModel.podaciLista.Items[p].Equals(staraOperacija))
                        {
                            if (ulogovaniLekar.Mbr.Equals(trenutnaOperacija.Lekar.Mbr))
                            {
                                LekarViewModel.podaciLista.Items[p] = trenutnaOperacija;
                                LekarViewModel.RefreshPodaciListu();
                                Operacija o = new Operacija();
                                o.Id = trenutnaOperacija.Id;
                                o.Hitan = trenutnaOperacija.Hitan;
                                o.Lekar = trenutnaOperacija.Lekar;
                                o.Pacijent = trenutnaOperacija.Pacijent;
                                o.TipOperacije = trenutnaOperacija.TipOperacije;
                                o.Trajanje = trenutnaOperacija.Trajanje;
                                o.Zavrsen = trenutnaOperacija.Zavrsen;
                                o.Anamneza = trenutnaOperacija.Anamneza;
                                o.Prostorija = trenutnaOperacija.Prostorija;
                                o.Datum = trenutnaOperacija.Datum;

                                skladistePregledi.Izmeni(o);
                            }
                            else
                            {
                                LekarViewModel.podaciLista.Items.RemoveAt(p);
                                LekarViewModel.RefreshPodaciListu();
                                Operacija o = new Operacija();
                                o.Id = trenutnaOperacija.Id;
                                o.Hitan = trenutnaOperacija.Hitan;
                                o.Lekar = trenutnaOperacija.Lekar;
                                o.Pacijent = trenutnaOperacija.Pacijent;
                                o.TipOperacije = trenutnaOperacija.TipOperacije;
                                o.Trajanje = trenutnaOperacija.Trajanje;
                                o.Zavrsen = trenutnaOperacija.Zavrsen;
                                o.Anamneza = trenutnaOperacija.Anamneza;
                                o.Prostorija = trenutnaOperacija.Prostorija;
                                o.Datum = trenutnaOperacija.Datum;

                                skladistePregledi.Izmeni(o);
                            }

                        }
                    }
                }
                else
                {


                    for (int i = 0; i < LekarViewModel.listaPregleda.Count; i++)
                    {
                        if (LekarViewModel.listaPregleda[i].Equals(stariPregled))
                        {
                            if (ulogovaniLekar.Mbr.Equals(trenutniPregled.Lekar.Mbr))
                            {
                                LekarViewModel.listaPregleda[i].Id = trenutniPregled.Id;
                                LekarViewModel.listaPregleda[i].Hitan = trenutniPregled.Hitan;
                                LekarViewModel.listaPregleda[i].Lekar = trenutniPregled.Lekar;
                                LekarViewModel.listaPregleda[i].Pacijent = trenutniPregled.Pacijent;
                                LekarViewModel.listaPregleda[i].Trajanje = trenutniPregled.Trajanje;
                                LekarViewModel.listaPregleda[i].Zavrsen = trenutniPregled.Zavrsen;
                                LekarViewModel.listaPregleda[i].Anamneza = trenutniPregled.Anamneza;
                                LekarViewModel.listaPregleda[i].Prostorija = trenutniPregled.Prostorija;
                                LekarViewModel.listaPregleda[i].Datum = trenutniPregled.Datum;

                            }
                            else
                            {
                                LekarViewModel.listaOperacija.RemoveAt(i);
                            }


                        }
                    }
                    for (int p = 0; p < LekarViewModel.podaciLista.Items.Count; p++)
                    {
                        if (LekarViewModel.podaciLista.Items[p].Equals(stariPregled))
                        {
                            if (ulogovaniLekar.Mbr.Equals(trenutniPregled.Lekar.Mbr))
                            {
                                LekarViewModel.podaciLista.Items[p] = trenutniPregled;
                                LekarViewModel.RefreshPodaciListu();
                                Pregled o = new Pregled();
                                o.Id = trenutniPregled.Id;
                                o.Hitan = trenutniPregled.Hitan;
                                o.Lekar = trenutniPregled.Lekar;
                                o.Pacijent = trenutniPregled.Pacijent;
                                o.Trajanje = trenutniPregled.Trajanje;
                                o.Zavrsen = trenutniPregled.Zavrsen;
                                o.Anamneza = trenutniPregled.Anamneza;
                                o.Prostorija= trenutniPregled.Prostorija;
                                o.Datum = trenutniPregled.Datum;
                                skladistePregledi.Izmeni(o);
                            }
                            else
                            {
                                LekarViewModel.podaciLista.Items.RemoveAt(p);
                                LekarViewModel.RefreshPodaciListu();
                                Pregled o = new Pregled();
                                o.Id = trenutniPregled.Id;
                                o.Hitan = trenutniPregled.Hitan;
                                o.Lekar = trenutniPregled.Lekar;
                                o.Pacijent = trenutniPregled.Pacijent;
                                o.Trajanje = trenutniPregled.Trajanje;
                                o.Zavrsen = trenutniPregled.Zavrsen;
                                o.Anamneza = trenutniPregled.Anamneza;
                                o.Prostorija = trenutniPregled.Prostorija;
                                o.Datum = trenutniPregled.Datum;
                                skladistePregledi.Izmeni(o);
                            }
                        }

                    }
                }
            }
        }
       


        public void PotvrdiIzmenu(string datumB, string vremeB, string trajanjeB, List<Lekar> lekariTrenutni, string lekarB, List<Pacijent> pacijentiZa, string prezimeB, List<Prostorija> prostorijaZa, bool ItemSourceDaLiJeOperacija, TipOperacije tipOper, bool ItemSourceDaLiJeHitan, string prostorijaB, Lekar ulogovaniLekar)
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
                    zaId = skladistePregledi.GetAllOperacije();
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

                        LekarViewModel.listaOperacija.Add(o);
                        LekarViewModel.podaciLista.Items.Add(trenutnaOperacija);
                        LekarViewModel.RefreshPodaciListu();
                    }
                    skladistePregledi.Save(o);

                }
                else
                {
                    List<Pregled> zaId = new List<Pregled>();
                    zaId = skladistePregledi.GetAllPregledi();
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

                        LekarViewModel.listaPregleda.Add(o);
                        LekarViewModel.podaciLista.Items.Add(trenutniPregled);
                        LekarViewModel.RefreshPodaciListu();
                    }
                    skladistePregledi.Save(o);

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
            List<TimeSpan> zauzetiTermini = new List<TimeSpan>();
            zauzetiTermini = DobijZauzeteTermineLekara(lekarB, datumB);

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
            List<Pacijent> pacijenti = skladistePacijenti.GetAll();
            List<TimeSpan> zauzetiTermini = new List<TimeSpan>();
            List<Pregled> preglediPacijenta1 = skladistePregledi.GetAllPregledi();
            List<Operacija> operacijePacijenta1 = skladistePregledi.GetAllOperacije();
            for (int lek = 0; lek < pacijenti.Count; lek++)
            {
                string s;
                s = pacijentiZa[lek].Prezime + ' ' + pacijentiZa[lek].Ime + ' ' + pacijentiZa[lek].Jmbg;


                if (s.Equals(prezimeB))
                {


                    List<Pacijent> pacijent = new List<Pacijent>();
                    pacijent = skladistePacijenti.GetAll();
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


        public bool LekarSlobodanUToVremeIzmeni(string lekarB, DateTime datumB, string trajanjeB, string vremeB, PrikazPregleda trenutniPregled, PrikazOperacije trenutnaOperacija)
        {
            FileStorageLekar ProveraL = new FileStorageLekar();
            List<Lekar> lekari = new List<Lekar>();
            lekari = ProveraL.GetAll();
            List<TimeSpan> zauzetiTermini = new List<TimeSpan>();
            List<Pregled> preglediLekara1 = skladistePregledi.GetAllPregledi();
            List<Operacija> operacijeLekara1 = skladistePregledi.GetAllOperacije();
            for (int lek = 0; lek < lekari.Count; lek++)
            {
                string ss = lekari[lek].Prezime + ' ' + lekari[lek].Ime + ' ' + lekari[lek].Jmbg;
                if (ss.Equals(lekarB) && lekari[lek].Specijalizacija.OblastMedicine != null)
                {


                    List<Lekar> lekar = new List<Lekar>();
                    lekar = skladisteLekari.GetAll();
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
                        if (preglediLekara1[pre].Datum.Date.Equals(datumB.Date) && !preglediLekara1[pre].Id.Equals(trenutniPregled.Id))
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
                        if (operacijeLekara1[ope].Datum.Date.Equals(datumB.Date) && !operacijeLekara1[ope].Id.Equals(trenutnaOperacija.Id))
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

        public List<TimeSpan> DobijZauzeteTermineLekara(string lekarB, DateTime datumB)
        {
            FileStorageLekar ProveraL = new FileStorageLekar();
            List<Lekar> lekari = new List<Lekar>();
            lekari = ProveraL.GetAll();
            List<TimeSpan> zauzetiTermini = new List<TimeSpan>();
            List<Pregled> preglediLekara1 = skladistePregledi.GetAllPregledi();
            List<Operacija> operacijeLekara1 = skladistePregledi.GetAllOperacije();
            for (int lek = 0; lek < lekari.Count; lek++)
            {
                string ss = lekari[lek].Prezime + ' ' + lekari[lek].Ime + ' ' + lekari[lek].Jmbg;
                if (ss.Equals(lekarB) && lekari[lek].Specijalizacija.OblastMedicine != null)
                {
                    zauzetiTermini = RacunajZauzeteTermine(lekari, datumB, lekarB);
                }

            }
            return zauzetiTermini;
        }

        public bool PacijentSlobodanUToVremeIzmeni(List<Pacijent> pacijentiZa, string prezimeB, DateTime datumB, string trajanjeB, string vremeB, PrikazPregleda trenutniPregled, PrikazOperacije trenutnaOperacija)
        {
            FileStoragePacijenti ProveraP = new FileStoragePacijenti();
            List<Pacijent> pacijenti = skladistePacijenti.GetAll();
            List<TimeSpan> zauzetiTermini = new List<TimeSpan>();
            List<Pregled> preglediPacijenta1 = skladistePregledi.GetAllPregledi();
            List<Operacija> operacijePacijenta1 = skladistePregledi.GetAllOperacije();
            for (int lek = 0; lek < pacijenti.Count; lek++)
            {
                string s;
                s = pacijentiZa[lek].Prezime + ' ' + pacijentiZa[lek].Ime + ' ' + pacijentiZa[lek].Jmbg;


                if (s.Equals(prezimeB))
                {


                    List<Pacijent> pacijent = new List<Pacijent>();
                    pacijent = skladistePacijenti.GetAll();
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
                        if (preglediPacijenta1[pre].Datum.Date.Equals(datumB.Date) && !preglediPacijenta1[pre].Id.Equals(trenutniPregled.Id))
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
                        if (operacijePacijenta1[ope].Datum.Date.Equals(datumB.Date) && !operacijePacijenta1[ope].Id.Equals(trenutnaOperacija.Id))
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
                    if (naRenoviranju(prostorijaZa[i], datumB))
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


        public List<TimeSpan> LekarFiltriranje(List<TimeSpan> itemSourceVremeB, string zaFilLek, List<Lekar> lekariTrenutni, string lekarB, DateTime datumB)
        {
            if (zaFilLek != lekarB)
            {
                itemSourceVremeB = InicirajVreme();

                itemSourceVremeB = FiltrirajZauzeteTermine(itemSourceVremeB, lekariTrenutni, lekarB, datumB);
               
                return itemSourceVremeB;

            }
            return null;
        }
        public List<TimeSpan> FiltrirajZauzeteTermine(List<TimeSpan> vremeB, List<Lekar> lekariTrenutni, string lekarB, DateTime datumB)
        {
            for (int lek = 0; lek < lekariTrenutni.Count; lek++)
            {
                string h = lekariTrenutni[lek].Prezime + ' ' + lekariTrenutni[lek].Ime + ' ' + lekariTrenutni[lek].Jmbg;
                if (h.Equals(lekarB) && lekariTrenutni[lek].Specijalizacija.OblastMedicine != null)
                {
                    List<TimeSpan> zauzetiTermini = RacunajZauzeteTermine(lekariTrenutni, datumB, lekarB);
                    for (int tm = 0; tm < zauzetiTermini.Count; tm++)
                    {
                        vremeB.Remove(zauzetiTermini[tm]);
                    }
                    return vremeB;
                }

            }
            return vremeB;
        }
        public List<TimeSpan> RacunajZauzeteTermine(List<Lekar> lekariTrenutni,DateTime datumB,string lekarB)
        {
            List<TimeSpan> zauzetiTermini = new List<TimeSpan>();
            List<Pregled> preglediLekara = skladistePregledi.GetAllPregledi();
            List<Operacija> operacijeLekara = skladistePregledi.GetAllOperacije();
            List<Lekar> lekari = new List<Lekar>();
            lekari = lekariTrenutni;
            string jmbgLekar = DobijJmbgLekara(lekarB, lekari);

            preglediLekara = FiltrirajPregledeLekara(preglediLekara, jmbgLekar);
            operacijeLekara = FiltrirajOperacijeLekara(operacijeLekara, jmbgLekar);
            zauzetiTermini = OdrediZauzeteTermine(preglediLekara, operacijeLekara, zauzetiTermini, datumB);
            return zauzetiTermini;
        }
        
        public List<Operacija> FiltrirajOperacijeLekara(List<Operacija> operacijeLekara,string jmbgLekar)
        {
            for (int ad = 0; ad < operacijeLekara.Count; ad++)
            {
                if (!operacijeLekara[ad].Lekar.Jmbg.Equals(jmbgLekar))
                {
                    operacijeLekara.RemoveAt(ad);
                    ad = ad - 1;
                }
            }
            return operacijeLekara;
        }
        public List<Pregled> FiltrirajPregledeLekara(List<Pregled> preglediLekara, string jmbgLekar)
        {
            for (int da = 0; da < preglediLekara.Count; da++)
            {
                if (!preglediLekara[da].Lekar.Jmbg.Equals(jmbgLekar))
                {
                    preglediLekara.RemoveAt(da);
                    da = da - 1;
                }
            }
            return preglediLekara;
        }
        public string DobijJmbgLekara(string lekarB,List<Lekar> lekari)
        {
            string jmbgLekar="";
            for (int l = 0; l < lekari.Count; l++)
            {
                string hh = lekari[l].Prezime + ' ' + lekari[l].Ime + ' ' + lekari[l].Jmbg;
                if (hh.Equals(lekarB))
                {
                    jmbgLekar = lekari[l].Jmbg;
                    return jmbgLekar;
                }
            }
            return jmbgLekar;
        }
        public List<TimeSpan> InicirajVreme()
        {
            List<TimeSpan> itemSourceVremeB = new List<TimeSpan>();
            for (int vre = 0; vre < 24; vre++)
            {
                for (int min = 0; min < 59;)
                {
                    TimeSpan ts = new TimeSpan(vre, min, 0);
                    min = min + 15;
                    itemSourceVremeB.Add(ts);
                }

            }
            return itemSourceVremeB;
        }




        public List<string> DatumFiltriranje(DateTime zaFilLekDat, DateTime datumB, List<Prostorija> prostorijaZa,List<string> ItemSourceProstorijaB)
        {
            if (zaFilLekDat != datumB)
            {
                zaFilLekDat = datumB;
                ItemSourceProstorijaB = new List<string>();
                for (int pr = 0; pr < prostorijaZa.Count; pr++)
                {
                    if (prostorijaZa[pr].Obrisana == false && prostorijaZa[pr].Zauzeta == false && prostorijaZa[pr].TipProstorije.Equals(TipProstorije.salaZaPreglede) && !naRenoviranju(prostorijaZa[pr], datumB))
                    {
                        ItemSourceProstorijaB.Add(prostorijaZa[pr].BrojProstorije);
                    }
                }
                return ItemSourceProstorijaB;
            }
            return null;

        }

        public string LekarComboNaTab(string zaFilLek, string lekarB, List<Lekar> lekariTrenutni, string specijalizacija)
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

        public List<string> SpecijalizacijaComboNaTab(List<string> specijalizacije, String specijalizacija, List<Lekar> lekariTrenutni)
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


        public List<TimeSpan> OdrediZauzeteTermine(List<Pregled> preglediLekara, List<Operacija> operacijeLekara, List<TimeSpan> zauzetiTermini, DateTime datumB)
        {
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
            return zauzetiTermini;
        }



    }
}
