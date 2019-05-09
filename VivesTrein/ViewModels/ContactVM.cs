using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VivesTrein.ViewModels
{
    public class ContactVM
    {
        [Required(ErrorMessage = "Gelieve uw naam in te vullen"), Display(Name = "Uw naam")]
        public string Naam { get; set; }
        [Required(ErrorMessage = "Gelieve uw email-adres in te vullen"), Display(Name = "Uw email"), EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Gelieve uw reden tot contact op te geven"), Display(Name = "Inhoud bericht")]
        [DataType(DataType.MultilineText)]
        public string Message { get; set; }
    }
}
