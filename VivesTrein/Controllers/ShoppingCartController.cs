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
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace VivesTrein.Controllers
{
    public class ShoppingCartController : Controller
    {
        private ReisService reisService;
        private BoekingService boekingService;
        private TreinritReisService treinritreisService;

        public IActionResult Index()
        {
            ShoppingCartVM cartList =
                HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart");

            treinritreisService = new TreinritReisService();

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
                var toDeleteTreinritreis = treinritreisService.FindByReisId(Convert.ToInt16(reisId));

                foreach (TreinritReis treinritreis in toDeleteTreinritreis)
                {
                    treinritreisService.Delete(treinritreis);
                }
                reisService.Delete(reisService.FindById(Convert.ToInt16(reisId)));
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

        [Authorize]
        [HttpPost]
        public IActionResult Payment(ShoppingCartVM model)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }

            List<CartVM> carts = model.Cart;

            // opvragen UserId
            string userID = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            reisService = new ReisService();
            boekingService = new BoekingService();

            try
            {
                Boeking boeking; // in domain
                foreach (CartVM cart in carts)
                { // create order object
                    boeking = new Boeking();
                    boeking.UserId = userID;
                    boeking.ReisId = cart.ReisId;
                    boeking.Status = "Betaald";
                    //boeking.Created = DateTime.UtcNow;

                    boekingService.Create(boeking);
                }
            }
            catch (Exception e)
            {
                Console.Write(e);
            }

            return View();
        }
    }
}