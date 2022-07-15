using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NestedTable.Models
{
    public class OrderModel
    {
        public int ID { get; set; }
        public Nullable<System.DateTime> OrderDate { get; set; }
        public int CustomerID { get; set; }
        public int CreatedBy { get; set; }
        public string customerName { get; set; }
        public List<CustomerModel> CustomerModelLsit { get; set; }
        public string allproducts { get; set; }
        public List<OrderModel> OrderList { get; set; }
        public List<ProductModel> ProductList { get; set; }
    }
}