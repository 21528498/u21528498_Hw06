using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using u21528498_Hw06.Models;

namespace u21528498_Hw06.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        private readonly BikeStoresEntities db = new BikeStoresEntities();

        public ActionResult Report()
        {

            return View();
        }
        public string GetReports()
        {
            db.Configuration.ProxyCreationEnabled = false;
            object bikes = db.orders.Select(o => new
            {
                //orderid = o.order_id,

                month = o.order_date.Month,
                bike = db.order_items.Where(x => x.order_id == o.order_id && x.product.category.category_id == 6).ToList(),
            }).ToList();

            return JsonConvert.SerializeObject(bikes);
        }
    }
}