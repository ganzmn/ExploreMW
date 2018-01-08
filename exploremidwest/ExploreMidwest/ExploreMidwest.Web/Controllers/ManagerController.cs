using ExploreMidwest.Data;
using ExploreMidwest.Data.BlogRepositories;
using ExploreMidwest.Model;
using ExploreMidwest.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExploreMidwest.Web.Controllers
{
    [Authorize(Roles ="Manager, admin")]
    public class ManagerController : Controller
    {
        IBlogRepo repo = BlogRepoFactory.Create();
        ExploreMidwestDBContext context = new ExploreMidwestDBContext();
        // GET: Manager
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ChangePassword()
        {
            return View(new ChangePasswordVM());
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordVM password)
        {
            if (password.newPassword == password.newPasswordConfirm)
            {
                var userMgr = new UserManager<IdentityUser>(new UserStore<IdentityUser>(context));

                userMgr.ChangePassword(User.Identity.GetUserId(), password.oldPassword, password.newPassword);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("newPassword", "Passwords must be the same");
            }
            return View(password);
        }

        [HttpGet]
        [ValidateInput(false)]
        public ActionResult AddBlog()
        {
            BlogVM model = new BlogVM()
            {
                BlogCategory = new Category(),
                BlogTags = new List<Tag>(),
                Author = User.Identity.Name,
                Date = DateTime.Today
            };

            var context = new ExploreMidwestDBContext();

            model.SetCategories(context.Category.ToList());

            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddBlog(BlogVM b)
        {
            var repo = BlogRepoFactory.Create();
            var context = new ExploreMidwestDBContext();
            if (ModelState.IsValid)
            {
                Blog blog = new Blog();
                if (b.File != null)
                {
                    string pic = Path.GetFileName(b.File.FileName);
                    string path = Path.Combine(
                                           Server.MapPath("~/images"), pic);
                    // file is uploaded
                    b.File.SaveAs(path);

                    blog = new Blog
                    {
                        BlogId = b.BlogId,
                        Body = b.Body,
                        IsDeleted = b.IsDeleted,
                        IsFinished = b.IsFinished,
                        BlogTags = new List<Tag>(),
                        Title = b.Title,
                        Author = User.Identity.Name,
                        Date = DateTime.Now,
                        ImageLocation = "images/" + Path.GetFileName(b.File.FileName),
                    };
                }
                else
                {
                    blog = new Blog
                    {
                        BlogId = b.BlogId,
                        Body = b.Body,
                        IsDeleted = b.IsDeleted,
                        IsFinished = b.IsFinished,
                        BlogTags = new List<Tag>(),
                        Title = b.Title,
                        Author = User.Identity.Name,
                        Date = DateTime.Now,
                    };
                }
                if (b.BlogCategory.CategoryId == 0)
                {
                    Category c = new Category
                    {
                        CategoryType = b.NewCategory
                    };
                    context.Category.Add(c);
                    context.SaveChanges();
                    blog.BlogCategory = context.Category.FirstOrDefault(g => g.CategoryType == c.CategoryType);
                }
                else
                {
                    blog.BlogCategory = context.Category.FirstOrDefault(c => c.CategoryId == b.BlogCategory.CategoryId);
                }
                repo.AddBlog(blog);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                b.SetCategories(context.Category.ToList());
                return View(b);
            }
        }

        [ValidateInput(false)]
        public ActionResult SavedBlogs()
        {
            var model = new List<Blog>();

            model = repo.GetSavedFromAuthor(User.Identity.Name);

            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult EditBlog(int id)
        {
            Blog b = repo.GetBlogById(id);

            var model = new BlogVM()
            {
                Author = b.Author,
                Body = b.Body,
                BlogId = b.BlogId,
                BlogCategory =  b.BlogCategory,
                Date = b.Date,
                IsDeleted = b.IsDeleted,
                IsFinished = b.IsFinished,
                BlogTags = b.BlogTags,
                Title = b.Title,
                ImageLocation = b.ImageLocation,
                
            };

            model.SetCategories(context.Category.ToList());

            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditBlog(BlogVM b)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(b.Author))
                {
                    b.Author = User.Identity.Name;
                }
                Blog blog = new Blog();
                if (b.File != null)
                {
                    string pic = Path.GetFileName(b.File.FileName);
                    string path = Path.Combine(
                                           Server.MapPath("~/images"), pic);
                    // file is uploaded
                    b.File.SaveAs(path);
                    blog = new Blog
                    {
                        BlogId = b.BlogId,
                        Body = b.Body,
                        IsDeleted = b.IsDeleted,
                        IsFinished = b.IsFinished,
                        BlogTags = new List<Tag>(),
                        Title = b.Title,
                        Author = b.Author,
                        Date = DateTime.Now,
                        ImageLocation = "images/" + Path.GetFileName(b.File.FileName),
                    };
                }
                else
                {
                    blog = new Blog
                    {
                        BlogId = b.BlogId,
                        Body = b.Body,
                        IsDeleted = b.IsDeleted,
                        IsFinished = b.IsFinished,
                        BlogTags = new List<Tag>(),
                        Title = b.Title,
                        Author = b.Author,
                        Date = DateTime.Now,
                        ImageLocation = b.ImageLocation,
                    };
                }
                if (b.BlogCategory.CategoryId == 0)
                {
                    Category c = new Category
                    {
                        CategoryType = b.NewCategory
                    };
                    context.Category.Add(c);
                    context.SaveChanges();
                    blog.BlogCategory = context.Category.FirstOrDefault(g => g.CategoryType == c.CategoryType);
                }
                else
                {
                    blog.BlogCategory = context.Category.FirstOrDefault(c => c.CategoryId == b.BlogCategory.CategoryId);
                }
                repo.EditBlog(blog);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                b.SetCategories(context.Category.ToList());
                return View(b);
            }
        }

        public ActionResult DeleteBlog(int id)
        {
            repo.DeleteBlog(id);

            return RedirectToAction("SavedBlogs");
        }
    }
}