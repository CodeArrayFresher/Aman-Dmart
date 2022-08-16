using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace NestedTable.Models
{
    public class OrderDetailModel
    {
        public int ID { get; set; }
        public int OrderId { get; set; }
        public string ProductName { get; set; }
        public int p_Id { get; set; }

        public int quantity { get; set; }
        public double price { get; set; }
        public int subtotal { get; set; }

        public int cust_id { get; set; }
        public string customer_name { get; set; }
        public string customer_contact { get; set; }
        public string customer_email { get; set; }

      
        public string user_name { get; set; }
        public string user_contact { get; set; }
        public string user_email { get; set; }
        public DateTime order_date { get; set; }
        public List<OrderDetailModel> odlist { get; set; }

        public OrderModel order { get; set; }
        public List<OrderModel> OrderList { get; set; }
        public List<ProductModel> ProductList { get; set; }
        public List<OrderDetailModel> prlist { get; set; }
        public List<OrderDetailModel> clist { get; set; }
       
    }
}