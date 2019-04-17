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
        // GET: Home
        //[ChildActionOnly]
        public ActionResult Index()
        {
            //    var items = new List<MenuItemModel>
            //{
            //    new MenuItemModel{ Text = "AnaSayfa", Action = "Index", Controller = "Home", Selected=false },
            //    new MenuItemModel{ Text = "Ürünler", Action = "Index", Controller = "Product", Selected = false},
            //    //new MenuItem{ Text = "About", Action = "About", Controller = "Home", Selected = false }
            //};

            //    string action = ControllerContext.ParentActionViewContext.RouteData.Values["action"].ToString();
            //    string controller = ControllerContext.ParentActionViewContext.RouteData.Values["controller"].ToString();

            //    foreach (var item in items)
            //    {
            //        if (item.Controller == controller && item.Action == action)
            //        {
            //            item.Selected = true;
            //        }
            //    }

            // return PartialView(items);
            return View();
            
        }
    }
}