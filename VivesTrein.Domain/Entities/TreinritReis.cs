using System;
using System.Collections.Generic;

namespace VivesTrein.Domain.Entities
{
    public partial class TreinritReis
    {
        public int Id { get; set; }
        public int TreinritId { get; set; }
        public int ReisId { get; set; }
        public int Plaats { get; set; }
        public bool Klasse { get; set; }

        public Reis Reis { get; set; }
        public Treinrit Treinrit { get; set; }
    }
}
