using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace NestedTable.Models
{
    public class OrderDetailModel
    {
        public int ID { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quatity { get; set; }
        public decimal UnitPrice { get; set; }
        public OrderModel order { get; set; }
        public ProductModel product { get; set; }
    }
}