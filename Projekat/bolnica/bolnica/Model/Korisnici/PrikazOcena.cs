using System;

namespace Model.Korisnici
{
    public class PrikazOcena
    {
        public PrikazOcena()
        {
            this.Pacijent = new Pacijent();
        }
        public int IdOcene { get; set; }
        public DateTime Datum { get; set; }
        public int BrojOcene { get; set; }
        public string Sadrzaj { get; set; }
        public Pacijent Pacijent { get; set; }
        public string ImeIPrezime { get; set; }
    }
}
