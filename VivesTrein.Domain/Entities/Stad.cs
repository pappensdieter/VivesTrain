using System;
using System.Collections.Generic;

namespace VivesTrein.Domain.Entities
{
    public partial class Stad
    {
        public Stad()
        {
            ReisBestemmingsstad = new HashSet<Reis>();
            ReisVertrekstad = new HashSet<Reis>();
            TreinritBestemmingsstad = new HashSet<Treinrit>();
            TreinritVertrekstad = new HashSet<Treinrit>();
        }

        public int Id { get; set; }
        public string Naam { get; set; }

        public ICollection<Reis> ReisBestemmingsstad { get; set; }
        public ICollection<Reis> ReisVertrekstad { get; set; }
        public ICollection<Treinrit> TreinritBestemmingsstad { get; set; }
        public ICollection<Treinrit> TreinritVertrekstad { get; set; }
    }
}
