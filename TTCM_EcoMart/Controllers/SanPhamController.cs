using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TTCM_EcoMart.Models;
namespace TTCM_EcoMart.Controllers
{
    public class SanPhamController : Controller
    {
        Beta_E_CommerceEntities1 db = new Beta_E_CommerceEntities1();
        // GET: SanPham
        public ActionResult Products(bool notice)
        {
            if(notice == true)
            {
                ViewBag.Notice = notice;
                return View();
            }
            else 
            {
                ViewBag.Notice = notice;
                return View();
            }
        }
        // Lấy ra tất cả các loại giày "Nam" đã có trong database và hiển thị lên partialView
        public PartialViewResult RightCategoriesArea(bool Categorize_gender)
        {
            //return PartialView(db.tblCategories.ToList());
            if(Categorize_gender == true)
            {
                var category_data = (from c in db.tblCategories where c.Gender == 1 select c).ToList();
                ViewBag.CategoryData = category_data;
                return PartialView();
            }
            else
            {
                var category_data = (from c in db.tblCategories where c.Gender == 0 select c).ToList();
                ViewBag.CategoryData = category_data;
                return PartialView();
            }
        }
        // Lấy ra toàn bộ sản phẩm theo giới tính 
        public PartialViewResult AllProductsPartialView(bool genderView)
        {
            if(genderView == true)
            {
                var data = (from p in db.tblProducts where p.tblCategory.Gender == 1 select p).ToList();
                ViewBag.Products = data;
                return PartialView();
            }
            else 
            {
                var data = (from p in db.tblProducts where p.tblCategory.Gender == 0 select p).ToList();
                ViewBag.Products = data;
                return PartialView();
            }


        }
        //Lấy ra các sản phẩm theo loại đã được hiển thị
        public ActionResult Selected_products(string Category_ID, byte Gender)
        {
            var data = (from p in db.tblProducts where p.tblCategory.Category_ID == Category_ID & 
                        p.tblCategory.Gender == Gender
                        select p).ToList();
            ViewBag.Detail_products = data;
            if(Gender == 0)
            {
                ViewBag.flag = true;
            }
            else
            {
                ViewBag.flag = false;
            }
            return View();
        }

    }
}