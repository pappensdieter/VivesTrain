﻿using System;
using System.Collections.Generic;

namespace VivesTrein.Domain.Entities
{
    public partial class Reis
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public int? VertrekstadId { get; set; }
        public int? BestemmingsstadId { get; set; }
        public double? Prijs { get; set; }

        public Stad Bestemmingsstad { get; set; }
        public Stad Vertrekstad { get; set; }
    }
}