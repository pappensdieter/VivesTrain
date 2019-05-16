﻿using System;
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
using VivesTrein.Services;

namespace VivesTrein.Controllers
{
    public class ShoppingCartController : Controller
    {
        private ReisService reisService;
        private BoekingService boekingService;
        private TreinritReisService treinritreisService;
        public ShoppingCartController()
        {
            reisService = new ReisService();
            treinritreisService = new TreinritReisService();
            boekingService = new BoekingService();
        }


        public IActionResult Index()
        {
            ShoppingCartVM cartList =
                HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart");

            return View(cartList);
        }

        public IActionResult Delete(int? reisId)
        {
            if (reisId == null)
            {
                return NotFound();
            }
            
            ShoppingCartVM cartList = HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart");

            // remove item form shoppingcart
            var itemToRemove = cartList.Cart.FirstOrDefault(r => r.ReisId == reisId);
            if (itemToRemove != null)
            {
                cartList.Cart.Remove(itemToRemove);

                if (cartList.Cart.Count == 0)
                {
                    cartList = null;
                }
                HttpContext.Session.SetObject("ShoppingCart", cartList);

                // remove item from database
                try
                {
                    var toDeleteTreinritreis = treinritreisService.FindByReisId(Convert.ToInt16(reisId));

                    foreach (TreinritReis treinritreis in toDeleteTreinritreis)
                    {
                        treinritreisService.Delete(treinritreis);
                    }
                    reisService.Delete(reisService.FindById(Convert.ToInt16(reisId)));
                }
                catch (Exception e)
                {
                    Console.Write(e);
                }
            }

            return View("index", cartList);
        }

        public IActionResult DeleteAll()
        {
            ShoppingCartVM cartList = HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart");
            if (cartList != null)
            {
                // remove items from database
                try
                {
                    List<CartVM> carts = cartList.Cart;

                    foreach (CartVM cart in carts)
                    {
                        var toDeleteTreinritreis = treinritreisService.FindByReisId(Convert.ToInt16(cart.ReisId));

                        foreach (TreinritReis treinritreis in toDeleteTreinritreis)
                        {
                            treinritreisService.Delete(treinritreis);
                        }
                        reisService.Delete(reisService.FindById(Convert.ToInt16(cart.ReisId)));
                    }
                }
                catch (Exception e)
                {
                    Console.Write(e);
                }

                // empty shoppingcart
                cartList = null;
                HttpContext.Session.SetObject("ShoppingCart", cartList);
            }
            
            return View("index", cartList);
        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Payment(ShoppingCartVM model)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }

            List<CartVM> carts = model.Cart;

            // opvragen UserId en email
            string userID = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            string userEmail = User.FindFirst(ClaimTypes.Name).Value;

            // opvragen cartlist
            ShoppingCartVM cartList = HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart");
            try
            {
                Boeking boeking;
                var amount = cartList.Cart.Count();
                var begin = "<h2>Hallo " + userEmail + "</h2>" + "<p>U heeft " + amount + " reizen besteld.</p>"; // begin string voor email
                var tickets = "<b>Your tickets:</b>"; // begin voor alle tickets

                foreach (CartVM cart in carts)
                { 
                    // create order object
                    boeking = new Boeking();
                    boeking.UserId = userID;
                    boeking.ReisId = cart.ReisId;
                    boeking.Status = "Betaald";
                    boeking.Datecreated = DateTime.UtcNow;

                    boekingService.Create(boeking);


                    // reis in email steken
                    tickets += "<p>Ticket: " + cart.ReisId + " voor " + cart.Naam + " van " + cart.VertrekStad + " naar " + cart.AankomstStad + "</p>";
                }


                // send email
                var body = string.Format(begin + tickets);
                EmailSender mail = new EmailSender();
                await mail.SendEmailAsync(userEmail, "VivesTrein - Ticket(s)", body);


                // empty shoppingcart
                cartList = null;
                HttpContext.Session.SetObject("ShoppingCart", cartList);
            }
            catch (Exception e)
            {
                Console.Write(e);
            }

            return View();
        }
    }
}