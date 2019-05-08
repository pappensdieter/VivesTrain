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

namespace VivesTrein.Controllers
{
    [Authorize]
    public class BoekingController : Controller
    {
        private BoekingService boekingService;
        
        public BoekingController()
        {
            boekingService = new BoekingService();
        }

        public IActionResult Index()
        {
            string userID = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            
            var list = boekingService.GetXForUser(userID, 10);

            return View(list);
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