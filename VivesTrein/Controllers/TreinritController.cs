using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VivesTrein.Service;
using VivesTrein.Domain.Entities;
using VivesTrein.Domain;
using System.Diagnostics;

namespace VivesTrein.Controllers
{
    public class TreinritController : Controller
    {
        private TreinritService treinritService;
        private ReisService reisService;
        private StadService stadService;

        public TreinritController()
        {
            treinritService = new TreinritService();
            reisService = new ReisService();
            stadService = new StadService();
        }


        public ActionResult Index()
        {
            var list = treinritService.GetAll();
            return View(list);
        }
    }
}