using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Etkezde.Models
{
    public class OrderItemViewModel
    {
        [Required(ErrorMessage = "You must enter a value for the First Name field!")]
        public int? EmployeeId { get; set; } // ne legyen nullable
        public string EmployeeName { get; set; }
        public string ItemName { get; set; }
        public int? Quantity { get; set; }
    }
}