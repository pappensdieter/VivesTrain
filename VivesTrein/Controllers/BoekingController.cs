using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VivesTrein.Service;
using VivesTrein.Domain;
using VivesTrein.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace VivesTrein.Controllers
{
    [Authorize]
    public class BoekingController : Controller
    {
        private BoekingService boekingService;
        private ReisService reisService;
        private StadService stadService;
        private TreinritService treinritService;
        private TreinritReisService treinritReisService;

        public BoekingController()
        {
            boekingService = new BoekingService();
            reisService = new ReisService();
            stadService = new StadService();
            treinritService = new TreinritService();
            treinritReisService = new TreinritReisService();
        }

        public IActionResult Index()
        {
            string userID = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            IEnumerable<Boeking> listBoeking = boekingService.GetXForUser(userID, 10);

            foreach (var item in listBoeking)
            {
                IEnumerable<TreinritReis> treinritReis = treinritReisService.FindByReisId(item.ReisId);
                Treinrit treinrit = treinritService.FindById(treinritReis.First().TreinritId);
                var vertekDatum = treinrit.Vertrek;

                var dateNow = DateTime.UtcNow;

                if (vertekDatum < dateNow)
                {
                    if (item.Status != "Voltooid")
                    {
                        item.Status = "Voltooid";
                        boekingService.Update(item);
                    }
                }
            }

                foreach (var item in listBoeking)
            {
                IEnumerable<TreinritReis> treinritReis = treinritReisService.FindByReisId(item.ReisId);
                Treinrit treinrit = treinritService.FindById(treinritReis.First().TreinritId);
                var vertekDatum = treinrit.Vertrek;

                var dateNow = DateTime.UtcNow;

                if (vertekDatum < dateNow)
                {
                    if (item.Status != "Voltooid")
                    {
                        item.Status = "Voltooid";
                        boekingService.Update(item);
                    }
                }


                item.Reis = reisService.FindById(item.ReisId);
                item.Reis.Vertrekstad = stadService.FindById(item.Reis.VertrekstadId);
                item.Reis.Bestemmingsstad = stadService.FindById(item.Reis.BestemmingsstadId);
                item.Reis.TreinritReis = treinritReisService.FindByReisId(item.ReisId);

                foreach (var rit in item.Reis.TreinritReis)
                {
                    rit.Treinrit = treinritService.FindById(Convert.ToInt16(rit.TreinritId));
                }
            }

            return View(listBoeking);
        }

        public IActionResult Annuleren (int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // vertrek datum opvragen
            Boeking boeking = boekingService.FindById(id);
            IEnumerable<TreinritReis> treinritReis = treinritReisService.FindByReisId(boeking.ReisId);
            Treinrit treinrit = treinritService.FindById(treinritReis.First().TreinritId);
            var vertekDatum = treinrit.Vertrek;

            var dateNow = DateTime.UtcNow;

            if (boeking.Status != "Geannuleerd")
            {
                if (boeking.Status != "Voltooid")
                {
                    if (dateNow.AddDays(3) < vertekDatum)
                    {
                        boeking.Status = "Geannuleerd";
                        boekingService.Update(boeking);
                    }
                    else
                    {
                        TempData["Message"] = "1";
                    }
                }
                else
                {
                    TempData["Message"] = "2";
                }
            }
            else
            {
                TempData["Message"] = "3"; 
            }

            return RedirectToAction("Index");
        }
    }
}