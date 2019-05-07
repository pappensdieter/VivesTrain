using System;
using System.Collections.Generic;

namespace VivesTrein.Domain.Entities
{
    public partial class Reis
    {
        public Reis()
        {
            Boeking = new HashSet<Boeking>();
            TreinritReis = new HashSet<TreinritReis>();
        }

        public int Id { get; set; }
        public string Naam { get; set; }
        public int VertrekstadId { get; set; }
        public int BestemmingsstadId { get; set; }
        public double Prijs { get; set; }
        public int Aantal { get; set; }

        public Stad Bestemmingsstad { get; set; }
        public Stad Vertrekstad { get; set; }
        public ICollection<Boeking> Boeking { get; set; }
        public ICollection<TreinritReis> TreinritReis { get; set; }
    }
}
