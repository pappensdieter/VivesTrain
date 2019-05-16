using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VivesTrein.Models;
using VivesTrein.Services;
using VivesTrein.Utilities;
using VivesTrein.ViewModels;

namespace VivesTrein.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //Seed.CreateTreinritten().Wait();
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Contact(ContactVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var body = "<p>Email From: " + "{0} ({1})</p><p>Message:" + "</p><p>{2}</p>";

                    body = string.Format(body, model.Naam, model.Email, model.Message);
                    EmailSender mail = new EmailSender();
                    await mail.ReceiveEmailAsync(model.Email, "Contact VivesTrein", body);
                    return RedirectToAction("Sent");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            return View(model);
        }

        public IActionResult Sent()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
