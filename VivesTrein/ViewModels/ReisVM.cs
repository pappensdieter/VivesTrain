using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VivesTrein.ViewModels
{
    public class ReisVM
    {
        public string Naam { get; set; }
        public int VerstrekStadId { get; set; }
        public int AankomstStadId { get; set; }
        public Boolean BussinessClass { get; set; }
        public float Prijs { get; set; }
        public System.DateTime VertrekDatum { get; set; }

    }
}
