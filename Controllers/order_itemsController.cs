using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using u21528498_Hw06.Models;
using PagedList.Mvc;
using PagedList;


namespace u21528498_Hw06.Controllers
{
    public class order_itemsController : Controller
    {
        private BikeStoresEntities db = new BikeStoresEntities();

        // GET: order_items
        public ActionResult Index(DateTime date, int? i)
        {
            var order_items = db.order_items.Include(o => o.product).Include(o => o.order);
            //List<order_items> prodDate = db.order_items.Where(y => y.order.order_date <= date).Select(p => new order_items { order_id = p.order_id, category = p.product.category.category_name, product = db.products.Where(x => x.product_id == p.product_id).FirstOrDefault(), quantity = p.quantity, list_price = p.list_price, Total = (p.list_price * p.quantity), dates = db.orders.Where(o => o.order_id == p.order_id).FirstOrDefault().order_date }).ToList().ToPagedList(i ?? 1,2);
            List<order_items> prodData = db.order_items.ToList();
            // return View(db.order_items.Where(y => y.order.order_date <= date).Select(p => new order_items { order_id = p.order_id, category = p.product.category.category_name, product = db.products.Where(x => x.product_id == p.product_id).FirstOrDefault(), quantity = p.quantity, list_price = p.list_price, Total = (p.list_price * p.quantity), dates = db.orders.Where(o => o.order_id == p.order_id).FirstOrDefault().order_date }).ToList().ToPagedList(i ?? 1, 2));
            //return View(db.products.Where(x => x.product_name.StartsWith(search) || search == null).ToList().ToPagedList(i ?? 1, 10)); ;

           // return View(order_items.ToList().ToPagedList(i ?? 1,2));
            return View(db.order_items.Where(x => x.order.order_date == date).ToList().ToPagedList(i ?? 1,10));
        }

        // GET: order_items/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            order_items order_items = db.order_items.Find(id);
            if (order_items == null)
            {
                return HttpNotFound();
            }
            return View(order_items);
        }

        // GET: order_items/Create
        public ActionResult Create()
        {
            ViewBag.product_id = new SelectList(db.products, "product_id", "product_name");
            ViewBag.order_id = new SelectList(db.orders, "order_id", "order_id");
            return View();
        }

        // POST: order_items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "order_id,item_id,product_id,quantity,list_price,discount")] order_items order_items)
        {
            if (ModelState.IsValid)
            {
                db.order_items.Add(order_items);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.product_id = new SelectList(db.products, "product_id", "product_name", order_items.product_id);
            ViewBag.order_id = new SelectList(db.orders, "order_id", "order_id", order_items.order_id);
            return View(order_items);
        }

        // GET: order_items/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            order_items order_items = db.order_items.Find(id);
            if (order_items == null)
            {
                return HttpNotFound();
            }
            ViewBag.product_id = new SelectList(db.products, "product_id", "product_name", order_items.product_id);
            ViewBag.order_id = new SelectList(db.orders, "order_id", "order_id", order_items.order_id);
            return View(order_items);
        }

        // POST: order_items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "order_id,item_id,product_id,quantity,list_price,discount")] order_items order_items)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order_items).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.product_id = new SelectList(db.products, "product_id", "product_name", order_items.product_id);
            ViewBag.order_id = new SelectList(db.orders, "order_id", "order_id", order_items.order_id);
            return View(order_items);
        }

        // GET: order_items/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            order_items order_items = db.order_items.Find(id);
            if (order_items == null)
            {
                return HttpNotFound();
            }
            return View(order_items);
        }

        // POST: order_items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            order_items order_items = db.order_items.Find(id);
            db.order_items.Remove(order_items);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
