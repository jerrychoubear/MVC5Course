using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;
using System.Data.Entity.Validation;

namespace MVC5Course.Controllers
{
    public class EFController : Controller
    {
        FabricsEntities db = new FabricsEntities();

        // GET: EF
        public ActionResult Index()
        {
            var allData = db.Product.AsQueryable();

            var data = allData
                .Where(p => p.Active == true && 
                            p.ProductName.Contains("Black") &&
                            p.is刪除 == false)
                .OrderByDescending(p => p.ProductId);

            return View(data);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Product.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        public ActionResult Edit(int id)
        {
            var product = db.Product.Find(id);
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(int id, Product product)
        {
            if (ModelState.IsValid)
            {
                var item = db.Product.Find(id);

                item.ProductName = product.ProductName;
                item.Price = product.Price;
                item.Stock = product.Stock;
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        public ActionResult Details(int id)
        {
            //var product = db.Product.Find(id);
            var product = db.Database.SqlQuery<Product>(
                @"SELECT ProductId,
                         ProductName,
                         Price,
                         Active,
                         Stock,
                         is刪除,
                         CreatedOn
                    FROM dbo.Product
                   WHERE ProductId = @p0", id).FirstOrDefault();

            return View(product);
        }

        public ActionResult Delete(int id)
        {
            var product = db.Product.Find(id);

            //foreach (var item in product.OrderLine.ToList())
            //{
            //    db.OrderLine.Remove(item);
            //}
            //db.OrderLine.RemoveRange(product.OrderLine.ToList());

            //db.Product.Remove(product);

            product.is刪除 = true;

            try
            {
                db.SaveChanges();

            }
            catch (DbEntityValidationException ex)
            {
                foreach (var item in ex.EntityValidationErrors)
                {
                    foreach (var error in item.ValidationErrors)
                    {
                        Console.WriteLine($"{error.PropertyName}: {error.ErrorMessage}");
                    }
                }
            }
            return RedirectToAction("Index");
        }
    }
}