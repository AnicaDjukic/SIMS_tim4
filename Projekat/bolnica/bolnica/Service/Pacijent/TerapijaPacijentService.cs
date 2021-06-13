using Bolnica.Forms;
using Bolnica.Model.Pregledi;
using Bolnica.Services.Pregledi;
using Model.Korisnici;
using Model.Pregledi;
using System;
using System.Collections.Generic;

namespace Bolnica.Service
{
    public class TerapijaPacijentService
    {
        private PregledService servicePregled = new PregledService();
        private AnamnezaPacijentService serviceAnamneza = new AnamnezaPacijentService();
        private ServiceLek serviceLek = new ServiceLek();

        public void DobijTrenutnuSedmicu()
        {
            String danasnjiDan = DateTime.Today.DayOfWeek.ToString();
            switch (danasnjiDan)
            {
                case "Monday":
                    FormLekoviTerapijePage.PrviDanSedmice = DateTime.Today.ToShortDateString() + ".";
                    FormLekoviTerapijePage.PoslednjiDanSedmice = DateTime.Today.AddDays(6).ToShortDateString() + ".";
                    FormLekoviTerapijePage.Ponedeljak = DateTime.Today;
                    FormLekoviTerapijePage.Nedelja = DateTime.Today.AddDays(6);
                    break;
                case "Tuesday":
                    FormLekoviTerapijePage.PrviDanSedmice = DateTime.Today.AddDays(-1).ToShortDateString() + ".";
                    FormLekoviTerapijePage.PoslednjiDanSedmice = DateTime.Today.AddDays(5).ToShortDateString() + ".";
                    FormLekoviTerapijePage.Ponedeljak = DateTime.Today.AddDays(-1);
                    FormLekoviTerapijePage.Nedelja = DateTime.Today.AddDays(5);
                    break;
                case "Wednesday":
                    FormLekoviTerapijePage.PrviDanSedmice = DateTime.Today.AddDays(-2).ToShortDateString() + ".";
                    FormLekoviTerapijePage.PoslednjiDanSedmice = DateTime.Today.AddDays(4).ToShortDateString() + ".";
                    FormLekoviTerapijePage.Ponedeljak = DateTime.Today.AddDays(-2);
                    FormLekoviTerapijePage.Nedelja = DateTime.Today.AddDays(4);
                    break;
                case "Thursday":
                    FormLekoviTerapijePage.PrviDanSedmice = DateTime.Today.AddDays(-3).ToShortDateString() + ".";
                    FormLekoviTerapijePage.PoslednjiDanSedmice = DateTime.Today.AddDays(3).ToShortDateString() + ".";
                    FormLekoviTerapijePage.Ponedeljak = DateTime.Today.AddDays(-3);
                    FormLekoviTerapijePage.Nedelja = DateTime.Today.AddDays(3);
                    break;
                case "Friday":
                    FormLekoviTerapijePage.PrviDanSedmice = DateTime.Today.AddDays(-4).ToShortDateString() + ".";
                    FormLekoviTerapijePage.PoslednjiDanSedmice = DateTime.Today.AddDays(2).ToShortDateString() + ".";
                    FormLekoviTerapijePage.Ponedeljak = DateTime.Today.AddDays(-4);
                    FormLekoviTerapijePage.Nedelja = DateTime.Today.AddDays(2);
                    break;
                case "Saturday":
                    FormLekoviTerapijePage.PrviDanSedmice = DateTime.Today.AddDays(-5).ToShortDateString() + ".";
                    FormLekoviTerapijePage.PoslednjiDanSedmice = DateTime.Today.AddDays(1).ToShortDateString() + ".";
                    FormLekoviTerapijePage.Ponedeljak = DateTime.Today.AddDays(-5);
                    FormLekoviTerapijePage.Nedelja = DateTime.Today.AddDays(1);
                    break;
                case "Sunday":
                    FormLekoviTerapijePage.PrviDanSedmice = DateTime.Today.AddDays(-6).ToShortDateString() + ".";
                    FormLekoviTerapijePage.PoslednjiDanSedmice = DateTime.Today.ToShortDateString() + ".";
                    FormLekoviTerapijePage.Ponedeljak = DateTime.Today.AddDays(-6);
                    FormLekoviTerapijePage.Nedelja = DateTime.Today;
                    break;
            }
        }

