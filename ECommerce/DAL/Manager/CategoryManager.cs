using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace DAL.Manager
{
   public class CategoryManager
    {
        CategoryModel db_category = new CategoryModel();
        public string AddCategory(Category obj)
        {
            int result = 0;
            var objUser = db_category.Category.Where(e => e.name == obj.name && e.status != "D").SingleOrDefault();
            if (objUser == null)
            {
                obj.status = "A";
                db_category.Category.Add(obj);
                result = db_category.SaveChanges();
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
        public string updateCategory(Category obj)
        {
            int result = 0;
            var objUser = db_category.Category.Where(e => e.name == obj.name && e.status != "D").SingleOrDefault();
            if (objUser != null)
            {
                objUser.name = obj.name;
                objUser.description = obj.description;
                objUser.image = obj.image;
                objUser.status = "A";
                objUser.lastModifiedBy = obj.lastModifiedBy;
                objUser.lastModifiedDate = DateTime.Now.ToString();
                db_category.Entry(objUser).State = EntityState.Modified;
                result = db_category.SaveChanges();
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
        public List<Category> allCategories()
        {
            return db_category.Category.Where(e => e.status != "D").ToList();
        }
        public Category category_details(int Id)
        {
            Category return_ent = new Category();
            return return_ent = db_category.Category.Where(e => e.id == Id && e.status != "D").SingleOrDefault();

        }
        public int delete_category(int Id)
        {
            int result = 0;
            Category return_ent = new Category();
            var del = db_category.Category.Where(e => e.id == Id && e.status != "D").SingleOrDefault();
            if (del != null)
            {
                del.status = "D";
                db_category.Entry(del).State = EntityState.Modified;
                result = db_category.SaveChanges();

            }
            return result;
        }
    }
}
