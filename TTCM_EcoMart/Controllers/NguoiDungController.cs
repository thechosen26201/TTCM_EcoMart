using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TTCM_EcoMart.Models;

namespace TTCM_EcoMart.Controllers
{
    public class NguoiDungController : Controller
    {
        Beta_E_CommerceEntities1 db = new Beta_E_CommerceEntities1();
        
        // GET: NguoiDung
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangNhap(FormCollection f)
        {
            
            string email = f["Email"];
            string matkhau = f["Password"];
            string encrypt_pw = GetMD5(matkhau);
            tblAccount kh = db.tblAccounts.SingleOrDefault(n => n.Customer_email == email && n.Customer_password == encrypt_pw);
            

            if (kh != null)
            {
                ViewBag.Id_acc = kh.Account_ID;

                ViewBag.Success = "Chúc mừng đăng nhập thành công";
                Session["Account"] = kh;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Failed = "Sai tên tài khoản hoặc mật khẩu";
                return View();
            }
        }

        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangKy(tblAccount cs)
        {
            if (ModelState.IsValid)
            {
                string str = "U";
                int count = 0;
                string res = str + count.ToString();
                bool flag = false;
                tblAccount getAccID;
                tblAccount getEmail; 
                tblAccount getPhone;
                
                bool afterCheck = true;
                while (flag == false)
                {
                    getAccID = db.tblAccounts.SingleOrDefault(n => n.Account_ID.Equals(res));
                    if (getAccID != null)
                    {
                        count++;
                        res = str + count.ToString();
                    }
                    else
                    {
                        //flag = true;
                        int _getEmail = (from c in db.tblAccounts where c.Customer_email.Equals(cs.Customer_email) select c).Count();
                        int _getPhone = (from c in db.tblAccounts where c.Customer_phone.Equals(cs.Customer_phone) select c).Count();
                        if (_getEmail > 0 )
                        {
                            ViewBag.Email = "Email number has been used";
                            flag = true;
                            afterCheck = false;
                            return View();
                        }
                        else if(_getEmail > 0)
                        {
                            ViewBag.Phone = "Phone number has been used";
                            flag = true;
                            afterCheck = false;
                            return View();
                        }
                        else
                        {
                            flag = true;
                            afterCheck = true; // Nếu đúng ta mới gán afterCheck = true
                        }
                        
                    }
                }
                if(afterCheck == true)
                {
                    cs.Account_ID = res;
                    cs.ID_wishlist = "wl" + count;
                    cs.User_Role = true;
                    cs.DateOfBirth = Convert.ToDateTime(cs.DateOfBirth);
                    cs.Customer_password = GetMD5(cs.Customer_password);
                    cs.CreatedDate = DateTime.Now;
                    db.tblAccounts.Add(cs);
                    db.SaveChanges();
                    ModelState.Clear();
                    ViewBag.Message1 = "Đăng ký tài khoản thành công!!";
                    return RedirectToAction("DangNhap", "NguoiDung");
                }
            }


            return View();
        }
        //create a string MD5
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = System.Text.Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }
        
    }
}
