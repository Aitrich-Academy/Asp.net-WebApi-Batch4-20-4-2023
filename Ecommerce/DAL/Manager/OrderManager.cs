using DAL.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Manager
{
    public class OrderManager
    {

        string email;
        Model1 db_order = new Model1();
        public string AddOrder(Orders obj)
        {
            try
            {
                int result = 0;

                var objUser = db_order.Orders.Where(e => e.id == obj.id && e.status != "D").SingleOrDefault();
                var prod = db_order.Products.Where(e => e.id == obj.productId && e.status != "D").SingleOrDefault();


                if (objUser == null)
                {
                    obj.totalPrice = (int)prod.price * obj.quantity;
                    obj.status = "A";
                    db_order.Orders.Add(obj);
                    result = db_order.SaveChanges();
                }

                if (result > 0)
                {
                    return obj.id.ToString();
                }
                else
                {
                    return "Error";
                }
            }
            catch (Exception e)
            {
                return e.Message.ToString();
            }
        }
        public string getEmail(int? userid)
        {

            var objUser = db_order.Users.Where(e => e.id == userid && e.status != "D").SingleOrDefault();
            if (objUser != null)
            {
                email = objUser.email;
            }
            return email;
        }
        public string updateOrder(Orders obj,int Id)
        {
            int result = 0;
            var objUser = db_order.Orders.Where(e => e.id == Id && e.status != "D").SingleOrDefault();
            var prod = db_order.Products.Where(e => e.id == obj.productId && e.status != "D").SingleOrDefault();

            if (objUser != null)
            {
                // objUser.userId = obj.userId;
                objUser.productId = obj.productId;
                objUser.quantity = obj.quantity;
                objUser.totalPrice = (int)prod.price * obj.quantity;
                objUser.status = "A";
                objUser.lastModifiedBy = obj.lastModifiedBy;
                objUser.lastModifiedDate = DateTime.Now.ToString();
                db_order.Entry(objUser).State = EntityState.Modified;
                result = db_order.SaveChanges();
            }
            if (result > 0)
            {
                return obj.id.ToString();
            }
            else
            {
                return "Error";
            }

        }
        public List<Orders> allOrders()
        {
            return db_order.Orders.Where(e => e.status != "D").ToList();
        }
        public Orders order_details(int Id)
        {
            Orders return_ent = new Orders();
            return return_ent = db_order.Orders.Where(e => e.id == Id && e.status != "D").SingleOrDefault();

        }
        public void changeStock(int q,int pid)
        {
            int result = 0;
            Products return_pro = new Products();
            var pro = db_order.Products.Where(e => e.id == pid && e.status != "D").SingleOrDefault();
            if (pro != null)
            {
                pro.stock = pro.stock - q;
                db_order.Entry(pro).State = EntityState.Modified;
                result = db_order.SaveChanges();
            }
        }
        public List<Orders> get_orderByUserId(int Id)
        {
           
            return db_order.Orders.Where(e => e.userId == Id && e.status != "D").ToList();

        }
        public int delete_orders(int Id)
        {
            int result = 0;
            Orders return_ent = new Orders();
            var del = db_order.Orders.Where(e => e.id == Id && e.status != "D").SingleOrDefault();
            if (del != null)
            {
                del.status = "D";
                db_order.Entry(del).State = EntityState.Modified;
                result = db_order.SaveChanges();

            }
            return result;
        }
    }
}
