using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MySens.Infrastructure;
using MySens.Models;
using Microsoft.AspNet.Identity.Owin;

namespace MySens.Controllers
{
    public class CourseController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: Course
        [Authorize(Roles = "Administrators")]
        public ActionResult Index()
        {
            var courses = db.Courses.Include(c => c.Teacher);
            return View(courses.ToList());
        }

        // GET: Course/Details/5
        [Authorize(Roles = "Administrators")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // GET: Course/Create
        [Authorize(Roles = "Administrators")]
        public ActionResult Create()
        {
            //  ViewBag.AppUserID = new SelectList(db.AppUsers, "Id", "FirstName");
            ViewBag.AppUserID = new SelectList(UserManager.Users.ToList(), "Id", "UserName");

            return View();
        }

        // POST: Course/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CourseID,Title,Description,StartDate,EndDate,NumberOfLessons,AppUserID")] Course course, string[] selectedTags)
        {
            if (selectedTags != null)
            {
                course.CourseTags = new List<Tag>();
                foreach (var tag in selectedTags)
                {
                    var tagToAdd = db.Tags.Find(int.Parse(tag));
                    course.CourseTags.Add(tagToAdd);
                }
            }
            if (ModelState.IsValid)
            {
                db.Courses.Add(course);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AppUserID = new SelectList(UserManager.Users.ToList(), "Id", "UserName", course.AppUserID);
            //ViewBag.AppUserID = new SelectList(db.AppUsers, "Id", "Username", course.AppUserID);
            return View(course);
        }

        //GET
        [Authorize(Roles = "Administrators")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses
            .Include(c => c.CourseTags)
            .Where(c => c.CourseID == id)
            .Single();
            PopulateTagsData(course);
            if (course == null)
            {
                return HttpNotFound();
            }

            ViewBag.AppUserID = new SelectList(
            UserManager.Users
            .Where(u => u.Roles.Select(r => r.RoleId)
            .Contains("50844948-9520-4322-9703-4764bb317d99"))
            .ToList(), "Id", "UserName");
            return View(course);
        }
        // POST: Course/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? CourseID, string[] selectedTags)
        {
            var courseToUpdate = db.Courses.Include(c => c.Teacher).Include(c => c.CourseTags).Where(c => c.CourseID == CourseID).Single();
            if (TryUpdateModel(courseToUpdate, "", new string[] { "Title", "Description", "StartDate", "EndDate", "NumberOfLessons",
"AppUserID" }))
            {
                try
                {
                    UpdateCourseTags(selectedTags, courseToUpdate);
                    db.SaveChanges();
                }
                catch
                {
                    ModelState.AddModelError("", "Unable to save changes.");
                }
            }
            // Instructors dropdown list
            ViewBag.AppUserID = new SelectList(
            UserManager.Users
            .Where(u => u.Roles.Select(r => r.RoleId)
            .Contains("9ce36aac-c95b-41a7-8013-0aab4a395c95"))
            .ToList(), "Id",
            "UserName");
            // Tags check boxes
            PopulateTagsData(courseToUpdate);
            return View(courseToUpdate);
        }

        // GET: Course/Delete/5
        [Authorize(Roles = "Administrators")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }


        [Authorize(Roles = "Administrators")]
        // POST: Course/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = db.Courses
            .Include(c => c.CourseTags)
            .Where(c => c.CourseID == id)
            .Single();
            db.Courses.Remove(course);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public void UpdateCourseTags(string[] selectedTags, Course courseToUpdate)
        {
            if (selectedTags == null)
            {
                courseToUpdate.CourseTags = new List<Tag>();
                return;
            }
            var selectedTagsHS = new HashSet<string>(selectedTags);
            var courseTags = new HashSet<int>(courseToUpdate.CourseTags.Select(t => t.TagID));
            foreach (var tag in db.Tags)
            {
                if (selectedTagsHS.Contains(tag.TagID.ToString()))
                {
                    if (!courseTags.Contains(tag.TagID))
                    {
                        courseToUpdate.CourseTags.Add(tag);
                    }
                }
                else
                {
                    if (courseTags.Contains(tag.TagID))
                    {
                        courseToUpdate.CourseTags.Remove(tag);
                    }
                }
            }
        }

        private void PopulateTagsData(Course course)
        {
            var allTags = db.Tags;
            var coursesTags = new HashSet<int>(course.CourseTags.Select(t => t.TagID));
            var viewModel = new List<AssignedTagData>();
            foreach (var tag in allTags)
            {
                viewModel.Add(new AssignedTagData
                {
                    TagID = tag.TagID,
                    TagName = tag.Name,
                    Assigned = coursesTags.Contains(tag.TagID)
                });
            }
            ViewBag.Tags = viewModel;
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
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
