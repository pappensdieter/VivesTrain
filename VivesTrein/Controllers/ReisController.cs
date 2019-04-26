﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VivesTrein.Service;
using VivesTrein.Domain.Entities;
using VivesTrein.Domain;

namespace VivesTrein.Controllers
{
    public class ReisController : Controller
    {
        private ReisService reisService;

        public ReisController()
        {
            reisService = new ReisService();
        }

        public IActionResult Index()
        {
            var list = reisService.GetAll();
            return View(list);
        }
    }
}