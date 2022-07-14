using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace NestedTable.Models
{
    public class OrderDetailModel
    {
        public int Id { get; set; }
        public DateTime Order_Date { get; set; }
        public string Customer_Name { get; set; }
        //public int Total_Order_Quantity { get; set; }
        //public double Total_Amount { get; set; }

        //public int Product_ID { get; set; }
        //public string Product_Name { get; set; }
        //public double Unit_Price { get; set; }
        //public ProductModel pro { get; set; }
        public List<ProductModel> productdata { get; set; }

        //public ProductModel products { get; set; }
        //public List<ProductModel> productList { get; set; }
    }
}