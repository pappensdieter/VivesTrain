using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VivesTrein.ViewModels
{
    public class ReisVM
    {
        [Required]
        public string Naam { get; set; }
        [Required]
        public int VerstrekStadId { get; set; }
        [Required]
        public int AankomstStadId { get; set; }
        public Boolean BussinessClass { get; set; }
        [DataType(DataType.DateTime)]
        [Required]
        public System.DateTime VertrekDatum { get; set; }
        [Required]
        [Range(1, 10)]
        public int Aantal { get; set; }

    }
}
