using MVC_CircloidTemplate.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_CircloidTemplate.Controllers
{
   // [Authorize(Roles = "Üye")]
    public class CategoryController : Controller
    {
        NorthwindEntities ctx = new NorthwindEntities();
        // GET: Category

        public CategoryController()
        {
            ViewBag.CategorySelected = "selected";
        }
        public byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(image.InputStream);
            imageBytes = reader.ReadBytes(image.ContentLength);
            byte[] bytes = new byte[imageBytes.Length + 78];
            Array.Copy(imageBytes, 0, bytes, 78, imageBytes.Length);
            return bytes;
        }
        public ActionResult Index()
        {
            List<Category> catList = ctx.Categories.ToList();
            return View(catList);
        }
        public ActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCategory([Bind(Include="CategoryName,Description")] Category cat, HttpPostedFileBase Picture)
        {
            if (Picture==null)
            {
                return View();
            }
            cat.Picture = ConvertToBytes(Picture);
            ctx.Categories.Add(cat);
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }
        
        //public ActionResult DeleteCategory(int id)
        //{
        //    Category ctg = ctx.Categories.FirstOrDefault(x => x.CategoryID == id);
        //    return View(ctg);
        //}
        [HttpPost]
        public ActionResult DeleteCategory(int? id)
        {
            Category category = ctx.Categories.FirstOrDefault(x => x.CategoryID == id);
            ctx.Categories.Remove(category);
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UpdateCategory(int id)
        {
            Category ctg = ctx.Categories.FirstOrDefault(x => x.CategoryID == id);
            return View(ctg);
        }
        [HttpPost]
        public ActionResult UpdateCategory([Bind(Include = "CategoryID, CategoryName, Description")] Category ktg, HttpPostedFileBase Picture)
        {
            if (ModelState.IsValid)
            {
                Category k = ctx.Categories.Find(ktg.CategoryID);
                k.CategoryName = ktg.CategoryName;
                k.Description = ktg.Description;
                if (Picture != null)
                {
                    k.Picture = ConvertToBytes(Picture);
                }
                ctx.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}