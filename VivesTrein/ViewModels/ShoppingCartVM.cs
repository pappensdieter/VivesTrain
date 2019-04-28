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
        public string Naam { get; set; }
        public int VertrekStadId { get; set; }
        public int AankomstStadId { get; set; }
        public float Prijs { get; set; }
        public System.DateTime VertrekDatum { get; set; }
        public System.DateTime DateCreated { get; set; }
    }
}
