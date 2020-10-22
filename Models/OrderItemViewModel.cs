using System;
using System.Collections.Generic;

namespace Etkezde.Models
{
    public class OrderItemViewModel
    {
        public int? Id { get; set; }
        public string ItemName { get; set; }
        public int? Quantity { get; set; }
    }
}