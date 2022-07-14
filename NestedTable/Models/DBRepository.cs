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
        public List<OrderDetailModel> GetAllRecords()
        {
            List<OrderDetailModel> productlist = new List<OrderDetailModel>();
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
                    productlist.Add(
                        new OrderDetailModel
                        {
                            Id = Convert.ToInt32(dr["Order_Id"]),
                            Order_Date=Convert.ToDateTime(dr["Order_Date"]),
                            Customer_Name=dr["Customer_Name"].ToString(),
                            //Total_Order_Quantity=Convert.ToInt32(dr["Quantity"]),
                            //Total_Amount=Convert.ToDouble(dr["Amount"]),
                            //Product_ID=Convert.ToInt32(dr["Product_Id"]),
                            //Product_Name=dr["Product_Name"].ToString(),
                            //Unit_Price=Convert.ToDouble(dr["Unit_Price"])
                        });

                }
               
            }

            return productlist;
        }

        public List<ProductModel> GetProduct()
        {
            List<ProductModel> allproducts = new List<ProductModel>();
            using (SqlConnection con = new SqlConnection(constring))
            {
                SqlCommand cmd = new SqlCommand("get_products", con);
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

                            Product_ID = Convert.ToInt32(dr["Id"]),
                            Product_Name = dr["Name"].ToString(),
                            Quantity=Convert.ToInt32(dr["Quantity"]),
                            unitp=Convert.ToDouble(dr["Unit_Price"]),
                            amount=Convert.ToDouble(dr["Amount"])
                        });

                }

            }

            return allproducts;
        }
    }
}