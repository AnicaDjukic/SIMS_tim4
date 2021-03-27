using Model.Korisnici;
using Model.Pregledi;
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

namespace Bolnica.Forms
{
    /// <summary>
    /// Interaction logic for FormIstorijaPregledaPacijent.xaml
    /// </summary>
    public partial class FormIstorijaPregledaPacijent : Window
    {
        private static List<Pregled> listaPregleda;
        private static List<Operacija> listaOperacija;

        public FormIstorijaPregledaPacijent()
        {
            InitializeComponent();

            listaPregleda = new List<Pregled>();
            listaOperacija = new List<Operacija>();

            Lekar l1 = new Lekar();
            l1.Ime = "Vatroslav";
            l1.Prezime = "Pap";
            Pregled p1 = new Pregled();
            p1.Lekar = l1;
            p1.Trajanje = 30;
            p1.Datum = new DateTime(2020, 5, 15, 18, 30, 0);
            listaPregleda.Add(p1);

            Lekar l2 = new Lekar();
            l2.Ime = "Vjekoslav";
            l2.Prezime = "Bevanda";
            Pregled p2 = new Pregled();
            p2.Lekar = l2;
            p2.Trajanje = 25;
            p2.Datum = new DateTime(2020, 4, 11, 10, 0, 0);
            listaPregleda.Add(p2);

            Lekar l3 = new Lekar();
            l3.Ime = "Radenko";
            l3.Prezime = "Salapura";
            Pregled p3 = new Pregled();
            p3.Lekar = l3;
            p3.Trajanje = 40;
            p3.Datum = new DateTime(2017, 11, 5, 14, 0, 0);
            listaPregleda.Add(p3);

            Pregled p4 = new Pregled();
            p4.Lekar = l1;
            p4.Trajanje = 15;
            p4.Datum = new DateTime(2019, 3, 2, 10, 45, 0);
            listaPregleda.Add(p4);

            for (int i = 0; i < listaPregleda.Count; i++)
            {
                pacijentIstorijaGrid.Items.Add(listaPregleda[i]);
            }


            Lekar l4 = new Lekar();
            l4.Ime = "Radovan";
            l4.Prezime = "Frganja";
            Operacija o1 = new Operacija();
            o1.Lekar = l4;
            o1.Trajanje = 90;
            o1.Datum = new DateTime(2020, 1, 15, 11, 20, 0);
            o1.TipOperacije = TipOperacije.srednja;
            listaOperacija.Add(o1);

            Operacija o2 = new Operacija();
            o2.Lekar = l3;
            o2.Trajanje = 85;
            o2.Datum = new DateTime(2011, 7, 10, 12, 0, 0);
            o2.TipOperacije = TipOperacije.srednja;
            listaOperacija.Add(o2);

            Lekar l5 = new Lekar();
            l5.Ime = "Svetislav";
            l5.Prezime = "Fejsa";
            Operacija o3 = new Operacija();
            o3.Lekar = l5;
            o3.Trajanje = 400;
            o3.Datum = new DateTime(2019, 1, 1, 2, 0, 0);
            o3.TipOperacije = TipOperacije.teska;
            listaOperacija.Add(o3);

            Operacija o4 = new Operacija();
            o4.Lekar = l1;
            o4.Trajanje = 15;
            o4.Datum = new DateTime(2017, 7, 12, 21, 45, 0);
            o4.TipOperacije = TipOperacije.laka;
            listaOperacija.Add(o4);

            for (int i = 0; i < listaOperacija.Count; i++)
            {
                pacijentIstorijaGrid.Items.Add(listaOperacija[i]);
            }
        }
    }
}
