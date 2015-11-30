using System.Threading.Tasks;
using System.Web.Mvc;
using MySens.Models;
using Microsoft.Owin.Security;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using MySens.Infrastructure;
using System.Web;
using System;

namespace MySens.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return View("Error", new string[] { "Access Denied" });
            }

            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel details, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await UserManager.FindAsync(details.UserName, details.Password);
                if (user == null)
                {
                    ModelState.AddModelError("", "Invalid name or password.");
                }
                else
                {
                    ClaimsIdentity ident = await UserManager.CreateIdentityAsync(user,
                    DefaultAuthenticationTypes.ApplicationCookie);
                    AuthManager.SignOut();
                    AuthManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = false
                    }, ident);

                    System.Diagnostics.Debug.WriteLine("Logging in ...................");

                    System.Diagnostics.Debug.WriteLine(user.Id);

                    if (UserManager.IsInRole(user.Id, "Instructors"))
                    {
                        System.Diagnostics.Debug.WriteLine("Instructors");
                        return RedirectToAction("Index", "Home");
                    }
                    //role Admin go to Admin page
                    else if (UserManager.IsInRole(user.Id, "Administrators"))
                    {
                        System.Diagnostics.Debug.WriteLine("Admin");
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        //no role
                        System.Diagnostics.Debug.WriteLine("No role");
                        return RedirectToAction("Index", "Home");
                    }


                 //   return RedirectToLocal(returnUrl);
                }
            }
            ViewBag.returnUrl = returnUrl;
                        return View(details);
        }

        public ActionResult Create()
        {
            return View();
        }

        [Authorize]
        public ActionResult Logout()
        {
            AuthManager.SignOut();
            return RedirectToAction("Login", "Account");
        }
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        private IAuthenticationManager AuthManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private AppUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }
    }
}