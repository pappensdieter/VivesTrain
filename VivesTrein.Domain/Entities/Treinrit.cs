using System;
using System.Collections.Generic;

namespace VivesTrein.Domain.Entities
{
    public partial class Treinrit
    {
        public Treinrit()
        {
            TreinritReis = new HashSet<TreinritReis>();
        }

        public int Id { get; set; }
        public int VertrekstadId { get; set; }
        public int BestemmingsstadId { get; set; }
        public int AtlZitplaatsen { get; set; }
        public double Prijs { get; set; }
        public DateTime Vertrek { get; set; }
        public DateTime Aankomst { get; set; }
        public int Vrijeplaatsen { get; set; }

        public Stad Bestemmingsstad { get; set; }
        public Stad Vertrekstad { get; set; }
        public ICollection<TreinritReis> TreinritReis { get; set; }
    }
}
