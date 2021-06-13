using System.Collections.Generic;

namespace Bolnica.Forms.Upravnik.FactoryMethod
{
    public interface IOperacijeNadEntitetima<T, ID>
    {
        void Dodavanje();
        void Prikazivanje(ID id);
        void Izmena(ID id);
        void Brisanje(T zaBrisanje);
        List<T> Pretraga(string text);
    }
}