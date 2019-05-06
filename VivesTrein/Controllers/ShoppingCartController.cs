using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VivesTrein.Extentions;
using VivesTrein.ViewModels;
using VivesTrein.Service;
using VivesTrein.Domain;
using VivesTrein.Domain.Entities;

namespace VivesTrein.Controllers
{
    public class ShoppingCartController : Controller
    {
        private ReisService reisService;

        public IActionResult Index()
        {
            ShoppingCartVM cartList =
                HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart");

            // call sessionId
            var SessionId = HttpContext.Session.Id;

            return View(cartList);
        }

        public IActionResult Delete(int? reisId)
        {
            if (reisId == null)
            {
                return NotFound();
            }

            reisService = new ReisService();
            ShoppingCartVM cartList = HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart");

            var itemToRemove = cartList.Cart.FirstOrDefault(r => r.ReisId == reisId);
            if (itemToRemove != null)
            {
                cartList.Cart.Remove(itemToRemove);
                HttpContext.Session.SetObject("ShoppingCart", cartList);
                reisService.Delete(reisService.Get(Convert.ToInt16(reisId)));
            }

            if (cartList.Cart.Count != 0)
            {
                return View("index", cartList);
            }
            else
            {
                return RedirectToAction("Index", "Reis");
            }
            
        }
    }
}