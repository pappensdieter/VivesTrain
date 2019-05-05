using System;
using System.Collections.Generic;

namespace VivesTrein.Domain.Entities
{
    public partial class Boeking
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int ReisId { get; set; }
        public string Status { get; set; }

        public Reis Reis { get; set; }
        public AspNetUsers User { get; set; }
    }
}
