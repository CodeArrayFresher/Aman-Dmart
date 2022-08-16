using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NestedTable.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int OId { get; set; }
        public int Quantity { get; set; }
        public double Unit_Price { get; set; }
        public double Total_Amaount { get; set; }
        public bool IsPercentile { get; set; }
        public int disc_value { get; set; }

        [DataType(DataType.Date)]
        public DateTime PEfromdate { get; set; }

        [DataType(DataType.Date)]
        public DateTime PEfTodate { get; set; }
        public ProductModel proid { get; set; }
        public List<ProductModel> productlist { get; set; }
      
      
        



    }
}