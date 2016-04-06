using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;

namespace MVC5Course.Controllers
{
    [Action執行時間]
    [Authorize]
    public class ProductsController : BaseController
    {
        

        //private FabricsEntities db = new FabricsEntities();

        // GET: Products
        [Action執行時間]
        public ActionResult Index()
        {
            //return View(db.Product.ToList());
            return View(repoProduct.All().Take(5));
        }

        [HttpPost]
        public ActionResult Index(IList<BatchUpdateProductViewModel> data)
        {
            if (ModelState.IsValid)
            {

                foreach (var item in data)
                {

                    var product = repoProduct.Find(item.ProductId);

                        product.Price = item.Price;

                        product.Active = item.Active;


                        product.Stock = item.Stock;
                    
                }
                repoProduct.UnitOfWork.Commit();

                return RedirectToAction("Index", "Products");
            }

            return View(repoProduct.All().Take(5));
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Product product = db.Product.Find(id);
            Product product = repoProduct.Find(id.Value);

            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        {
            if (ModelState.IsValid)
            {
                //db.Product.Add(product);
                //db.SaveChanges();
                repoProduct.Add(product);
                repoProduct.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Product product = db.Product.Find(id);
            Product product = repoProduct.Find(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        public ActionResult Edit(int id,FormCollection from)
        {
            //from 只是區別Function用，這裡沒用到

            //if (ModelState.IsValid)
            //{
            //    var dbproduct = (FabricsEntities)repoProduct.UnitOfWork.Context;

            //    dbproduct.Entry(product).State = EntityState.Modified;
            //    dbproduct.SaveChanges();


            //    TempData["ReturnMsg"] = product.ProductName + " 資料更新成功！";

            //    return RedirectToAction("Index");
            //}
            var product = repoProduct.Find(id);
            if (TryUpdateModel(product, new string[] { "ProductId", "ProductName", "Price", "Active", "Stock" }))
            {
                repoProduct.UnitOfWork.Commit();
                TempData["ReturnMsg"] = product.ProductName + " 資料更新成功！";
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Product product = db.Product.Find(id);
            Product product = repoProduct.Find(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Product product = db.Product.Find(id);
            //db.Product.Remove(product);
            //db.SaveChanges();
            Product product = repoProduct.Find(id);
            repoProduct.Delete(product);
            repoProduct.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
                repoProduct.UnitOfWork.Context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
