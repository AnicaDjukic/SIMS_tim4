using Model.Korisnici;
using Model.Pregledi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for FormNapraviAnamnezuLekar.xaml
    /// </summary>
    public partial class FormNapraviAnamnezuLekar : Window
    {

        public string simptomi { get; set; }
        public string dijagnoza { get; set; }

        public List<Recept> recepti { get; set; }
        private int dozvola = 0;

        private FileStoragePregledi sviPregledi = new FileStoragePregledi();
        private List<Pregled> sviPreg = new List<Pregled>();
        private List<Operacija> sveOpe = new List<Operacija>();
        private List<Lekar> lekariTrenutni = new List<Lekar>();
        private Lekar ulogovaniLekar = new Lekar();
        private Pregled trenutniPregled = new Pregled();
        private Operacija trenutnaOperacija = new Operacija();
        private Pregled stariPregled = new Pregled();
        private Operacija staraOperacija = new Operacija();

        public static ObservableCollection<Recept> Recepti
        {
            get;
            set;
        }


        public FormNapraviAnamnezuLekar(Pregled p1, List<Lekar> l1, Lekar neki)
        {
            Lek l11 = new Lek();
            Lek l22 = new Lek();
            l11.Naziv = "Aspirin";
            l11.Odobren = true;
            l11.Id = 1;
            l11.KolicinaUMg = 200;
            l22.Naziv = "Brufen";
            l22.Odobren = false;
            l22.Id = 2;
            l22.KolicinaUMg = 300;
            Anamneza an = new Anamneza();
            an.Id = 1;
            an.Simptomi = "temparatura";
            an.Dijagnoza = "korona";
            Recept rc = new Recept();
            rc.Id = 1;
            rc.Kolicina = 2;
            rc.lek = l11;
            rc.Trajanje = new DateTime();
            rc.VremeUzimanja = new TimeSpan();
            an.Recept.Add(rc);
            List<Anamneza> ann = new List<Anamneza>();
            ann.Add(an);

            
            
            simptomi = "";
            dijagnoza = "";
            
            trenutniPregled = p1;
            lekariTrenutni = l1;
            stariPregled = p1;
            ulogovaniLekar = neki;
            
            


            InitializeComponent();
            this.DataContext = this;

            


            for (int i = 0; i < ann.Count; i++)
            {
                if (p1.AnamnezaId == ann[i].Id)
                {
                    simptomi = ann[i].Simptomi;
                    dijagnoza = ann[i].Dijagnoza;
                    dozvola = 1;
                    break;
                }
            }

            if (dozvola == 0)
            {



                textSimptomi.Text = simptomi;
                textDijagnoza.Text = dijagnoza;
                Recepti = new ObservableCollection<Recept>();
            }

        }

        public FormNapraviAnamnezuLekar(Operacija op, List<Lekar> l1, Lekar neki)
        {
            Lek l11 = new Lek();
            Lek l22 = new Lek();
            l11.Naziv = "Aspirin";
            l11.Odobren = true;
            l11.Id = 1;
            l11.KolicinaUMg = 200;
            l22.Naziv = "Brufen";
            l22.Odobren = false;
            l22.Id = 2;
            l22.KolicinaUMg = 300;
            Anamneza an = new Anamneza();
            an.Id = 1;
            an.Simptomi = "temparatura";
            an.Dijagnoza = "korona";
            Recept rc = new Recept();
            rc.Id = 1;
            rc.Kolicina = 2;
            rc.lek = l11;
            rc.Trajanje = new DateTime();
            rc.VremeUzimanja = new TimeSpan();
            an.Recept.Add(rc);
            List<Anamneza> ann = new List<Anamneza>();
            ann.Add(an);
            simptomi = "";
            dijagnoza = "";

            List<Lek> lekovi = new List<Lek>();
            

            trenutnaOperacija = op;
            lekariTrenutni = l1;
            staraOperacija = op;
            ulogovaniLekar = neki;




            InitializeComponent();

            this.DataContext = this;

            



            for (int i = 0; i < ann.Count; i++)
            {
                if (op.AnamnezaId == ann[i].Id)
                {
                    simptomi = ann[i].Simptomi;
                    dijagnoza = ann[i].Dijagnoza;
                    dozvola = 1;
                    break;
                }
            }

                if (dozvola == 0)
                {



                   textSimptomi.Text = simptomi;
                   textDijagnoza.Text = dijagnoza;
                Recepti = new ObservableCollection<Recept>();
            }

            

        }



        private void OtkaziDugme(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DodajLek(object sender, RoutedEventArgs e)
        {
            FormNapraviReceptLekar form = new FormNapraviReceptLekar();
            form.Show();
        }

        private void Potvrdi(object sender, RoutedEventArgs e)
        {
            Anamneza a = new Anamneza();
            for (int i = 0; i < Recepti.Count; i++) {
                
                a.Recept.Add(Recepti[i]);
            }
            a.Simptomi = textSimptomi.Text;
            a.Dijagnoza = textDijagnoza.Text;
        }
    }
}
