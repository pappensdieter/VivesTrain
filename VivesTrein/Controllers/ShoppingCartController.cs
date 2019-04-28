using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VivesTrein.Extentions;
using VivesTrein.ViewModels;

namespace VivesTrein.Controllers
{
    public class ShoppingCartController : Controller
    {
        public IActionResult Index()
        {
            ShoppingCartVM cartList =
                HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart");
            

            return View(cartList);
        }
    }
}