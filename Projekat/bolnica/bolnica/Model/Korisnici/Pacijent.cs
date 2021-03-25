using Model.Pacijenti;

namespace Model.Korisnici
{
    public class Pacijent : Osoba
    {
        private int brojKartona;
        private bool guest;
        private bool obrisan;

        public ZdravstveniKarton zdravstveniKarton;

    }
}