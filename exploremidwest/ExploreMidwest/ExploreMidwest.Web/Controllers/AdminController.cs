using ExploreMidwest.Data;
using ExploreMidwest.Data.BlogRepositories;
using ExploreMidwest.Data.PageRepositories;
using ExploreMidwest.Model;
using ExploreMidwest.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace ExploreMidwest.Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult PendingPosts()
        {
            var repo = BlogRepoFactory.Create();

            var model = repo.GetUnpublishedBlogs();

            return View(model);
        }

        public ActionResult PublishBlog(int id)
        {
            var repo = BlogRepoFactory.Create();

            Blog blog = repo.GetBlogById(id);

            blog.IsFinished = true;

            repo.EditBlog(blog);

            return RedirectToAction("PendingPosts");
        }

        [HttpGet]
        public ActionResult SavedPages()
        {
            var repo = PageRepoFactory.Create();

            var model = repo.GetAllPages();

            return View(model);
        }

        public ActionResult DeleteBlog(int id)
        {
            var repo = BlogRepoFactory.Create();

            repo.DeleteBlog(id);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [ValidateInput(false)]
        public ActionResult AddPage()
        {
            return View(new Page());
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddPage(Page p)
        {
            var repo = PageRepoFactory.Create();
            if (ModelState.IsValid)
            {
                if (p.IsInNavigation)
                {
                    p.IsFinished = true;
                }
                repo.AddPage(p);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(p);
            }
        }

        [HttpGet]
        [ValidateInput(false)]
        public ActionResult EditPage(int id)
        {
            var repo = PageRepoFactory.Create();
            var page = repo.GetPage(id);

            return View(page);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditPage(Page p)
        {
            var repo = PageRepoFactory.Create();
            if (ModelState.IsValid)
            {
                if (p.IsInNavigation)
                {
                    p.IsFinished = true;
                }
                repo.EditPage(p);
                return RedirectToAction("SavedPages");
            }
            else
            {
                return View(p);
            }
        }

        [HttpGet]
        public ActionResult DeletePage(int id)
        {
            var repo = PageRepoFactory.Create();

            repo.RemovePage(id);

            return RedirectToAction("SavedPages");
        }

        public ActionResult ResetPassword()
        {
            var context = new ExploreMidwestDBContext();

            var userMgr = new UserManager<IdentityUser>(new UserStore<IdentityUser>(context));

            return RedirectToAction("Login", "Home");
        }

        [HttpGet]
        public ActionResult AddManager()
        {
            return View(new Manager());
        }


        [HttpPost]
        public ActionResult AddManager(Manager m)
        {
            if (ModelState.IsValid)
            {
                ExploreMidwest.Data.ExploreMidwestDBContext context = new Data.ExploreMidwestDBContext();

                var userMgr = new UserManager<IdentityUser>(new UserStore<IdentityUser>(context));
                var roleMgr = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

                if (!userMgr.Users.Any(u => u.UserName == m.Name))
                {
                    var user = new IdentityUser()
                    {
                        UserName = m.Name
                    };
                    userMgr.Create(user, m.Password);
                }
                var findmanager = userMgr.FindByName(m.Name);
                // create the user with the manager class
                if (!userMgr.IsInRole(findmanager.Id, "Manager"))
                {
                    userMgr.AddToRole(findmanager.Id, "Manager");
                }
                return RedirectToAction("Index", "Home");

            }
            return View(m);
        }


        [HttpGet]
        public ActionResult DeleteManager()
        {
            return View(new DeleteManager());
        }

        [HttpPost]
        public ActionResult DeleteManager(DeleteManager m)
        {
            if (!string.IsNullOrEmpty(m.Name))
            {
                ExploreMidwest.Data.ExploreMidwestDBContext context = new Data.ExploreMidwestDBContext();

                var userMgr = new UserManager<IdentityUser>(new UserStore<IdentityUser>(context));
                var roleMgr = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

                var findmanager = userMgr.FindByName(m.Name);
                // create the user with the manager class
                if (findmanager != null)
                {
                    userMgr.Delete(findmanager);
                }
                else
                {
                    return View(m);
                }
                return RedirectToAction("Index", "Home");


            }
            else
            {
                ModelState.AddModelError("Name", "Please Enter A Name");
            }
            return View(m);
        }
    }
}




