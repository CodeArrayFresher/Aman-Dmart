using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using NestedTable.Models;

namespace NestedTable.Models
{
    public class DBRepository
    {
        string constring = ConfigurationManager.ConnectionStrings["TestEntities"].ToString();
        public List<OrderModel> GetOrders()
        {
            List<OrderModel> OrderList = new List<OrderModel>();
            using (SqlConnection con = new SqlConnection(constring))
            {
                SqlCommand cmd = new SqlCommand("Get_Customer", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dtable = new DataTable();
                con.Open();
                sda.Fill(dtable);
                con.Close();

                foreach (DataRow dr in dtable.Rows)
                {
                    OrderList.Add(
                        new OrderModel
                        {
                            ID= Convert.ToInt32(dr["Order_Id"]),
                            OrderDate=Convert.ToDateTime(dr["Order_Date"]),
                            customerName= dr["Customer_Name"].ToString(),
                           
                           
                            
                           
                            //Total_Order_Quantity=Convert.ToInt32(dr["Quantity"]),
                            //Total_Amount=Convert.ToDouble(dr["Amount"]),
                            //Product_ID=Convert.ToInt32(dr["Product_Id"]),
                            //Product_Name=dr["Product_Name"].ToString(),
                            //Unit_Price=Convert.ToDouble(dr["Unit_Price"])
                        });

                }
               
            }

            return OrderList;
        }

        public List<ProductModel> GetProduct()
        {
            List<ProductModel> allproducts = new List<ProductModel>();
            using (SqlConnection con = new SqlConnection(constring))
            {
                SqlCommand cmd = new SqlCommand("Product_tbl", con);
                //cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandText = "ordertetails";
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dtable = new DataTable();
                con.Open();
                sda.Fill(dtable);
                con.Close();

                foreach (DataRow dr in dtable.Rows)
                {
                    allproducts.Add(
                        new ProductModel
                        {

                           Id = Convert.ToInt32(dr["Product_Id"]),
                            Name= dr["Product_Name"].ToString(),
                            Quantity = Convert.ToInt32(dr["Quantity"]),
                            Unit_Price = Convert.ToDouble(dr["Unit_Price"]),
                            Total_Amaount= Convert.ToDouble(dr["Amount"])
                        });

                }

            }

            return allproducts;
        }
    }
}