using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Etkezde.Models;
using Etkezde.Database;
using System.Data.SQLite;

namespace Etkezde.Controllers
{
    public class HomeController : Controller
    {
        private readonly FoodRepository _foodRepository;
        private readonly SQLiteConnectionStringBuilder _connectionStringBuilder = new SQLiteConnectionStringBuilder();  
        private readonly string _connectionString;        
        private static OrderItemViewModel OrderItem = new OrderItemViewModel();
        private static List<Tuple<string, int>> _basket = new List<Tuple<string, int>>();

        public HomeController()
        {
            _connectionStringBuilder.DataSource = "./Database/memory.db";
            _connectionString = _connectionStringBuilder.ConnectionString;
            _foodRepository = new FoodRepository(_connectionString);
        }

        [Route("/")]
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Basket = _basket;
            return View(OrderItem);
        }

        [HttpPost]
        public IActionResult Index(OrderItemViewModel model)
        {
            if(!GetEmployeeIdsFromDatabase().Contains(model.EmployeeId.GetValueOrDefault()) || model.EmployeeId == null)
            {
                foreach(var item in GetEmployeeIdsFromDatabase())
                {
                    System.Console.WriteLine(item);
                }
            }
            OrderItem.EmployeeName = _foodRepository.GetEmployeeName(model.EmployeeId.GetValueOrDefault()); // VALIDÁLNI KELL, HGOY VAN-E ILYEN ID
            OrderItem.EmployeeId = model.EmployeeId;
            return View(OrderItem);
        }

        private List<int> GetEmployeeIdsFromDatabase()
        {
            return _foodRepository.GetEmployeeIds();            
        }

        public IActionResult OnPostOrderItems(OrderItemViewModel model)
        {
            ViewBag.Basket = _basket;
            OrderItem.ItemName = model.ItemName;
            OrderItem.Quantity = model.Quantity;
            _basket.Add(Tuple.Create(OrderItem.ItemName, OrderItem.Quantity.GetValueOrDefault()));
            return RedirectToAction("Index", OrderItem);
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
