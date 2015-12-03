using System.Web.Mvc;
using System.Collections.Generic;
using System.Web;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MySens.Infrastructure;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using MySens.Models;


namespace MySens.Controllers
{
    public class HomeController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();
        // GET: Home

       // [Authorize]
        public ActionResult Index()
        {
            return View(GetData("Index"));
        }

        [Authorize(Roles = "Users")]
        public ActionResult OtherAction()
        {
            return View("Index", GetData("OtherAction"));
        }

        // GET: Home/Create
        public ActionResult Create()
        {
            //  ViewBag.AppUserID = new SelectList(db.AppUsers, "Id", "FirstName");
            ViewBag.AppUserID = new SelectList(UserManager.Users.ToList(), "Id", "UserName");

            return View();
        }

        // POST: Home/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CourseID,Title,Description,StartDate,EndDate,NumberOfLessons,AppUserID")] Course course)
        {
            if (ModelState.IsValid)
            {
                course.AppUserID = HttpContext.User.Identity.GetUserId();
                db.Courses.Add(course);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
                      
            return View(course);
        }

        private Dictionary<string, object> GetData(string actionName)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("Action", actionName);
            dict.Add("User", HttpContext.User.Identity.Name);
            dict.Add("Authenticated", HttpContext.User.Identity.IsAuthenticated);
            dict.Add("Auth Type", HttpContext.User.Identity.AuthenticationType);
           // dict.Add("In Users Role", HttpContext.User.IsInRole("Users"));
            return dict;
        }

        [Authorize]
        public ActionResult UserProps()
        {
            return View(CurrentUser);
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> UserProps(Cities city)
        {
            AppUser user = CurrentUser;
            user.City = city;
            await UserManager.UpdateAsync(user);
            return View(user);
        }
        private AppUser CurrentUser
        {
            get
            {
                return UserManager.FindByName(HttpContext.User.Identity.Name);
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