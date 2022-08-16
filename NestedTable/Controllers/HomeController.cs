using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using NestedTable.Models;

namespace NestedTable.Controllers
{
    public class HomeController : Controller
    {
        DBRepository report = new DBRepository();
        // GET: Home

        //Get Orderlist table
        public ActionResult ListOfOrders()
        {
            var result = new OrderModel();
           // result.ProductList = report.GetProductdetail();
            result.OrderList = report.GetOrders();
            return View(result);
          
        }

        // get nested productlist table data
        public ActionResult _ListOfOrders(int id)
        {
            var model = new OrderModel();
            model.ProductList = report.GetProductbyid(id);
            return PartialView(model);
        }

        // get partial data while click on add btn
        public ActionResult _Orderpage()
        {
            var model = new OrderDetailModel();
            model.ProductList = report.Get_Products();
            model.OrderList = report.GetOrders();
            model.clist = report.GetCustomer();
            return PartialView(model);
        }

       // path Add new order 
        public ActionResult Orderpage()
        {
            var model = new OrderDetailModel();
            model.clist = report.GetCustomer();
            return View(model);
        }
        [HttpPost]
        public ActionResult orderpage(OrderDetailModel model)
        {
            var cust = model.cust_id;
            report.Add_Records(model,cust);
            return RedirectToAction("ListOfOrders");
        }

        //selected product price
        public JsonResult selecteddata(int id)
        {
           //var UnitPrice = report.Getselectedprice(id);
            var model = new ProductModel();
            model.productlist = report.Getselectedprice(id);
            double disc = 0;
            double price = 0;
            var val = 0;
            bool type = false;
          
            foreach (var item in model.productlist)
            {
                if (item.IsPercentile == true)
                {
                    disc =(item.Unit_Price) -(item.Unit_Price * item.disc_value / 100);
                }
                else
                {
                    disc = item.Unit_Price - item.disc_value;
                }
                price = item.Unit_Price;
                val = item.disc_value;
                type = item.IsPercentile;
            }
            return Json (new { unit_price = price,disc_price=disc,disc_val=val,disc_type=type}, JsonRequestBehavior.AllowGet);
        }

        //get orderd product list for edit
        public ActionResult editOrder(int id)
        {
            var model = new OrderDetailModel();
            model.odlist = report.editorder(id);
        
            foreach (var lis in model.odlist) {
                lis.ProductList= report.Get_Products();
            }
            return View(model);

        }

        //Save edit
        [HttpPost]
        public ActionResult editOrder(OrderDetailModel model)
        {
           
            report.Update_Order(model);
            
            return RedirectToAction("ListOfOrders");
        }

        //invoice page
        public ActionResult printfunk(int id)
        {
            var model = new OrderDetailModel();
            model.odlist = report.Get_invoice(id);
            model.prlist = report.editorder(id);
          
            return View(model);
        }

        //show all productlist table 
        public ActionResult Getorderids(string ids)
        {
            var model = new OrderModel();
            model.ProductList = report.show_alllist(ids);
            return PartialView("~/Views/Shared/_ListOfOrders.cshtml", model);
        }

        //product table data
        public ActionResult Product_List()
        {
            var model = new ProductModel();
            model.productlist = report.Get_Products();
            return View(model);
        }

        //update price of product
       public ActionResult _Add_Price(int id)
        {

            var model = new ProductModel();
            model.Id = id;
          
            return PartialView(model);
        }

        //save updated price
        [HttpPost]
         public ActionResult _Add_Price(ProductModel model)
        {
            if (ModelState.IsValid)
            {
               
                report.Update_Price(model);
            }
            return RedirectToAction("Product_List");
        }

        //Get discount table
        public ActionResult Discount_page()
        {
            var model = new ProductModel();
            model.productlist = report.Get_discounttbl();
            return View(model);
        }
        public ActionResult _Add_Discount()
        {
            var model = new ProductModel();
            model.productlist = report.Get_Products();
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult _Add_Discount(ProductModel model)
        {
                report.discount_insert(model);
            return RedirectToAction("Discount_page");
        }

        



    }
}  
