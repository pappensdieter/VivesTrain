using System;
using System.Collections.Generic;

namespace VivesTrein.Domain.Entities
{
    public partial class Hotel
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public string Adres { get; set; }
        public string Link { get; set; }
        public int StadId { get; set; }
    }
}
