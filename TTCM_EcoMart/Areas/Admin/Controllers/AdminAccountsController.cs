using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TTCM_EcoMart.Models;

namespace TTCM_EcoMart.Areas.Admin.Controllers
{
    public class AdminAccountsController : Controller
    {
        private Beta_E_CommerceEntities1 db = new Beta_E_CommerceEntities1();

        // GET: Admin/AdminAccounts
        public ActionResult Index()
        {
            return View(db.tblAccounts.ToList());
        }

        // GET: Admin/AdminAccounts/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblAccount tblAccount = db.tblAccounts.Find(id);
            if (tblAccount == null)
            {
                return HttpNotFound();
            }
            return View(tblAccount);
        }

        // GET: Admin/AdminAccounts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminAccounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Account_ID,Customer_ID,Customer_email,Customer_password,Customer_name,Customer_Address,Customer_gender,Customer_phone,DateOfBirth,User_Role,ID_wishlist,CreatedDate")] tblAccount tblAccount)
        {
            if (ModelState.IsValid)
            {
                db.tblAccounts.Add(tblAccount);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblAccount);
        }

        // GET: Admin/AdminAccounts/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblAccount tblAccount = db.tblAccounts.Find(id);
            if (tblAccount == null)
            {
                return HttpNotFound();
            }
            return View(tblAccount);
        }

        // POST: Admin/AdminAccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Account_ID,Customer_ID,Customer_email,Customer_password,Customer_name,Customer_Address,Customer_gender,Customer_phone,DateOfBirth,User_Role,ID_wishlist,CreatedDate")] tblAccount tblAccount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblAccount).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblAccount);
        }

        // GET: Admin/AdminAccounts/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblAccount tblAccount = db.tblAccounts.Find(id);
            if (tblAccount == null)
            {
                return HttpNotFound();
            }
            return View(tblAccount);
        }

        // POST: Admin/AdminAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            tblAccount tblAccount = db.tblAccounts.Find(id);
            db.tblAccounts.Remove(tblAccount);
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
