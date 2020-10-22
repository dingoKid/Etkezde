using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Etkezde.Models;
using Etkezde.Database;
using Microsoft.Data.Sqlite;

namespace Etkezde.Controllers
{
    public class HomeController : Controller
    {
        private readonly FoodRepository _foodRepository;
        private readonly SqliteConnectionStringBuilder connectionStringBuilder = new SqliteConnectionStringBuilder();  
        private readonly string _connectionString;
        private static int empID;

        public HomeController()
        {
            connectionStringBuilder.DataSource = "./Database/memory.db";
            _connectionString = connectionStringBuilder.ConnectionString;
            _foodRepository = new FoodRepository(_connectionString);
        }

        [Route("/")]
        [HttpGet]
        public IActionResult Index()
        {
            var obj = new OrderItemViewModel();
            return View(obj);
        }

        [HttpPost]
        public IActionResult Index(int emloyeeid)
        {
            empID = emloyeeid;
            return Content(empID.ToString());
        }

        [HttpPost]
        public IActionResult Index(OrderItemViewModel model)
        {
            if(model.Id == null) return View(model);
            return Content(model.ItemName.ToString());
        }

        [HttpGet]
        public IActionResult Eating()
        {
            int currentMonth = DateTime.Now.Month;
            var employees = _foodRepository.GetEmployeeConsumptions(currentMonth); // ez nem kell, ha a kövi sor már él sztem
            
            //return RedirectToAction("Eating", "Home", new {currentMonth});
            return View(employees);
        }

        [HttpPost]
        public IActionResult Eating(int month)
        {
            var employees = _foodRepository.GetEmployeeConsumptions(month);
            return View(employees);
        }

        public IActionResult Consuming()
        {
            int currentMonth = DateTime.Now.Month;
            var products = _foodRepository.GetProductConsumption(currentMonth);

            //return RedirectToAction("Consuming", "Home", new {currentMonth});
            return View(products);
        }

        public IActionResult Consuming(int month)
        {
            var products = _foodRepository.GetProductConsumption(month);
            return View(products);
        }

        /*[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }*/
    }
}
