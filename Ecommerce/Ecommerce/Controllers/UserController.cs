using DAL.Manager;
using DAL.Model;
using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Text;
using System.IO;
using System.Net.Http;
//using Microsoft.SqlServer.Management.SqlParser.Parser;
using System.Net;
using static Ecommerce.utils.Tocken_Manager;
using Ecommerce.utils;

namespace Ecommerce.Controllers
{
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {
        // GET: User
        #region user registration
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("userRegistration")]   // Url creation Route
        [HttpPost]
        public string userRegistration(UserRegistrationModel Obj)
        {
            UserManager mngr = new UserManager();
            UserRegistrationModel objUser = Obj;
            Users tbl_Obj = new Users();
            tbl_Obj.name = objUser.name;
            tbl_Obj.email = objUser.email;
            tbl_Obj.phone = objUser.phone;
            tbl_Obj.address = objUser.address;
            tbl_Obj.image = Encoding.ASCII.GetBytes(objUser.image);
            tbl_Obj.password = objUser.password;
            tbl_Obj.Role = objUser.Role;
            tbl_Obj.status = objUser.status;
            tbl_Obj.createdDate = DateTime.Now.ToString();
            tbl_Obj.createdBy = objUser.createdBy;
            tbl_Obj.lastModifiedDate = DateTime.Now.ToString();

            tbl_Obj.lastModifiedBy= objUser.lastModifiedBy;


            return mngr.userRegistration(tbl_Obj);
        }

        #endregion
       
        [System.Web.Http.HttpPatch]
        [Route("updateuser")]   // Url creation Route
        [HttpPost]
        public string updateUser(UserRegistrationModel Obj, int id)
        {
            UserManager mngr = new UserManager();
            UserRegistrationModel objUser = Obj;
            Users tbl_Obj = new Users();
            tbl_Obj.id = id;
            tbl_Obj.name = objUser.name;
            tbl_Obj.email = objUser.email;
            tbl_Obj.phone = objUser.phone;
            tbl_Obj.address = objUser.address;
            tbl_Obj.image = Encoding.ASCII.GetBytes(objUser.image);
            tbl_Obj.password = objUser.password;
            tbl_Obj.Role = objUser.Role;
            tbl_Obj.status = objUser.status;


            tbl_Obj.lastModifiedDate= DateTime.Now.ToString();

            tbl_Obj.lastModifiedBy = objUser.lastModifiedBy;

            return mngr.UpdateUser(tbl_Obj);
        }

        #region All users details

        //api/sample/allUsers
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("allUsers")]
        public List<UserRegistrationModel> allUsers()
        {
            UserManager mngr = new UserManager();
            List<UserRegistrationModel> return_List = new List<UserRegistrationModel>();
            List<Users> tbl_obj = mngr.allUsers();
            if (tbl_obj.Count != 0)
            {

                foreach (var Obj in tbl_obj)
                {
                    return_List.Add(new UserRegistrationModel
                    {
                        id = Obj.id,
                        name = Obj.name,
                        email = Obj.email,
                        phone = Obj.phone,
                        address = Obj.address,
                        image = BitConverter.ToString(Obj.image),
                        password = Obj.password,
                        Role = Obj.Role,
                        status = "A",
                        createdBy = Obj.createdBy,
                        createdDate = DateTime.Now.ToString(),
                        lastModifiedBy = Obj.lastModifiedBy,
                        lastModifiedDate = DateTime.Now.ToString(),


                    });
                }
            }
            return return_List;
        }

        #endregion

        #region Get user details by id

        //api/sample/userDetailsByID?id=1
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("userDetailsByID")]
        [HttpPost]
        public UserRegistrationModel userDetailsByID(string id)
        {
            UserManager mngr = new UserManager();
            UserRegistrationModel return_Obj = new UserRegistrationModel();
            Users tbl_obj = mngr.userDetails(Convert.ToInt32(id));

            if (tbl_obj != null)
            {
                return_Obj.id = tbl_obj.id;
                return_Obj.name = tbl_obj.name;
                return_Obj.email = tbl_obj.email;
                return_Obj.phone = tbl_obj.phone;
                return_Obj.address = tbl_obj.address;
                return_Obj.password = tbl_obj.password;
                return_Obj.Role = tbl_obj.Role;
                return_Obj.image = BitConverter.ToString(tbl_obj.image);
                return_Obj.createdBy = tbl_obj.createdBy;
                return_Obj.lastModifiedBy = tbl_obj.lastModifiedBy;
                return_Obj.createdDate = Convert.ToDateTime(tbl_obj.createdDate).ToShortDateString();
                return_Obj.lastModifiedDate = Convert.ToDateTime(tbl_obj.lastModifiedDate).ToShortDateString();
                return_Obj.status = tbl_obj.status;

            }
            return return_Obj;
        }


        #endregion
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("deleteuser")]
        [HttpPost]

        public string DeleteUse(string id)
        {
            UserManager mngr = new UserManager();
            UserRegistrationModel return_obj = new UserRegistrationModel();
            int uis = mngr.DeleteUser(Convert.ToInt32(id));
            if (uis > 0)
            {
                return "Deleted successfully";
            }
            else
            {
                return "error";
            }



        }
        [HttpPost]
        [Route("uploadFile")]
        public string UploadFile()
        {
            var file = HttpContext.Current.Request.Files.Count > 0 ?
            HttpContext.Current.Request.Files[0] : null;
            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(HttpContext.Current.Server.MapPath("~/images"), fileName);
                file.SaveAs(path);
            }

            return file != null ? "/images/" + file.FileName : null;
        }

        #region login

        [Route("Login")]
        [HttpPost]

        public HttpResponseMessage Login(UserRegistrationModel a)
        {
            UserRegistrationModel tbl_obj = (UserRegistrationModel)a;
            UserManager mngr = new UserManager();
            Users return_Obj = new Users();

            if (tbl_obj != null)
            {
                return_Obj.email = tbl_obj.email;
                return_Obj.password = tbl_obj.password;
            }
            Users result = mngr.Login(return_Obj);

            if (result != null)
            {
                // string token = TokenManager.GenerateToken(result);
                string token = Tocken_Manager.GenerateToken(result);
                LoginResponseDTO loginResponseDTO = new LoginResponseDTO();

                loginResponseDTO.Token = token;

                loginResponseDTO.email = result.email;
                loginResponseDTO.id = result.id;
                loginResponseDTO.address = result.address;
                loginResponseDTO.phone = result.phone;
                loginResponseDTO.Role = result.Role;
                loginResponseDTO.name = result.name;

                ResponsDataDTO response = new ResponsDataDTO(true, "Success", loginResponseDTO);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid User name and password !!!!");
            }
        }
        #endregion

    }
}