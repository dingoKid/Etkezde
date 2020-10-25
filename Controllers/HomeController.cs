using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
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
        private static Dictionary<string, int> _basket = new Dictionary<string, int>();

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
            ViewBag.Basket = _basket;
            if(!IsValidNumber(model.EmployeeId ??= "0") || !GetEmployeeIdsFromDatabase().Contains(int.Parse(model.EmployeeId))
                || (OrderItem.EmployeeId != model.EmployeeId && _basket.Count != 0))
            {
                return View(OrderItem);
            }
            OrderItem.EmployeeName = _foodRepository.GetEmployeeName(int.Parse(model.EmployeeId));
            OrderItem.EmployeeId = model.EmployeeId;
            return View(OrderItem);
        }       

        public IActionResult OnPostOrderItems(OrderItemViewModel model)
        {
            if(!string.IsNullOrEmpty(OrderItem.EmployeeId) && GetMenuCardFromDatabase().ContainsKey(model.ItemName ??= "") && IsValidNumber(model.Quantity ??= "0"))
            {
                ViewBag.Basket = _basket;
                OrderItem.ItemName = model.ItemName;
                OrderItem.Quantity = model.Quantity;
                UpdateBasket(OrderItem.ItemName, int.Parse(OrderItem.Quantity));
            }            
            return RedirectToAction("Index", OrderItem);
        }        

        public IActionResult OnPostModifyItem(OrderItemViewModel model)
        {
            if(_basket.Keys.Contains(model.ItemName ??= "") && IsValidNumber(model.Quantity ??= "0")) UpdateBasket(model.ItemName, int.Parse(model.Quantity)*(-1));
            return RedirectToAction("Index", OrderItem);
        }

        public IActionResult OnPostDeleteItem(OrderItemViewModel model)
        {
            if(_basket.Keys.Contains(model.ItemName ??= "")) _basket.Remove(model.ItemName);
            return RedirectToAction("Index", OrderItem);
        }

        public IActionResult OnPostFinishShopping()
        {
            if(_basket.Count != 0)
            {
                SaveBasketToDatabase(_basket);
                SaveConsumptionToDatabase(new EmployeeConsumption(int.Parse(OrderItem.EmployeeId), CalculateTotalCost(_basket)));
                _basket.Clear();
                OrderItem.Clear();
            }
            return RedirectToAction("Index", OrderItem);
        }

        public IActionResult OnPostClearBasket()
        {
            if(_basket.Count != 0)
            {                
                _basket.Clear();
            }
            return RedirectToAction("Index", OrderItem);
        }

        private void SaveBasketToDatabase(Dictionary<string, int> basket)
        {
            _foodRepository.SaveItems(basket);
        }

        private void SaveConsumptionToDatabase(EmployeeConsumption consumption)
        {
           _foodRepository.SaveConsumption(consumption);
        }

        private int CalculateTotalCost(Dictionary<string, int> basket)
        {
            Dictionary<string, int> items = _foodRepository.GetFoods();
            int totalCost = 0;
            foreach(var item in basket.Keys)
            {
                totalCost += items[item] * basket[item];
            }
            return totalCost;
        }

        private void UpdateBasket(string itemName, int quantity)
        {
            if(_basket.ContainsKey(itemName))
            {
                _basket[itemName] += quantity;
                if(_basket[itemName] <= 0) _basket.Remove(itemName);
            }
            else _basket.Add(itemName, quantity);
        }

        private bool IsValidNumber(string number)
        {
            return number.Length == number.Where(x => char.IsDigit(x)).Count() && int.Parse(number) > 0 ? true : false;
        }

        private List<int> GetEmployeeIdsFromDatabase()
        {
            return _foodRepository.GetEmployeeIds();            
        }

        private Dictionary<string, int> GetMenuCardFromDatabase()
        {
            return _foodRepository.GetFoods();
        }

        [HttpGet]
        public IActionResult Eating()
        {
            int currentMonth = DateTime.Now.Month;
            //var employees = _foodRepository.GetEmployeeConsumptions(currentMonth); // ez nem kell, ha a kövi sor már él sztem            
            return RedirectToAction("Eating", "Home", new {currentMonth});
            //return View(employees);
        }

        [HttpGet("/Home/Eating/id")]
        public IActionResult Eating(int month)
        {
            var employees = _foodRepository.GetEmployeeConsumptions(month);
            return View(employees);
        }

        [HttpGet]
        public IActionResult Consuming()
        {
            int currentMonth = DateTime.Now.Month;
            var products = _foodRepository.GetProductConsumption(currentMonth);

            //return RedirectToAction("Consuming", "Home", new {currentMonth});
            return View(products);
        }

        [HttpPost]
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
