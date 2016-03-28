using MVC5Course.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class TestController : BaseController
    {
        FabricsEntities db = new FabricsEntities();

        // GET: Test
        public ActionResult EDE()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EDE(EDEViewModel data)
        {
            return View(data);
        }

        public ActionResult CreateProduct()
        {
            
            var product = new Product()
            {
                ProductName = "Tercel",
                Active = true,
                Price = 100,
                Stock = 5
            };
            db.Product.Add(product);
            db.SaveChanges();

            return View(product);
        }

        public ActionResult ReadProduct(bool? Active)
        {
            var data = db.Product.AsQueryable();
            data=data.Where(p => p.ProductId > 1500).OrderByDescending(p => p.Price);

            if (Active.HasValue)
            {
                data = data.Where(p => p.Active == Active);
            }

            return View(data);
        }

        public ActionResult OneProduct(int id)
        {
            var data = db.Product.FirstOrDefault(p => p.ProductId == id);

            return View(data);
        }

        public ActionResult EditProduct(int id)
        {
            var data = db.Product.FirstOrDefault(p => p.ProductId == id);

            return View(data);
        }

        [HttpPost]
        public ActionResult EditProduct(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here


                return RedirectToAction("ReadProduct");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult UpdateProduct(int id)
        {
            var data = db.Product.FirstOrDefault(p => p.ProductId == id);
            if (data == null)
            {
                return HttpNotFound();
            }
            data.Price = data.Price * 2;
            try
            {
                db.SaveChanges();
            }
            catch(DbEntityValidationException ex)
            {
                string errorstr = string.Empty;
                foreach (var entityErrors in ex.EntityValidationErrors)
                {
                    foreach (var err in entityErrors.ValidationErrors)
                    {
                        errorstr += err.PropertyName + " : " + err.ErrorMessage +"<br>";

                    }
                }

                return Content(errorstr);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            

            return RedirectToAction("ReadProduct");
        }

        public ActionResult DeleteProduct(int id)
        {
            var data = db.Product.FirstOrDefault(p => p.ProductId == id);
            if (data == null)
            {
                return HttpNotFound();
            }

            //foreach (var item in db.OrderLine.Where(p=>p.ProductId==id))
            //{
            //    db.OrderLine.Remove(item);
            //}

            //foreach (var item in data.OrderLine.ToList())
            //{
            //    db.OrderLine.Remove(item);
            //}

            db.Database.ExecuteSqlCommand("DELETE FROM dbo.OrderLine WHERE ProductId=@p0", id);


            //db.OrderLine.RemoveRange(data.OrderLine);

            db.Product.Remove(data);
            db.SaveChanges();

            return RedirectToAction("ReadProduct");
        }

        public ActionResult ProductView()
        {
            var data = db.Database.SqlQuery<ProductViewModel>(@"select * from dbo.Product where Active=@p0 AND ProductName like @p1",true,"%Yellow%");


            return View(data);

        }

        public ActionResult ProductSP()
        {
            var data = db.GetProduct(true, "%Red%");
            return View(data);
        }
    }
}