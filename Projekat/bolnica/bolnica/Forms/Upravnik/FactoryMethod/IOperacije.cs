namespace Bolnica.Forms.Upravnik.FactoryMethod
{
    public interface IOperacije<T, ID>
    {
        void Dodavanje();
        void Prikazivanje(ID id);
        void Izmena(ID id);
        void Brisanje(T zaBrisanje);
    }
}