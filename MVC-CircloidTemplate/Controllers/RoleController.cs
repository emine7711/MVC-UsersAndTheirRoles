﻿using MVC_CircloidTemplate.App_Classes;
using MVC_CircloidTemplate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVC_CircloidTemplate.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        NorthwindEntities ctx = new NorthwindEntities();
        public RoleController()
        {
            ViewBag.RoleSelected = "selected";
        }
        // GET: Role
        public ActionResult Index()
        {
            List<string> rolesList= Roles.GetAllRoles().ToList();
            return View(rolesList);
        }
        //public ActionResult AddRole()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult AddRole(RoleClass rc)
        //{
        //    string message = "Kullanılmış rol adı";
        //    if (Roles.RoleExists(rc.RoleName))
        //    {
        //        ViewBag.Message = message;
        //    }
        //    else
        //    {
        //        Roles.CreateRole(rc.RoleName);
        //        ViewBag.Message = "Başarılı";
        //    }
        //    return View();
        //}
        public ActionResult AddRole(string message = null)
        {
            ViewBag.Message = message;
            return View();
        }
        [HttpPost]
        [ActionName("AddRole")]
        public ActionResult AddRolePost(string role)
        {
            if (string.IsNullOrWhiteSpace(role))
            {
                return RedirectToAction("AddRole", new { message = "Rol boş olamaz" });
            }
            if (Roles.RoleExists(role))
            {
                return RedirectToAction("AddRole", new { message = "Rol zaten kayıtlı" });
            }
            Roles.CreateRole(role);
            return RedirectToAction("AddRole", new { message = "Başarılı" });
        }
        [HttpPost]
        public ActionResult DeleteRole(string id)
        {
            Roles.DeleteRole(id);
           
            return RedirectToAction("Index");
        }

    }
}