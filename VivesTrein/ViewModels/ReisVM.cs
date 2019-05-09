using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VivesTrein.ViewModels
{
    public class ReisVM
    {
        [Required(ErrorMessage = "Gelieve een naam op te geven voor uw reis")]
        public string Naam { get; set; }
        [Required(ErrorMessage = "Gelieve uw stad van vertrek te selecteren")]
        public int VerstrekStadId { get; set; }
        [Required(ErrorMessage = "Gelieve uw stad van aankomst te selecteren")]
        public int AankomstStadId { get; set; }
        public Boolean BussinessClass { get; set; }
        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "Gelieve een geprefereerde datum en uur van vertrek in te vullen")]
        public System.DateTime VertrekDatum { get; set; }
        [Required(ErrorMessage = "Gelieve het aantal tickets die u wilt boeken in te vullen")]
        [Range(1, 10)]
        public int Aantal { get; set; }

    }
}
