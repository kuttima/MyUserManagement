using System;
using System.Web.Mvc;
using System.Web.Security;
using AQuIP.Admin.Models;
using AQuIP.Admin.Helpers;
using AQuIP.Admin.Services;

namespace AQuIP.Admin.Controllers
{
    public class HomeController : Controller
    {

        //private HomeRepository repo = new HomeRepository();
        private AccountService _accountService = new AccountService();

        [AllowAnonymous]
        public ActionResult Index()
        {         
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        public ActionResult Index(LoginViewModel user)
        {
            
            var existingUser = _accountService.GetUserForLogin(user);

            if (existingUser != null)
            {
                if (existingUser.RoleName == Constant.RoleAdmin)
                {
                    FormsAuthentication.SetAuthCookie(existingUser.UserName, false);
                    return RedirectToAction("Index", "User");
                }
            }                           

            ViewBag.Message = Constant.LoginErrorMsg;
            return View(user);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index");
        }      

    }
}