using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TTCM_EcoMart.Models;

namespace TTCM_EcoMart.Controllers
{
    public class ShopDetailsController : Controller
    {
        Beta_E_CommerceEntities1 db = new Beta_E_CommerceEntities1();
        // GET: ShopDetails
        public ActionResult Shop_details(string product_ID, string Category_ID)
        {
            var data = db.tblProducts.FirstOrDefault(p => p.product_ID == product_ID & 
                            p.tblCategory.Category_ID.Equals(Category_ID));
            return View(data);
        }
    }
}