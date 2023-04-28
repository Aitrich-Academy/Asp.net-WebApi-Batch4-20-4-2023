using DAL.Manager;
using DAL.Model;
using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;

namespace Ecommerce.Controllers
{
    [RoutePrefix("api/Categories")]
    public class CategoryController : ApiController
    {
        public string GetMe()
        {
            return "Hello develper, welcome to Sample App services.";
        }



        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("addCategories")]   // Url creation Route
        [HttpPost]
        public string Addcategory(AddCategories Obj)
        {
            CategoryManager mngr = new CategoryManager();
            AddCategories objUser = Obj;
            Category tbl_Obj = new Category();
            tbl_Obj.name = objUser.name;
            tbl_Obj.description = objUser.description;
            tbl_Obj.image = Encoding.ASCII.GetBytes(objUser.image);
            tbl_Obj.status = objUser.status;
            tbl_Obj.createdBy = objUser.createdBy;
            tbl_Obj.createdDate = DateTime.Now.ToString();
            tbl_Obj.lastModifiedBy = objUser.lastModifiedBy;
            tbl_Obj.lastModifiedDate = DateTime.Now.ToString();
            return mngr.AddCategory(tbl_Obj);
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

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("updatecategory")]   // Url creation Route
        [HttpPost]
        public string updateCategory(AddCategories Obj, int id)
        {
            CategoryManager mngr = new CategoryManager();
            AddCategories objUser = Obj;
            Category tbl_Obj = new Category();
            tbl_Obj.name = objUser.name;
            tbl_Obj.description = objUser.description;
            tbl_Obj.image = Encoding.ASCII.GetBytes(objUser.image);
            tbl_Obj.status = objUser.status;
            // tbl_Obj.createdBy = objUser.createdBy;
            //tbl_Obj.createdDate = DateTime.Now.ToString();
            tbl_Obj.lastModifiedBy = objUser.lastModifiedBy;
            tbl_Obj.lastModifiedDate = DateTime.Now.ToString();
            return mngr.updateCategory(tbl_Obj, id);
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("allCategories")]
        [HttpPost]
        public List<AddCategories> AllCategories()
        {
            CategoryManager mngr = new CategoryManager();
            List<AddCategories> return_List = new List<AddCategories>();
            List<Category> tbl_obj = mngr.allCategories();
            if (tbl_obj.Count != 0)
            {

                foreach (var obj in tbl_obj)
                {
                    return_List.Add(new AddCategories
                    {
                        id = obj.id,
                        name = obj.name,
                        description = obj.description,
                        image = BitConverter.ToString(obj.image),
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
        [Route("categoryDetailsByID")]
        [HttpPost]
        public AddCategories categoryDetailsByID(string id)
        {
            CategoryManager mngr = new CategoryManager();
            AddCategories return_Obj = new AddCategories();
            Category tbl_obj = mngr.category_details(Convert.ToInt32(id));


            if (tbl_obj != null)
            {
                return_Obj.id = tbl_obj.id;
                return_Obj.name = tbl_obj.name;
                return_Obj.description = tbl_obj.description;
                return_Obj.image = BitConverter.ToString(tbl_obj.image);
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
        [Route("deleteCategory")]
        [HttpPost]
        public string deletecategory(string id)
        {
            CategoryManager mngr = new CategoryManager();
            AddCategories return_obj = new AddCategories();
            int rel = mngr.delete_category(Convert.ToInt32(id));
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