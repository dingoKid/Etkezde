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

        [Required(ErrorMessage="Add meg a termeket!")]
        [Display(Name="Termek")]
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

        [Required(ErrorMessage="Add meg a mennyiseget!")]
        [RegularExpression("[1-9]|[1-9][0-9]", ErrorMessage="Helytelen mennyiseg!")]
        [Display(Name="Darab")]
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