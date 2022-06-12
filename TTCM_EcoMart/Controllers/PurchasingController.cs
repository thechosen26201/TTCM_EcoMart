using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TTCM_EcoMart.Models;

namespace TTCM_EcoMart.Controllers
{
    public class PurchasingController : Controller
    {
        Beta_E_CommerceEntities1 db = new Beta_E_CommerceEntities1();
        int q = 0;
        string id2;
        // GET: Purchasing
        
        public Cart GetCart()
        {
            Cart cart = Session["Cart"] as Cart;
            if(cart == null || Session["Cart"] == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }

        public ActionResult AddToCart(string product_ID)
        {
            var pro = db.tblProducts.SingleOrDefault(s => s.product_ID == product_ID);
            if(pro != null)
            {
                GetCart().Add(pro);
            }
            return RedirectToAction("ShowToCart", "Purchasing");
        }

        public ActionResult ShowToCart()
        {
            if (Session["Cart"] == null)
            {
                return RedirectToAction("ShowToCart", "Purchasing");
            }
            Cart cart = Session["Cart"] as Cart;
            return View(cart);
        }
        public ActionResult Update_Quantity_Cart(FormCollection form)
        {
            Cart cart = Session["Cart"] as Cart;
            string id = form["id_product"];
            int quantity = int.Parse(form["Quantity"]);
            cart.Update_Quantity_Shopping(id, quantity);
            q = quantity;
            
            return RedirectToAction("ShowToCart", "Purchasing");
        }
        // Xóa giỏ hàng
        public ActionResult RemoveCart(string id)
        {
            Cart cart = Session["Cart"] as Cart;
            cart.Remove_CartItem(id);
            return RedirectToAction("ShowToCart", "Purchasing");
        }

        public PartialViewResult BagCart()
        {
            var i_item = 0;
            Cart cart = Session["Cart"] as Cart;
            if(cart != null)
            {
                i_item = cart.Total_quantity();
            }
            ViewBag.cartInfo = i_item;
            return PartialView("BagCart");
        }
        public ActionResult Checkout(string id)
        {
            if(Session["Account"] == null || Session["Account"].ToString() == "")
            {
                return RedirectToAction("DangNhap", "NguoiDung");
            }
            else
            {
               
                try
                {
                    Cart cart = Session["Cart"] as Cart;
                    tblBill bill = new tblBill();
                    tblAccount acc = Session["Account"] as tblAccount;
                    bill.Bill_ID = "B10";
                    bill.Account_ID = "ACC001";
                    bill.Bill_status = false;
                    bill.Total = null;
                    bill.Payment_ID = "PMO1";
                    bill.PurchasedDate = DateTime.Now;
                    db.tblBills.Add(bill);
                    foreach(var item in cart.Items)
                    {
                        tblBill_details details = new tblBill_details();
                        details.Bill_ID = bill.Bill_ID;
                        details.Bill_details_ID = "DT10";
                        details.Quantity = item.shopping_quantity;
                        //details.Total_prices = Convert.ToDecimal(details.Quantity * item.shopping_product.prices);
                        db.tblBill_details.Add(details);
                    }
                    db.SaveChanges();
                    cart.ClearCart();
                    return View();
                }
                catch (Exception ex)
                {

                }
            }
            
            return View();
        }
    }
}