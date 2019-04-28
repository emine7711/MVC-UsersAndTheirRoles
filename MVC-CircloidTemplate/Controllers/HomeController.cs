using MVC_CircloidTemplate.App_Classes;
using MVC_CircloidTemplate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace MVC_CircloidTemplate.Controllers
{
    
    public class HomeController : Controller
    {
        public HomeController()
        {
            ViewBag.HomeSelected = "selected";
        }
        NorthwindEntities ctx = new NorthwindEntities();
        // GET: Home
        //[ChildActionOnly]
        public ActionResult Index()
        {
            ViewBag.ActiveUserCount = HttpContext.Application["ActiveUserCount"];
            ViewBag.TotalUserCount = HttpContext.Application["TotalUserCount"];
            return View();
            
        }
        public ActionResult AssignCookie()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AssignCookie(string CookieName,string CookieValue)
        {
            HttpCookie hc = new HttpCookie(CookieName);
            hc.Value = CookieValue;
            hc.Expires = DateTime.Now.AddDays(2);
            Response.Cookies.Add(hc);
            return View();
        }
        public ActionResult RetrieveCookie()
        {
            string cookieVal=Request.Cookies["SC201-Morning"].Value
                ;
            ViewBag.CookieValue = cookieVal;
            return View();
        }
        public ActionResult MyCart()
        {
            //List<Product> myCartList = new List<Product>();
            //if (Session["CurrentCart"] != null)
            //{
            //   Cart crt = (Cart)Session["CurrentCart"];
            //}

            Cart crt;
            if (Session["CurrentCart"] == null)
            {
                crt = new Cart();
            }
            else
            {
                crt = (Cart)Session["CurrentCart"];
            }
            Session["CurrentCart"] = crt;
            return View();
        }
        [HttpPost]
        public string RemoveCart(int id)
        {
            //Sepet varsa,gelen ürünün varolan sepete ekle,sepet yoksa önce aktif session için(oturum) için sepet oluştur ve sepete gelen ürünü ekle

            string cartMessage = "";

            Cart crt = (Cart)Session["CurrentCart"];
            Product prd = ctx.Products.FirstOrDefault(x => x.ProductID == id); //Producttan o id'li ürünü bul
                                                                              //crt.PrdList.Remove(prd);

            crt.PrdList.RemoveAll(x => x.ProductID == id);
            Session["CurrentCart"] = crt;
            cartMessage = "Ürün sepetten çıkarılmıştır. ";
            return cartMessage;
        }
        public ActionResult PartialCartListView()
        {
            if (Session["CurrentCart"] != null)
            {
                Cart c = (Cart)Session["CurrentCart"];
                return PartialView(c.PrdList);
            }
            return PartialView();
        }
    }
}