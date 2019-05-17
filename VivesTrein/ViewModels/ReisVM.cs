using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VivesTrein.ViewModels
{
    public class CustomReg : ValidationAttribute
    {
        public new string ErrorMessage { get; set; }
        public CustomReg(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value.ToString() != "")
                return ValidationResult.Success;

            else
                return new ValidationResult(ErrorMessage);
        }
    }

    public class ReisVM
    {
        [Required(ErrorMessage = "Gelieve een naam op te geven voor uw reis")]
        public string Naam { get; set; }

        [CustomReg("Gelieve uw stad van vertrek te selecteren")]
        public int VerstrekStadId { get; set; }

        [CustomReg("Gelieve uw stad van aankomst te selecteren")]
        public int AankomstStadId { get; set; }

        public Boolean BussinessClass { get; set; }

        [DataType(DataType.DateTime)]
        [CustomReg("Gelieve een geprefereerde datum en uur van vertrek in te vullen")]
        public System.DateTime VertrekDatum { get; set; }

        [CustomReg("Gelieve het aantal tickets die u wilt boeken in te vullen")]
        [Range(1, 10)]
        public int Aantal { get; set; }

    }
}
