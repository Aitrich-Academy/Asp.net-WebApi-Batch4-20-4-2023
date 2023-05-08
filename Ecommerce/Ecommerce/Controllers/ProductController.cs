using DAL.Manager;
using DAL.Model;
using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;

namespace Ecommerce.Controllers
{
    [RoutePrefix("api / Product")]
    public class ProductController : ApiController
    {

        // GET: Product
        public string GetMe()
        {
            return "Hello develper, welcome to Sample App services.";
        }
        [System.Web.Http.AcceptVerbs("POST")]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("addProduct")]   // Url creation Route
        [System.Web.Http.HttpPost]
        public string Addproduct(AddProducts Obj)
        {
            ProductManager mngr = new ProductManager();
            AddProducts objUser = Obj;
            Products tbl_Obj = new Products();
            tbl_Obj.name = objUser.name;
            tbl_Obj.categoryId = objUser.categoryId;
            tbl_Obj.description = objUser.description;
            tbl_Obj.stock = objUser.stock;
            tbl_Obj.image = Encoding.ASCII.GetBytes(objUser.image);
            tbl_Obj.status = objUser.status;
            tbl_Obj.createdBy = "Admin";
            tbl_Obj.createdDate = DateTime.Now.ToString();
            tbl_Obj.lastModifiedBy = "Admin";
            tbl_Obj.lastModifiedDate = DateTime.Now.ToString();
            tbl_Obj.price = objUser.price;
            return mngr.AddProduct(tbl_Obj);
        }

        #region Update Product
        [System.Web.Http.Route("updateProduct")]   // Url creation Route
        [System.Web.Http.HttpPatch]
        public string updateProduct(int id, AddProducts Obj)
        {
            ProductManager mngr = new ProductManager();
            AddProducts objUser = Obj;
            Products tbl_Obj = new Products();
            tbl_Obj.id = id;
            tbl_Obj.name = objUser.name;
            tbl_Obj.description = objUser.description;
            tbl_Obj.stock = objUser.stock;
            tbl_Obj.image = Encoding.ASCII.GetBytes(objUser.image);
            tbl_Obj.status = objUser.status;
            tbl_Obj.createdBy = "Admin";
            tbl_Obj.createdDate = DateTime.Now.ToString();
            tbl_Obj.lastModifiedBy = "Admin";
            tbl_Obj.lastModifiedDate = DateTime.Now.ToString();
            tbl_Obj.price = objUser.price;
            return mngr.updateProduct(tbl_Obj);
        }
        #endregion

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("allProducts")]
        [System.Web.Http.HttpPost]
        public List<AddProducts> AllProducts()
        {
            ProductManager mngr = new ProductManager();
            List<AddProducts> return_List = new List<AddProducts>();
            List<Products> tbl_obj = mngr.allProducts();
            if (tbl_obj.Count != 0)
            {

                foreach (var obj in tbl_obj)
                {
                    return_List.Add(new AddProducts
                    {
                        id = obj.id,
                        name = obj.name,
                        categoryId = (int)obj.categoryId,
                        description = obj.description,
                        stock = (int)obj.stock,
                        image = BitConverter.ToString(obj.image),
                        status = obj.status,
                        createdBy = obj.createdBy,
                        createdByDate = obj.createdDate,
                        lastModifiedBy = obj.lastModifiedBy,
                        lastModifiedDate = obj.lastModifiedDate,
                        price = (int)obj.price,
                    });
                }
            }
            return return_List;
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("ProductDetailsByID")]
        [System.Web.Http.HttpPost]
        public AddProducts ProductDetailsByID(string id)
        {
            ProductManager mngr = new ProductManager();
            AddProducts return_Obj = new AddProducts();
            Products tbl_obj = mngr.Product_details(Convert.ToInt32(id));


            if (tbl_obj != null)
            {
                return_Obj.id = tbl_obj.id;
                return_Obj.name = tbl_obj.name;
                return_Obj.categoryId = (int)tbl_obj.categoryId;
                return_Obj.description = tbl_obj.description;
                return_Obj.stock = (int)tbl_obj.stock;
                return_Obj.image = BitConverter.ToString(tbl_obj.image);
                return_Obj.status = tbl_obj.status;
                return_Obj.createdBy = tbl_obj.createdBy;
                return_Obj.createdByDate = tbl_obj.createdDate;
                return_Obj.lastModifiedBy = tbl_obj.lastModifiedBy;
                return_Obj.lastModifiedDate = tbl_obj.lastModifiedDate;
                return_Obj.price = (int)tbl_obj.price;

            }
            return return_Obj;
        }
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.Route("deleteProduct")]
        [System.Web.Http.HttpPost]
        public string deleteProduct(string id)
        {
            ProductManager mngr = new ProductManager();
            AddProducts return_obj = new AddProducts();
            int rel = mngr.delete_Product(Convert.ToInt32(id));
            if (rel > 0)
            {
                return "Deleted Successfully";
            }
            else
            {
                return "Error";
            }

        }
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("searchbyName")]
        [System.Web.Http.HttpPost]
        public List<AddProducts> SearchByName(string name)
        {
            ProductManager mngr = new ProductManager();
            List<AddProducts> return_List = new List<AddProducts>();
            List<Products> tbl_obj = mngr.SearchByName(name);
            if (tbl_obj.Count != 0)
            {

                foreach (var obj in tbl_obj)
                {
                    return_List.Add(new AddProducts
                    {
                        id = obj.id,
                        name = obj.name,
                        categoryId = (int)obj.categoryId,
                        description = obj.description,
                        stock = (int)obj.stock,
                        image = BitConverter.ToString(obj.image),
                        status = obj.status,
                        createdBy = obj.createdBy,
                        createdByDate = obj.createdDate,
                        lastModifiedBy = obj.lastModifiedBy,
                        lastModifiedDate = obj.lastModifiedDate,
                        price = (int)obj.price,
                    });
                }
            }
            return return_List;
        }
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getProductByCategoryName")]
        [System.Web.Http.HttpPost]
        public List<AddProducts> getProductByCategoryName(string name)
        {
            ProductManager mngr = new ProductManager();
            List<AddProducts> return_List = new List<AddProducts>();
            List<Products> tbl_obj = mngr.getProductByCatName(name);
            if (tbl_obj.Count != 0)
            {

                foreach (var obj in tbl_obj)
                {
                    return_List.Add(new AddProducts
                    {
                        id = obj.id,
                        name = obj.name,
                        categoryId = (int)obj.categoryId,
                        description = obj.description,
                        stock = (int)obj.stock,
                        image = BitConverter.ToString(obj.image),
                        status = obj.status,
                        createdBy = obj.createdBy,
                        createdByDate = obj.createdDate,
                        lastModifiedBy = obj.lastModifiedBy,
                        lastModifiedDate = obj.lastModifiedDate,
                        price = (int)obj.price,
                    });
                }
            }
            return return_List;
        }


            [HttpPost]
        [Route("UploadFile")]

        public string UploadFile()
        {
            var file = HttpContext.Current.Request.Files.Count > 0 ?
                HttpContext.Current.Request.Files[0] : null;
            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(
                    HttpContext.Current.Server.MapPath("~/images"),
                    fileName);
                file.SaveAs(path);
            }
            return file != null ? "/images/" + file.FileName : null;
        }
    }
}