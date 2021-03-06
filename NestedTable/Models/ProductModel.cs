using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NestedTable.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double Unit_Price { get; set; }
        public double Total_Amaount { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<System.DateTime> DateUpdated { get; set; }
        public int UpdatedBy { get; set; }
        public OrderDetailModel OrderDetail { get; set; }



    }
}