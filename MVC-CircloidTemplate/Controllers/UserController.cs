using MVC_CircloidTemplate.App_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVC_CircloidTemplate.Controllers
{
    //[Authorize(Roles = "Admin")]
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
        [HttpPost]
        public ActionResult DeleteUser(string id)
        {
            Membership.DeleteUser(id);
            return RedirectToAction("Index");
        }
        public ActionResult AssignRole(string username,string message = null)
        {
            /*Parametre olarak id yazmak zorundayız,sebebi projenin App_Start klasörünün altınsa Route_Config.cs dosyasında "{controller}/{action}/{id}" bu parametre adının default adı id olduğu için parametre adınında id olması gerekiyor.
             Kullanıcıya rol ataya tıkladığınfda kullanıcı adını parametre olarak buraya alıyoruz buradan da kullanıcının adını viewe gönderiyoruz amacımız parametre bilgisini view a taşımak.View tarafında ekle butonuna basınca tekrar kullanıcı adını ve rol adını viewden alıp post tarafına taşımak.
             */
            if (string.IsNullOrWhiteSpace(username))
            {
                return RedirectToAction("Index");
            }
            MembershipUser user = Membership.GetUser(username);
            if (user==null)
            {
                return HttpNotFound();
            }
            string[] userRoles = Roles.GetRolesForUser(username);
            string[] allRoles = Roles.GetAllRoles();
            List<string> availableRoles = new List<string>();
            foreach (string role in allRoles)
            {
                if (userRoles.Contains(role)==false)
                {
                    availableRoles.Add(role);
                }
            }
            ViewBag.AvailableRoles = availableRoles;
            ViewBag.UserRoles = userRoles;
            ViewBag.Username = username;
            ViewBag.Message = message;
            return View();

        }
        [HttpPost]
        public ActionResult AssignRole(string username,List<string> addedRoles)
        {
            if (addedRoles==null)
                return RedirectToAction("AssignRole", new { username = username, message = "Önce rol seçiniz" });

            if (addedRoles.Count < 1)
                return RedirectToAction("AssignRole", new { username = username, message = "Hata" });
            
            Roles.AddUserToRoles(username, addedRoles.ToArray());
            return RedirectToAction("AssignRole", new { username = username, message = "Başarılı" });
        }
        [HttpPost]
        public string DeleteRole(string username,string removedRoles)
        {
            string[] removedRolesArray = removedRoles.Split(',');
            if (removedRolesArray.Length<1||string.IsNullOrWhiteSpace(removedRolesArray[0]))
            {
                return "Hata";
            }
            Roles.RemoveUserFromRoles(username, removedRolesArray);
            return "Başarılı";
        }
        public string RolesForUser(string username)
        {
            string roles = "";
            List<string> role = Roles.GetRolesForUser(username).ToList();
            foreach (string r in role)
            {
                roles += r + ",";
            }
            return roles;
        }
        public ActionResult RolesForUser2(string username)
        {
            List<string> role = Roles.GetRolesForUser(username).ToList();
            ViewBag.UserRoles = role;
            return View();
        }
        public ActionResult UpdateUser(string id)
        {
            MembershipUser mu=Membership.GetUser(id);
            return View(mu);
        }
        [HttpPost]
        public ActionResult UpdateUser(MembershipUser mu)
        {
            Membership.UpdateUser(mu);
            return RedirectToAction("Index");
        }
    }
}