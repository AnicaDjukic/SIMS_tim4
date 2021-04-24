using bolnica.Forms;
using Bolnica.Model.Prostorije;
using Model.Prostorije;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static Bolnica.Forms.Upravnik.FormSkladiste;

namespace Bolnica.Forms.Upravnik
{
    /// <summary>
    /// Interaction logic for CreateFormOprema.xaml
    /// </summary>
    public partial class CreateFormOprema : Window
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private string sifra;
        private string naziv;
        private int kolicina;
        private FileStorageOprema storage;
        private Oprema oprema;
        public Oprema Oprema
        {
            get
            {
                return oprema;
            }
            set
            {
                if(value != oprema)
                {
                    oprema = value;
                }
            }
        }

        public string Sifra
        {
            get
            {
                return sifra;
            }
            set
            {
                if (value != sifra)
                {
                    sifra = value;
                    OnPropertyChanged("Sifra");
                }
            }
        }

        public string Naziv
        {
            get
            {
                return naziv;
            }
            set
            {
                if (value != naziv)
                {
                    naziv = value;
                    OnPropertyChanged("Naziv");
                }
            }
        }

        public int Kolicina
        {
            get
            {
                return kolicina;
            }
            set
            {
                if (value != kolicina)
                {
                    kolicina = value;
                    OnPropertyChanged("Kolicina");
                }
            }
        }

        public CreateFormOprema()
        {
            InitializeComponent();
            if (FormUpravnik.clickedDodaj)
                oprema = new Oprema();
            storage = new FileStorageOprema();
            this.DataContext = this;
        }

        private void Button_Click_Potvrdi(object sender, RoutedEventArgs e)
        {
            oprema.Sifra = sifra;
            oprema.Naziv = naziv;
            oprema.Kolicina = kolicina;
            if (ComboTipOpreme.SelectedIndex == 0)
                oprema.TipOpreme = TipOpreme.staticka;
            else
                oprema.TipOpreme = TipOpreme.dinamicka;

            //update();

            /*if (FormUpravnik.clickedDodaj)
            {
                foreach (Zaliha z in FormSkladiste.Zalihe)
                {
                    z.Oprema = oprema;
                    FormSkladiste.storageZaliha.Save(z);
                }
            }*/
            List<Oprema> svaOprema = storage.GetAll();
            bool postoji = false;
            if (svaOprema != null)
            {
                foreach (Oprema o in svaOprema)
                {
                    if (o.Sifra == oprema.Sifra)
                    {
                        if (FormUpravnik.clickedDodaj)
                        {
                            MessageBox.Show("Oprema sa istom šifrom već postoji");
                            postoji = true;
                            FormUpravnik.clickedDodaj = false;
                        }
                        else
                        {
                            storage.Delete(o);
                            for (int i = 0; i < FormUpravnik.Oprema.Count; i++)
                            {
                                if (FormUpravnik.Oprema[i].Sifra == oprema.Sifra)
                                {
                                    FormUpravnik.Oprema.Remove(FormUpravnik.Oprema[i]);
                                    break;
                                }

                            }
                        }
                    }
                }
                if (!postoji)
                {
                    storage.Save(oprema);
                    FormUpravnik.Oprema.Add(oprema);
                }
            }
            else
            {
                storage.Save(oprema);
                FormUpravnik.Oprema.Add(oprema);
            }

        Close();

        }

        private void Button_Click_Skladisti(object sender, RoutedEventArgs e)
        {
            oprema.Sifra = sifra;
            oprema.Naziv = naziv;
            oprema.Kolicina = kolicina;
            if (ComboTipOpreme.SelectedIndex == 0)
                oprema.TipOpreme = TipOpreme.staticka;
            else
                oprema.TipOpreme = TipOpreme.dinamicka;

            if (UkKolicinaValidna(kolicina))
            {
                var s = new FormSkladiste(oprema);
                s.Show();
            }
        }

        private bool UkKolicinaValidna(int ukKolicina)
        {
            if (ukKolicina <= 0)
            {
                MessageBox.Show("Unesite validnu količinu! Količina mora biti veća od 0.");
                return false;
            }
            else
            {
                if (!FormUpravnik.clickedDodaj)
                {
                    FileStorageZaliha storageZalihe = new FileStorageZaliha();
                    List<Zaliha> zalihe = storageZalihe.GetAll();

                    if (zalihe != null)
                    {
                        int rezervisanaKolicina = 0;
                        foreach (Zaliha z in zalihe)
                        {
                            if (z.Oprema.Sifra == oprema.Sifra && z.Prostorija.BrojProstorije != "magacin")
                                rezervisanaKolicina += z.Kolicina;
                        }

                        foreach (Zaliha z in zalihe)
                        {
                            if (z.Oprema.Sifra == oprema.Sifra && z.Prostorija.BrojProstorije == "magacin")
                            {
                                if (ukKolicina - rezervisanaKolicina >= 0)
                                {
                                    return true;
                                }
                                else
                                {
                                    MessageBox.Show("Nije moguće toliko smajiti količinu. Količina ne sme biti manja od " + rezervisanaKolicina);
                                    return false;
                                }
                            }
                        }
                    }
                }
            }

            return true;
        }

    }
}
