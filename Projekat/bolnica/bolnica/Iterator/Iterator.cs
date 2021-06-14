using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Iterator
{
    abstract class Iterator : IEnumerator
    {
        object IEnumerator.Current => Trenutni();

        // Returns the key of the current element
        public abstract int Kljuc();

        // Returns the current element
        public abstract object Trenutni();

        // Move forward to next element
        public abstract bool MoveNext();

        // Rewinds the Iterator to the first element
        public abstract void Reset();
    }

    abstract class IteratorAgregacija : IEnumerable
    {
        // Returns an Iterator or another IteratorAggregate for the implementing
        // object.
        public abstract IEnumerator GetEnumerator();
    }

    // Concrete Iterators implement various traversal algorithms. These classes
    // store the current traversal position at all times.
    class SortiranIterator : Iterator
    {
        private Kolekcija kolekcija;

        // Stores the current traversal position. An iterator may have a lot of
        // other fields for storing iteration state, especially when it is
        // supposed to work with a particular kind of collection.
        private int pozicija = -1;

        private bool unazad = false;

        public SortiranIterator(Kolekcija kolekcija, bool reverse = false)
        {
            this.kolekcija = kolekcija;
            this.unazad = reverse;

            if (reverse)
            {
                this.pozicija = kolekcija.dobijPredmete().Count;
            }
        }

        public override object Trenutni()
        {
            return this.kolekcija.dobijPredmete()[pozicija];
        }

        public override int Kljuc()
        {
            return this.pozicija;
        }

        public override bool MoveNext()
        {
            int novaPozicija = this.pozicija + (this.unazad ? -1 : 1);

            if (novaPozicija >= 0 && novaPozicija < this.kolekcija.dobijPredmete().Count)
            {
                this.pozicija = novaPozicija;
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void Reset()
        {
            this.pozicija = this.unazad ? this.kolekcija.dobijPredmete().Count - 1 : 0;
        }

    }

    class Kolekcija : IteratorAgregacija
    {
        List<Object> kolekcija = new List<Object>();

        bool smer = false;

        public void ObrnutiRedosled()
        {
            smer = !smer;
        }

        public List<Object> dobijPredmete()
        {
            return kolekcija;
        }

        public void Dodaj(Object item)
        {
            this.kolekcija.Add(item);
        }

        public override IEnumerator GetEnumerator()
        {
            return new SortiranIterator(this, smer);
        }
    }

}
