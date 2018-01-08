using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExploreMidwest.Model;
using System.Data.Entity.Migrations;
using System.Text.RegularExpressions;
using System.Data.Entity;

namespace ExploreMidwest.Data.BlogRepositories
{
    public class EFBlogRepo : IBlogRepo
    {
        ExploreMidwestDBContext context = new ExploreMidwestDBContext();

        public List<Blog> GetNumberOfBlogs(int number, int set)
        {
            var toReturn = context.Blog.Include("BlogCategory").OrderByDescending(b => b.Date).Where(b => b.IsFinished).Skip(number * set).Take(number).ToList();
            return toReturn;
        }

        public List<Blog> GetBlogsByCategory(string category)
        {
            return context.Blog.Include("BlogCategory").Where(c => c.BlogCategory.CategoryType == category).ToList();
        }


        public List<Blog> GetBlogsByDate(string date)
        {
            try{
                DateTime day = DateTime.Parse(date);
                return context.Blog.Include("BlogCategory").Where(d => DbFunctions.TruncateTime(d.Date) == day.Date).ToList();
            } catch
            {
                return new List<Blog>();
            }
        }

        public void DeleteBlog(int blogId)
        {
            Blog blog = context.Blog.Find(blogId);
            context.Blog.Remove(blog);
            context.SaveChanges();
           
        }

        public void EditBlog(Blog b)
        {
            var regex = new Regex(@"(?<=#)\w+");
            var matches = regex.Matches(b.Body);
            foreach (Match m in matches)
            {
                if (context.Tags.Where(t => t.TagName == m.Value).Count() == 0)
                {
                    context.Tags.Add(new Tag { TagName = m.Value });
                    context.SaveChanges();
                }
                b.BlogTags.Add(context.Tags.SingleOrDefault(t => t.TagName == m.Value));



            }
            var change = context.Blog.FirstOrDefault(bl => bl.BlogId == b.BlogId);
            change.Author = b.Author;
            change.Body = b.Body;
            change.Date = b.Date;
            change.Title = b.Title;
            change.ImageLocation = b.ImageLocation;
            change.IsDeleted = b.IsDeleted;
            change.IsFinished = b.IsFinished;
            change.BlogCategory = context.Category.FirstOrDefault(c => c.CategoryId == b.BlogCategory.CategoryId);
            change.BlogTags.Clear();
            change.BlogTags = b.BlogTags;
            context.SaveChanges();
        }

        public void AddBlog(Blog b)
        {
            var regex = new Regex(@"(?<=#)\w+");
            var matches = regex.Matches(b.Body);

            foreach (Match m in matches)
            {
                if (context.Tags.Where(t => t.TagName == m.Value).Count() == 0)
                { 
                    context.Tags.Add(new Tag { TagName=m.Value});
                    context.SaveChanges();
                }
                b.BlogTags.Add(context.Tags.SingleOrDefault(t => t.TagName == m.Value));
            }
            b.BlogCategory = context.Category.SingleOrDefault(c => c.CategoryId == b.BlogCategory.CategoryId);
            context.Blog.Add(b);
            context.SaveChanges();
        }


        public List<Blog> GetBlogsByTag(string tag)
        {
            //return context.Tags.Where(t => t.Tags.)
            return context.Blog.Include("BlogCategory").Where(d => d.BlogTags.Where(t => t.TagName == tag).Count() >= 1).ToList();
        }

        public List<Blog> GetBlogsByTitle(string title)
        {
            return context.Blog.Include("BlogCategory").Where(t => t.Title == title).ToList();
        }

        public Blog GetBlogById(int id)
        {
            return context.Blog.Include("BlogCategory").Where(d => d.BlogId == id).FirstOrDefault();
        }

        public List<Blog> GetUnpublishedBlogs()
        {
            return context.Blog.Include("BlogCategory").Where(d => d.IsFinished == false).ToList();
        }

        public List<Blog> GetSavedFromAuthor(string author)
        {
            return context.Blog.Include("BlogCategory").Where(d => d.Author == author).Where(b => b.IsFinished == false).ToList();
        }
    }
}
