using DAL.Manager;
using DAL.Model;
using Ecommerce.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;

namespace Ecommerce.Controllers
{
    [RoutePrefix("api/Order")]
    public class OrderController : ApiController
    {
        // GET: Order
        // GET: Orders
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("addOrder")]   // Url creation Route
        [HttpPost]
        public string Addorder(AddOrders Obj)
        {
            OrderManager mngr = new OrderManager();
            AddOrders objUser = Obj;
            Orders tbl_Obj = new Orders();
            tbl_Obj.userId = objUser.userId;
            tbl_Obj.productId = objUser.productId;
            tbl_Obj.quantity = objUser.quantity;
            tbl_Obj.totalPrice = objUser.totalprice;
            tbl_Obj.createdBy = objUser.createdBy;
            tbl_Obj.createdDate = DateTime.Now.ToString();
            tbl_Obj.lastModifiedBy = objUser.lastModifiedBy;
            tbl_Obj.lastModifiedDate = DateTime.Now.ToString();
            tbl_Obj.status = objUser.status;
            mngr.changeStock((int)tbl_Obj.quantity,(int)tbl_Obj.productId);
            string email = mngr.getEmail(tbl_Obj.userId);
            //string subject = "";
            // string body = " Thank you for registering with our website.";
            sendEmail(email);

            return mngr.AddOrder(tbl_Obj);
        }

        //[HttpPost]
        //[Route("uploadFile")]
        //public string UploadFile()
        //{
        //    var file = HttpContext.Current.Request.Files.Count > 0 ?
        //    HttpContext.Current.Request.Files[0] : null;
        //    if (file != null && file.ContentLength > 0)
        //    {
        //        var fileName = Path.GetFileName(file.FileName);
        //        var path = Path.Combine(HttpContext.Current.Server.MapPath("~/images"), fileName);
        //        file.SaveAs(path);
        //    }

        //    return file != null ? "/images/" + file.FileName : null;
        //}

       // [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("updateorder")]   // Url creation Route
        [HttpPost]
        public string updateOrder(AddOrders Obj, int id)
        {
            OrderManager mngr = new OrderManager();
            AddOrders objUser = Obj;
            Orders tbl_Obj = new Orders();
            //tbl_Obj.id = id;
            tbl_Obj.productId = objUser.productId;
            tbl_Obj.quantity = objUser.quantity;
            tbl_Obj.totalPrice = objUser.totalprice;
            tbl_Obj.status = objUser.status;
            // tbl_Obj.createdBy = objUser.createdBy;
            //tbl_Obj.createdDate = DateTime.Now.ToString();
            tbl_Obj.lastModifiedBy = objUser.lastModifiedBy;
            tbl_Obj.lastModifiedDate = DateTime.Now.ToString();
            return mngr.updateOrder(tbl_Obj,id);
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("allOrders")]
        [HttpPost]
        public List<AddOrders> AllOrders()
        {
            OrderManager mngr = new OrderManager();
            List<AddOrders> return_List = new List<AddOrders>();
            List<Orders> tbl_obj = mngr.allOrders();
            if (tbl_obj.Count != 0)
            {

                foreach (var obj in tbl_obj)
                {
                    return_List.Add(new AddOrders
                    {
                        id = obj.id,
                        userId = (int)obj.userId,
                        productId = (int)obj.productId,
                        quantity = (int)obj.quantity,
                        totalprice = (int)obj.totalPrice,
                        status = obj.status,
                        createdBy = obj.createdBy,
                        createdByDate = obj.createdDate,
                        lastModifiedBy = obj.lastModifiedBy,
                        lastModifiedDate = obj.lastModifiedDate,
                    });
                }
            }
            return return_List;
        }




        [AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("orderDetailsByID")]
        [HttpPost]
        public AddOrders orderDetailsByID(string id)
        {
            OrderManager mngr = new OrderManager();
            AddOrders return_Obj = new AddOrders();
            Orders tbl_obj = mngr.order_details(Convert.ToInt32(id));


            if (tbl_obj != null)
            {
                return_Obj.id = tbl_obj.id;
                return_Obj.userId = (int)tbl_obj.userId;
                return_Obj.productId = (int)tbl_obj.productId;
                return_Obj.quantity = (int)tbl_obj.quantity;
                return_Obj.totalprice = (int)tbl_obj.totalPrice;
                return_Obj.status = tbl_obj.status;
                return_Obj.createdBy = tbl_obj.createdBy;
                return_Obj.createdByDate = tbl_obj.createdDate;
                return_Obj.lastModifiedBy = tbl_obj.lastModifiedBy;
                return_Obj.lastModifiedDate = tbl_obj.lastModifiedDate;

            }
            return return_Obj;
        }

        [AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("orderByuserID")]
        [HttpPost]
        public List<AddOrders> orderByuserID(int id)
        {
            OrderManager mngr = new OrderManager();
            List<AddOrders> return_List = new List<AddOrders>();
            List<Orders> tbl_obj = mngr.get_orderByUserId(id);
            if (tbl_obj.Count != 0)
            {

                foreach (var obj in tbl_obj)
                {
                    return_List.Add(new AddOrders
                    {
                        id = obj.id,
                        userId = (int)obj.userId,
                        productId = (int)obj.productId,
                        quantity = (int)obj.quantity,
                        totalprice = (int)obj.totalPrice,
                        status = obj.status,
                        createdBy = obj.createdBy,
                        createdByDate = obj.createdDate,
                        lastModifiedBy = obj.lastModifiedBy,
                        lastModifiedDate = obj.lastModifiedDate,
                    });
                }
            }
            return return_List;
        }




        private void sendEmail(string email)
        {
            var host = "smtp.gmail.com";
            var port = 587;

            var username = "tripping.manage.official@gmail.com"; // get from Mailtrap
            var password = "athbirwmszunyunm"; // get from Mailtrap

            var message = new MimeMessage();

            message.From.Add(new MailboxAddress("User", "tripping.manage.official@gmail.com"));
            message.To.Add(new MailboxAddress("Admin", email));
            message.Subject = "Your order is confirmed!";

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = "<p>Thanks for registering with our website <b>.</p>";
            message.Body = bodyBuilder.ToMessageBody();

            var client = new SmtpClient();

            client.Connect(host, port, SecureSocketOptions.Auto);
            client.Authenticate(username, password);

            client.Send(message);
            client.Disconnect(true);

        }



        [AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("deleteOrders")]
        [HttpPost]
        public string deleteorder(string id)
        {
            OrderManager mngr = new OrderManager();
            AddOrders return_obj = new AddOrders();
            int rel = mngr.delete_orders(Convert.ToInt32(id));
            if (rel > 0)
            {
                return "Deleted Successfully";
            }
            else
            {
                return "Error";
            }

        }
    }
}