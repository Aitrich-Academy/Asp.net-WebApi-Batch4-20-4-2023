using DAL.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Manager
{
   public class ProductManager
    {
        Model1 db_product = new Model1();
        public string AddProduct(Products obj)
        {
            int result = 0;
            var objUser = db_product.Products.Where(e => e.name == obj.name && e.status != "D").SingleOrDefault();
            if (objUser == null)
            {
                obj.status = "A";
                db_product.Products.Add(obj);
                result = db_product.SaveChanges();
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

        public string updateProduct(Products obj)
        {
            int result = 0;
            var objUser = db_product.Products.Where(e => e.id == obj.id && e.status != "D").SingleOrDefault();
            if (objUser != null)
            {
                objUser.name = obj.name == null ? objUser.name : obj.name;
                objUser.description = obj.description == null ? objUser.description : obj.description;
                objUser.stock = obj.stock == null ? objUser.stock : obj.stock;
                objUser.image = obj.image == null ? objUser.image : obj.image;
                objUser.status = "A";
                objUser.lastModifiedBy = obj.lastModifiedBy == null ? objUser.lastModifiedBy : obj.lastModifiedBy;
                objUser.lastModifiedDate = DateTime.Now.ToString();
                objUser.price = obj.price == null ? objUser.price : obj.price;
                db_product.Entry(objUser).State = EntityState.Modified;
                result = db_product.SaveChanges();
                return obj.id.ToString();
            }
            else
            {
                return "Error";
            }
        }

        public List<Products> allProducts()
        {
            return db_product.Products.Where(e => e.status != "D").ToList();
        }

        public Products Product_details(int id)
        {
            Products return_ent = new Products();
            return return_ent = db_product.Products.Where(e => e.id == id && e.status != "D").SingleOrDefault();
        }
        public List<Products> getProductByCatName(string cname)
        {
            Category return_cat = new Category();
            return_cat = db_product.Category.Where(e => e.name == cname && e.status != "D").SingleOrDefault();
           // List<Products> return_product = new List<Products>();
           return db_product.Products.Where(e => e.categoryId == return_cat.id && e.status != "D").ToList();
        }

        public int delete_Product(int id)
        {
            int result = 0;
            Products return_ent = new Products();
            var del = db_product.Products.Where(e => e.id == id && e.status != "D").SingleOrDefault();
            if (del != null)
            {
                del.status = "D";
                db_product.Entry(del).State = EntityState.Modified;
                result = db_product.SaveChanges();

            }
            return result;
        }

        public List<Products> SearchByName(string name)
        {
            return db_product.Products.Where(e => e.name == name && e.status != "D").ToList();
        }
    }
}
