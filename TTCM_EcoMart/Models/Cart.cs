using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TTCM_EcoMart.Models;
namespace TTCM_EcoMart.Models
{
    public class CartItem
    {
        public tblProduct shopping_product { get; set; }
        public int shopping_quantity { get; set; }
    }
    public class Cart
    {

        List<CartItem> items = new List<CartItem>();
        public IEnumerable<CartItem> Items
        {
            get { return items; }
        }

        public void Add(tblProduct pro, int quantity = 1)
        {
            var item = items.SingleOrDefault(s => s.shopping_product.product_ID.Equals(pro.product_ID));
            if (item == null)
            {
                items.Add(new CartItem { shopping_product = pro, shopping_quantity = quantity });
            }
            else
            {
                item.shopping_quantity += quantity;
            }
        }

        public void Update_Quantity_Shopping(string id, int quantity)
        {
            var item = items.Find(s => s.shopping_product.product_ID.Equals(id));
            if(item != null)
            {
                item.shopping_quantity = quantity;
            }
        }

        public double Total_money()
        {
            var total = items.Sum(s => s.shopping_quantity * s.shopping_product.prices);
            return (double)total;
        }
        public void Remove_CartItem(string id)
        {
            items.RemoveAll(s => s.shopping_product.product_ID == id); 
        }

        public int Total_quantity()
        {
            return items.Sum(s => s.shopping_quantity);
        }

        //public string getID(string id)
        //{
        //    return items.Find(s => s.shopping_product.product_ID.Equals(id)).ToString();
        //}
        //public int Get_Quantity_Shopping(string id, int quantity)
        //{
        //    var item = items.Find(s => s.shopping_product.product_ID.Equals(id));
            
          
        //        return item.shopping_quantity = quantity;
            
        //}
        public void ClearCart()
        {
            items.Clear();
        }
    }
}