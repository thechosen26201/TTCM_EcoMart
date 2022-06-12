using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TTCM_EcoMart.Models;
namespace TTCM_EcoMart.Areas.Admin.Controllers
{
    public class AdminProductController : Controller
    {
        Beta_E_CommerceEntities1 db = new Beta_E_CommerceEntities1();
        //Thêm sản phẩm
        [HttpGet]
        public ActionResult ThemSanPham()
        {
            ViewBag.product_ID = new SelectList(db.tblProducts.ToList().OrderBy(n => n.product_ID), "product_ID", "product_Name");
            ViewBag.Category_ID = new SelectList(db.tblProducts.ToList().OrderBy(n => n.Category_ID), "Category_ID");

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemSanPham([Bind(Include = "product_ID, product_Name, Category_ID, prices, quantity, Color, Size, ImagePath")] tblProduct product)
        {
            if (ModelState.IsValid)
            {
                db.tblProducts.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }
        //Sửa sản phẩm
        [HttpGet]
        public ActionResult SuaSanPham(string product_ID)
        {
            if (product_ID == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            tblProduct product = db.tblProducts.Find(product_ID);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.product_ID = new SelectList(db.tblProducts.ToList().OrderBy(n => n.product_ID), "product_ID", "product_Name");
            ViewBag.Category_ID = new SelectList(db.tblProducts.ToList().OrderBy(n => n.Category_ID), "Category_ID");

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SuaSanPham([Bind(Include = "product_ID, product_Name, Category_ID, prices, quantity, Color, Size, ImagePath")] tblProduct product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.product_ID = new SelectList(db.tblProducts.ToList().OrderBy(n => n.product_ID), "product_ID", "product_Name");
            ViewBag.Category_ID = new SelectList(db.tblProducts.ToList().OrderBy(n => n.Category_ID), "Category_ID");

            return RedirectToAction("Index");
        }
        //Xoá sản phầm
        [HttpGet]
        public ActionResult XoaSanPham(string product_ID)
        {
            tblProduct product = db.tblProducts.Single(n => n.product_ID == product_ID);
            if (product == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(product);
        }
        [HttpPost, ActionName("XoaSanPham")]
        public ActionResult XacNhanXoa(string product_ID)
        {
            tblProduct product = db.tblProducts.Single(n => n.product_ID == product_ID);

            if (product == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            db.tblProducts.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}