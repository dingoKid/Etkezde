using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Etkezde.Models
{
    public class OrderItemViewModel
    {
        [Required(ErrorMessage = "You must enter a value for the First Name field!")]
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string ItemName { get; set; }
        public string Quantity { get; set; }

        public void Clear()
        {
            EmployeeId = null;
            EmployeeName = null;
            ItemName = null;
            Quantity = null;
        }
    }
}