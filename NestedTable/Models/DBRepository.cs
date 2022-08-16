using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using NestedTable.Models;
using Newtonsoft.Json;



namespace NestedTable.Models
{
    public class DBRepository
    {
        string constring = ConfigurationManager.ConnectionStrings["TestEntities"].ToString();

        public List<ProductModel> GetProductbyid(int id)
        {
            List<ProductModel> orderidproduct = new List<ProductModel>();
            using (SqlConnection con = new SqlConnection(constring))
            {
                SqlCommand cmd = new SqlCommand("pbyorderid", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dtable = new DataTable();
                con.Open();
                sda.Fill(dtable);
                con.Close();
                foreach (DataRow dr in dtable.Rows)
                {
                    orderidproduct.Add(
                        new ProductModel
                        {
                            Id = Convert.ToInt32(dr["product_id"]),
                            Name = Convert.ToString(dr["product_name"]),
                            Unit_Price = Convert.ToDouble(dr["UnitPrice"]),
                            Quantity = Convert.ToInt32(dr["Quatity"]),
                            Total_Amaount = Convert.ToInt32(dr["total"])

                        });
                }
            }
            return orderidproduct;
        }
        public List<OrderModel> GetOrders()
        {
            List<OrderModel> OrderList = new List<OrderModel>();
            using (SqlConnection con = new SqlConnection(constring))
            {
                SqlCommand cmd = new SqlCommand("Get_Order", con);
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
                            ID = Convert.ToInt32(dr["Order_Id"]),
                            OrderDate = Convert.ToDateTime(dr["Order_Date"]),
                            customerName = dr["Customer_Name"].ToString()
                        });

                }

            }

