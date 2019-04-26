using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VivesTrein.Service;
using VivesTrein.Domain.Entities;
using VivesTrein.Domain;

namespace VivesTrein.Controllers
{
    public class TreinritController : Controller
    {
        private TreinritService treinritService;

        public TreinritController()
        {
            treinritService = new TreinritService();
        }


        public IActionResult Index()
        {
            var list = treinritService.GetAll();
            return View(list);
        }
    }
}