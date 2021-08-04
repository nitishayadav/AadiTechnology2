using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoginTask.Models;
using System.Web.Security;


namespace LoginTask.Controllers
{
    public class AccountController : Controller
    {
        Repository repository = null;
        public AccountController()
        {
            repository = new Repository();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(UserModel models)
        {
            if (ModelState.IsValid)
            {
                var result = repository.CheckValid(models.UserName, models.Password);

                if (result == null)
                {
                    ModelState.Clear();
                    ViewBag.error = "Invalid Username and Password";
                }
                else
                {
                    TempData["mykey"] = result;
                    return RedirectToAction("GetProfile", "Home", new { id = result.Id });
                }


            }

            return View();

        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
         
        }
    }
}