using MVC_CircloidTemplate.App_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVC_CircloidTemplate.Controllers
{
    public class UserController : Controller
    {
        public UserController()
        {
            ViewBag.UserSelected = "selected";
        }
        // GET: User
        public ActionResult Index()
        {
            //Veritabanındaki bütün kullanıcıları çekip users isimli collectionda toplayacak
            MembershipUserCollection users = Membership.GetAllUsers();
            return View(users);
        }
        public ActionResult AddUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddUser(UserClass uc)
        {
            Membership.CreateUser(uc.UserName, uc.Password, uc.Email, uc.PasswordQuestion, uc.PasswordAnswer, true, out MembershipCreateStatus status);
            string createMessage = "";
            
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
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
               
          
        }
    }
}