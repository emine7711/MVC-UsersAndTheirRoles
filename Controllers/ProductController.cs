using MVC_CircloidTemplate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_CircloidTemplate.Controllers
{
    public class ProductController : Controller
    {
        public ProductController()
        {
            ViewBag.ProductSelected = "selected";
        }
        NorthwindEntities ctx = new NorthwindEntities();
        // GET: Product
        public ActionResult Index()
        {
            List<Product> prdList = ctx.Products.ToList();
            return View(prdList);
        }
        public ActionResult AddProduct()
        {
            ViewBag.catList = ctx.Categories.ToList();
            ViewBag.supList = ctx.Suppliers.ToList();

            return View();
        }
        [HttpPost]
        public ActionResult AddProduct(Product p)
        {
            ctx.Products.Add(p);
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }
        //Sil işlemini 3 farklı yolla yapacağız.İlk çözümümüz sil butonuna basılınca yeni bir view açılacak yani
        //kullanıcı yeni bir sayfaya yönlendirilecek ve evet derse silinecek.
        //2. yol; sil butonuna basılınca yukarıda alert kutusu çıkacak ve kayıt silinsin mi diye soracak, evet derse
        //silinecek. Bu yöntemin dez avantajı alert kutusu bir kaç kez görüntülendikten sonra browser otomatik olaraka alert
        //kutusunun altına checkbox ekliyor ve bu mesajı tekrar gösterme seçeneği sunuyor. eğer kullanıcı checkbox ı işaretlerse
        //tekrar alert kutusu gözükmeyeceği için silme işlemi gerçekleştirilemiyor.(AJAX kodu yazılacak)
        //3.yol; Sil butonuna basılınca küçük bir pencere açılacak (başka bir sayfaya yönlendirilmeyecek) evet seçilirse
        //silme işlemi gerçekleştirilecek.Bu işlem için de ajax kodu yazılacak.
        public ActionResult DeleteProduct(int prdID)
        {
            Product prd = ctx.Products.FirstOrDefault(x=>x.ProductID==prdID);
            return View(prd);
        }
        [HttpPost]
        public ActionResult DeleteProduct(Product p)
        {
            Product prod= ctx.Products.FirstOrDefault(x => x.ProductID == p.ProductID);
            ctx.Products.Remove(prod);
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}