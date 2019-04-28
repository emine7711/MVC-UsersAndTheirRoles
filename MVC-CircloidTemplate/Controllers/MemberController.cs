
using MVC_CircloidTemplate.App_Classes;
using MVC_CircloidTemplate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVC_CircloidTemplate.Controllers
{
    
    public class MemberController : Controller
    {
        NorthwindEntities ctx = new NorthwindEntities();
        // GET: Member
        public ActionResult MemberLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult MemberLogin(UserClass uc,string RememberMe)
        {
            bool validationResult = Membership.ValidateUser(uc.UserName, uc.Password);
            if (validationResult==true)
            {
                //Girilen kullanıcı ve şifre bilgileri doğru ise kullanıcının web sitemize giriş yapabilmesi gerekir
                //bunun için öncelikle web.config de authorization ayarlarının yapılması gerekir. ayarlar yapıldıktan sora FormsAuthentication.RedirectFromLoginPage() metodu çağrılacaktır
                if (RememberMe == "on")
                {
                    FormsAuthentication.RedirectFromLoginPage(uc.UserName, true);
                }
                else
                {
                    FormsAuthentication.RedirectFromLoginPage(uc.UserName, false);
                }

            }
            else
            {
                ViewBag.Message = "Kullanıcı adı/parola hatalı.";
            }
            return View();
        }
        public ActionResult MemberLogout(UserClass uc)
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("MemberLogin", "Member");
        }
        [HttpGet]
        public ActionResult ForgotMyPassword(string id)
        {
            MembershipUser mu = Membership.GetUser(id);
            return View(mu);
        }
        [HttpPost]
        public ActionResult ForgotMyPassword(UserClass uc,string passwordConfirm)
        {
            ViewBag.userName = uc.UserName;
            if (uc.UserName==null)
            {
                ViewBag.errorMessage = "Kullanıcı adı giriniz";
                return View(uc);
            }
            MembershipUser mu = Membership.GetUser(uc.UserName);
           if (mu == null)
            {
                ViewBag.errorMessage = "Kullanıcı adı yanlış";
                return View(uc);
            }
            if (uc.Password != passwordConfirm)
            {
                ViewBag.errorMessage = "Şifre uyumsuz";
                return View(uc);
            }
            if (mu.PasswordQuestion == uc.PasswordQuestion)
            {
                string oldNewPwd = mu.ResetPassword(uc.PasswordAnswer);
                if (oldNewPwd != null)
                {
                    mu.ChangePassword(oldNewPwd, uc.Password);
                    ViewBag.errorMessage = "Şifre başarıyla değiştirildi";
                }
            }
            else
            {
                ViewBag.errorMessage = "Girilen bilgiler yanlıştır";
            }
            return View(uc);
            
        }
        public ActionResult CreateNewAccount()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateNewAccount(UserClass uc,string ConfirmPassword)
        {
            string message2 = "";
            if (uc.Password != ConfirmPassword)
            {
                message2 = "Şifre eşleşmiyor";
                ViewBag.Message2 = message2;
                return View();
            }
            else
            {
                Membership.CreateUser(uc.UserName, uc.Password, uc.Email, uc.PasswordQuestion, uc.PasswordAnswer, true, out MembershipCreateStatus status);
                string createMessage = "";
                string message = "";


                switch (status)
                {
                    case MembershipCreateStatus.Success:
                        break;
                    case MembershipCreateStatus.InvalidUserName:
                        createMessage = "Geçersiz kullanıcı adı.";
                        break;
                    case MembershipCreateStatus.InvalidPassword:
                        createMessage = "Geçersiz parola";
                        break;
                    case MembershipCreateStatus.InvalidQuestion:
                        createMessage = "Geçersiz gizli soru.";
                        break;
                    case MembershipCreateStatus.InvalidAnswer:
                        createMessage = "Geçersiz gizli cevap.";
                        break;
                    case MembershipCreateStatus.InvalidEmail:
                        createMessage = "Geçersiz email.";
                        break;
                    case MembershipCreateStatus.DuplicateUserName:
                        createMessage = "Kullanılmış kullanıcı adı.";
                        break;
                    case MembershipCreateStatus.DuplicateEmail:
                        createMessage = "Kullanılmış mail adresi.";
                        break;
                    case MembershipCreateStatus.UserRejected:
                        createMessage = "Kullanıcı engellenedi";
                        break;
                    case MembershipCreateStatus.InvalidProviderUserKey:
                        createMessage = "Geçersiz kullanıcı anahtarı.";
                        break;
                    case MembershipCreateStatus.DuplicateProviderUserKey:
                        createMessage = "Tekrarlanmış kullanıcı anahtarı.";
                        break;
                    case MembershipCreateStatus.ProviderError:
                        createMessage = "Sağlayıcı hatası.";
                        break;
                    default:
                        break;
                }
                ViewBag.createMessage = createMessage;
                if (createMessage == "")
                {
                    message = "Başarılı";
                    ViewBag.Message = message;
                    return View();
                }
                else
                {
                    return View();
                }
            }
        }
    }
}