using Bolnica.Model;
using Bolnica.Model.Korisnici;
using Model.Korisnici;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace Bolnica.Forms
{
    /// <summary>
    /// Interaction logic for FormIstorijaOcenaPage.xaml
    /// </summary>
    public partial class FormIstorijaOcenaPage : Page
    {
        public static ObservableCollection<PrikazOcena> PrikazOcenaIKomentara
        {
            get;
            set;
        }

        private FileStorageOcene storageOcene = new FileStorageOcene();
        private FileStorageLekar storageLekar = new FileStorageLekar();
        
        private List<Ocena> ocene = new List<Ocena>();
        private List<Lekar> lekari = new List<Lekar>();

        public FormIstorijaOcenaPage(Pacijent trenutniPacijent)
        {
            InitializeComponent();

            this.DataContext = this;

            PrikazOcenaIKomentara = new ObservableCollection<PrikazOcena>();

            ocene = storageOcene.GetAll();
            foreach (Ocena o in ocene)
            {
                if (trenutniPacijent.Jmbg.Equals(o.Pacijent.Jmbg))
                {
                    PrikazOcena prikaz = new PrikazOcena
                    {
                        IdOcene = o.IdOcene,
                        Datum = o.Datum,
                        BrojOcene = o.BrojOcene,
                        Sadrzaj = o.Sadrzaj,
                        Pacijent = o.Pacijent,
                        ImeIPrezime = "Bolnica"
                    };
                    lekari = storageLekar.GetAll();
                    foreach (Lekar l in lekari)
                    {
                        if (l.Jmbg.Equals(o.Lekar.Jmbg))
                        {
                            prikaz.ImeIPrezime = l.Ime + " " + l.Prezime;
                            break;
                        }
                    }
                    PrikazOcenaIKomentara.Add(prikaz);
                }
            }
        }
    }
}