        public void InicijalizujSedmicnuTerapiju()
        {
            FormLekoviTerapijePage.SedmicnaTerapija = new List<SedmicnaTerapija>();
            FormLekoviTerapijePage.SedmicnaTerapija.Add(new SedmicnaTerapija("00:00h", "", "", "", "", "", "", ""));
            FormLekoviTerapijePage.SedmicnaTerapija.Add(new SedmicnaTerapija("04:00h", "", "", "", "", "", "", ""));
            FormLekoviTerapijePage.SedmicnaTerapija.Add(new SedmicnaTerapija("08:00h", "", "", "", "", "", "", ""));
            FormLekoviTerapijePage.SedmicnaTerapija.Add(new SedmicnaTerapija("12:00h", "", "", "", "", "", "", ""));
            FormLekoviTerapijePage.SedmicnaTerapija.Add(new SedmicnaTerapija("16:00h", "", "", "", "", "", "", ""));
            FormLekoviTerapijePage.SedmicnaTerapija.Add(new SedmicnaTerapija("20:00h", "", "", "", "", "", "", ""));
        }

        public void DobijTerapijuPacijenta(Pacijent trenutniPacijent)
        {
            List<Pregled> pregledi = servicePregled.DobijSvePregledeIOperacije();
            foreach (Pregled pregled in pregledi)
            {
                if (trenutniPacijent.Jmbg.Equals(pregled.Pacijent.Jmbg))
                {
                    NadjiAnamnezu(pregled);
                }
            }
        }

        private void NadjiAnamnezu(Pregled pregled)
        {
            List<Anamneza> anamneze = serviceAnamneza.DobaviSveAnamneze();
            foreach (Anamneza anamneza in anamneze)
            {
                if (pregled.Anamneza.Id.Equals(anamneza.Id))
                {
                    DodajLekoveIzRecepta(anamneza);
                    break;
                }
            }
        }

        private void DodajLekoveIzRecepta(Anamneza anamneza)
        {
            foreach (SedmicnaTerapija s in FormLekoviTerapijePage.SedmicnaTerapija)
            {
                if (s.Vreme.Equals("00:00h"))
                {
                    PopuniTerapijuUPonoc(anamneza, s);
                }
                else if (s.Vreme.Equals("04:00h"))
                {
                    PopuniTerapijuUCetiri(anamneza, s);
                }
                else if (s.Vreme.Equals("08:00h"))
                {
                    PopuniTerapijuUOsam(anamneza, s);
                }
                else if (s.Vreme.Equals("12:00h"))
                {
                    PopuniTerapijuUDvanaest(anamneza, s);
                }
                else if (s.Vreme.Equals("16:00h"))
                {
                    PopuniTerapijuUSesnaest(anamneza, s);
                }
                else if (s.Vreme.Equals("20:00h"))
                {
                    PopuniTerapijuUDvadeset(anamneza, s);
                }
            }
        }

        private void PopuniTerapijuUPonoc(Anamneza anamneza, SedmicnaTerapija s)
        {
            foreach (Recept recept in anamneza.Recept)
            {
                Lek lek = NadjiLek(recept);
                if (recept.VremeUzimanja == 4 || recept.VremeUzimanja == 8 || recept.VremeUzimanja == 12)
                {
                    PopuniTerapiju(s, recept, lek);
                }
            }
        }

        private void PopuniTerapijuUCetiri(Anamneza anamneza, SedmicnaTerapija s)
        {
            foreach (Recept recept in anamneza.Recept)
            {
                Lek lek = NadjiLek(recept);
                if (recept.VremeUzimanja == 4 || recept.VremeUzimanja == 8)
                {
                    PopuniTerapiju(s, recept, lek);
                }
            }
        }

        private void PopuniTerapijuUOsam(Anamneza anamneza, SedmicnaTerapija s)
        {
            foreach (Recept recept in anamneza.Recept)
            {
                Lek lek = NadjiLek(recept);
                if (recept.VremeUzimanja == 4 || recept.VremeUzimanja == 6)
                {
                    PopuniTerapiju(s, recept, lek);
                }
            }
        }

        private void PopuniTerapijuUDvanaest(Anamneza anamneza, SedmicnaTerapija s)
        {
            foreach (Recept recept in anamneza.Recept)
            {
                Lek lek = NadjiLek(recept);
                if (recept.VremeUzimanja == 4 || recept.VremeUzimanja == 8 || recept.VremeUzimanja == 12)
                {
                    PopuniTerapiju(s, recept, lek);
                }
            }
        }

