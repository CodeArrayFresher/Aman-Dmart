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
        public ActionResult ListOfOrders()
        {
            var result = new OrderModel();
            result.ProductList = report.GetProduct();
            result.OrderList = report.GetOrders();
            return View(result);
          
        }
        public ActionResult jquerylesons()
        {
            return View();
        }
    }
}