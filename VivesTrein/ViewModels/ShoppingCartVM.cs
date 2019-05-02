using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VivesTrein.ViewModels
{
    public class ShoppingCartVM
    {
        public List<CartVM> Cart { get; set; }
    }

    public class CartVM
    {
        public int ReisId { get; set; }
        public string Naam { get; set; }
        public string VertrekStad { get; set; }
        public string AankomstStad { get; set; }
        public int Aantal { get; set; }
        public float Prijs { get; set; }
        public System.DateTime DateCreated { get; set; }
    }
}