        private void PopuniTerapijuUSesnaest(Anamneza anamneza, SedmicnaTerapija s)
        {
            foreach (Recept recept in anamneza.Recept)
            {
                Lek lek = NadjiLek(recept);
                if (recept.VremeUzimanja == 4 || recept.VremeUzimanja == 6 || recept.VremeUzimanja == 24)
                {
                    PopuniTerapiju(s, recept, lek);
                }
            }
        }

        private void PopuniTerapijuUDvadeset(Anamneza anamneza, SedmicnaTerapija s)
        {
            foreach (Recept recept in anamneza.Recept)
            {
                Lek lek = NadjiLek(recept);
                if (recept.VremeUzimanja == 4 || recept.VremeUzimanja == 8)
                {
                    PopuniTerapiju(s, recept, lek);
                }
            }
        }

        private static void PopuniTerapiju(SedmicnaTerapija s, Recept recept, Lek lek)
        {
            if (recept.Trajanje.CompareTo(FormLekoviTerapijePage.Nedelja) >= 0)
            {
                PopuniTerapijuPoDanima(s, lek, 7);
            }
            else if (recept.Trajanje.CompareTo(FormLekoviTerapijePage.Nedelja.AddDays(-1)) >= 0)
            {
                PopuniTerapijuPoDanima(s, lek, 6);
            }
            else if (recept.Trajanje.CompareTo(FormLekoviTerapijePage.Nedelja.AddDays(-2)) >= 0)
            {
                PopuniTerapijuPoDanima(s, lek, 5);
            }
            else if (recept.Trajanje.CompareTo(FormLekoviTerapijePage.Nedelja.AddDays(-3)) >= 0)
            {
                PopuniTerapijuPoDanima(s, lek, 4);
            }
            else if (recept.Trajanje.CompareTo(FormLekoviTerapijePage.Nedelja.AddDays(-4)) >= 0)
            {
                PopuniTerapijuPoDanima(s, lek, 3);
            }
            else if (recept.Trajanje.CompareTo(FormLekoviTerapijePage.Nedelja.AddDays(-5)) >= 0)
            {
                PopuniTerapijuPoDanima(s, lek, 2);
            }
            else if (recept.Trajanje.CompareTo(FormLekoviTerapijePage.Ponedeljak) >= 0)
            {
                PopuniTerapijuPoDanima(s, lek, 1);
            }
        }

        private static void PopuniTerapijuPoDanima(SedmicnaTerapija s, Lek lek, int brojDana)
        {
            switch (brojDana)
            {
                case 1:
                    s.Ponedeljak += lek.Naziv + "\n";
                    break;
                case 2:
                    s.Ponedeljak += lek.Naziv + "\n";
                    s.Utorak += lek.Naziv + "\n";
                    break;
                case 3:
                    s.Ponedeljak += lek.Naziv + "\n";
                    s.Utorak += lek.Naziv + "\n";
                    s.Sreda += lek.Naziv + "\n";
                    break;
                case 4:
                    s.Ponedeljak += lek.Naziv + "\n";
                    s.Utorak += lek.Naziv + "\n";
                    s.Sreda += lek.Naziv + "\n";
                    s.Cetvrtak += lek.Naziv + "\n";
                    break;
                case 5:
                    s.Ponedeljak += lek.Naziv + "\n";
                    s.Utorak += lek.Naziv + "\n";
                    s.Sreda += lek.Naziv + "\n";
                    s.Cetvrtak += lek.Naziv + "\n";
                    s.Petak += lek.Naziv + "\n";
                    break;
                case 6:
                    s.Ponedeljak += lek.Naziv + "\n";
                    s.Utorak += lek.Naziv + "\n";
                    s.Sreda += lek.Naziv + "\n";
                    s.Cetvrtak += lek.Naziv + "\n";
                    s.Petak += lek.Naziv + "\n";
                    s.Subota += lek.Naziv + "\n";
                    break;
                case 7:
                    s.Ponedeljak += lek.Naziv + "\n";
                    s.Utorak += lek.Naziv + "\n";
                    s.Sreda += lek.Naziv + "\n";
                    s.Cetvrtak += lek.Naziv + "\n";
                    s.Petak += lek.Naziv + "\n";
                    s.Subota += lek.Naziv + "\n";
                    s.Nedelja += lek.Naziv + "\n";
                    break;
            }
        }

        private Lek NadjiLek(Recept recept)
        {
            List<Lek> lekovi = serviceLek.DobaviSveLekove();
            foreach (Lek lek in lekovi)
            {
                if (recept.Lek.Id.Equals(lek.Id))
                {
                    return lek;
                }
            }
            return null;
        }
    }
}
