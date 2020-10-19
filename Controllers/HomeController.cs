using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Etkezde.Models;
using Database;

namespace Etkezde.Controllers
{
    public class HomeController : Controller
    {
        private readonly FoodRepository _foodRepository;
        private readonly string _connectionString = "Data Source=.\\Database\\memory.db;";

        public HomeController()
        {
            _foodRepository = new FoodRepository(_connectionString);
        }

        public IActionResult Index()
        {

            return View();
        }

        [HttpGet]
        public IActionResult Eating()
        {
            int currentMonth = DateTime.Now.Month;
            var dolgozok = _foodRepository.GetEmployeeConsumptions(currentMonth);
            //return RedirectToAction("Eating", "Home", new {currentMonth});
            return View(dolgozok);
        }

        [HttpPost]
        public IActionResult Eating(int month)
        {
            var dolgozok = _foodRepository.GetEmployeeConsumptions(month);
            //ViewBag.Dolgozok = dolgozok;
            return View(dolgozok);
        }

        public IActionResult Consuming()
        {
            return View();
        }

        /*[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }*/
    }
}
