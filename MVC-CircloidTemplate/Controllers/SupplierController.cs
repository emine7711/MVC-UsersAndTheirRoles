using MVC_CircloidTemplate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_CircloidTemplate.Controllers
{
    [Authorize(Roles = "Üye")]
    public class SupplierController : Controller
    {
        public SupplierController()
        {
            ViewBag.SupplierSelected = "selected";
        }
        NorthwindEntities ctx = new NorthwindEntities();
        // GET: Supplier
        public ActionResult Index()
        {
            List<Supplier> supList = ctx.Suppliers.ToList();
            return View(supList);
        }
       
        public ActionResult AddSupplier()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddSupplier(Supplier s)
        {
            ctx.Suppliers.Add(s);
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }
        //public ActionResult DeleteSupplier(int supID)
        //{
        //    Supplier sup = ctx.Suppliers.FirstOrDefault(x => x.SupplierID == supID);
        //    return View(sup);
        //}
        //[HttpPost]
        //public ActionResult DeleteSupplier(int id)
        //{
        //    Supplier spl = ctx.Suppliers.FirstOrDefault(x => x.SupplierID == id);
        //    ctx.Suppliers.Remove(spl);
        //    ctx.SaveChanges();
        //    return RedirectToAction("Index");
        //}
        //Bu methodun içinde oluşan hata Ajaxı etkilemez. Ajax için success, Ajax'ın doğru bir şekilde action a ulaşmış olmasıyla ilgilenir.Bu metotta veritabanındaki ilişkilerden dolayı kayıt silinememesi ve benzeri hatalar Ajax ı ilgilendirmez. Bu yüzden bu metot içinde oluşan hatalarla ilgili Ajax a bilgi göndermeliyiz.
        [HttpPost]
        public string DeleteSupplier(int id)
        {
            try
            {
                Supplier spl = ctx.Suppliers.FirstOrDefault(x => x.SupplierID == id);
                ctx.Suppliers.Remove(spl);
                ctx.SaveChanges();
                return "OK";
            }
            catch (Exception)
            {
                return "ERROR";
            }
           
        }
        public ActionResult UpdateSupplier(int id)
        {
            Supplier sup = ctx.Suppliers.FirstOrDefault(x => x.SupplierID == id);
            return View(sup);
        }
        [HttpPost]
        public ActionResult UpdateSupplier(Supplier s)
        {
            if (ModelState.IsValid)
            {
                //Supplier spl = ctx.Suppliers.FirstOrDefault(x => x.SupplierID == s.SupplierID);
                //spl.CompanyName = s.CompanyName;
                //spl.Address = s.Address;
                ctx.Entry(s).State = System.Data.Entity.EntityState.Modified;
                ctx.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("UpdateSupplier", new { id = s.SupplierID });
        }
    }
}