            return OrderList;
        }

        public List<ProductModel> GetProductdetail()
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
                            OId = Convert.ToInt32(dr["ID"]),
                            Name = dr["Product_Name"].ToString(),
                            Quantity = Convert.ToInt32(dr["Quantity"]),
                            Unit_Price = Convert.ToDouble(dr["Unit_Price"]),
                            Total_Amaount = Convert.ToDouble(dr["Amount"])
                        });
                }
            }
            return allproducts;
        }
        public List<OrderDetailModel> GetCustomer()
        {
            List<OrderDetailModel> CustomerList = new List<OrderDetailModel>();
            using (SqlConnection con = new SqlConnection(constring))
            {
                SqlCommand cmd = new SqlCommand("Get_Customer", con);
                //cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandText = "ordertetails";
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dtable = new DataTable();
                con.Open();
                sda.Fill(dtable);
                con.Close();

                foreach (DataRow dr in dtable.Rows)
                {
                    CustomerList.Add(
                        new OrderDetailModel
                        {
                            cust_id = Convert.ToInt32(dr["ID"]),
                            customer_email = (dr["EmailID"]).ToString(),
                            customer_name = dr["Name"].ToString()

                        });
                }
            }
            return CustomerList;
        }

        public List<ProductModel> Get_Products()
        {
            List<ProductModel> Productdata = new List<ProductModel>();
            using (SqlConnection con = new SqlConnection(constring))
            {
                SqlCommand cmd = new SqlCommand("Pro_Names", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dtable = new DataTable();
                con.Open();
                sda.Fill(dtable);
                con.Close();

                foreach (
                    DataRow dr in dtable.Rows)
                {
                    Productdata.Add(
                        new ProductModel
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            Name = (dr["Name"]).ToString(),
                            Unit_Price = Convert.ToDouble(dr["Price"]),
                            PEfromdate=Convert.ToDateTime(dr["EffectiveFromDate"])
                            
                        });
                }
            }
            return Productdata;
        }

        //get selected price
        public List<ProductModel> Getselectedprice(int id)
        {
            List<ProductModel> Productdata = new List<ProductModel>();
            var dat = DateTime.Now;
            var date=dat.ToString("yyyy-MM-dd");
            using (SqlConnection con = new SqlConnection(constring))
            {

                SqlCommand cmd = new SqlCommand("Get_Products", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@fromdate", date);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dtable = new DataTable();
                con.Open();
                sda.Fill(dtable);
                con.Close();
                foreach (
                   DataRow dr in dtable.Rows)
                {
                    Productdata.Add(
                        new ProductModel
                        {
                            
                            Unit_Price = Convert.ToDouble(dr["Price"]),
                            IsPercentile = Convert.IsDBNull(dr["IsPercentile"]) ? false : Convert.ToBoolean(dr["IsPercentile"]),
                            disc_value = Convert.IsDBNull(dr["Discount_Value"]) ? 0 : Convert.ToInt32(dr["Discount_Value"])

                        });
                }
                //if (dtable.Rows.Count > 0)
                //{
                //    DataRow dr = dtable.Rows[0];

                //    price = Convert.ToInt32(dr["Price"]);
                //    Istrue = Convert.IsDBNull(dr["IsPercentile"]) ? true :Convert.ToBoolean(dr["IsPercentile"]);
                //    value = Convert.IsDBNull(dr["Discount_Value"]) ? 0 : Convert.ToInt32(dr["Discount_Value"]);
                //}
            }
            return Productdata;
        }
        //add records
        public void Add_Records(OrderDetailModel model, int cust)
        {
            var customerId = cust;
            using (SqlConnection con = new SqlConnection(constring))
            {
                con.Open();
                var output = JsonConvert.SerializeObject(model.prlist.Select(x => new { customerId, x.p_Id, x.quantity, x.price }));

                var cmd = new SqlCommand("storeorder", con);
                cmd.CommandType = CommandType.StoredProcedure;
                var param = cmd.Parameters.AddWithValue("@JsonData", output);
                cmd.Parameters.AddWithValue("@customerid", customerId);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        //get product details
        public List<OrderDetailModel> editorder(int id)
        {
            List<OrderDetailModel> Plist = new List<OrderDetailModel>();
            using (var con = new SqlConnection(constring))
            {
                SqlCommand cmd = new SqlCommand("edit_odetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dtable = new DataTable();
                con.Open();
                sda.Fill(dtable);
                con.Close();
                foreach (DataRow dr in dtable.Rows)
                {
                    Plist.Add(
                        new OrderDetailModel
                        {
                            p_Id = Convert.ToInt32(dr["ProductId"]),
                            quantity = Convert.ToInt32(dr["Quatity"]),
                            price = Convert.ToDouble(dr["UnitPrice"]),
                            ProductName = Convert.ToString(dr["Product_Name"]),
                            customer_name = Convert.ToString(dr["Name"]),
                            subtotal = Convert.ToInt32(dr["subtotal"])

                        });
                }
            }
            return Plist;

        }
        //Get Customer and user data
        public List<OrderDetailModel> Get_invoice(int id)
        {
            List<OrderDetailModel> invoiceList = new List<OrderDetailModel>();
            using (var con = new SqlConnection(constring))
            {
                SqlCommand cmd = new SqlCommand("get_invoice", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dtable = new DataTable();
                con.Open();
                sda.Fill(dtable);
                con.Close();
                foreach (DataRow dr in dtable.Rows)
                {
                    invoiceList.Add(
                        new OrderDetailModel
                        {
                            customer_name = Convert.ToString(dr["customer"]),
                            customer_email = Convert.ToString(dr["customer_email"]),
                            customer_contact = Convert.ToString(dr["customer_contact"]),

                            user_email = Convert.ToString(dr["user_email"]),
                            user_contact = Convert.ToString(dr["user_contact"]),
                            user_name = Convert.ToString(dr["UserName"]),

                            order_date = Convert.ToDateTime(dr["order_date"]),
                            OrderId = Convert.ToInt32(dr["order_id"])

                        });
                }
            }
            return invoiceList;

        }
        //Update Orders
        public void Update_Order(OrderDetailModel model)
        {

            using (SqlConnection con = new SqlConnection(constring))
            {
                con.Open();
                var output = JsonConvert.SerializeObject(model.prlist.Select(x => new { model.ID, x.p_Id, x.quantity, x.price }));
                var cmd = new SqlCommand("updateprocedure", con);
                cmd.CommandType = CommandType.StoredProcedure;
                var param = cmd.Parameters.AddWithValue("@updateddata", output);

                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        //get products lists
        public List<ProductModel> show_alllist(string ids)
        {
            List<ProductModel> prolists = new List<ProductModel>();
            using (SqlConnection con = new SqlConnection(constring))
            {
                SqlCommand cmd = new SqlCommand("showall_plist", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@list", ids);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dtable = new DataTable();
                con.Open();
                sda.Fill(dtable);
                con.Close();
                foreach (DataRow dr in dtable.Rows)
                {
                    prolists.Add(
                        new ProductModel
                        {
                            Id = Convert.ToInt32(dr["ProductId"]),
                            Name = Convert.ToString(dr["Name"]),
                            Unit_Price = Convert.ToDouble(dr["UnitPrice"]),
                            Quantity = Convert.ToInt32(dr["Quatity"]),
                            OId = Convert.ToInt32(dr["OrderId"]),
                            Total_Amaount = Convert.ToDouble(dr["Amount"])


                        });
                }
            }
            return prolists;
        }
        //gives active price value
        public void Update_Price(ProductModel model)
        {
            using (SqlConnection con = new SqlConnection(constring))
            {
                SqlCommand cmd = new SqlCommand("add_price", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", model.Id);
                cmd.Parameters.AddWithValue("@price", model.Unit_Price);
                cmd.Parameters.AddWithValue("@Effectfrom", model.PEfromdate);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

            }

        }

        public void discount_insert(ProductModel model)
        {
            using (SqlConnection con = new SqlConnection(constring))
            {
                SqlCommand cmd = new SqlCommand("discount_insert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@pid", model.Id);
                cmd.Parameters.AddWithValue("@price", model.Unit_Price);
                cmd.Parameters.AddWithValue("@isperce", model.IsPercentile);
                cmd.Parameters.AddWithValue("@dis_val", model.disc_value);
                cmd.Parameters.AddWithValue("@from_date", model.PEfromdate);
                cmd.Parameters.AddWithValue("@to_date", model.PEfTodate);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

            }

        }
        public List<ProductModel> Get_discounttbl()
        {
            List<ProductModel> Productdata = new List<ProductModel>();
            using (SqlConnection con = new SqlConnection(constring))
            {
                SqlCommand cmd = new SqlCommand("get_discount", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dtable = new DataTable();
                con.Open();
                sda.Fill(dtable);
                con.Close();

                foreach (
                    DataRow dr in dtable.Rows)
                {
                    Productdata.Add(
                        new ProductModel
                        {
                            Id = Convert.ToInt32(dr["Product_Id"]),
                            Name = (dr["Name"]).ToString(),
                            Unit_Price = Convert.ToDouble(dr["Unit_Price"]),
                            IsPercentile=Convert.ToBoolean(dr["IsPercentile"]),
                            disc_value=Convert.ToInt32(dr["Discount_Value"]),
                            PEfromdate = Convert.ToDateTime(dr["EffectiveFromDate"]),
                            PEfTodate=Convert.ToDateTime(dr["EffectiveToDate"])

                        });
                }
            }
            return Productdata;
        }
    }
}