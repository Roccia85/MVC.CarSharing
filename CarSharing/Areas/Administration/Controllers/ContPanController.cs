using CarSharing.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarSharing.Areas.Administration.Models;

namespace CarSharing.Areas.Administration.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ContPanController : Controller
    {

        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        public ContPanController()
        {
        }
        public ContPanController(ApplicationUserManager userManager, ApplicationRoleManager roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
        }

        // GET: Administration/ContPan
        public ActionResult Index()
        {
            return View();
        }

        #region Roles Management
        public ActionResult ListRoles(string Message = "")
        {
            ViewBag.Message = Message;
            var model = dbContext.db.Roles.ToList();
            return View(model);
        }

        public ActionResult DetailsRole(String Id)
        {
            Microsoft.AspNet.Identity.EntityFramework.IdentityRole model = RoleManager.FindById(Id);
            if (model == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }

        public ActionResult CreateRole()
        {
            Microsoft.AspNet.Identity.EntityFramework.IdentityRole model = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateRole([Bind(Include = "Name")] Microsoft.AspNet.Identity.EntityFramework.IdentityRole ruolo)
        {
            if (!ModelState.IsValid) { return View(ruolo); }
            else
            {
                var role = RoleManager.FindByName(ruolo.Name);
                if (role == null)
                {
                    role = new IdentityRole(ruolo.Name);
                    var roleresult = RoleManager.Create(role);
                }
                return RedirectToAction("ListRoles", new { Message = "Ruolo " + ruolo.Name + " creato" });
            }
        }

        public ActionResult EditRole(String Id)
        {
            Microsoft.AspNet.Identity.EntityFramework.IdentityRole model = RoleManager.FindById(Id);
            return View(model);
        }

        [HttpPost]
        public ActionResult EditRole([Bind(Include = "Id,Name")] Microsoft.AspNet.Identity.EntityFramework.IdentityRole ruolo)
        {
            if (!ModelState.IsValid) { return View(ruolo); }
            else
            {
                var role = RoleManager.FindById(ruolo.Id);
                if (role != null)
                {
                    role.Name = ruolo.Name;
                    var roleresult = RoleManager.Update(role);
                }
                return RedirectToAction("ListRoles", new { Message = "Ruolo " + ruolo.Name + " modificato" });
            }
        }

        public ActionResult DeleteRole(String Id)
        {
            Microsoft.AspNet.Identity.EntityFramework.IdentityRole model = RoleManager.FindById(Id);
            if (model == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }

        [HttpPost, ActionName("DeleteRole")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRoleConfirmed(String Id)
        {
            var role = RoleManager.FindById(Id);
            try
            {
                foreach (IdentityUserRole idu in role.Users)
                {
                    var usr = UserManager.FindById(idu.UserId);
                    UserManager.Delete(usr);
                }
                RoleManager.Delete(role);

                return RedirectToAction("ListRoles", new { Message = "Eliminati Ruolo ed utenti relativi!" });
            }
            catch (Exception)
            {

                return View(role);
            }
        }

        #endregion

        #region Users Management
        public ActionResult ListUsers(
                int page = 1, 
                int itemsPerPage = 2, 
                string Message = "", 
                string Order = "", 
                string OppositeDirection = "desc"
            )
        {
            ViewBag.Message = Message;
            UsersPaginator model = new UsersPaginator();
            model.OrderDirection = "asc";
            model.OrderField = Order;
            switch (Order)
            {
                case "Utente":
                    model = OppositeDirection == "desc" ? (UsersPaginator)model.Asc() : (UsersPaginator)model.Desc();
                    model.OrderBy = o => o.UserName;
                    break;
                case "Email":
                    model = OppositeDirection == "desc" ? (UsersPaginator)model.Asc() : (UsersPaginator)model.Desc();
                    model.OrderBy = o => o.Email;
                    break;
            }
            model.SetItems(page, itemsPerPage);
            return View(model);
        }

        public ActionResult DetailsUser(String Id)
        {
            ApplicationUser model = UserManager.FindById(Id);
            if (model == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }

        public ActionResult CreateUser()
        {
            CreateUserModel model = new CreateUserModel();

            ViewBag.Ruoli = dbContext.db.Roles.ToList();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUser(CreateUserModel usr, params string[] chkRuoli)
        {
            try
            {
                ApplicationUser user = dbContext.db.Users.Where(w => w.UserName == usr.UserName || w.Email == usr.Email).FirstOrDefault();
                if (user != null) { throw new Exception("Username o mail già registrati!"); }
                user = new ApplicationUser { UserName = usr.UserName, Email = usr.Email, Name = usr.Name, Surname = usr.Surname, Active = usr.Active };
                UserManager.Create(user, usr.Password);

                UserManager.AddToRoles(user.Id, chkRuoli);

                return RedirectToAction("ListUsers", new { Message = "Utente creato correttamente." });
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(usr);
            }
        }

        public ActionResult EditUser(string Id)
        {
            ApplicationUser usr = UserManager.FindById(Id);
            EditUserModel model = new EditUserModel() { Id = Id, Active = usr.Active, Name = usr.Name, Surname = usr.Surname, PhoneNumber=usr.PhoneNumber };

            List<IdentityRole> Ruoli = dbContext.db.Roles.ToList();
            Dictionary<String, bool> RuoliAttivi = new Dictionary<string, bool>();
            foreach(IdentityRole rl in Ruoli)
            {
                RuoliAttivi.Add(rl.Name, UserManager.IsInRole(Id, rl.Name));
            }
            ViewBag.RuoliAttivi = RuoliAttivi;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser(EditUserModel user, params string[] chkRuoli)
        {
            try
            {
                ApplicationUser usr = UserManager.FindById(user.Id);
                usr.Name = user.Name;
                usr.Surname = user.Surname;
                usr.PhoneNumber = user.PhoneNumber;
                usr.Active = user.Active;
                UserManager.Update(usr);
                var lista = usr.Roles.Select(i => i.RoleId).ToList();
                List<String> lst = new List<string>();
                foreach (String s in lista) { lst.Add(RoleManager.FindById(s).Name); }
                UserManager.RemoveFromRoles(usr.Id, lst.ToArray());
                UserManager.AddToRoles(usr.Id, chkRuoli);

                return RedirectToAction("ListUsers", new { Message = "Utente salvato correttamente." });
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(user);
            }

        }

        public ActionResult DeleteUser(String Id)
        {
            ApplicationUser model = UserManager.FindById(Id);
            if (model == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }

        [HttpPost, ActionName("DeleteUser")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteUserConfirmed(String Id)
        {
            var usr = UserManager.FindById(Id);
            try
            {
                UserManager.Delete(usr);

                return RedirectToAction("ListUsers", new { Message = "Eliminato Utente!" });
            }
            catch (Exception)
            {

                return View(usr);
            }
        }

        #endregion
    }
}