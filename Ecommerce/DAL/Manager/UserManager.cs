using DAL.Model;
//using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Manager
{
    public class UserManager
    {
        Model1 context = new Model1();

        public string userRegistration(Users Obj)
        {
            int result = 0;
            var objUser = context.Users.Where(e => e.email == Obj.email && e.name == Obj.name && e.status != "D").SingleOrDefault();
            if (objUser == null)
            {
                Obj.status = "A";
                context.Users.Add(Obj);
                string subject = "Welcome";
                string body = "Thanks for register";
                SendEmail(Obj.email, subject, body);
                result = context.SaveChanges();
            }


            if (result > 0)
            {
                return Obj.id.ToString();
            }
            else
            {
                return "Error";
            }



        }
        public string UpdateUser(Users ur)
        {

            int result = 0;
            var objuser = context.Users.Where(e => e.id == ur.id && e.status != "D").SingleOrDefault();
            if (objuser != null)
            {


                objuser.name = ur.name == null ? objuser.name : ur.name;
                objuser.email = ur.email == null ? objuser.email : ur.email;
                objuser.phone = ur.phone == null ? objuser.phone : ur.phone;
                objuser.address = ur.address == null ? objuser.address : ur.address;
                objuser.image = ur.image == null ? objuser.image : ur.image;
                objuser.Role = ur.Role == null ? objuser.Role : ur.Role;
                objuser.password = ur.password == null ? objuser.password : ur.password;
                objuser.status = ur.status == null ? objuser.status : ur.status;
                objuser.lastModifiedBy = ur.lastModifiedBy == null ? objuser.lastModifiedBy : ur.lastModifiedBy;
                objuser.lastModifiedDate = DateTime.Now.ToString();
                context.Entry(objuser).State = EntityState.Modified;
                result = context.SaveChanges();
            }

            if (result > 0)
            {
                return ur.id.ToString();
            }
            else
            {
                return "Error";
            }


        }
        public int DeleteUser(int id)
        {
            int result = 0;
            Users return_ui = new Users();
            var de = context.Users.Where(e => e.id == id && e.status != "D").SingleOrDefault();
            if (de != null)
            {
                de.status = "D";
                context.Entry(de).State = EntityState.Modified;
                result = context.SaveChanges();

            }

            return result;

        }

        public List<Users> allUsers()
        {
            return context.Users.Where(e => e.status != "D").ToList();
        }
        public Users userDetails(int Id)
        {
            Users return_Obj = new Users();
            return return_Obj = context.Users.Where(e => e.id == Id && e.status != "D").SingleOrDefault();
        }

        public Users Login(Users a)
        {
            try
            {
                var objUser = context.Users.Where(e => e.email == a.email && e.password == a.password && e.status != "D").FirstOrDefault();
                return objUser;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public string UserPatch(Users ur)
        {
            var objuser = context.Users.Where(e => e.id == ur.id && e.status != "D").SingleOrDefault();
            if (objuser != null)
            {
                return "Error";
            }
            else
            {
                objuser.name = ur.name == null ? objuser.name : ur.name;
                objuser.email = ur.email == null ? objuser.email : ur.email;
                objuser.phone = ur.phone == null ? objuser.phone : ur.phone;
                objuser.address = ur.address == null ? objuser.address : ur.address;
                objuser.image = ur.image;
                objuser.Role = ur.Role == null ? objuser.Role : ur.Role;
                objuser.password = ur.password == null ? objuser.password : ur.password;
                objuser.status = ur.status == null ? objuser.status : ur.status;

                context.Entry(objuser).State = EntityState.Modified;
                context.SaveChanges();
                return objuser.id.ToString();
            }
        }
        private void SendEmail(string email, string subject, string body)
        {
            var host = "smtp.gmail.com";
            var port = 587;

            var name = "dotstore2023@gmail.com";

            var Password = "ltjfandirqwqziwe";

            var message = new MimeMessage();

            message.From.Add(new MailboxAddress("Admin", "dotstore2023@gmail.com"));
            message.To.Add(new MailboxAddress("Admin", email));
            message.Subject = "Thanks for choosing";

            var bodyBuilder = new BodyBuilder();

            bodyBuilder.HtmlBody = "<P> Thanks for registering with our website. </p>";
            message.Body = bodyBuilder.ToMessageBody();

            var client = new MailKit.Net.Smtp.SmtpClient();

            client.Connect(host, port, SecureSocketOptions.Auto);
            client.Authenticate(name, Password);

            client.Send(message);
            client.Disconnect(true);
        }
    }
}
