using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VivesTrein.Service;
using VivesTrein.Domain.Entities;
using VivesTrein.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using VivesTrein.ViewModels;

namespace VivesTrein.Controllers
{
    public class ReisController : Controller
    {
        private ReisService reisService;
        private StadService stadService;

        public ReisController()
        {
            reisService = new ReisService();
            stadService = new StadService();
        }

        public IActionResult Index()
        {
            ViewBag.lstStad = new SelectList(stadService.GetAll(), "Id", "Naam");
            return View();
        }

        [HttpPost]
        public IActionResult Index(ReisVM reisVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return PartialView("_ReisPartial");
        }

        //public IActionResult AddToCart()
        //{
        //    return View();
        //}
    }
}