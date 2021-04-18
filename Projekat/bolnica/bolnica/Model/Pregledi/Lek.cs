using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Model.Pregledi
{
    public class Lek
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public int KolicinaUMg { get; set; }
        public bool Odobren { get; set; }

        public List<Sastojak> alergen;

        /// <summary>
        /// Property for collection of Alergen
        /// </summary>
        /// <pdGenerated>Default opposite class collection property</pdGenerated>
        public List<Sastojak> Alergen
        {
            get
            {
                if (alergen == null)
                    alergen = new List<Sastojak>();
                return alergen;
            }
            set
            {
                RemoveAllAlergen();
                if (value != null)
                {
                    foreach (Sastojak oAlergen in value)
                        AddAlergen(oAlergen);
                }
            }
        }

        /// <summary>
        /// Add a new Alergen in the collection
        /// </summary>
        /// <pdGenerated>Default Add</pdGenerated>
        public void AddAlergen(Sastojak newAlergen)
        {
            if (newAlergen == null)
                return;
            if (this.alergen == null)
                this.alergen = new List<Sastojak>();
            if (!this.alergen.Contains(newAlergen))
                this.alergen.Add(newAlergen);
        }

        /// <summary>
        /// Remove an existing Alergen from the collection
        /// </summary>
        /// <pdGenerated>Default Remove</pdGenerated>
        public void RemoveAlergen(Sastojak oldAlergen)
        {
            if (oldAlergen == null)
                return;
            if (this.alergen != null)
                if (this.alergen.Contains(oldAlergen))
                    this.alergen.Remove(oldAlergen);
        }

        /// <summary>
        /// Remove all instances of Alergen from the collection
        /// </summary>
        /// <pdGenerated>Default removeAll</pdGenerated>
        public void RemoveAllAlergen()
        {
            if (alergen != null)
                alergen.Clear();
        }
    }
}
