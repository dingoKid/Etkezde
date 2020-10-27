using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Etkezde.Models
{
    public class OrderItemViewModel
    {
        private string _itemName;
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string ItemName { 
            get
            {
                return _itemName;
            }
            set 
            {
                if(!string.IsNullOrEmpty(value)) _itemName = char.ToUpper(value[0]) + value.Substring(1);
                else _itemName = null;
            }
        }
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