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
            foreach (var item in listBoeking) {

                item.Reis = reisService.FindById(item.ReisId);
                item.Reis.Vertrekstad = stadService.FindById(item.Reis.VertrekstadId);
                item.Reis.Bestemmingsstad = stadService.FindById(item.Reis.BestemmingsstadId);

                var list = treinritReisService.FindByReisId(item.ReisId);
            }

            return View(listBoeking);
        }

        public IActionResult Annuleren (int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Boeking boeking = boekingService.FindById(id);

            if (boeking.Status == "Betaald")
            {
                boeking.Status = "Geannuleerd";
                boekingService.Update(boeking);
            }

            return RedirectToAction("Index");
        }
    }
}