using System;
using System.Collections.Generic;

namespace VivesTrein.Domain.Entities
{
    public partial class Boeking
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? Reis { get; set; }
        public string Status { get; set; }
    }
}
