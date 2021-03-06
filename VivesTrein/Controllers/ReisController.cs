﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VivesTrein.Service;
using VivesTrein.Domain.Entities;
using VivesTrein.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using VivesTrein.ViewModels;
using VivesTrein.Extentions;

namespace VivesTrein.Controllers
{
    public class ReisController : Controller
    {
        private ReisService reisService;
        private StadService stadService;
        private TreinritService treinritService;
        private TreinritReisService treinritreisService;

        public ReisController()
        {
            reisService = new ReisService();
            stadService = new StadService();
            treinritreisService = new TreinritReisService();
            treinritService = new TreinritService();
        }

        public IActionResult Index()
        {
            ViewBag.lstStad = new SelectList(stadService.GetAll(), "Id", "Naam");
            return View();
        }

        [HttpPost]
        public IActionResult Index(ReisVM reisVM)
        {
            if (ModelState.IsValid)
            {
                //Data uit de VM halen
                Stad vertrekstad = stadService.FindById(reisVM.VerstrekStadId);
                Stad aankomststad = stadService.FindById(reisVM.AankomstStadId);
                DateTime date = reisVM.VertrekDatum;

                if (!((date - DateTime.Now).TotalDays < 14))
                {
                    ViewBag.lstStad = new SelectList(stadService.GetAll(), "Id", "Naam");
                    ViewBag.Error = "U kan alleen reizen boeken binnen 14 dagen.";
                    return View();
                }
                if(date < DateTime.Now)
                {
                    ViewBag.lstStad = new SelectList(stadService.GetAll(), "Id", "Naam");
                    ViewBag.Error = "Uw vertrekdatum moet in de toekomst zijn.";
                    return View();
                }

                Boolean bussiness = reisVM.BussinessClass;
                String naam = reisVM.Naam;
                int aantal = reisVM.Aantal;

                //Reis maken
                (Reis reis, String error) = reisService.MakeReis(naam, bussiness, vertrekstad, aankomststad, date, aantal);

                if (error.Equals(""))
                {
                    return View("ShowReis", reis); // later mss met ajax en de partial in reis
                }
                else
                {
                    ViewBag.lstStad = new SelectList(stadService.GetAll(), "Id", "Naam");
                    ViewBag.Error = error;
                    return View();
                }
                 
            }
            ViewBag.lstStad = new SelectList(stadService.GetAll(), "Id", "Naam");
            return View();
        }

        public IActionResult AddToCart(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Reis reis = reisService.FindById(Convert.ToInt16(id));

            // CartVM item aanmaken
            CartVM item = new CartVM
            {
                ReisId = reis.Id,
                Naam = reis.Naam,
                VertrekStad = stadService.FindById(reis.VertrekstadId).Naam,
                AankomstStad = stadService.FindById(reis.BestemmingsstadId).Naam,
                Aantal = reis.Aantal,
                Prijs = (float) reis.Prijs,
                DateCreated = DateTime.Now,
            };


            // ShoppingCartVM opvullen
            ShoppingCartVM shopping;

            if (HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart") != null)
            {
                shopping = HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart");
            }
            else
            {
                shopping = new ShoppingCartVM();
                shopping.Cart = new List<CartVM>();
            }

            shopping.Cart.Add(item);
            HttpContext.Session.SetObject("ShoppingCart", shopping);

            return RedirectToAction("Index", "ShoppingCart");
        }

        public IActionResult RemoveReis(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var toDeleteTreinritreis = treinritreisService.FindByReisId(Convert.ToInt16(id));

            foreach(TreinritReis treinritreis in toDeleteTreinritreis)
            {
                //Vrij plaatsen terugzetten
                Treinrit treinrit = treinritService.FindById(treinritreis.TreinritId);
                treinrit.Vrijeplaatsen = treinrit.Vrijeplaatsen + 1;
                treinritService.Update(treinrit);

                treinritreisService.Delete(treinritreis);
            }

            reisService.Delete(reisService.FindById(Convert.ToInt16(id)));


            return RedirectToAction("Index", "Reis");
        }
    }
}