using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        sqlfunciones sqlin = new sqlfunciones();
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult info()
        {
            ViewData["Message"] = "Your application description info.";

            return View();
        }

        public IActionResult RegistroPersona()
        {
            var valD = sqlin.HabilitarBiometricoSI();
            sqlin.RegTiendaNO();
            return View();
        }

        public IActionResult Cuenta()
        {
            var valD = sqlin.HabilitarBiometricoSI();
            sqlin.RegTiendaSI();
            return View();
        }

        public IActionResult EjecutarInstruccion(int id)
        {
            //ViewData["DetailInfo"] = id;
            return View();
        }

        public IActionResult About()
        {



            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

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
