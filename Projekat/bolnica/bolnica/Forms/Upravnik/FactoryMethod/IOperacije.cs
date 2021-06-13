namespace Bolnica.Forms.Upravnik.FactoryMethod
{
    public interface IOperacije<T>
    {
        void Brisanje(T zaBrisanje);
    }
}